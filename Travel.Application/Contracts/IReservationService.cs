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
    public interface IReservationService
    {
        Task<ReservationResponseDto> GetByIdAsync(long id, CancellationToken cancellationToken = default);
        Task<IEnumerable<ReservationSummaryDto>> GetByUserAsync(long userId, CancellationToken cancellationToken = default);
        Task<PagedResponse<ReservationSummaryDto>> GetPagedAsync(int page, int pageSize, CancellationToken cancellationToken = default);
        Task<IEnumerable<ReservationSummaryDto>> GetByStatusAsync(long statusId, CancellationToken cancellationToken = default);
        Task<IEnumerable<ReservationSummaryDto>> GetByDateRangeAsync(System.DateTime from, System.DateTime to, CancellationToken cancellationToken = default);
        Task<ReservationResponseDto> CreateAsync(long userId, CreateReservationRequest request, CancellationToken cancellationToken = default);
        Task<ReservationResponseDto> UpdateStatusAsync(long id, UpdateReservationStatusRequest request, CancellationToken cancellationToken = default);
        Task CancelAsync(long id, CancellationToken cancellationToken = default);
        Task<DashboardSummaryDto> GetDashboardSummaryAsync(CancellationToken cancellationToken = default);
    }
}
