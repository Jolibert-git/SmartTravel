using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Application.DTOs
{
    public class RolDto
    {
        public long Id { get; set; }
        public string NameRol { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
    }
}
