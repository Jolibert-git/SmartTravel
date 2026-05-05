using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Application.Request
{
    public class FlightSeatAssignmentRequest
    {
        public long IdFlightSeat { get; set; }
        public long IdPassenger { get; set; }
    }
}
