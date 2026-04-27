using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Domain.Entities
{
    [Table("document_type")]
    public class DocumentType
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Required]
        [MaxLength(30)]
        [Column("document_name")]
        public string DocumentName { get; set; } = string.Empty;

        // Navigation
        public ICollection<Passenger> Passengers { get; set; } = new List<Passenger>();
    }
}
