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
    public class PackageService : IPackageService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public PackageService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<PackageResponseDto> GetByIdAsync(long id, CancellationToken ct = default)
        {
            var entity = await _uow.Packages.GetWithDetails()
                                            .FirstOrDefaultAsync(p => p.Id == id, ct)
                ?? throw new NotFoundException("Paquete", id);
            return _mapper.Map<PackageResponseDto>(entity);
        }

        public async Task<IEnumerable<PackageResponseDto>> GetActiveAsync(CancellationToken ct = default)
            => _mapper.Map<IEnumerable<PackageResponseDto>>(
                await _uow.Packages.GetActive().ToListAsync(ct));

        public async Task<IEnumerable<PackageResponseDto>> GetExpiringAsync(int days, CancellationToken ct = default)
            => _mapper.Map<IEnumerable<PackageResponseDto>>(
                await _uow.Packages.GetExpiring(days).ToListAsync(ct));

        public async Task<PackageResponseDto> CreateAsync(CreatePackageRequest request, CancellationToken ct = default)
        {
            if (request.OfferEnd <= request.OfferStart)
                throw new BusinessRuleException(
                    "La fecha de fin de oferta debe ser posterior a la de inicio.",
                    "INVALID_OFFER_DATES");

            var package = new Package
            {
                PackageName = request.PackageName,
                Price = request.Price,
                OfferStart = request.OfferStart,
                OfferEnd = request.OfferEnd
            };

            await _uow.Packages.AddAsync(package, ct);
            await _uow.SaveChangesAsync(ct);

            foreach (var detail in request.Details)
            {
                _ = await _uow.OfferedServices.GetByIdAsync(detail.IdService, ct)
                    ?? throw new NotFoundException("Servicio", detail.IdService);

                await _uow.DetailPackages.AddAsync(new DetailPackage
                {
                    IdPackage = package.Id,
                    IdService = detail.IdService,
                    NumberPersons = detail.NumberPersons,
                    CostPrice = detail.CostPrice
                }, ct);
            }

            await _uow.SaveChangesAsync(ct);
            return await GetByIdAsync(package.Id, ct);
        }

        public async Task DeleteAsync(long id, CancellationToken ct = default)
        {
            var entity = await _uow.Packages.GetByIdAsync(id, ct)
                ?? throw new NotFoundException("Paquete", id);

            // Eliminar detalles primero para evitar FK violation
            await _uow.DetailPackages.DeleteByPackageAsync(id, ct);
            _uow.Packages.Delete(entity);
            await _uow.SaveChangesAsync(ct);
        }
    }
}
