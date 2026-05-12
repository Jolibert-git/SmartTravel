using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Entities;

namespace Travel.Domain.Contracts
{
    public interface IReservationRepository : IGenericRepository<Reservation>
    {
        /// <summary>IQueryable de reservas de un usuario.</summary>
        IQueryable<Reservation> GetByUser(long userId);

        /// <summary>IQueryable de reservas por estado.</summary>
        IQueryable<Reservation> GetByStatus(long statusId);

        /// <summary>IQueryable con todos los includes para la vista detalle.</summary>
        IQueryable<Reservation> GetWithFullDetails();

        /// <summary>IQueryable de reservas en un rango de fechas.</summary>
        IQueryable<Reservation> GetByDateRange(DateTime from, DateTime to);

        /// <summary>IQueryable paginado ordenado por fecha descendente.</summary>
        IQueryable<Reservation> GetPaged(int page, int pageSize);

        /// <summary>Retorna el conteo de reservas agrupado por estado (para dashboard).</summary>
        Task<Dictionary<string, int>> GetCountByStatusAsync(CancellationToken cancellationToken = default);
    }
}
