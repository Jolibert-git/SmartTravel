using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Domain.Entities
{
    [Table("rol_user")]
    public class RolUser
    {
        [Column("id_rol")]
        public long IdRol { get; set; }

        [Column("id_system_user")]
        public long IdSystemUser { get; set; }

        // Navigation
        public Rol Rol { get; set; } = null!;
        public SystemsUser SystemsUser { get; set; } = null!;
    }

   
   
}
