using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Entities;

namespace Travel.Domain.Contracts
{
    public interface IFlightSeatRepository : IGenericRepository<FlightSeat>
    {
        /// <summary>IQueryable de todos los asientos de un vuelo.</summary>
        IQueryable<FlightSeat> GetByFlight(long flightId);

        /// <summary>IQueryable de asientos disponibles de un vuelo.</summary>
        IQueryable<FlightSeat> GetAvailableByFlight(long flightId);

        /// <summary>IQueryable de asientos disponibles filtrados por clase.</summary>
        IQueryable<FlightSeat> GetAvailableByClass(long flightId, long seatClassId);

        /// <summary>Marca un asiento como disponible o no disponible.</summary>
        Task SetAvailabilityAsync(long seatId, bool isAvailable, CancellationToken cancellationToken = default);
    }

}
