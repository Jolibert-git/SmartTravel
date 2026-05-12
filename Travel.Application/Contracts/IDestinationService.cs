using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Application.DTOs;
using Travel.Application.Request;

namespace Travel.Application.Contracts
{
    public interface IDestinationService
    {
        Task<DestinationResponseDto> GetByIdAsync(long id, CancellationToken cancellationToken = default);
        Task<IEnumerable<DestinationResponseDto>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<DestinationResponseDto>> GetByCountryAsync(long countryId, CancellationToken cancellationToken = default);
        Task<IEnumerable<DestinationResponseDto>> SearchByCityAsync(string city, CancellationToken cancellationToken = default);
        Task<DestinationResponseDto> CreateAsync(CreateDestinationRequest request, CancellationToken cancellationToken = default);
        Task DeleteAsync(long id, CancellationToken cancellationToken = default);
    }
}
