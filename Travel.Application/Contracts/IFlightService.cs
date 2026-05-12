using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Application.DTOs;
using Travel.Application.Request;

namespace Travel.Application.Contracts
{
    public interface IFlightService
    {
        Task<FlightResponseDto> GetByIdAsync(long id, CancellationToken cancellationToken = default);
        Task<IEnumerable<FlightResponseDto>> SearchAsync(SearchFlightRequest request, CancellationToken cancellationToken = default);
        Task<IEnumerable<FlightResponseDto>> GetWithAvailableSeatsAsync(long originId, long arriveId, CancellationToken cancellationToken = default);
        Task<FlightResponseDto> CreateAsync(CreateFlightRequest request, CancellationToken cancellationToken = default);
        Task DeleteAsync(long id, CancellationToken cancellationToken = default);

        // Seats
        Task<IEnumerable<FlightSeatResponseDto>> GetSeatsByFlightAsync(long flightId, CancellationToken cancellationToken = default);
        Task<IEnumerable<FlightSeatResponseDto>> GetAvailableSeatsByClassAsync(long flightId, long seatClassId, CancellationToken cancellationToken = default);
    }
}
