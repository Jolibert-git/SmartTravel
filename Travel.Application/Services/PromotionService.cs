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
    public class PromotionService : IPromotionService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public PromotionService(IUnitOfWork uow, IMapper mapper) { _uow = uow; _mapper = mapper; }

        public async Task<PromotionResponseDto> GetByIdAsync(long id, CancellationToken ct = default)
        {
            var entity = await _uow.Promotions.GetWithDetails().FirstOrDefaultAsync(p => p.Id == id, ct)
                ?? throw new NotFoundException("Promoción", id);
            return _mapper.Map<PromotionResponseDto>(entity);
        }

        public async Task<IEnumerable<PromotionResponseDto>> GetActiveAsync(CancellationToken ct = default)
            => _mapper.Map<IEnumerable<PromotionResponseDto>>(
                await _uow.Promotions.GetActive().ToListAsync(ct));

        public async Task<IEnumerable<PromotionResponseDto>> GetByServiceAsync(long serviceId, CancellationToken ct = default)
            => _mapper.Map<IEnumerable<PromotionResponseDto>>(
                await _uow.Promotions.GetByService(serviceId).ToListAsync(ct));

        public async Task<IEnumerable<PromotionResponseDto>> GetByPackageAsync(long packageId, CancellationToken ct = default)
            => _mapper.Map<IEnumerable<PromotionResponseDto>>(
                await _uow.Promotions.GetByPackage(packageId).ToListAsync(ct));

        public async Task<PromotionResponseDto> CreateAsync(CreatePromotionRequest request, CancellationToken ct = default)
        {
            if (request.DiscountValue <= 0)
                throw new BusinessRuleException("El valor del descuento debe ser mayor a 0.", "INVALID_DISCOUNT");

            var promotion = new Promotion
            {
                PromotionName = request.PromotionName,
                IdDiscountType = request.IdDiscountType,
                DiscountValue = request.DiscountValue,
                DateFrom = request.DateFrom,
                DateTo = request.DateTo,
                MinPersons = request.MinPersons,
                IsActive = true
            };

            await _uow.Promotions.AddAsync(promotion, ct);
            await _uow.SaveChangesAsync(ct);

            foreach (var detail in request.Details)
                await _uow.PromotionDetails.AddAsync(new PromotionDetail
                {
                    IdPromotion = promotion.Id,
                    IdService = detail.IdService,
                    IdPackage = detail.IdPackage
                }, ct);

            await _uow.SaveChangesAsync(ct);
            return await GetByIdAsync(promotion.Id, ct);
        }

        public async Task ToggleActiveAsync(long id, CancellationToken ct = default)
        {
            var promotion = await _uow.Promotions.GetByIdAsync(id, ct)
                ?? throw new NotFoundException("Promoción", id);
            promotion.IsActive = !promotion.IsActive;
            _uow.Promotions.Update(promotion);
            await _uow.SaveChangesAsync(ct);
        }

        public async Task DeleteAsync(long id, CancellationToken ct = default)
        {
            var entity = await _uow.Promotions.GetByIdAsync(id, ct) ?? throw new NotFoundException("Promoción", id);
            _uow.Promotions.Delete(entity);
            await _uow.SaveChangesAsync(ct);
        }
    }
}
