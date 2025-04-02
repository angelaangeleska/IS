using EShop.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Domain.Dto
{
    public class ShoppingCartDto
    {
        public List<ProductInShoppingCart> Products { get; set; }
        public double TotalPrice { get; set; }
    }
}
