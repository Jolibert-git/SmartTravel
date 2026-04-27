using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Domain.Entities
{
    [Table("supplier")]
    public class Supplier
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Required]
        [MaxLength(45)]
        [Column("company_name")]
        public string CompanyName { get; set; } = string.Empty;

        [Required]
        [MaxLength(15)]
        [Column("rnc")]
        public string Rnc { get; set; } = string.Empty;

        [Required]
        [MaxLength(60)]
        [Column("email")]
        public string Email { get; set; } = string.Empty;

        // Navigation
        public ICollection<OfferedService> OfferedServices { get; set; } = new List<OfferedService>();
        public ICollection<PhoneSupplier> PhoneSuppliers { get; set; } = new List<PhoneSupplier>();
    }
}
