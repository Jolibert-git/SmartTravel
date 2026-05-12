using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Application.DTOs;
using Travel.Application.Request;

namespace Travel.Application.Contracts
{
    public interface IAuthService
    {
        /// <summary>Valida credenciales y retorna JWT + RefreshToken.</summary>
        Task<AuthResponseDto> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default);

        /// <summary>Rota el access token usando el refresh token.</summary>
        Task<AuthResponseDto> RefreshTokenAsync(RefreshTokenRequest request, CancellationToken cancellationToken = default);

        /// <summary>Invalida el refresh token del usuario (logout).</summary>
        Task LogoutAsync(long userId, CancellationToken cancellationToken = default);
    }
}
