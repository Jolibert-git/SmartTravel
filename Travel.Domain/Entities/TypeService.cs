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
    [Table("type_service")]
    public class TypeService : HasId
    {
        

        [Required]
        [MaxLength(30)]
        [Column("type_description")]
        public string TypeDescription { get; set; } = string.Empty;

        // Navigation
        public ICollection<OfferedService> OfferedServices { get; set; } = new List<OfferedService>();
    }
}
