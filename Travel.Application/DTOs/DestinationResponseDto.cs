using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Application.DTOs
{
    public class DestinationResponseDto
    {
        public long Id { get; set; }
        public string City { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;
        public string FullAddress { get; set; } = string.Empty;
        public CountryDto Country { get; set; } = null!;
    }
}
