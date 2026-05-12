using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Application.DTOs;
using Travel.Application.Request;

namespace Travel.Application.Contracts
{
    public interface IAirportService
    {
        Task<AirportResponseDto> GetByIdAsync(long id, CancellationToken cancellationToken = default);
        Task<IEnumerable<AirportResponseDto>> GetByDestinationAsync(long destinationId, CancellationToken cancellationToken = default);
        Task<AirportResponseDto> GetByIataCodeAsync(string iataCode, CancellationToken cancellationToken = default);
        Task<AirportResponseDto> CreateAsync(CreateAirportRequest request, CancellationToken cancellationToken = default);
        Task DeleteAsync(long id, CancellationToken cancellationToken = default);
    }
}
