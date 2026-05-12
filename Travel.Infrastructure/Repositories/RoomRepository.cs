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
    public class RoomRepository : GenericRepository<Room>, IRoomRepository
    {
        public RoomRepository(DbContext context) : base(context) { }

        public IQueryable<Room> GetByHotel(long hotelId)
            => _dbSet.AsNoTracking()
                     .Include(r => r.TypeRoom)
                     .Where(r => r.IdHotel == hotelId);

        public IQueryable<Room> GetByType(long typeRoomId)
            => _dbSet.AsNoTracking()
                     .Include(r => r.Hotel)
                     .Where(r => r.IdTypeRoom == typeRoomId);

        public IQueryable<Room> GetWithDetails()
            => _dbSet.AsNoTracking()
                     .Include(r => r.Hotel)
                         .ThenInclude(h => h.Destination)
                             .ThenInclude(d => d.Country)
                     .Include(r => r.TypeRoom)
                     .Include(r => r.OfferedService);

        public IQueryable<Room> GetAvailable(long hotelId, DateTime checkIn, DateTime checkOut)
            => _dbSet.AsNoTracking()
                     .Include(r => r.TypeRoom)
                     .Include(r => r.OfferedService)
                     .Where(r =>
                         r.IdHotel == hotelId &&
                         !r.DetailReservations.Any(dr =>
                             dr.DateCheckIn < checkOut &&
                             dr.DateCheckOut > checkIn));
    }
}
