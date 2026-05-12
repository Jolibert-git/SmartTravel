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
    public class FlightSeatRepository : GenericRepository<FlightSeat>, IFlightSeatRepository
    {
        public FlightSeatRepository(DbContext context) : base(context) { }

        public IQueryable<FlightSeat> GetByFlight(long flightId)
            => _dbSet.AsNoTracking()
                     .Include(fs => fs.SeatClass)
                     .Where(fs => fs.IdFlight == flightId)
                     .OrderBy(fs => fs.SeatNumber);

        public IQueryable<FlightSeat> GetAvailableByFlight(long flightId)
            => _dbSet.AsNoTracking()
                     .Include(fs => fs.SeatClass)
                     .Where(fs => fs.IdFlight == flightId && fs.IsAvailable)
                     .OrderBy(fs => fs.SeatNumber);

        public IQueryable<FlightSeat> GetAvailableByClass(long flightId, long seatClassId)
            => _dbSet.AsNoTracking()
                     .Include(fs => fs.SeatClass)
                     .Where(fs =>
                         fs.IdFlight == flightId &&
                         fs.IdSeatClass == seatClassId &&
                         fs.IsAvailable)
                     .OrderBy(fs => fs.SeatNumber);

        public async Task SetAvailabilityAsync(long seatId, bool isAvailable, CancellationToken cancellationToken = default)
        {
            var seat = await _dbSet.FindAsync(new object[] { seatId }, cancellationToken);
            if (seat is not null)
            {
                seat.IsAvailable = isAvailable;
                _context.Entry(seat).Property(s => s.IsAvailable).IsModified = true;
            }
        }
    }
}
