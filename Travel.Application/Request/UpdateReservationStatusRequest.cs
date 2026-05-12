using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Application.Request
{
    public class UpdateReservationStatusRequest
    {
        public long IdReservationStatus { get; set; }
        public string? Reason { get; set; }
    }
}
