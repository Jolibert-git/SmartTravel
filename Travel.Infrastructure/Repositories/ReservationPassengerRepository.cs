using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Contracts;
using Travel.Domain.Entities;

namespace Travel.Infrastructure.Repositories
{
    public class ReservationPassengerRepository : GenericRepository<ReservationPassenger>, IReservationPassengerRepository
    {
        public ReservationPassengerRepository(DbContext context) : base(context) { }

        public IQueryable<ReservationPassenger> GetByReservation(long reservationId)
            => _dbSet.AsNoTracking()
                     .Include(rp => rp.Passenger)
                         .ThenInclude(p => p.DocumentType)
                     .Include(rp => rp.Passenger)
                         .ThenInclude(p => p.Country)
                     .Where(rp => rp.IdReservation == reservationId);

        public IQueryable<ReservationPassenger> GetByPassenger(long passengerId)
            => _dbSet.AsNoTracking()
                     .Include(rp => rp.Reservation)
                         .ThenInclude(r => r.ReservationStatus)
                     .Where(rp => rp.IdPassenger == passengerId);

        public async Task<bool> ExistsAsync(long reservationId, long passengerId, CancellationToken cancellationToken = default)
            => await _dbSet.AsNoTracking()
                           .AnyAsync(rp =>
                               rp.IdReservation == reservationId &&
                               rp.IdPassenger == passengerId,
                               cancellationToken);

        public async Task DeleteByReservationAsync(long reservationId, CancellationToken cancellationToken = default)
        {
            var items = await _dbSet
                .Where(rp => rp.IdReservation == reservationId)
                .ToListAsync(cancellationToken);
            _dbSet.RemoveRange(items);
        }

    }
}
