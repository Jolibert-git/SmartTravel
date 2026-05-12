using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Entities;

namespace Travel.Domain.Contracts
{
    public interface IReservationPassengerRepository : IGenericRepository<ReservationPassenger>
    {
        /// <summary>IQueryable de pasajeros de una reserva.</summary>
        IQueryable<ReservationPassenger> GetByReservation(long reservationId);

        /// <summary>IQueryable de reservas de un pasajero.</summary>
        IQueryable<ReservationPassenger> GetByPassenger(long passengerId);

        /// <summary>Verifica si un pasajero ya está vinculado a una reserva.</summary>
        Task<bool> ExistsAsync(long reservationId, long passengerId, CancellationToken cancellationToken = default);

        /// <summary>Elimina todos los pasajeros de una reserva.</summary>
        Task DeleteByReservationAsync(long reservationId, CancellationToken cancellationToken = default);
    }
}
