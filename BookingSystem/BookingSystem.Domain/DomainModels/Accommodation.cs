using BookingSystem.Domain.IdentityModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Display(Name = "City")]
        public Guid CityId { get; set; }
        [Display(Name = "City")]
        public City? City { get; set; }
        [Display(Name = "Price per night")]
        public double PricePerNight { get; set; }
        public int Capacity { get; set; }
        [Display(Name = "Image URL")]
        public string? ImageUrl { get; set; }
        public double Rating { get; set; }
        [Display(Name = "Is it available?")]
        public bool IsAvailable { get; set; }
        public string? CreatedFromUserId { get; set; }
        public SystemUser? CreatedFromUser { get; set; }
        public virtual ICollection<Reservation>? Reservations { get; set; }
    }
}
