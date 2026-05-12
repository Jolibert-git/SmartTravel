using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Entities;

namespace Travel.Domain.Contracts
{
    public interface IPaymentRepository : IGenericRepository<Payment>
    {
        /// <summary>IQueryable de pagos de una reserva.</summary>
        IQueryable<Payment> GetByReservation(long reservationId);

        /// <summary>Suma el total pagado de una reserva.</summary>
        Task<decimal> GetTotalPaidAsync(long reservationId, CancellationToken cancellationToken = default);

        /// <summary>IQueryable de pagos filtrados por estado.</summary>
        IQueryable<Payment> GetByStatus(long statusId);

        /// <summary>IQueryable de pagos en un rango de fechas (reportes).</summary>
        IQueryable<Payment> GetByDateRange(DateTime from, DateTime to);

        /// <summary>IQueryable con método y estado de pago incluidos.</summary>
        IQueryable<Payment> GetWithDetails();
    }
}
