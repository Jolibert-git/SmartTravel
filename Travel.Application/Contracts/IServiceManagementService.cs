using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Application.DTOs;
using Travel.Application.Request;

namespace Travel.Application.Contracts
{
    public interface IServiceManagementService
    {
        Task<ServiceResponseDto> GetByIdAsync(long id, CancellationToken cancellationToken = default);
        Task<IEnumerable<ServiceResponseDto>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<ServiceResponseDto>> GetByTypeAsync(long typeId, CancellationToken cancellationToken = default);
        Task<IEnumerable<ServiceResponseDto>> GetAvailableAsync(System.DateTime date, CancellationToken cancellationToken = default);
        Task<IEnumerable<ServiceResponseDto>> GetByPriceRangeAsync(decimal min, decimal max, CancellationToken cancellationToken = default);
        Task<ServiceResponseDto> CreateAsync(CreateServiceRequest request, CancellationToken cancellationToken = default);
        Task<ServiceResponseDto> UpdateAsync(long id, UpdateServiceRequest request, CancellationToken cancellationToken = default);
        Task DeleteAsync(long id, CancellationToken cancellationToken = default);
    }
}
