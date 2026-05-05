using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Application.DTOs
{
    public class ServiceResponseDto
    {
        public long Id { get; set; }
        public string ServiceDescription { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string FormattedPrice { get; set; } = string.Empty;
        public TypeServiceDto TypeService { get; set; } = null!;
        public SupplierResponseDto Supplier { get; set; } = null!;
        public string? CurrentStatus { get; set; }
    }
}
