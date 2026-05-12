using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Application.DTOs
{
    public class FlightSeatResponseDto
    {
        public long Id { get; set; }
        public string SeatNumber { get; set; } = string.Empty;
        public string SeatLabel { get; set; } = string.Empty;
        public bool IsAvailable { get; set; }
        public string SeatStatusColor { get; set; } = string.Empty;
        public SeatClassDto SeatClass { get; set; } = null!;
    }
}
