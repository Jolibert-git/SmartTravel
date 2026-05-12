using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Core;

namespace Travel.Domain.Entities
{
    public class PayPalPayment: HasId
    {
        //public long Id { get; set; }

        [Column(" id_payment")]
        public long IdPayment { get; set; }
        [Column("paypal_order_id")]
        public string PayPalOrderId { get; set; } = string.Empty;
        [Column("paypal_capture_id")]
        public string? PayPalCaptureId { get; set; }
        [Column("paypal_payer_id")]
        public string? PayPalPayerId { get; set; }
        [Column("payer_email")]
        public string? PayerEmail { get; set; }
        [Column("currency_code")]
        public string CurrencyCode { get; set; } = string.Empty;
        [Column("paypal_status")]
        public string PayPalStatus { get; set; } = string.Empty;
        [Column("provider_response")]
        public string? ProviderResponse { get; set; }
        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        // Navigation
        [ForeignKey("IdPayment")]
        public Payment Payment { get; set; } = null!;
    }
}
