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
    public class DestinationService : IDestinationService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public DestinationService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<DestinationResponseDto> GetByIdAsync(long id, CancellationToken ct = default)
        {
            var entity = await _uow.Destinations.GetWithDetails()
                                                .FirstOrDefaultAsync(d => d.Id == id, ct)
                ?? throw new NotFoundException("Destino", id);
            return _mapper.Map<DestinationResponseDto>(entity);
        }

        public async Task<IEnumerable<DestinationResponseDto>> GetAllAsync(CancellationToken ct = default)
            => _mapper.Map<IEnumerable<DestinationResponseDto>>(
                await _uow.Destinations.GetWithDetails().ToListAsync(ct));

        public async Task<IEnumerable<DestinationResponseDto>> GetByCountryAsync(long countryId, CancellationToken ct = default)
            => _mapper.Map<IEnumerable<DestinationResponseDto>>(
                await _uow.Destinations.GetByCountry(countryId).ToListAsync(ct));

        public async Task<IEnumerable<DestinationResponseDto>> SearchByCityAsync(string city, CancellationToken ct = default)
            => _mapper.Map<IEnumerable<DestinationResponseDto>>(
                await _uow.Destinations.SearchByCity(city).ToListAsync(ct));

        public async Task<DestinationResponseDto> CreateAsync(CreateDestinationRequest request, CancellationToken ct = default)
        {
            _ = await _uow.Countries.GetByIdAsync(request.IdCountry, ct)
                ?? throw new NotFoundException("País", request.IdCountry);

            var entity = new Destination
            {
                IdCountry = request.IdCountry,
                City = request.City,
                Street = request.Street
            };

            await _uow.Destinations.AddAsync(entity, ct);
            await _uow.SaveChangesAsync(ct);
            return await GetByIdAsync(entity.Id, ct);
        }

        public async Task DeleteAsync(long id, CancellationToken ct = default)
        {
            var entity = await _uow.Destinations.GetByIdAsync(id, ct)
                ?? throw new NotFoundException("Destino", id);
            _uow.Destinations.Delete(entity);
            await _uow.SaveChangesAsync(ct);
        }
    }
}
