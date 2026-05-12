using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Application.DTOs
{
    public class VehicleResponseDto
    {
        public long Id { get; set; }
        public string Make { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public string FullVehicleName { get; set; } = string.Empty;
        public string? Transmission { get; set; }
        public string TransmissionLabel { get; set; } = string.Empty;
        public DestinationResponseDto Destination { get; set; } = null!;
        public ServiceResponseDto Service { get; set; } = null!;
    }
}
