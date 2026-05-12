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
    [Table("reservation_status")]
    public class ReservationStatus: HasId
    {

        [Required]
        [MaxLength(45)]
        [Column("status_description")]
        public string StatusDescription { get; set; } = string.Empty;

        // Navigation
        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

        // => Para el frontend: color del badge por estado
        [NotMapped]
        public string BadgeColor => StatusDescription?.ToLower() switch
        {
            "confirmada" => "green",
            "pendiente" => "yellow",
            "cancelada" => "red",
            _ => "gray"
        };
    }
}
