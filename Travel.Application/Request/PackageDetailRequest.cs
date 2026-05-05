using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Application.Request
{
    public class PackageDetailRequest
    {
        public int NumberPersons { get; set; }
        public decimal CostPrice { get; set; }
        public long IdService { get; set; }
    }
}
