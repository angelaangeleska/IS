using BookingSystem.Domain.DomainModels;
using BookingSystem.Repository;
using BookingSystem.Service.Implementation;
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
    public class CitiesController : Controller
    {
        private readonly ICityService _cityService;
        private readonly IDataFetchService _dataFetchService;

        public CitiesController(ICityService cityService, IDataFetchService dataFetchService)
        {
            _cityService = cityService;
            _dataFetchService = dataFetchService;
        }

        // GET: Cities
        public IActionResult Index()
        {
            return View(_cityService.GetAll());
        }

        // GET: Cities/Details/5
        public IActionResult Details(Guid id)
        {
            var city = _cityService.GetById(id);
            if (city == null)
            {
                return NotFound();
            }

            return View(city);
        }

        // GET: Cities/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Name,CountryId,Latitude,Longitude,Population,SizeCategory")] City city)
        {
            if (ModelState.IsValid)
            {
                _cityService.Insert(city);
                return RedirectToAction(nameof(Index));
            }
            return View(city);
        }

        // GET: Cities/Edit/5
        public IActionResult Edit(Guid id)
        {
            var city = _cityService.GetById(id);
            if (city == null)
            {
                return NotFound();
            }
            return View(city);
        }

        // POST: Cities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Name,CountryId,Latitude,Longitude,Population,SizeCategory,Id")] City city)
        {
            if (id != city.Id)
            {
                return NotFound();
            }

            _cityService.Update(city);
            return RedirectToAction(nameof(Index));
        }

        // GET: Cities/Delete/5
        public IActionResult Delete(Guid id)
        {
            var city = _cityService.GetById(id);
            if (city == null)
            {
                return NotFound();
            }

            return View(city);
        }

        // POST: Cities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _cityService.DeleteById(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> FetchCities()
        {
            await _dataFetchService.FetchCitiesFromApi();
            return RedirectToAction(nameof(Index));
        }

        //private bool CityExists(Guid id)
        //{
        //    return _context.Cities.Any(e => e.Id == id);
        //}
    }
}
