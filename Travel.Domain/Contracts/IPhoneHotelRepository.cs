using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Entities;

namespace Travel.Domain.Contracts
{
    public interface IPhoneHotelRepository : IGenericRepository<PhoneHotel>
    {
        /// <summary>IQueryable de teléfonos de un hotel.</summary>
        IQueryable<PhoneHotel> GetByHotel(long hotelId);

        /// <summary>Verifica si un número ya está registrado para ese hotel.</summary>
        Task<bool> PhoneExistsAsync(long hotelId, string phoneNumber, CancellationToken cancellationToken = default);

        /// <summary>Elimina todos los teléfonos de un hotel (para reemplazarlos).</summary>
        Task DeleteByHotelAsync(long hotelId, CancellationToken cancellationToken = default);
    }
}
