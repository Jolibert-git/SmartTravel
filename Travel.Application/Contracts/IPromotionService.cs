using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Application.DTOs;
using Travel.Application.Request;

namespace Travel.Application.Contracts
{
    public interface IPromotionService
    {
        Task<PromotionResponseDto> GetByIdAsync(long id, CancellationToken cancellationToken = default);
        Task<IEnumerable<PromotionResponseDto>> GetActiveAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<PromotionResponseDto>> GetByServiceAsync(long serviceId, CancellationToken cancellationToken = default);
        Task<IEnumerable<PromotionResponseDto>> GetByPackageAsync(long packageId, CancellationToken cancellationToken = default);
        Task<PromotionResponseDto> CreateAsync(CreatePromotionRequest request, CancellationToken cancellationToken = default);
        Task ToggleActiveAsync(long id, CancellationToken cancellationToken = default);
        Task DeleteAsync(long id, CancellationToken cancellationToken = default);
    }
}
