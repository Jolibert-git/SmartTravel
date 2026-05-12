using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Application.Request
{
    public class SearchFlightRequest
    {
        public long OriginAirportId { get; set; }
        public long ArriveAirportId { get; set; }
        public DateTime DepartureDate { get; set; }
    }
}
