using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Entities;

namespace Travel.Domain.Contracts
{
    public interface IDetailReservationRepository : IGenericRepository<DetailReservation>
    {
        /// <summary>IQueryable de detalles de una reserva.</summary>
        IQueryable<DetailReservation> GetByReservation(long reservationId);

        /// <summary>IQueryable con servicio, habitación, vuelo y vehículo incluidos.</summary>
        IQueryable<DetailReservation> GetWithDetails();
    }

    // ============================================================
    // IDiscountTypeRepository
    // ============================================================
    public interface IDiscountTypeRepository : IGenericRepository<DiscountType>
    {
        /// <summary>Busca tipo de descuento por nombre.</summary>
        Task<DiscountType?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
    }
}
