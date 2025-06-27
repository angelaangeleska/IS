using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Domain.DomainModels
{
    public enum SizeCategory
    {
        Small,
        Medium,
        Large
    }

    public class City : BaseEntity
    {
        private int _population;
        public string? Name { get; set; }
        public Guid CountryId { get; set; }
        public Country? Country { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int Population
        {
            get => _population;
            set
            {
                _population = value;
                UpdateSizeCategory(); 
            }
        }
        public SizeCategory SizeCategory { get; set; }
        public virtual ICollection<Accommodation>? Accommodations { get; set; }
        private void UpdateSizeCategory()
        {
            if (_population < 100000)
                SizeCategory = SizeCategory.Small;
            else if (_population < 1000000)
                SizeCategory = SizeCategory.Medium;
            else
                SizeCategory = SizeCategory.Large;
        }   
    }


}
