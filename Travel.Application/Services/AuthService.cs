using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Travel.Application.Contracts;
using Travel.Application.DTOs;
using Travel.Application.Request;
using Travel.Domain.Contracts;
using Travel.Domain.Entities;
using Travel.Domain.Exceptions;

namespace Travel.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public AuthService(IUnitOfWork uow, IMapper mapper, IConfiguration config)
        {
            _uow = uow;
            _mapper = mapper;
            _config = config;
        }

        public async Task<AuthResponseDto> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default)
        {
            var user = await _uow.Users.GetByEmailAsync(request.Email, cancellationToken)
                ?? throw new UnauthorizedException("Email o contraseña incorrectos.");

            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
                throw new UnauthorizedException("Email o contraseña incorrectos.");

            //var userWithRoles = await _uow.Users
            //    .GetWithRoles()
            //    .FirstOrDefaultAsync(u => u.Id == user.Id, cancellationToken)
            //    ?? user;

            // Use .Include and .ThenInclude to load the nested Role data
            var userWithRoles = await _uow.Users
                .GetWithRoles()
                .Include(u => u.RolUsers)
                    .ThenInclude(ru => ru.Rol) // This is the missing piece!
                .FirstOrDefaultAsync(u => u.Id == user.Id, cancellationToken)
                ?? user;

            var accessToken = GenerateAccessToken(userWithRoles);
            var refreshToken = GenerateRefreshToken();
            var expiresAt = DateTime.UtcNow.AddMinutes(GetJwtExpiryMinutes());

            return new AuthResponseDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                ExpiresAt = expiresAt,
                User = _mapper.Map<UserSummaryDto>(userWithRoles)
            };
        }

        public async Task<AuthResponseDto> RefreshTokenAsync(RefreshTokenRequest request, CancellationToken cancellationToken = default)
        {
            var principal = GetPrincipalFromExpiredToken(request.AccessToken);
            var userIdStr = principal.FindFirstValue(ClaimTypes.NameIdentifier)
                ?? throw new UnauthorizedException("Token inválido.");

            var userId = long.Parse(userIdStr);
            var user = await _uow.Users.GetWithRoles()
                                        .FirstOrDefaultAsync(u => u.Id == userId, cancellationToken)
                ?? throw new NotFoundException("Usuario", userId);

            var newAccessToken = GenerateAccessToken(user);
            var newRefreshToken = GenerateRefreshToken();

            return new AuthResponseDto
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken,
                ExpiresAt = DateTime.UtcNow.AddMinutes(GetJwtExpiryMinutes()),
                User = _mapper.Map<UserSummaryDto>(user)
            };
        }

        public Task LogoutAsync(long userId, CancellationToken cancellationToken = default)
        {
            // Si implementas refresh token en BD, aquí invalidas el registro.
            // Por ahora el cliente simplemente descarta el token.
            return Task.CompletedTask;
        }

        // ----------------------------------------------------------------
        // Helpers privados
        // ----------------------------------------------------------------
        private string GenerateAccessToken(SystemsUser user)
        {
            var roles = user.RolUsers?.Select(ru => ru.Rol?.NameRol ?? string.Empty) ?? Enumerable.Empty<string>();

            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new(ClaimTypes.Email,          user.Email),
                new(ClaimTypes.GivenName,      user.FirstName),
                new(ClaimTypes.Surname,        user.LastName),
            };
            claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(GetJwtExpiryMinutes()),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private static string GenerateRefreshToken()
        {
            var bytes = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(bytes);
            return Convert.ToBase64String(bytes);
        }

        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var parameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!)),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false   // permitir token expirado
            };

            return new JwtSecurityTokenHandler()
                .ValidateToken(token, parameters, out _);
        }

        private int GetJwtExpiryMinutes()
            => int.TryParse(_config["Jwt:ExpiryMinutes"], out var m) ? m : 60;
    }

}
