using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Core;

namespace Travel.Domain.Entities
{
    [Table("type_room")]
    public class TypeRoom : HasId
    {
        

        [Required]
        [MaxLength(45)]
        [Column("type_description")]
        public string TypeDescription { get; set; } = string.Empty;

        // Navigation
        public ICollection<Room> Rooms { get; set; } = new List<Room>();
    }
}
