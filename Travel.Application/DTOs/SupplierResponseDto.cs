using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Application.DTOs
{
    public class SupplierResponseDto
    {
        public long Id { get; set; }
        public string CompanyName { get; set; } = string.Empty;
        public string Rnc { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public List<string> PhoneNumbers { get; set; } = new();
    }
}
