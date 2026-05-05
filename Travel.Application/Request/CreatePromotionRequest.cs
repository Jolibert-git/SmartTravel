using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Application.Request
{
    public class CreatePromotionRequest
    {
        public string PromotionName { get; set; } = string.Empty;
        public long IdDiscountType { get; set; }
        public decimal DiscountValue { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public int MinPersons { get; set; } = 1;
        public List<PromotionDetailRequest> Details { get; set; } = new();
    }
}
