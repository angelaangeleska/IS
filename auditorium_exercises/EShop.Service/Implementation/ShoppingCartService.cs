using EShop.Domain.DomainModels;
using EShop.Domain.Dto;
using EShop.Repository.Interface;
using EShop.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EShop.Service.Implementation
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IRepository<ShoppingCart> _shoppingCartRepository;

        public ShoppingCartService(IRepository<ShoppingCart> shoppingCartRepository)
        {
            _shoppingCartRepository = shoppingCartRepository;
        }

        public ShoppingCart? GetByUserId(Guid userId)
        {
            return _shoppingCartRepository.Get(selector: x => x,
                                                       predicate: x => x.OwnerId.Equals(userId.ToString()));
        }

        public ShoppingCartDto GetByUserIdWithIncludedPrducts(Guid userId)
        {
            var userCart = _shoppingCartRepository.Get(selector: x => x,
                                               predicate: x => x.OwnerId.Equals(userId.ToString()),
                                               include: x => x.Include(z => z.Products).ThenInclude(m => m.Product));

            var allProducts = userCart.Products.ToList();

            var allProductPrices = allProducts.Select(z => new
            {
                ProductPrice = z.Product.Price,
                Quantity = z.Quantity
            }).ToList();

            double totalPrice = 0.0;

            foreach (var item in allProductPrices)
            {
                totalPrice += (double)(item.Quantity * item.ProductPrice);
            }

            ShoppingCartDto model = new ShoppingCartDto
            {
                Products = allProducts,
                TotalPrice = totalPrice
            };

            return model;
        }
    }
}
