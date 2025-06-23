using BookingSystem.Domain.IdentityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Repository.Interface
{
    public interface IUserRepository
    {
        SystemUser GetUserById(string id);
    }
}
