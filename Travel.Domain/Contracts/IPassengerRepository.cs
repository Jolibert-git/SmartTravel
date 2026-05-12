using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Entities;

namespace Travel.Domain.Contracts
{
    public interface IPassengerRepository : IGenericRepository<Passenger>
    {
        /// <summary>Busca pasajero por número de documento.</summary>
        Task<Passenger?> GetByDocumentAsync(string documentNumber, CancellationToken cancellationToken = default);

        /// <summary>Verifica si el documento ya está registrado.</summary>
        Task<bool> DocumentExistsAsync(string documentNumber, CancellationToken cancellationToken = default);

        /// <summary>IQueryable de pasajeros de una reserva.</summary>
        IQueryable<Passenger> GetByReservation(long reservationId);

        /// <summary>IQueryable con tipo de documento y país incluidos.</summary>
        IQueryable<Passenger> GetWithDetails();
    }
}
