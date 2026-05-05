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
    [Table("reservation")]
    public class Reservation: HasId
    {
        

        [Column("date_request")]
        public DateTime DateRequest { get; set; } = DateTime.Now;

        [Column("id_reservation_status")]
        public long IdReservationStatus { get; set; }

        [Column("id_system_user")]
        public long IdSystemUser { get; set; }

        [Column("id_package")]
        public long? IdPackage { get; set; }

        // Navigation
        public ReservationStatus ReservationStatus { get; set; } = null!;
        public SystemsUser SystemsUser { get; set; } = null!;
        public Package? Package { get; set; }
        public ICollection<DetailReservation> DetailReservations { get; set; } = new List<DetailReservation>();
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
        public ICollection<ReservationPassenger> ReservationPassengers { get; set; } = new List<ReservationPassenger>();
        public ICollection<ReservationPromotion> ReservationPromotions { get; set; } = new List<ReservationPromotion>();

        // => Para el frontend: total acumulado de todos los detalles
        [NotMapped]
        public decimal TotalAmount => DetailReservations?.Sum(d => d.Total) ?? 0;

        // => Para el frontend: total formateado
        [NotMapped]
        public string FormattedTotal => $"${TotalAmount:N2}";

        // => Para el frontend: cantidad de pasajeros en esta reserva
        [NotMapped]
        public int PassengerCount => ReservationPassengers?.Count ?? 0;
    }
}
