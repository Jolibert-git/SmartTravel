using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Application.DTOs
{
    public class DashboardSummaryDto
    {
        public int TotalReservations { get; set; }
        public int ActivePackages { get; set; }
        public int TotalPassengers { get; set; }
        public decimal TotalRevenue { get; set; }
        public string FormattedRevenue { get; set; } = string.Empty;
        public Dictionary<string, int> ReservationsByStatus { get; set; } = new();
    }
}
