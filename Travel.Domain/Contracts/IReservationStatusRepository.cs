using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Entities;

namespace Travel.Domain.Contracts
{
    public interface IReservationStatusRepository : IGenericRepository<ReservationStatus>
    {
        /// <summary>Busca un estado por descripción.</summary>
        Task<ReservationStatus?> GetByDescriptionAsync(string description, CancellationToken cancellationToken = default);
    }
}
