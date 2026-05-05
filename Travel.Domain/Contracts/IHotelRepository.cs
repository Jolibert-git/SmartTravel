using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Entities;

namespace Travel.Domain.Contracts
{
    public interface IHotelRepository : IGenericRepository<Hotel>
    {
        /// <summary>IQueryable de hoteles en un destino.</summary>
        IQueryable<Hotel> GetByDestination(long destinationId);

        /// <summary>IQueryable filtrado por estrellas.</summary>
        IQueryable<Hotel> GetByStars(int stars);

        /// <summary>IQueryable con destino, teléfonos y habitaciones.</summary>
        IQueryable<Hotel> GetWithDetails();

        /// <summary>IQueryable de hoteles con habitaciones disponibles en el rango de fechas.</summary>
        IQueryable<Hotel> GetAvailable(long destinationId, DateTime checkIn, DateTime checkOut);
    }

}
