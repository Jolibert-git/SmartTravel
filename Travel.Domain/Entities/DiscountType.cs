using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Domain.Entities
{
    [Table("discount_type")]
    public class DiscountType
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Required]
        [MaxLength(45)]
        [Column("type_name")]
        public string TypeName { get; set; } = string.Empty;

        // Navigation
        public ICollection<Promotion> Promotions { get; set; } = new List<Promotion>();
    }
}
