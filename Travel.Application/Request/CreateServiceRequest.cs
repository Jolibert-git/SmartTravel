using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Application.Request
{
    public class CreateServiceRequest
    {
        public string ServiceDescription { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public long IdTypeService { get; set; }
        public long IdSupplier { get; set; }
    }
}
