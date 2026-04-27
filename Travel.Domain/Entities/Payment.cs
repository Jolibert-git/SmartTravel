using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Domain.Entities
{
    [Table("payment")]
    public class Payment
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Required]
        [Column("amount")]
        public decimal Amount { get; set; }

        [Column("payment_date")]
        public DateTime PaymentDate { get; set; } = DateTime.Now;

        [Column("id_payment_status")]
        public long IdPaymentStatus { get; set; }

        [Column("id_reservation")]
        public long IdReservation { get; set; }

        [Column("id_payment_method")]
        public long IdPaymentMethod { get; set; }

        // Navigation
        public PaymentStatus PaymentStatus { get; set; } = null!;
        public Reservation Reservation { get; set; } = null!;
        public PaymentMethod PaymentMethod { get; set; } = null!;

        // => Para el frontend: monto formateado
        [NotMapped]
        public string FormattedAmount => $"${Amount:N2}";

        // => Para el frontend: fecha formateada
        [NotMapped]
        public string PaymentDateDisplay => PaymentDate.ToString("dd/MM/yyyy HH:mm");
    }
}
