using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Application.DTOs;
using Travel.Application.Request;

namespace Travel.Application.Contracts
{
    public interface IPackageService
    {
        Task<PackageResponseDto> GetByIdAsync(long id, CancellationToken cancellationToken = default);
        Task<IEnumerable<PackageResponseDto>> GetActiveAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<PackageResponseDto>> GetExpiringAsync(int days, CancellationToken cancellationToken = default);
        Task<PackageResponseDto> CreateAsync(CreatePackageRequest request, CancellationToken cancellationToken = default);
        Task DeleteAsync(long id, CancellationToken cancellationToken = default);
    }
}
