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
    public class AirportService : IAirportService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public AirportService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<AirportResponseDto> GetByIdAsync(long id, CancellationToken ct = default)
        {
            var entity = await _uow.Airports.GetWithFlights()
                                            .Include(a => a.Destination)
                                                .ThenInclude(d => d.Country)
                                            .Include(a => a.PhoneAirports)
                                            .FirstOrDefaultAsync(a => a.Id == id, ct)
                ?? throw new NotFoundException("Aeropuerto", id);
            return _mapper.Map<AirportResponseDto>(entity);
        }

        public async Task<IEnumerable<AirportResponseDto>> GetByDestinationAsync(long destinationId, CancellationToken ct = default)
            => _mapper.Map<IEnumerable<AirportResponseDto>>(
                await _uow.Airports.GetByDestination(destinationId).ToListAsync(ct));

        public async Task<AirportResponseDto> GetByIataCodeAsync(string iataCode, CancellationToken ct = default)
        {
            var entity = await _uow.Airports.GetByIataCodeAsync(iataCode, ct)
                ?? throw new NotFoundException("Aeropuerto", iataCode);
            return _mapper.Map<AirportResponseDto>(entity);
        }

        public async Task<AirportResponseDto> CreateAsync(CreateAirportRequest request, CancellationToken ct = default)
        {
            _ = await _uow.Destinations.GetByIdAsync(request.IdDestination, ct)
                ?? throw new NotFoundException("Destino", request.IdDestination);

            // Verificar que el código IATA no esté duplicado
            var existing = await _uow.Airports.GetByIataCodeAsync(request.CodeIata, ct);
            if (existing is not null)
                throw new ConflictException("Aeropuerto", "código IATA", request.CodeIata.ToUpper());

            var airport = new Airport
            {
                IdDestination = request.IdDestination,
                AirportName = request.AirportName,
                CodeIata = request.CodeIata.ToUpper()
            };

            await _uow.Airports.AddAsync(airport, ct);
            await _uow.SaveChangesAsync(ct);

            foreach (var phone in request.PhoneNumbers)
                await _uow.PhoneAirports.AddAsync(
                    new PhoneAirport { IdAirport = airport.Id, PhoneNumber = phone }, ct);

            await _uow.SaveChangesAsync(ct);
            return await GetByIdAsync(airport.Id, ct);
        }

        public async Task DeleteAsync(long id, CancellationToken ct = default)
        {
            var entity = await _uow.Airports.GetByIdAsync(id, ct)
                ?? throw new NotFoundException("Aeropuerto", id);

            await _uow.PhoneAirports.DeleteByAirportAsync(id, ct);


            _uow.Airports.Delete(entity);
            await _uow.SaveChangesAsync(ct);
        }
    }
}
