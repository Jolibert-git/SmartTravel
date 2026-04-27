using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Domain.Entities
{
    [Table("flight")]
    public class Flight
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Column("id_service")]
        public long IdService { get; set; }

        [Required]
        [Column("date_departure")]
        public DateTime DateDeparture { get; set; }

        [Required]
        [Column("date_arrival")]
        public DateTime DateArrival { get; set; }

        [Required]
        [Column("capacity")]
        public int Capacity { get; set; }

        [Column("airport_id_origen")]
        public long AirportIdOrigen { get; set; }

        [Column("airport_id_arrive")]
        public long AirportIdArrive { get; set; }

        // Navigation
        public OfferedService OfferedService { get; set; } = null!;
        public Airport AirportOrigen { get; set; } = null!;
        public Airport AirportArrive { get; set; } = null!;
        public ICollection<FlightSeat> FlightSeats { get; set; } = new List<FlightSeat>();
        public ICollection<DetailReservation> DetailReservations { get; set; } = new List<DetailReservation>();

        // => Para el frontend: duración del vuelo
        [NotMapped]
        public TimeSpan Duration => DateArrival - DateDeparture;

        // => Para el frontend: duración formateada
        [NotMapped]
        public string DurationDisplay => $"{(int)Duration.TotalHours}h {Duration.Minutes}m";

        // => Para el frontend: ruta del vuelo
        [NotMapped]
        public string RouteDisplay =>
            $"{AirportOrigen?.CodeIata ?? "?"} → {AirportArrive?.CodeIata ?? "?"}";
    }
}
