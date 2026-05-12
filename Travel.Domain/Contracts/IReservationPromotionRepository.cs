using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Entities;

namespace Travel.Domain.Contracts
{
    public interface IReservationPromotionRepository: IGenericRepository<ReservationPromotion>
    {
        /// <summary>IQueryable de promociones aplicadas a una reserva.</summary>
        IQueryable<ReservationPromotion> GetByReservation(long reservationId);

        /// <summary>IQueryable de reservas donde se usó una promoción específica.</summary>
        IQueryable<ReservationPromotion> GetByPromotion(long promotionId);

        /// <summary>Suma el total de descuentos aplicados en una reserva.</summary>
        Task<decimal> GetTotalDiscountByReservationAsync(long reservationId, CancellationToken cancellationToken = default);

        /// <summary>Verifica si una promoción ya fue aplicada a una reserva.</summary>
        Task<bool> PromotionAlreadyAppliedAsync(long reservationId, long promotionId, CancellationToken cancellationToken = default);
    }
}
