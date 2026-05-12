using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Application.Request
{
    public class CreateHotelRequest
    {
        public long IdDestination { get; set; }
        public string HotelName { get; set; } = string.Empty;
        public int? Stars { get; set; }
        public string? Email { get; set; }
        public List<string> PhoneNumbers { get; set; } = new();
    }
}
