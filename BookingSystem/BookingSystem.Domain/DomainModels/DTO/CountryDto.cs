using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BookingSystem.Domain.DomainModels.DTO
{
    public class CountryDto
    {
        [JsonPropertyName("code")]
        public string? Code { get; set; }

        [JsonPropertyName("currencyCodes")]
        public List<string>? CurrencyCodes { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("wikiDataId")]
        public string? WikiDataId { get; set; }
    }
}
