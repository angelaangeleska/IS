using BookingSystem.Domain.IdentityModels;
using BookingSystem.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Repository.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<SystemUser> entites;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
            this.entites = _context.Set<SystemUser>();
        }

        public SystemUser GetUserById(string id)
        {
            return entites.First(ent => ent.Id == id);
        }
    }
}
