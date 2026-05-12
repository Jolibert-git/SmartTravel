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
    public class ServiceManagementService : IServiceManagementService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public ServiceManagementService(IUnitOfWork uow, IMapper mapper) { _uow = uow; _mapper = mapper; }

        public async Task<ServiceResponseDto> GetByIdAsync(long id, CancellationToken ct = default)
        {
            var entity = await _uow.OfferedServices.GetWithDetails().FirstOrDefaultAsync(s => s.Id == id, ct)
                ?? throw new NotFoundException("Servicio", id);
            return _mapper.Map<ServiceResponseDto>(entity);
        }

        public async Task<IEnumerable<ServiceResponseDto>> GetAllAsync(CancellationToken ct = default)
            => _mapper.Map<IEnumerable<ServiceResponseDto>>(
                await _uow.OfferedServices.GetWithDetails().ToListAsync(ct));

        public async Task<IEnumerable<ServiceResponseDto>> GetByTypeAsync(long typeId, CancellationToken ct = default)
            => _mapper.Map<IEnumerable<ServiceResponseDto>>(
                await _uow.OfferedServices.GetByType(typeId).ToListAsync(ct));

        public async Task<IEnumerable<ServiceResponseDto>> GetAvailableAsync(DateTime date, CancellationToken ct = default)
            => _mapper.Map<IEnumerable<ServiceResponseDto>>(
                await _uow.OfferedServices.GetAvailable(date).ToListAsync(ct));

        public async Task<IEnumerable<ServiceResponseDto>> GetByPriceRangeAsync(decimal min, decimal max, CancellationToken ct = default)
            => _mapper.Map<IEnumerable<ServiceResponseDto>>(
                await _uow.OfferedServices.GetByPriceRange(min, max).ToListAsync(ct));

        public async Task<ServiceResponseDto> CreateAsync(CreateServiceRequest request, CancellationToken ct = default)
        {
            _ = await _uow.TypeServices.GetByIdAsync(request.IdTypeService, ct)
                ?? throw new NotFoundException("Tipo de servicio", request.IdTypeService);
            _ = await _uow.Suppliers.GetByIdAsync(request.IdSupplier, ct)
                ?? throw new NotFoundException("Proveedor", request.IdSupplier);

            var entity = new OfferedService
            {
                ServiceDescription = request.ServiceDescription,
                Price = request.Price,
                IdTypeService = request.IdTypeService,
                IdSupplier = request.IdSupplier
            };

            await _uow.OfferedServices.AddAsync(entity, ct);
            await _uow.SaveChangesAsync(ct);
            return await GetByIdAsync(entity.Id, ct);
        }

        public async Task<ServiceResponseDto> UpdateAsync(long id, UpdateServiceRequest request, CancellationToken ct = default)
        {
            var entity = await _uow.OfferedServices.GetByIdAsync(id, ct)
                ?? throw new NotFoundException("Servicio", id);

            entity.ServiceDescription = request.ServiceDescription;
            entity.Price = request.Price;
            _uow.OfferedServices.Update(entity);
            await _uow.SaveChangesAsync(ct);
            return await GetByIdAsync(id, ct);
        }

        public async Task DeleteAsync(long id, CancellationToken ct = default)
        {
            var entity = await _uow.OfferedServices.GetByIdAsync(id, ct)
                ?? throw new NotFoundException("Servicio", id);
            _uow.OfferedServices.Delete(entity);
            await _uow.SaveChangesAsync(ct);
        }
    }

}
