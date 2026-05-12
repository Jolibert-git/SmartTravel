using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Entities;

namespace Travel.Domain.Contracts
{
    public interface IAirportRepository : IGenericRepository<Airport>
    {
        /// <summary>Busca un aeropuerto por código IATA.</summary>
        Task<Airport?> GetByIataCodeAsync(string iataCode, CancellationToken cancellationToken = default);

        /// <summary>IQueryable de aeropuertos en un destino.</summary>
        IQueryable<Airport> GetByDestination(long destinationId);

        /// <summary>IQueryable con vuelos de salida y llegada incluidos.</summary>
        IQueryable<Airport> GetWithFlights();
    }
}
