using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Application.DTOs
{
    public class ReservationSummaryDto
    {
        public long Id { get; set; }
        public DateTime DateRequest { get; set; }
        public string StatusDescription { get; set; } = string.Empty;
        public string BadgeColor { get; set; } = string.Empty;
        public string FormattedTotal { get; set; } = string.Empty;
        public int PassengerCount { get; set; }
    }
}
