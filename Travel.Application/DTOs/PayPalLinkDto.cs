using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Application.DTOs
{
    public class PayPalLinkDto
    {
        public string Href { get; set; } = string.Empty;

        public string Rel { get; set; } = string.Empty;

        public string Method { get; set; } = string.Empty;
    }
}
