using BookingSystem.Domain.DomainModels;
using BookingSystem.Repository;
using BookingSystem.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace BookingSystem.Web.Controllers
{
    public class CountriesController : Controller
    {
        private readonly ICountryService _countryService;

        public CountriesController(ICountryService countryService)
        {
            _countryService = countryService;
        }

        // GET: Countries
        public IActionResult Index()
        {
            return View(_countryService.GetAll());
        }

        // GET: Countries/Details/5
        public IActionResult Details(Guid id)
        {
            var country = _countryService.GetById(id);
            if (country == null)
            {
                return NotFound();
            }

            return View(country);
        }

        // GET: Countries/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Countries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Name,OfficialName,Capital,FlagUrl,CurrencyName,CurrencySymbol,Language")] Country country)
        {
            if (ModelState.IsValid)
            {
                _countryService.Insert(country);
                return RedirectToAction(nameof(Index));
            }
            return View(country);
        }

        // GET: Countries/Edit/5
        public IActionResult Edit(Guid id)
        {
            var country = _countryService.GetById(id);
            if (country == null)
            {
                return NotFound();
            }
            return View(country);
        }

        // POST: Countries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Name,OfficialName,Capital,FlagUrl,CurrencyName,CurrencySymbol,Language,Id")] Country country)
        {
            if (id != country.Id)
            {
                return NotFound();
            }

            _countryService.Update(country);
            return RedirectToAction(nameof(Index));
        }

        // GET: Countries/Delete/5
        public IActionResult Delete(Guid id)
        {
            var country = _countryService.GetById(id);
            if (country == null)
            {
                return NotFound();
            }

            return View(country);
        }

        // POST: Countries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _countryService.DeleteById(id);
            return RedirectToAction(nameof(Index));
        }

        //private bool CountryExists(Guid id)
        //{
        //    return _context.Countries.Any(e => e.Id == id);
        //}
    }
}
