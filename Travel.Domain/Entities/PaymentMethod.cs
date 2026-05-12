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
    [Table("payment_method")]
    public class PaymentMethod: HasId
    {
        

        [Required]
        [MaxLength(30)]
        [Column("method_name")]
        public string MethodName { get; set; } = string.Empty;

        [MaxLength(100)]
        [Column("pay_description")]
        public string? PayDescription { get; set; }

        [Column("is_active")]
        public bool IsActive { get; set; } = true;

        // Navigation
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();

        // => Para el frontend: ícono sugerido según método
        [NotMapped]
        public string IconName => MethodName?.ToLower() switch
        {
            var m when m.Contains("tarjeta") || m.Contains("card") => "credit_card",
            var m when m.Contains("transfer") => "swap_horiz",
            var m when m.Contains("efectivo") || m.Contains("cash") => "payments",
            _ => "account_balance_wallet"
        };
    }
}
