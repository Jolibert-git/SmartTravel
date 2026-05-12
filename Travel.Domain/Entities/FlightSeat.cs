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
    [Table("flight_seat")]
    public class FlightSeat: HasId
    {
        

        [Column("id_flight")]
        public long IdFlight { get; set; }

        [Column("id_seat_class")]
        public long IdSeatClass { get; set; }

        [Required]
        [MaxLength(5)]
        [Column("seat_number")]
        public string SeatNumber { get; set; } = string.Empty;

        [Column("is_available")]
        public bool IsAvailable { get; set; } = true;

        // Navigation
        [ForeignKey("IdFlight")]
        public Flight Flight { get; set; } = null!;
        [ForeignKey("IdSeatClass")]
        public SeatClass SeatClass { get; set; } = null!;
        public ICollection<FlightSeatReservation> FlightSeatReservations { get; set; } = new List<FlightSeatReservation>();

        // => Para el frontend: etiqueta del asiento con clase
        [NotMapped]
        public string SeatLabel => $"{SeatNumber} – {SeatClass?.ClassName}";

        // => Para el frontend: color del asiento en el mapa
        [NotMapped]
        public string SeatStatusColor => IsAvailable ? "green" : "red";
    }
}
