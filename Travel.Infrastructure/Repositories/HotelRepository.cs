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
    public class HotelRepository : GenericRepository<Hotel>, IHotelRepository
    {
        public HotelRepository(DbContext context) : base(context) { }

        public IQueryable<Hotel> GetByDestination(long destinationId)
            => _dbSet.AsNoTracking()
                     .Include(h => h.PhoneHotels)
                     .Where(h => h.IdDestination == destinationId);

        public IQueryable<Hotel> GetByStars(int stars)
            => _dbSet.AsNoTracking()
                     .Include(h => h.Destination)
                         .ThenInclude(d => d.Country)
                     .Where(h => h.Stars == stars);

        public IQueryable<Hotel> GetWithDetails()
            => _dbSet.AsNoTracking()
                     .Include(h => h.Destination)
                         .ThenInclude(d => d.Country)
                     .Include(h => h.PhoneHotels)
                     .Include(h => h.Rooms)
                         .ThenInclude(r => r.TypeRoom);

        public IQueryable<Hotel> GetAvailable(long destinationId, DateTime checkIn, DateTime checkOut)
            => _dbSet.AsNoTracking()
                     .Include(h => h.Destination)
                     .Where(h =>
                         h.IdDestination == destinationId &&
                         h.Rooms.Any(r =>
                             !r.DetailReservations.Any(dr =>
                                 dr.DateCheckIn < checkOut &&
                                 dr.DateCheckOut > checkIn)));
    }
}
