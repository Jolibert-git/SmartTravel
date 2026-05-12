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
    [Table("availability_status")]
    public class AvailabilityStatus : HasId
    {
        

        [Required]
        [MaxLength(45)]
        [Column("status_description")]
        public string StatusDescription { get; set; } = string.Empty;

        // Navigation
        public ICollection<ServiceAvailability> ServiceAvailabilities { get; set; } = new List<ServiceAvailability>();
    }
}
