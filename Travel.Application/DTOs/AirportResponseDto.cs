using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Application.DTOs
{
    public class AirportResponseDto
    {
        public long Id { get; set; }
        public string AirportName { get; set; } = string.Empty;
        public string CodeIata { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public List<string> PhoneNumbers { get; set; } = new();
        public DestinationResponseDto Destination { get; set; } = null!;
    }
}
