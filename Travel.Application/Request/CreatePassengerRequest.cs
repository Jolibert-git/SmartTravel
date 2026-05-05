using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Application.Request
{
    public class CreatePassengerRequest
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public string DocumentNumber { get; set; } = string.Empty;
        public long IdDocumentType { get; set; }
        public long IdCountry { get; set; }
    }
}
