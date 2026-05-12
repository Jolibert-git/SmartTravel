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
    public class FlightService : IFlightService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public FlightService(IUnitOfWork uow, IMapper mapper) { _uow = uow; _mapper = mapper; }

        public async Task<FlightResponseDto> GetByIdAsync(long id, CancellationToken ct = default)
        {
            var entity = await _uow.Flights.GetWithDetails().FirstOrDefaultAsync(f => f.Id == id, ct)
                ?? throw new NotFoundException("Vuelo", id);
            var dto = _mapper.Map<FlightResponseDto>(entity);
            dto.AvailableSeats = await _uow.Flights.GetAvailableSeatsCountAsync(id, ct);
            return dto;
        }

        public async Task<IEnumerable<FlightResponseDto>> SearchAsync(SearchFlightRequest request, CancellationToken ct = default)
        {
            var flights = await _uow.Flights.Search(request.OriginAirportId, request.ArriveAirportId, request.DepartureDate)
                                            .ToListAsync(ct);
            return _mapper.Map<IEnumerable<FlightResponseDto>>(flights);
        }

        public async Task<IEnumerable<FlightResponseDto>> GetWithAvailableSeatsAsync(long originId, long arriveId, CancellationToken ct = default)
            => _mapper.Map<IEnumerable<FlightResponseDto>>(
                await _uow.Flights.GetWithAvailableSeats(originId, arriveId).ToListAsync(ct));

        public async Task<FlightResponseDto> CreateAsync(CreateFlightRequest request, CancellationToken ct = default)
        {
            if (request.AirportIdOrigen == request.AirportIdArrive)
                throw new BusinessRuleException("El aeropuerto de origen y destino no pueden ser el mismo.", "SAME_AIRPORT");

            var flight = new Flight
            {
                IdService = request.IdService,
                DateDeparture = request.DateDeparture,
                DateArrival = request.DateArrival,
                Capacity = request.Capacity,
                AirportIdOrigen = request.AirportIdOrigen,
                AirportIdArrive = request.AirportIdArrive
            };

            await _uow.Flights.AddAsync(flight, ct);
            await _uow.SaveChangesAsync(ct);
            return await GetByIdAsync(flight.Id, ct);
        }

        public async Task DeleteAsync(long id, CancellationToken ct = default)
        {
            var entity = await _uow.Flights.GetByIdAsync(id, ct) ?? throw new NotFoundException("Vuelo", id);
            _uow.Flights.Delete(entity);
            await _uow.SaveChangesAsync(ct);
        }

        public async Task<IEnumerable<FlightSeatResponseDto>> GetSeatsByFlightAsync(long flightId, CancellationToken ct = default)
            => _mapper.Map<IEnumerable<FlightSeatResponseDto>>(
                await _uow.FlightSeats.GetByFlight(flightId).ToListAsync(ct));

        public async Task<IEnumerable<FlightSeatResponseDto>> GetAvailableSeatsByClassAsync(long flightId, long seatClassId, CancellationToken ct = default)
            => _mapper.Map<IEnumerable<FlightSeatResponseDto>>(
                await _uow.FlightSeats.GetAvailableByClass(flightId, seatClassId).ToListAsync(ct));
    }
}
