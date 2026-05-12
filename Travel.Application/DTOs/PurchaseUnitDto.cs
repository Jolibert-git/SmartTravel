using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Travel.Application.DTOs
{
    public class PurchaseUnitDto
    {
        [JsonPropertyName("amount")]
        public AmountDto Amount { get; set; } = new();

        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;
    }
}
