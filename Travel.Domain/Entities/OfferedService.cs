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
    [Table("offered_service")]
    public class OfferedService: HasId
    {
        

        [Required]
        [MaxLength(45)]
        [Column("service_description")]
        public string ServiceDescription { get; set; } = string.Empty;

        [Required]
        [Column("price")]
        public decimal Price { get; set; }

        [Column("id_type_service")]
        public long IdTypeService { get; set; }

        [Column("id_supplier")]
        public long IdSupplier { get; set; }

        // Navigation
        [ForeignKey("IdTypeService")]
        public TypeService TypeService { get; set; } = null!;
        [ForeignKey("IdSupplier")]
        public Supplier Supplier { get; set; } = null!;
        public ICollection<ServiceAvailability> ServiceAvailabilities { get; set; } = new List<ServiceAvailability>();
        public ICollection<DetailPackage> DetailPackages { get; set; } = new List<DetailPackage>();
        public ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
        public ICollection<Room> Rooms { get; set; } = new List<Room>();
        public ICollection<Flight> Flights { get; set; } = new List<Flight>();
        public ICollection<DetailReservation> DetailReservations { get; set; } = new List<DetailReservation>();
        public ICollection<PromotionDetail> PromotionDetails { get; set; } = new List<PromotionDetail>();

        // => Para el frontend: precio formateado con símbolo
        [NotMapped]
        public string FormattedPrice => $"${Price:N2}";

        // => Para el frontend: etiqueta del tipo de servicio (sin navegar al objeto)
        [NotMapped]
        public string ServiceTypeLabel => TypeService?.TypeDescription ?? string.Empty;
    }
}
