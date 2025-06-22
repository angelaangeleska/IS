using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Domain.DomainModels
{
    public enum SizeCategory
    {
        Unknown,
        Small,
        Medium,
        Large
    }

    public class City : BaseEntity
    {
        public string? Name { get; set; }
        public Guid CountryId { get; set; }
        public Country? Country { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int Population { get; set; }
        public SizeCategory SizeCategory { get; set; }
        public virtual ICollection<Accommodation>? Accommodations { get; set; }
    }
}
