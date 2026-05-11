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
    public class SupplierService : ISupplierService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public SupplierService(IUnitOfWork uow, IMapper mapper) { _uow = uow; _mapper = mapper; }

        public async Task<SupplierResponseDto> GetByIdAsync(long id, CancellationToken ct = default)
        {
            var entity = await _uow.Suppliers.GetWithDetails().FirstOrDefaultAsync(s => s.Id == id, ct)
                ?? throw new NotFoundException("Proveedor", id);
            return _mapper.Map<SupplierResponseDto>(entity);
        }

        public async Task<IEnumerable<SupplierResponseDto>> GetAllAsync(CancellationToken ct = default)
            => _mapper.Map<IEnumerable<SupplierResponseDto>>(
                await _uow.Suppliers.GetWithDetails().ToListAsync(ct));

        public async Task<SupplierResponseDto> CreateAsync(CreateSupplierRequest request, CancellationToken ct = default)
        {
            if (await _uow.Suppliers.RncExistsAsync(request.Rnc, ct))
                throw new ConflictException("Proveedor", "RNC", request.Rnc);

            var supplier = new Supplier
            {
                CompanyName = request.CompanyName,
                Rnc = request.Rnc,
                Email = request.Email
            };

            await _uow.Suppliers.AddAsync(supplier, ct);
            await _uow.SaveChangesAsync(ct);

            foreach (var phone in request.PhoneNumbers)
                await _uow.PhoneSuppliers.AddAsync(new PhoneSupplier { IdSupplier = supplier.Id, PhoneNumber = phone }, ct);

            await _uow.SaveChangesAsync(ct);
            return await GetByIdAsync(supplier.Id, ct);
        }

        public async Task<SupplierResponseDto> UpdateAsync(long id, UpdateSupplierRequest request, CancellationToken ct = default)
        {
            var supplier = await _uow.Suppliers.GetByIdAsync(id, ct)
                ?? throw new NotFoundException("Proveedor", id);

            supplier.CompanyName = request.CompanyName;
            supplier.Email = request.Email;
            _uow.Suppliers.Update(supplier);

            await _uow.PhoneSuppliers.DeleteBySupplierAsync(id, ct);
            foreach (var phone in request.PhoneNumbers)
                await _uow.PhoneSuppliers.AddAsync(new PhoneSupplier { IdSupplier = id, PhoneNumber = phone }, ct);

            await _uow.SaveChangesAsync(ct);
            return await GetByIdAsync(id, ct);
        }

        public async Task DeleteAsync(long id, CancellationToken ct = default)
        {
            var entity = await _uow.Suppliers.GetByIdAsync(id, ct) ?? throw new NotFoundException("Proveedor", id);


            // Verificar si tiene servicios asociados
            var hasServices = await _uow.OfferedServices
                .ExistsAsync(s => s.IdSupplier == id, ct);

            if (hasServices)
                throw new BusinessRuleException(
                    "No se puede eliminar el proveedor porque tiene servicios asociados. " +
                    "Elimine primero los servicios del proveedor.",
                    "SUPPLIER_HAS_SERVICES");

            // Eliminar teléfonos primero
            await _uow.PhoneSuppliers.DeleteBySupplierAsync(id, ct);


            _uow.Suppliers.Delete(entity);

            await _uow.SaveChangesAsync(ct);
        }
    }
}
