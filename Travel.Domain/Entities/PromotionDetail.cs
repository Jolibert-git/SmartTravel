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
    [Table("promotion_detail")]
    public class PromotionDetail: HasId
    {
        

        [Column("id_promotion")]
        public long IdPromotion { get; set; }

        [Column("id_service")]
        public long? IdService { get; set; }

        [Column("id_package")]
        public long? IdPackage { get; set; }

        // Navigation
        public Promotion Promotion { get; set; } = null!;
        public OfferedService? OfferedService { get; set; }
        public Package? Package { get; set; }

        // => Para el frontend: aplica a servicio o paquete
        [NotMapped]
        public string AppliesTo => IdService.HasValue ? "Servicio" : IdPackage.HasValue ? "Paquete" : "N/A";
    }
}
