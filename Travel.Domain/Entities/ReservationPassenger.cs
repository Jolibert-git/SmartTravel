using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Core;

namespace Travel.Domain.Entities
{
    [Table("reservation_passenger")]
    public class ReservationPassenger
    {
        [Column("id_reservation")]
        public long IdReservation { get; set; }

        [Column("id_passenger")]
        public long IdPassenger { get; set; }

        // Navigation
        public Reservation Reservation { get; set; } = null!;
        public Passenger Passenger { get; set; } = null!;
    }
}
