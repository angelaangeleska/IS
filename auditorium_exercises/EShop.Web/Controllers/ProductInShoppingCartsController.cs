using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EShop.Domain.DomainModels;
using EShop.Web.Data;

namespace EShop.Web.Controllers
{
    public class ProductInShoppingCartsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductInShoppingCartsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ProductInShoppingCarts
        public async Task<IActionResult> Index()
        {
            return View(await _context.ProductsInShoppingCarts.ToListAsync());
        }

        // GET: ProductInShoppingCarts/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productInShoppingCart = await _context.ProductsInShoppingCarts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productInShoppingCart == null)
            {
                return NotFound();
            }

            return View(productInShoppingCart);
        }

        // GET: ProductInShoppingCarts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProductInShoppingCarts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProductId,ShoppingCartId,Quantity")] ProductInShoppingCart productInShoppingCart)
        {
            if (ModelState.IsValid)
            {
                productInShoppingCart.Id = Guid.NewGuid();
                _context.Add(productInShoppingCart);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productInShoppingCart);
        }

        // GET: ProductInShoppingCarts/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productInShoppingCart = await _context.ProductsInShoppingCarts.FindAsync(id);
            if (productInShoppingCart == null)
            {
                return NotFound();
            }
            return View(productInShoppingCart);
        }

        // POST: ProductInShoppingCarts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,ProductId,ShoppingCartId,Quantity")] ProductInShoppingCart productInShoppingCart)
        {
            if (id != productInShoppingCart.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productInShoppingCart);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductInShoppingCartExists(productInShoppingCart.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(productInShoppingCart);
        }

        // GET: ProductInShoppingCarts/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productInShoppingCart = await _context.ProductsInShoppingCarts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productInShoppingCart == null)
            {
                return NotFound();
            }

            return View(productInShoppingCart);
        }

        // POST: ProductInShoppingCarts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var productInShoppingCart = await _context.ProductsInShoppingCarts.FindAsync(id);
            if (productInShoppingCart != null)
            {
                _context.ProductsInShoppingCarts.Remove(productInShoppingCart);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductInShoppingCartExists(Guid id)
        {
            return _context.ProductsInShoppingCarts.Any(e => e.Id == id);
        }
    }
}
