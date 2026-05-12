using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Application.DTOs
{
    public class PackageDetailDto
    {
        public long Id { get; set; }
        public int NumberPersons { get; set; }
        public decimal CostPrice { get; set; }
        public decimal CostPerPerson { get; set; }
        public ServiceResponseDto Service { get; set; } = null!;
    }
}
