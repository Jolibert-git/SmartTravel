using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Application.DTOs
{
    public class PassengerResponseDto
    {
        public long Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public int Age { get; set; }
        public bool IsMinor { get; set; }
        public string DocumentNumber { get; set; } = string.Empty;
        public DocumentTypeDto DocumentType { get; set; } = null!;
        public CountryDto Country { get; set; } = null!;
    }
}
