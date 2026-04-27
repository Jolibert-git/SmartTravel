using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Domain.Entities
{
    [Table("detail_reservation")]
    public class DetailReservation
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Required]
        [Column("date_check_in")]
        public DateTime DateCheckIn { get; set; }

        [Required]
        [Column("date_check_out")]
        public DateTime DateCheckOut { get; set; }

        [Required]
        [Column("total")]
        public decimal Total { get; set; }

        [Column("id_reservation")]
        public long IdReservation { get; set; }

        [Column("id_service")]
        public long IdService { get; set; }

        [Column("id_room")]
        public long? IdRoom { get; set; }

        [Column("id_flight")]
        public long? IdFlight { get; set; }

        [Column("id_vehicle")]
        public long? IdVehicle { get; set; }

        // Navigation
        public Reservation Reservation { get; set; } = null!;
        public OfferedService OfferedService { get; set; } = null!;
        public Room? Room { get; set; }
        public Flight? Flight { get; set; }
        public Vehicle? Vehicle { get; set; }
        public ICollection<FlightSeatReservation> FlightSeatReservations { get; set; } = new List<FlightSeatReservation>();

        // => Para el frontend: noches / días de estadía
        [NotMapped]
        public int Nights => (DateCheckOut - DateCheckIn).Days;

        // => Para el frontend: rango de fechas
        [NotMapped]
        public string DateRangeDisplay => $"{DateCheckIn:dd/MM/yyyy} → {DateCheckOut:dd/MM/yyyy}";

        // => Para el frontend: tipo de servicio usado en este detalle
        [NotMapped]
        public string ServiceTypeLabel =>
            IdRoom.HasValue ? "Habitación" :
            IdFlight.HasValue ? "Vuelo" :
            IdVehicle.HasValue ? "Vehículo" : "Servicio";
    }
}
