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
    public class DetailReservationRepository : GenericRepository<DetailReservation>, IDetailReservationRepository
    {
        public DetailReservationRepository(DbContext context) : base(context) { }

        public IQueryable<DetailReservation> GetByReservation(long reservationId)
            => _dbSet.AsNoTracking()
                     .Include(dr => dr.OfferedService)
                     .Include(dr => dr.Room)
                     .Include(dr => dr.Flight)
                     .Include(dr => dr.Vehicle)
                     .Where(dr => dr.IdReservation == reservationId);

        public IQueryable<DetailReservation> GetWithDetails()
            => _dbSet.AsNoTracking()
                     .Include(dr => dr.OfferedService)
                     .Include(dr => dr.Room)
                         .ThenInclude(r => r!.Hotel)
                     .Include(dr => dr.Flight)
                         .ThenInclude(f => f!.AirportOrigen)
                     .Include(dr => dr.Flight)
                         .ThenInclude(f => f!.AirportArrive)
                     .Include(dr => dr.Vehicle)
                     .Include(dr => dr.FlightSeatReservations)
                         .ThenInclude(fsr => fsr.FlightSeat)
                             .ThenInclude(fs => fs.SeatClass);
    }


}
