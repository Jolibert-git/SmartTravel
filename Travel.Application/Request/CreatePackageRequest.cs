using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Application.Request
{
    public class CreatePackageRequest
    {
        public string PackageName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public DateTime OfferStart { get; set; }
        public DateTime OfferEnd { get; set; }
        public List<PackageDetailRequest> Details { get; set; } = new();
    }
}
