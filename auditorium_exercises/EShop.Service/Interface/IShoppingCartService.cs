using EShop.Domain.DomainModels;
using EShop.Domain.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Service.Interface
{
    public interface IShoppingCartService
    {
        ShoppingCart? GetByUserId(Guid userId);
        ShoppingCartDto GetByUserIdWithIncludedPrducts(Guid userId);
    }
}
