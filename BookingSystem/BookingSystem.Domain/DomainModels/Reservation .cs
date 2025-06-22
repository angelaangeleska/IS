using BookingSystem.Domain.IdentityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Domain.DomainModels
{
    public class Reservation : BaseEntity
    {
        public string? UserId { get; set; }
        public SystemUser? User { get; set; }

        public Guid AccommodationId { get; set; }
        public Accommodation? Accommodation { get; set; }

        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int NumberOfGuests { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public double TotalPrice { get; set; }
    }
}
