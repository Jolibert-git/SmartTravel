using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Application.DTOs
{
    public class ReservationDetailDto
    {
        public long Id { get; set; }
        public DateTime DateCheckIn { get; set; }
        public DateTime DateCheckOut { get; set; }
        public string DateRangeDisplay { get; set; } = string.Empty;
        public int Nights { get; set; }
        public decimal Total { get; set; }
        public string ServiceTypeLabel { get; set; } = string.Empty;
        public ServiceResponseDto Service { get; set; } = null!;
        public RoomResponseDto? Room { get; set; }
        public FlightResponseDto? Flight { get; set; }
        public VehicleResponseDto? Vehicle { get; set; }
    }
}
