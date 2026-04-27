using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Domain.Entities
{
    [Table("detail_package")]
    public class DetailPackage
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Required]
        [Column("number_persons")]
        public int NumberPersons { get; set; }

        [Required]
        [Column("cost_price")]
        public decimal CostPrice { get; set; }

        [Column("id_service")]
        public long IdService { get; set; }

        [Column("id_package")]
        public long IdPackage { get; set; }

        // Navigation
        public OfferedService OfferedService { get; set; } = null!;
        public Package Package { get; set; } = null!;

        // => Para el frontend: costo por persona calculado
        [NotMapped]
        public decimal CostPerPerson => NumberPersons > 0 ? CostPrice / NumberPersons : 0;
    }
}
