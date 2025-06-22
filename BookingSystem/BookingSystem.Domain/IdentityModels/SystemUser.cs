using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingSystem.Domain.DomainModels;
using Microsoft.AspNetCore.Identity;

namespace BookingSystem.Domain.IdentityModels
{
    public class SystemUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public virtual ICollection<Reservation>? Reservations { get; set; }
    }
}
