using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Entities;

namespace Travel.Domain.Contracts
{
    public interface IRoomRepository : IGenericRepository<Room>
    {
        /// <summary>IQueryable de habitaciones de un hotel.</summary>
        IQueryable<Room> GetByHotel(long hotelId);

        /// <summary>IQueryable de habitaciones filtradas por tipo.</summary>
        IQueryable<Room> GetByType(long typeRoomId);

        /// <summary>IQueryable con hotel, tipo de habitación y servicio incluidos.</summary>
        IQueryable<Room> GetWithDetails();

        /// <summary>IQueryable de habitaciones disponibles en un rango de fechas.</summary>
        IQueryable<Room> GetAvailable(long hotelId, System.DateTime checkIn, System.DateTime checkOut);
    }
}
