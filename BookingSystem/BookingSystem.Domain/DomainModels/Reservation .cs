using BookingSystem.Domain.DomainModels.Validation;
using BookingSystem.Domain.IdentityModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [Required]
        [DataType(DataType.Date)]
        [FutureDate(ErrorMessage = "Check-in must be in the future")]
        public DateTime CheckInDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DateAfter("CheckInDate", ErrorMessage = "Check-out must be after check-in")]
        public DateTime CheckOutDate { get; set; }
        public int NumberOfGuests { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public double TotalPrice { get; set; }
    }
}
