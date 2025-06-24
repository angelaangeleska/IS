using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BookingSystem.Domain.DomainModels.DTO
{
    public class GeoApiResponseCities
    {
        [JsonPropertyName("data")]
        public List<CityDto> Data { get; set; }

        [JsonPropertyName("links")]
        public List<Link> Links { get; set; }

        [JsonPropertyName("metadata")]
        public MetadataDto Metadata { get; set; }
    }
}
