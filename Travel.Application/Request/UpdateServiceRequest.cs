using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Application.Request
{
    public class UpdateServiceRequest
    {
        public string ServiceDescription { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }

}
