using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Travel.Application.DTOs
{
    public class AmountDto
    {
        [JsonPropertyName("currency_code")]
        public string CurrencyCode { get; set; } = "USD";

        [JsonPropertyName("value")]
        public string Value { get; set; } = string.Empty;
    }
}
