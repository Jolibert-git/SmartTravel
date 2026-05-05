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
    public class ReservationRepository : GenericRepository<Reservation>, IReservationRepository
    {
        public ReservationRepository(DbContext context) : base(context) { }

        public IQueryable<Reservation> GetByUser(long userId)
            => _dbSet.AsNoTracking()
                     .Include(r => r.ReservationStatus)
                     .Include(r => r.Package)
                     .Where(r => r.IdSystemUser == userId)
                     .OrderByDescending(r => r.DateRequest);

        public IQueryable<Reservation> GetByStatus(long statusId)
            => _dbSet.AsNoTracking()
                     .Include(r => r.SystemsUser)
                     .Include(r => r.ReservationStatus)
                     .Where(r => r.IdReservationStatus == statusId)
                     .OrderByDescending(r => r.DateRequest);

        public IQueryable<Reservation> GetWithFullDetails()
            => _dbSet.AsNoTracking()
                     .Include(r => r.ReservationStatus)
                     .Include(r => r.SystemsUser)
                     .Include(r => r.Package)
                     .Include(r => r.DetailReservations)
                         .ThenInclude(dr => dr.OfferedService)
                     .Include(r => r.DetailReservations)
                         .ThenInclude(dr => dr.Room)
                             .ThenInclude(rm => rm!.Hotel)
                     .Include(r => r.DetailReservations)
                         .ThenInclude(dr => dr.Flight)
                             .ThenInclude(fl => fl!.AirportOrigen)
                     .Include(r => r.DetailReservations)
                         .ThenInclude(dr => dr.Flight)
                             .ThenInclude(fl => fl!.AirportArrive)
                     .Include(r => r.DetailReservations)
                         .ThenInclude(dr => dr.Vehicle)
                     .Include(r => r.ReservationPassengers)
                         .ThenInclude(rp => rp.Passenger)
                     .Include(r => r.Payments)
                         .ThenInclude(p => p.PaymentStatus)
                     .Include(r => r.ReservationPromotions)
                         .ThenInclude(rp => rp.Promotion);

        public IQueryable<Reservation> GetByDateRange(DateTime from, DateTime to)
            => _dbSet.AsNoTracking()
                     .Include(r => r.ReservationStatus)
                     .Include(r => r.SystemsUser)
                     .Where(r => r.DateRequest >= from && r.DateRequest <= to)
                     .OrderByDescending(r => r.DateRequest);

        public IQueryable<Reservation> GetPaged(int page, int pageSize)
            => _dbSet.AsNoTracking()
                     .Include(r => r.ReservationStatus)
                     .Include(r => r.SystemsUser)
                     .OrderByDescending(r => r.DateRequest)
                     .Skip((page - 1) * pageSize)
                     .Take(pageSize);

        public async Task<Dictionary<string, int>> GetCountByStatusAsync(CancellationToken cancellationToken = default)
            => await _dbSet.AsNoTracking()
                           .Include(r => r.ReservationStatus)
                           .GroupBy(r => r.ReservationStatus.StatusDescription)
                           .Select(g => new { Status = g.Key, Count = g.Count() })
                           .ToDictionaryAsync(x => x.Status, x => x.Count, cancellationToken);
    }
}
