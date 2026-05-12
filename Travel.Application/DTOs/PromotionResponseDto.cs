using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Application.DTOs
{
    public class PromotionResponseDto
    {
        public long Id { get; set; }
        public string PromotionName { get; set; } = string.Empty;
        public decimal DiscountValue { get; set; }
        public string DiscountDisplay { get; set; } = string.Empty;
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public int MinPersons { get; set; }
        public bool IsActive { get; set; }
        public bool IsCurrentlyValid { get; set; }
        public int DaysRemaining { get; set; }
        public DiscountTypeDto DiscountType { get; set; } = null!;
        public List<PromotionDetailDto> Details { get; set; } = new();

    }
}
