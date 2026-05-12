using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Application.DTOs
{
    public class PackageResponseDto
    {
        public long Id { get; set; }
        public string PackageName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string FormattedPrice { get; set; } = string.Empty;
        public DateTime OfferStart { get; set; }
        public DateTime OfferEnd { get; set; }
        public bool IsOfferActive { get; set; }
        public string OfferStatus { get; set; } = string.Empty;
        public List<PackageDetailDto> Details { get; set; } = new();

    }
}
