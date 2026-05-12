using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Application.Request
{
    public class CreateFlightRequest
    {
        public long IdService { get; set; }
        public DateTime DateDeparture { get; set; }
        public DateTime DateArrival { get; set; }
        public int Capacity { get; set; }
        public long AirportIdOrigen { get; set; }
        public long AirportIdArrive { get; set; }
    }
}
