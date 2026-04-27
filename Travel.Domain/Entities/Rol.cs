using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Domain.Entities
{
    [Table("rol")]
    public class Rol
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("name_rol")]
        public string NameRol { get; set; } = string.Empty;

        // Navigation
        public ICollection<RolUser> RolUsers { get; set; } = new List<RolUser>();

        // => Para el frontend: nombre legible del rol
        [NotMapped]
        public string DisplayName => NameRol?.ToUpper() ?? string.Empty;
    }
}
