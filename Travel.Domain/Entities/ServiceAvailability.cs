using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Domain.Entities
{
    [Table("service_availability")]
    public class ServiceAvailability
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Column("id_service")]
        public long IdService { get; set; }

        [Column("id_availability_status")]
        public long IdAvailabilityStatus { get; set; }

        [Required]
        [Column("date_from")]
        public DateTime DateFrom { get; set; }

        [Required]
        [Column("date_to")]
        public DateTime DateTo { get; set; }

        [MaxLength(100)]
        [Column("reason")]
        public string? Reason { get; set; }

        // Navigation
        public OfferedService OfferedService { get; set; } = null!;
        public AvailabilityStatus AvailabilityStatus { get; set; } = null!;

        // => Para el frontend: rango de fechas legible
        [NotMapped]
        public string DateRangeDisplay => $"{DateFrom:dd/MM/yyyy} – {DateTo:dd/MM/yyyy}";

        // => Para el frontend: si el servicio está actualmente no disponible
        [NotMapped]
        public bool IsCurrentlyUnavailable =>
            DateTime.Now >= DateFrom && DateTime.Now <= DateTo;
    }
}
