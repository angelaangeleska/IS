using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EShop.Domain.DomainModels;
using Microsoft.AspNetCore.Identity;

namespace EShop.Domain.Identity
{
    public class EShopUser : IdentityUser
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Address { get; set; }
        public ShoppingCart? UserShoppingCart { get; set; }
    }
}
