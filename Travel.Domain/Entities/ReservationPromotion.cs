using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Core;

namespace Travel.Domain.Entities
{
    [Table("reservation_promotion")]
    public class ReservationPromotion: HasId
    {
        

        [Column("id_reservation")]
        public long IdReservation { get; set; }

        [Column("id_promotion")]
        public long IdPromotion { get; set; }

        [Required]
        [Column("discount_applied")]
        public decimal DiscountApplied { get; set; }

        // Navigation
        public Reservation Reservation { get; set; } = null!;
        public Promotion Promotion { get; set; } = null!;

        // => Para el frontend: descuento formateado
        [NotMapped]
        public string FormattedDiscount => $"-${DiscountApplied:N2}";
    }
}
