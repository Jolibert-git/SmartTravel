using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Application.DTOs;
using Travel.Application.Request;
using Travel.Application.Responses;

namespace Travel.Application.Contracts
{
    public interface IUserService
    {
        Task<UserResponseDto> GetByIdAsync(long id, CancellationToken cancellationToken = default);
        Task<PagedResponse<UserResponseDto>> GetPagedAsync(int page, int pageSize, CancellationToken cancellationToken = default);
        Task<UserResponseDto> CreateAsync(CreateUserRequest request, CancellationToken cancellationToken = default);
        Task<UserResponseDto> UpdateAsync(long id, UpdateUserRequest request, CancellationToken cancellationToken = default);
        Task ChangePasswordAsync(long id, ChangePasswordRequest request, CancellationToken cancellationToken = default);
        Task DeleteAsync(long id, CancellationToken cancellationToken = default);
    }
}
