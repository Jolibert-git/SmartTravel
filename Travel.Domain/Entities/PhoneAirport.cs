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
    [Table("phone_airport")]
    public class PhoneAirport: HasId
    {
        

        [Required]
        [MaxLength(15)]
        [Column("phone_number")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Column("id_airport")]
        public long IdAirport { get; set; }

        // Navigation
        [ForeignKey("IdAirport")]
        public Airport Airport { get; set; } = null!;
    }
}
