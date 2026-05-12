using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Application.DTOs
{
    public class PaymentResponseDto
    {
        public long Id { get; set; }
        public decimal Amount { get; set; }
        public string FormattedAmount { get; set; } = string.Empty;
        public DateTime PaymentDate { get; set; }
        public string PaymentDateDisplay { get; set; } = string.Empty;
        public string StatusDescription { get; set; } = string.Empty;
        public string BadgeColor { get; set; } = string.Empty;
        public string MethodName { get; set; } = string.Empty;
        public string IconName { get; set; } = string.Empty;
    }
}
