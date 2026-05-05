using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Entities;

namespace Travel.Domain.Contracts
{
    public interface IPhoneAirportRepository : IGenericRepository<PhoneAirport>
    {
        /// <summary>IQueryable de teléfonos de un aeropuerto.</summary>
        IQueryable<PhoneAirport> GetByAirport(long airportId);

        /// <summary>Verifica si un número ya está registrado para ese aeropuerto.</summary>
        Task<bool> PhoneExistsAsync(long airportId, string phoneNumber, CancellationToken cancellationToken = default);

        /// <summary>Elimina todos los teléfonos de un aeropuerto (para reemplazarlos).</summary>
        Task DeleteByAirportAsync(long airportId, CancellationToken cancellationToken = default);
    }
}
