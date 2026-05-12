using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Application.DTOs
{
    public class HotelResponseDto
    {
        public long Id { get; set; }
        public string HotelName { get; set; } = string.Empty;
        public int? Stars { get; set; }
        public string StarDisplay { get; set; } = string.Empty;
        public string? Email { get; set; }
        public List<string> PhoneNumbers { get; set; } = new();
        public DestinationResponseDto Destination { get; set; } = null!;
    }
}
