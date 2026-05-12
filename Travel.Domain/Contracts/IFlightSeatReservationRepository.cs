using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Entities;

namespace Travel.Domain.Contracts
{
    public interface IFlightSeatReservationRepository : IGenericRepository<FlightSeatReservation>
    {
        /// <summary>IQueryable de asignaciones de asientos por detalle de reserva.</summary>
        IQueryable<FlightSeatReservation> GetByDetailReservation(long detailReservationId);

        /// <summary>IQueryable de asignaciones de un pasajero en todos sus vuelos.</summary>
        IQueryable<FlightSeatReservation> GetByPassenger(long passengerId);

        /// <summary>IQueryable con asiento, clase y pasajero incluidos.</summary>
        IQueryable<FlightSeatReservation> GetWithDetails();

        /// <summary>Verifica si un asiento ya está asignado en una reserva.</summary>
        Task<bool> SeatAlreadyReservedAsync(long flightSeatId, CancellationToken cancellationToken = default);
    }
}
