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
    [Table("payment_status")]
    public class PaymentStatus: HasId
    {
        

        [Required]
        [MaxLength(45)]
        [Column("status_description")]
        public string StatusDescription { get; set; } = string.Empty;

        // Navigation
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();

        // => Para el frontend: color del badge de pago
        [NotMapped]
        public string BadgeColor => StatusDescription?.ToLower() switch
        {
            "pagado" => "green",
            "pendiente" => "yellow",
            "fallido" => "red",
            "reembolsado" => "blue",
            _ => "gray"
        };
    }
}
