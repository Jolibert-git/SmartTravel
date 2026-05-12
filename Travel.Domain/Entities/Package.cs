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
    [Table("package")]
    public class Package: HasId
    {
        

        [Required]
        [MaxLength(60)]
        [Column("package_name")]
        public string PackageName { get; set; } = string.Empty;

        [Required]
        [Column("price")]
        public decimal Price { get; set; }

        [Required]
        [Column("offer_start")]
        public DateTime OfferStart { get; set; }

        [Required]
        [Column("offer_end")]
        public DateTime OfferEnd { get; set; }

        // Navigation
        public ICollection<DetailPackage> DetailPackages { get; set; } = new List<DetailPackage>();
        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
        public ICollection<PromotionDetail> PromotionDetails { get; set; } = new List<PromotionDetail>();

        // => Para el frontend: precio formateado
        [NotMapped]
        public string FormattedPrice => $"${Price:N2}";

        // => Para el frontend: si la oferta está vigente
        [NotMapped]
        public bool IsOfferActive => DateTime.Now >= OfferStart && DateTime.Now <= OfferEnd;

        // => Para el frontend: badge de estado de la oferta
        [NotMapped]
        public string OfferStatus => IsOfferActive ? "Activo" : DateTime.Now < OfferStart ? "Próximamente" : "Expirado";
    }
}
