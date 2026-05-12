using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Application.DTOs;
using Travel.Application.Request;

namespace Travel.Application.Contracts
{
    public interface IVehicleService
    {
        Task<VehicleResponseDto> GetByIdAsync(long id, CancellationToken cancellationToken = default);
        Task<IEnumerable<VehicleResponseDto>> GetByDestinationAsync(long destinationId, CancellationToken cancellationToken = default);
        Task<IEnumerable<VehicleResponseDto>> GetByTransmissionAsync(string transmission, CancellationToken cancellationToken = default);
        Task<VehicleResponseDto> CreateAsync(CreateVehicleRequest request, CancellationToken cancellationToken = default);
        Task DeleteAsync(long id, CancellationToken cancellationToken = default);
    }
}
