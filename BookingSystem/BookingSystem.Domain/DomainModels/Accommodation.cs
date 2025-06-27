using BookingSystem.Domain.IdentityModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Domain.DomainModels
{
    public class Accommodation : BaseEntity
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Address { get; set; }
        public Guid CityId { get; set; }
        public City? City { get; set; }
        public double PricePerNight { get; set; }
        public int Capacity { get; set; }
        public string? ImageUrl { get; set; }
        public double Rating { get; set; }
        public bool IsAvailable { get; set; }
        public string? CreatedFromUserId { get; set; }
        public SystemUser? CreatedFromUser { get; set; }
        public virtual ICollection<Reservation>? Reservations { get; set; }
    }
}
