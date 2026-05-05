using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Application.DTOs
{
    public class FlightResponseDto
    {
        public long Id { get; set; }
        public DateTime DateDeparture { get; set; }
        public DateTime DateArrival { get; set; }
        public string DurationDisplay { get; set; } = string.Empty;
        public string RouteDisplay { get; set; } = string.Empty;
        public int Capacity { get; set; }
        public int AvailableSeats { get; set; }
        public AirportResponseDto AirportOrigen { get; set; } = null!;
        public AirportResponseDto AirportArrive { get; set; } = null!;
        public ServiceResponseDto Service { get; set; } = null!;
    }
}
