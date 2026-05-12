using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Travel.Application.Contracts;
using Travel.Application.DTOs;
using Travel.Application.Request;
using Travel.Domain.Contracts;
using Travel.Domain.Entities;
using Travel.Domain.Exceptions;

namespace Travel.Application.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public VehicleService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<VehicleResponseDto> GetByIdAsync(long id, CancellationToken ct = default)
        {
            var entity = await _uow.Vehicles.GetWithDetails()
                                            .FirstOrDefaultAsync(v => v.Id == id, ct)
                ?? throw new NotFoundException("Vehículo", id);
            return _mapper.Map<VehicleResponseDto>(entity);
        }

        public async Task<IEnumerable<VehicleResponseDto>> GetByDestinationAsync(long destinationId, CancellationToken ct = default)
            => _mapper.Map<IEnumerable<VehicleResponseDto>>(
                await _uow.Vehicles.GetByDestination(destinationId).ToListAsync(ct));

        public async Task<IEnumerable<VehicleResponseDto>> GetByTransmissionAsync(string transmission, CancellationToken ct = default)
            => _mapper.Map<IEnumerable<VehicleResponseDto>>(
                await _uow.Vehicles.GetByTransmission(transmission).ToListAsync(ct));

        public async Task<VehicleResponseDto> CreateAsync(CreateVehicleRequest request, CancellationToken ct = default)
        {
            _ = await _uow.OfferedServices.GetByIdAsync(request.IdService, ct)
                ?? throw new NotFoundException("Servicio", request.IdService);

            _ = await _uow.Destinations.GetByIdAsync(request.IdDestination, ct)
                ?? throw new NotFoundException("Destino", request.IdDestination);

            var entity = new Vehicle
            {
                IdService = request.IdService,
                IdDestination = request.IdDestination,
                Make = request.Make,
                Model = request.Model,
                Transmission = request.Transmission
            };

            await _uow.Vehicles.AddAsync(entity, ct);
            await _uow.SaveChangesAsync(ct);
            return await GetByIdAsync(entity.Id, ct);
        }

        public async Task DeleteAsync(long id, CancellationToken ct = default)
        {
            var entity = await _uow.Vehicles.GetByIdAsync(id, ct)
                ?? throw new NotFoundException("Vehículo", id);
            _uow.Vehicles.Delete(entity);
            await _uow.SaveChangesAsync(ct);
        }
    }
}
