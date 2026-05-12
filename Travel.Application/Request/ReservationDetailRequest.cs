using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Application.Request
{
    public class ReservationDetailRequest
    {
        public DateTime DateCheckIn { get; set; }
        public DateTime DateCheckOut { get; set; }
        public long IdService { get; set; }
        public long? IdRoom { get; set; }
        public long? IdFlight { get; set; }
        public long? IdVehicle { get; set; }
        public List<FlightSeatAssignmentRequest> SeatAssignments { get; set; } = new();
    }
}
