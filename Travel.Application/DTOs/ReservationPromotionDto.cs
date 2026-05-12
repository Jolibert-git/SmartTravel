using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Application.DTOs
{
    public class ReservationPromotionDto
    {
        public long Id { get; set; }
        public string PromotionName { get; set; } = string.Empty;
        public decimal DiscountApplied { get; set; }
        public string FormattedDiscount { get; set; } = string.Empty;
    }
}
