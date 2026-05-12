using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Application.Contracts;
using Travel.Application.DTOs;
using Travel.Application.Request;
using Travel.Domain.Contracts;
using Travel.Domain.Entities;
using Travel.Domain.Exceptions;

namespace Travel.Application.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly  IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public CatalogService(IUnitOfWork uow, IMapper mapper) { _uow = uow; _mapper = mapper; }

        public async Task<IEnumerable<TypeServiceDto>> GetAllTypeServicesAsync(CancellationToken ct = default)
            => _mapper.Map<IEnumerable<TypeServiceDto>>(await _uow.TypeServices.GetQueryable().ToListAsync(ct));

        public async Task<TypeServiceDto> CreateTypeServiceAsync(CreateTypeServiceRequest request, CancellationToken ct = default)
        {
            var entity = new TypeService { TypeDescription = request.TypeDescription };
            await _uow.TypeServices.AddAsync(entity, ct);
            await _uow.SaveChangesAsync(ct);
            return _mapper.Map<TypeServiceDto>(entity);
        }

        public async Task DeleteTypeServiceAsync(long id, CancellationToken ct = default)
        {
            var entity = await _uow.TypeServices.GetByIdAsync(id, ct) ?? throw new NotFoundException("TypeService", id);
            _uow.TypeServices.Delete(entity);
            await _uow.SaveChangesAsync(ct);
        }

        public async Task<IEnumerable<TypeRoomDto>> GetAllTypeRoomsAsync(CancellationToken ct = default)
            => _mapper.Map<IEnumerable<TypeRoomDto>>(await _uow.TypeRooms.GetQueryable().ToListAsync(ct));

        public async Task<TypeRoomDto> CreateTypeRoomAsync(CreateTypeRoomRequest request, CancellationToken ct = default)
        {
            var entity = new TypeRoom { TypeDescription = request.TypeDescription };
            await _uow.TypeRooms.AddAsync(entity, ct);
            await _uow.SaveChangesAsync(ct);
            return _mapper.Map<TypeRoomDto>(entity);
        }

        public async Task DeleteTypeRoomAsync(long id, CancellationToken ct = default)
        {
            var entity = await _uow.TypeRooms.GetByIdAsync(id, ct) ?? throw new NotFoundException("TypeRoom", id);
            _uow.TypeRooms.Delete(entity);
            await _uow.SaveChangesAsync(ct);
        }

        public async Task<IEnumerable<CountryDto>> GetAllCountriesAsync(CancellationToken ct = default)
            => _mapper.Map<IEnumerable<CountryDto>>(await _uow.Countries.GetQueryable().ToListAsync(ct));

        public async Task<CountryDto> GetCountryByIdAsync(long id, CancellationToken ct = default)
        {
            var entity = await _uow.Countries.GetByIdAsync(id, ct) ?? throw new NotFoundException("País", id);
            return _mapper.Map<CountryDto>(entity);
        }

        public async Task<CountryDto> CreateCountryAsync(CreateCountryRequest request, CancellationToken ct = default)
        {
            if (await _uow.Countries.IsoCodeExistsAsync(request.IsoCode, ct))
                throw new ConflictException("País", "código ISO", request.IsoCode.ToUpper());

            var entity = new Country { CountryName = request.CountryName, IsoCode = request.IsoCode.ToUpper() };
            await _uow.Countries.AddAsync(entity, ct);
            await _uow.SaveChangesAsync(ct);
            return _mapper.Map<CountryDto>(entity);
        }

        public async Task<IEnumerable<SeatClassDto>> GetAllSeatClassesAsync(CancellationToken ct = default)
            => _mapper.Map<IEnumerable<SeatClassDto>>(await _uow.SeatClasses.GetQueryable().ToListAsync(ct));

        public async Task<IEnumerable<DocumentTypeDto>> GetAllDocumentTypesAsync(CancellationToken ct = default)
            => _mapper.Map<IEnumerable<DocumentTypeDto>>(await _uow.DocumentTypes.GetQueryable().ToListAsync(ct));

        public async Task<IEnumerable<DiscountTypeDto>> GetAllDiscountTypesAsync(CancellationToken ct = default)
            => _mapper.Map<IEnumerable<DiscountTypeDto>>(await _uow.DiscountTypes.GetQueryable().ToListAsync(ct));

        public async Task<IEnumerable<PaymentMethodDto>> GetActivePaymentMethodsAsync(CancellationToken ct = default)
            => _mapper.Map<IEnumerable<PaymentMethodDto>>(await _uow.PaymentMethods.GetActive().ToListAsync(ct));
    }
}
