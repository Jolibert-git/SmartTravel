using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Application.Request
{
    public class CreateAirportRequest
    {
        public long IdDestination { get; set; }
        public string AirportName { get; set; } = string.Empty;
        public string CodeIata { get; set; } = string.Empty;
        public List<string> PhoneNumbers { get; set; } = new();
    }
}
