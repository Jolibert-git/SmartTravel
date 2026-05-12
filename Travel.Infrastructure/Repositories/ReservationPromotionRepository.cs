using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Entities;
using Travel.Domain.Contracts;

namespace Travel.Infrastructure.Repositories
{
    public class ReservationPromotionRepository : GenericRepository<ReservationPromotion>, IReservationPromotionRepository
    {
        public ReservationPromotionRepository(DbContext context) : base(context) { }

        public IQueryable<ReservationPromotion> GetByReservation(long reservationId)
            => _dbSet.AsNoTracking()
                     .Include(rp => rp.Promotion)
                         .ThenInclude(p => p.DiscountType)
                     .Where(rp => rp.IdReservation == reservationId);

        public IQueryable<ReservationPromotion> GetByPromotion(long promotionId)
            => _dbSet.AsNoTracking()
                     .Include(rp => rp.Reservation)
                         .ThenInclude(r => r.SystemsUser)
                     .Where(rp => rp.IdPromotion == promotionId);

        public async Task<decimal> GetTotalDiscountByReservationAsync(long reservationId, CancellationToken cancellationToken = default)
            => await _dbSet.AsNoTracking()
                           .Where(rp => rp.IdReservation == reservationId)
                           .SumAsync(rp => rp.DiscountApplied, cancellationToken);

        public async Task<bool> PromotionAlreadyAppliedAsync(long reservationId, long promotionId, CancellationToken cancellationToken = default)
            => await _dbSet.AsNoTracking()
                           .AnyAsync(rp =>
                               rp.IdReservation == reservationId &&
                               rp.IdPromotion == promotionId,
                               cancellationToken);
    }
}
