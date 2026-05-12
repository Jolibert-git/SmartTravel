using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Travel.Application.DTOs;

namespace Travel.Application.Request
{
    public class PayPalOrderRequest
    {
        [JsonPropertyName("intent")]
        public string Intent { get; set; } = "CAPTURE";

        [JsonPropertyName("purchase_units")]
        public List<PurchaseUnitDto> PurchaseUnits { get; set; } = [];

        [JsonPropertyName("application_context")]
        public ApplicationContextDto ApplicationContext { get; set; } = new();
    }
}
