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
    [Table("phone_hotel")]
    public class PhoneHotel: HasId
    {
        

        [Required]
        [MaxLength(15)]
        [Column("phone_number")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Column("id_hotel")]
        public long IdHotel { get; set; }

        // Navigation
        [ForeignKey("IdHotel")]
        public Hotel Hotel { get; set; } = null!;
    }
}
