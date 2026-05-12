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
    public class FlightRepository : GenericRepository<Flight>, IFlightRepository
    {
        public FlightRepository(DbContext context) : base(context) { }

        public IQueryable<Flight> Search(long originAirportId, long arriveAirportId, DateTime departureDate)
            => _dbSet.AsNoTracking()
                     .Include(f => f.AirportOrigen)
                     .Include(f => f.AirportArrive)
                     .Include(f => f.OfferedService)
                     .Where(f =>
                         f.AirportIdOrigen == originAirportId &&
                         f.AirportIdArrive == arriveAirportId &&
                         f.DateDeparture.Date == departureDate.Date)
                     .OrderBy(f => f.DateDeparture);

        public IQueryable<Flight> GetWithDetails()
            => _dbSet.AsNoTracking()
                     .Include(f => f.AirportOrigen)
                         .ThenInclude(a => a.Destination)
                             .ThenInclude(d => d.Country)
                     .Include(f => f.AirportArrive)
                         .ThenInclude(a => a.Destination)
                             .ThenInclude(d => d.Country)
                     .Include(f => f.OfferedService)
                     .Include(f => f.FlightSeats)
                         .ThenInclude(fs => fs.SeatClass);

        public IQueryable<Flight> GetWithAvailableSeats(long originId, long arriveId)
            => _dbSet.AsNoTracking()
                     .Include(f => f.AirportOrigen)
                     .Include(f => f.AirportArrive)
                     .Where(f =>
                         f.AirportIdOrigen == originId &&
                         f.AirportIdArrive == arriveId &&
                         f.FlightSeats.Any(fs => fs.IsAvailable));

        public async Task<int> GetAvailableSeatsCountAsync(long flightId, CancellationToken cancellationToken = default)
            => await _context.Set<FlightSeat>()
                             .AsNoTracking()
                             .CountAsync(fs => fs.IdFlight == flightId && fs.IsAvailable, cancellationToken);
    }
}
