using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Application.Request
{
    public class CreateDestinationRequest
    {
        public long IdCountry { get; set; }
        public string City { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;
    }
}
