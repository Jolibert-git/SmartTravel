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
    public class PassengerService : IPassengerService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public PassengerService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<PassengerResponseDto> GetByIdAsync(long id, CancellationToken ct = default)
        {
            var entity = await _uow.Passengers.GetWithDetails()
                                              .FirstOrDefaultAsync(p => p.Id == id, ct)
                ?? throw new NotFoundException("Pasajero", id);
            return _mapper.Map<PassengerResponseDto>(entity);
        }

        public async Task<PassengerResponseDto> GetByDocumentAsync(string documentNumber, CancellationToken ct = default)
        {
            var entity = await _uow.Passengers.GetByDocumentAsync(documentNumber, ct)
                ?? throw new NotFoundException("Pasajero", documentNumber);
            return _mapper.Map<PassengerResponseDto>(entity);
        }

        public async Task<IEnumerable<PassengerResponseDto>> GetByReservationAsync(long reservationId, CancellationToken ct = default)
            => _mapper.Map<IEnumerable<PassengerResponseDto>>(
                await _uow.Passengers.GetByReservation(reservationId).ToListAsync(ct));

        public async Task<PassengerResponseDto> CreateAsync(CreatePassengerRequest request, CancellationToken ct = default)
        {
            if (await _uow.Passengers.DocumentExistsAsync(request.DocumentNumber, ct))
                throw new ConflictException("Pasajero", "número de documento", request.DocumentNumber);

            _ = await _uow.DocumentTypes.GetByIdAsync(request.IdDocumentType, ct)
                ?? throw new NotFoundException("Tipo de documento", request.IdDocumentType);

            _ = await _uow.Countries.GetByIdAsync(request.IdCountry, ct)
                ?? throw new NotFoundException("País", request.IdCountry);

            var entity = new Passenger
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                BirthDate = request.BirthDate,
                DocumentNumber = request.DocumentNumber,
                IdDocumentType = request.IdDocumentType,
                IdCountry = request.IdCountry
            };

            await _uow.Passengers.AddAsync(entity, ct);
            await _uow.SaveChangesAsync(ct);
            return await GetByIdAsync(entity.Id, ct);
        }

        public async Task DeleteAsync(long id, CancellationToken ct = default)
        {
            var entity = await _uow.Passengers.GetByIdAsync(id, ct)
                ?? throw new NotFoundException("Pasajero", id);
            _uow.Passengers.Delete(entity);
            await _uow.SaveChangesAsync(ct);
        }
    }

}
