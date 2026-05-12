using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Application.DTOs;
using Travel.Application.Request;

namespace Travel.Application.Contracts
{
    public interface IPaymentService
    {
        //Task<string> CreatePaymentAsync(decimal amount, string currency, string description);

        Task<PaymentResponseDto> GetByIdAsync(long id, CancellationToken cancellationToken = default);
        Task<IEnumerable<PaymentResponseDto>> GetByReservationAsync(long reservationId, CancellationToken cancellationToken = default);
        Task<decimal> GetTotalPaidAsync(long reservationId, CancellationToken cancellationToken = default);
        Task<IEnumerable<PaymentResponseDto>> GetByDateRangeAsync(System.DateTime from, System.DateTime to, CancellationToken cancellationToken = default);
        Task<PaymentResponseDto> CreateAsync(CreatePaymentRequest request, CancellationToken cancellationToken = default);
        
    }
}
