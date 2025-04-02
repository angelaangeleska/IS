using EShop.Domain.DomainModels;
using EShop.Repository.Interface;
using EShop.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Service.Implementation
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<ProductInShoppingCart> _productInShoppingCartRepository;
        private readonly IShoppingCartService _shoppingCartService;

        public ProductService(IRepository<Product> productRepository, IRepository<ProductInShoppingCart> productInShoppingCartRepository, IShoppingCartService shoppingCartService)
        {
            _productRepository = productRepository;
            _productInShoppingCartRepository = productInShoppingCartRepository;
            _shoppingCartService = shoppingCartService;
        }

        public void AddProductToSoppingCart(Guid id, Guid userId)
        {
            var shoppingCart = _shoppingCartService.GetByUserId(userId);

            if (shoppingCart == null)
            {
                throw new Exception("Shopping cart not found");
            }

            var product = GetById(id);

            if (product == null)
            {
                throw new Exception("Product not found");
            }

            UpdateCartItem(product, shoppingCart);

        }


        private ProductInShoppingCart? GetProductInShoppingCart(Guid productId, Guid cartId)
        {
            return _productInShoppingCartRepository.Get(selector: x => x,
                predicate: x => x.ShoppingCartId.ToString() == cartId.ToString()
                                                && x.ProductId.ToString() == productId.ToString());
        }

        private void UpdateCartItem(Product product, ShoppingCart shoppingCart)
        {
            var existingProduct = GetProductInShoppingCart(product.Id, shoppingCart.Id);

            if (existingProduct == null)
            {
                var productInShoppingCart = new ProductInShoppingCart
                {
                    Id = Guid.NewGuid(),
                    ProductId = product.Id,
                    ShoppingCartId = shoppingCart.Id,
                    Product = product,
                    ShoppingCart = shoppingCart,
                    Quantity = 1
                };

                _productInShoppingCartRepository.Insert(productInShoppingCart);
            }
            else
            {
                existingProduct.Quantity++;
                _productInShoppingCartRepository.Update(existingProduct);
            }
        }

        public Product DeleteById(Guid id)
        {
            var product = GetById(id);
            if (product == null)
            {
                throw new Exception("Product not found");
            }
            _productRepository.Delete(product);
            return product;
        }

        public List<Product> GetAll()
        {
            return _productRepository.GetAll(selector: x => x).ToList();
        }

        public Product? GetById(Guid id)
        {
            return _productRepository.Get(selector: x => x,
                                          predicate: x => x.Id.Equals(id));
        }

        public Product Insert(Product product)
        {
            product.Id = Guid.NewGuid();
            return _productRepository.Insert(product);
        }

        public Product Update(Product product)
        {
            return _productRepository.Update(product);
        }
    }
}
