using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Entities;

namespace Travel.Domain.Contracts
{
    public interface IFlightRepository : IGenericRepository<Flight>
    {
        /// <summary>IQueryable de vuelos por origen, destino y fecha.</summary>
        IQueryable<Flight> Search(long originAirportId, long arriveAirportId, DateTime departureDate);

        /// <summary>IQueryable con aeropuertos, asientos y servicio incluidos.</summary>
        IQueryable<Flight> GetWithDetails();

        /// <summary>IQueryable de vuelos con asientos disponibles.</summary>
        IQueryable<Flight> GetWithAvailableSeats(long originId, long arriveId);

        /// <summary>Cuenta los asientos disponibles de un vuelo.</summary>
        Task<int> GetAvailableSeatsCountAsync(long flightId, CancellationToken cancellationToken = default);
    }
}
