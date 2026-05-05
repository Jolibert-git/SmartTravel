using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Travel.Application.Contracts;
using Travel.Application.DTOs;
using Travel.Application.Request;
using Travel.Application.Responses;
using Travel.Domain.Contracts;
using Travel.Domain.Entities;
using Travel.Domain.Exceptions;

namespace Travel.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<UserResponseDto> GetByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            var user = await _uow.Users.GetWithRoles()
                                       .FirstOrDefaultAsync(u => u.Id == id, cancellationToken)
                ?? throw new NotFoundException("Usuario", id);
            return _mapper.Map<UserResponseDto>(user);
        }

        public async Task<PagedResponse<UserResponseDto>> GetPagedAsync(int page, int pageSize, CancellationToken cancellationToken = default)
        {
            var total = await _uow.Users.CountAsync(cancellationToken: cancellationToken);
            var users = await _uow.Users.GetPaged(page, pageSize).ToListAsync(cancellationToken);
            var dtos = _mapper.Map<IEnumerable<UserResponseDto>>(users);
            return PagedResponse<UserResponseDto>.Create(dtos, total, page, pageSize);
        }

        public async Task<UserResponseDto> CreateAsync(CreateUserRequest request, CancellationToken cancellationToken = default)
        {
            if (await _uow.Users.EmailExistsAsync(request.Email, cancellationToken))
                throw new ConflictException("Usuario", "email", request.Email);

            var rol = await _uow.Roles.GetByIdAsync(request.IdRol, cancellationToken)
                ?? throw new NotFoundException("Rol", request.IdRol);

            var user = new SystemsUser
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                RegistrationDate = DateTime.Now,

                

            };

            await _uow.Users.AddAsync(user, cancellationToken);

            await _uow.SaveChangesAsync(cancellationToken);

            var link = new RolUser
            {
                IdSystemUser = user.Id,
                IdRol = request.IdRol
            };

            await _uow.Roles.AddRolUserAsync(link, cancellationToken);

            // You can add it directly to the DbContext 
            await _uow.SaveChangesAsync(cancellationToken);


            //rol.RolUsers.Add(new RolUser
            //{
            //    IdSystemUser = user.Id,
            //    IdRol = rol.Id
            //});

            //var rolUser = new RolUser { IdRol = request.IdRol, IdSystemUser = user.Id };
            //await _uow.Roles.AddAsync(rolUser, cancellationToken); // handled via context
            // Note: Add RolUser via DbContext directly or expose a RolUser repo

            //await _uow.SaveChangesAsync(cancellationToken);

            return await GetByIdAsync(user.Id, cancellationToken);
        }

        public async Task<UserResponseDto> UpdateAsync(long id, UpdateUserRequest request, CancellationToken cancellationToken = default)
        {
            var user = await _uow.Users.GetByIdAsync(id, cancellationToken)
                ?? throw new NotFoundException("Usuario", id);

            // Re-fetch with tracking for update
            var tracked = new SystemsUser
            {
                Id = user.Id,
                FirstName = request.FirstName,
                LastName = request.LastName,
                PhoneNumber = request.PhoneNumber,
                Email = user.Email,
                PasswordHash = user.PasswordHash
            };

            _uow.Users.Update(tracked);
            await _uow.SaveChangesAsync(cancellationToken);
            return await GetByIdAsync(id, cancellationToken);
        }

        public async Task ChangePasswordAsync(long id, ChangePasswordRequest request, CancellationToken cancellationToken = default)
        {
            var user = await _uow.Users.GetByIdAsync(id, cancellationToken)
                ?? throw new NotFoundException("Usuario", id);

            if (!BCrypt.Net.BCrypt.Verify(request.CurrentPassword, user.PasswordHash))
                throw new BadRequestException("La contraseña actual es incorrecta.");

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
            _uow.Users.Update(user);
            await _uow.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(long id, CancellationToken cancellationToken = default)
        {
            var user = await _uow.Users.GetByIdAsync(id, cancellationToken)
                ?? throw new NotFoundException("Usuario", id);
            _uow.Users.Delete(user);
            await _uow.SaveChangesAsync(cancellationToken);
        }
    }
}
