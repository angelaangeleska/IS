using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BookingSystem.Domain.DomainModels.DTO
{
    public class GeoApiResponse
    {
        [JsonPropertyName("data")]
        public List<CountryDto>? Data { get; set; }
        public MetadataDto Metadata { get; set; }
    }
}
