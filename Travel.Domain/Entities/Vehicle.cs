using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Domain.Entities
{
    [Table("vehicle")]
    public class Vehicle
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Column("id_service")]
        public long IdService { get; set; }

        [Column("id_destination")]
        public long IdDestination { get; set; }

        [Required]
        [MaxLength(45)]
        [Column("make")]
        public string Make { get; set; } = string.Empty;

        [Required]
        [MaxLength(45)]
        [Column("model")]
        public string Model { get; set; } = string.Empty;

        [MaxLength(45)]
        [Column("transmission")]
        public string? Transmission { get; set; }

        // Navigation
        public OfferedService OfferedService { get; set; } = null!;
        public Destination Destination { get; set; } = null!;
        public ICollection<DetailReservation> DetailReservations { get; set; } = new List<DetailReservation>();

        // => Para el frontend: nombre completo del vehículo
        [NotMapped]
        public string FullVehicleName => $"{Make} {Model}";

        // => Para el frontend: etiqueta de transmisión
        [NotMapped]
        public string TransmissionLabel => Transmission ?? "No especificada";
    }
}
