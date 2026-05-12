using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Application.DTOs
{
    public class PaymentMethodDto
    {
        public long Id { get; set; }
        public string MethodName { get; set; } = string.Empty;
        public string? PayDescription { get; set; }
        public string IconName { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}
