using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Application.DTOs
{
    public class DocumentTypeDto
    {
        public long Id { get; set; }
        public string DocumentName { get; set; } = string.Empty;
    }
}
