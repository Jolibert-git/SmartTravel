using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Application.DTOs;
using Travel.Application.Request;

namespace Travel.Application.Contracts
{
    public interface IPassengerService
    {
        Task<PassengerResponseDto> GetByIdAsync(long id, CancellationToken cancellationToken = default);
        Task<PassengerResponseDto> GetByDocumentAsync(string documentNumber, CancellationToken cancellationToken = default);
        Task<IEnumerable<PassengerResponseDto>> GetByReservationAsync(long reservationId, CancellationToken cancellationToken = default);
        Task<PassengerResponseDto> CreateAsync(CreatePassengerRequest request, CancellationToken cancellationToken = default);
        Task DeleteAsync(long id, CancellationToken cancellationToken = default);
    }
}
