using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Application.Request
{
    public class CreateCountryRequest
    {
        public string CountryName { get; set; } = string.Empty;
        public string IsoCode { get; set; } = string.Empty;
    }
}
