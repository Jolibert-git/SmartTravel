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
    public class PaymentRepository : GenericRepository<Payment>, IPaymentRepository
    {
        public PaymentRepository(DbContext context) : base(context) { }

        public IQueryable<Payment> GetByReservation(long reservationId)
            => _dbSet.AsNoTracking()
                     .Include(p => p.PaymentStatus)
                     .Include(p => p.PaymentMethod)
                     .Where(p => p.IdReservation == reservationId)
                     .OrderByDescending(p => p.PaymentDate);

        public async Task<decimal> GetTotalPaidAsync(long reservationId, CancellationToken cancellationToken = default)
            => await _dbSet.AsNoTracking()
                           .Where(p => p.IdReservation == reservationId)
                           .SumAsync(p => p.Amount, cancellationToken);

        public IQueryable<Payment> GetByStatus(long statusId)
            => _dbSet.AsNoTracking()
                     .Include(p => p.PaymentStatus)
                     .Include(p => p.PaymentMethod)
                     .Include(p => p.Reservation)
                     .Where(p => p.IdPaymentStatus == statusId)
                     .OrderByDescending(p => p.PaymentDate);

        public IQueryable<Payment> GetByDateRange(DateTime from, DateTime to)
            => _dbSet.AsNoTracking()
                     .Include(p => p.PaymentStatus)
                     .Include(p => p.PaymentMethod)
                     .Where(p => p.PaymentDate >= from && p.PaymentDate <= to)
                     .OrderByDescending(p => p.PaymentDate);

        public IQueryable<Payment> GetWithDetails()
            => _dbSet.AsNoTracking()
                     .Include(p => p.PaymentStatus)
                     .Include(p => p.PaymentMethod)
                     .Include(p => p.Reservation)
                         .ThenInclude(r => r.SystemsUser);
    }
}
