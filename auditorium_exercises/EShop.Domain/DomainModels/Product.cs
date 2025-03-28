using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Domain.DomainModels
{
    public class Product
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? ImageURL { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        public double? Price { get; set; }
        [Required]
        public double? Rating { get; set; }
        public virtual ICollection<ProductInShoppingCart>? ProductInShoppingCart { get; set; }
    }
}
