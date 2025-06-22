using BookingSystem.Domain.DomainModels;
using BookingSystem.Domain.IdentityModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Repository
{
    public class ApplicationDbContext : IdentityDbContext<SystemUser>
    {
        public virtual DbSet<Accommodation> Accommodations { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Reservation> Reservations { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
