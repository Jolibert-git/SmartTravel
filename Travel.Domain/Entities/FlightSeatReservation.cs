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
    [Table("flight_seat_reservation")]
    public class FlightSeatReservation: HasId
    {
        

        [Column("id_flight_seat")]
        public long IdFlightSeat { get; set; }

        [Column("id_passenger")]
        public long IdPassenger { get; set; }

        [Column("id_detail_reservation")]
        public long IdDetailReservation { get; set; }

        // Navigation
        public FlightSeat FlightSeat { get; set; } = null!;
        public Passenger Passenger { get; set; } = null!;
        public DetailReservation DetailReservation { get; set; } = null!;
    }
}
