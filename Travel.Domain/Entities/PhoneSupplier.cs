using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Domain.Entities
{
    [Table("phone_supplier")]
    public class PhoneSupplier
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Required]
        [MaxLength(15)]
        [Column("phone_number")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Column("id_supplier")]
        public long IdSupplier { get; set; }

        // Navigation
        public Supplier Supplier { get; set; } = null!;
    }
}

