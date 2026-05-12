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
    public class FlightSeatReservationRepository : GenericRepository<FlightSeatReservation>, IFlightSeatReservationRepository
    {
        public FlightSeatReservationRepository(DbContext context) : base(context) { }

        public IQueryable<FlightSeatReservation> GetByDetailReservation(long detailReservationId)
            => _dbSet.AsNoTracking()
                     .Include(fsr => fsr.FlightSeat)
                         .ThenInclude(fs => fs.SeatClass)
                     .Include(fsr => fsr.Passenger)
                     .Where(fsr => fsr.IdDetailReservation == detailReservationId);

        public IQueryable<FlightSeatReservation> GetByPassenger(long passengerId)
            => _dbSet.AsNoTracking()
                     .Include(fsr => fsr.FlightSeat)
                         .ThenInclude(fs => fs.Flight)
                             .ThenInclude(f => f.AirportOrigen)
                     .Include(fsr => fsr.FlightSeat)
                         .ThenInclude(fs => fs.Flight)
                             .ThenInclude(f => f.AirportArrive)
                     .Include(fsr => fsr.FlightSeat)
                         .ThenInclude(fs => fs.SeatClass)
                     .Where(fsr => fsr.IdPassenger == passengerId);

        public IQueryable<FlightSeatReservation> GetWithDetails()
            => _dbSet.AsNoTracking()
                     .Include(fsr => fsr.FlightSeat)
                         .ThenInclude(fs => fs.SeatClass)
                     .Include(fsr => fsr.FlightSeat)
                         .ThenInclude(fs => fs.Flight)
                     .Include(fsr => fsr.Passenger)
                         .ThenInclude(p => p.DocumentType)
                     .Include(fsr => fsr.DetailReservation);

        public async Task<bool> SeatAlreadyReservedAsync(long flightSeatId, CancellationToken cancellationToken = default)
            => await _dbSet.AsNoTracking()
                           .AnyAsync(fsr => fsr.IdFlightSeat == flightSeatId, cancellationToken);
    }
}
