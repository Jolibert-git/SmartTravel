using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Application.DTOs
{
    public class ReservationResponseDto
    {
        public long Id { get; set; }
        public DateTime DateRequest { get; set; }
        public string StatusDescription { get; set; } = string.Empty;
        public string BadgeColor { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }
        public string FormattedTotal { get; set; } = string.Empty;
        public int PassengerCount { get; set; }
        public UserSummaryDto User { get; set; } = null!;
        public PackageResponseDto? Package { get; set; }
        public List<ReservationDetailDto> Details { get; set; } = new();
        public List<PassengerResponseDto> Passengers { get; set; } = new();
        public List<PaymentResponseDto> Payments { get; set; } = new();
        public List<ReservationPromotionDto> Promotions { get; set; } = new();
    }
}
