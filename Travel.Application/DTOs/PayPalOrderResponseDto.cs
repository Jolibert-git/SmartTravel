using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Application.DTOs
{
    public class PayPalOrderResponseDto
    {
        public string Id { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;

        public List<PayPalLinkDto> Links { get; set; } = [];
    }
}
