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
    public class AccommodationsController : Controller
    {
        private readonly IAccommodationService _acccommodationService;

        public AccommodationsController(IAccommodationService acccommodationService)
        {
            _acccommodationService = acccommodationService;
        }

        // GET: Accommodations
        public IActionResult Index()
        {
            return View(_acccommodationService.GetAll());
        }

        // GET: Accommodations/Details/5
        public IActionResult Details(Guid id)
        {
            var accommodation = _acccommodationService.GetById(id);
            if (accommodation == null)
            {
                return NotFound();
            }

            return View(accommodation);
        }

        // GET: Accommodations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Accommodations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Name,Description,Address,CityId,PricePerNight,Capacity,ImageUrl,Rating,IsAvailable")] Accommodation accommodation)
        {
            if (ModelState.IsValid)
            {
                _acccommodationService.Insert(accommodation);
                return RedirectToAction(nameof(Index));
            }
            return View(accommodation);
        }

        // GET: Accommodations/Edit/5
        public IActionResult Edit(Guid id)
        {
            var accommodation = _acccommodationService.GetById(id);
            if (accommodation == null)
            {
                return NotFound();
            }
            return View(accommodation);
        }

        // POST: Accommodations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Name,Description,Address,CityId,PricePerNight,Capacity,ImageUrl,Rating,IsAvailable,Id")] Accommodation accommodation)
        {
            if (id != accommodation.Id)
            {
                return NotFound();
            }

            _acccommodationService.Update(accommodation);
            return RedirectToAction(nameof(Index));
        }

        // GET: Accommodations/Delete/5
        public IActionResult Delete(Guid id)
        {
            var accommodation = _acccommodationService.GetById(id);
            if (accommodation == null)
            {
                return NotFound();
            }

            return View(accommodation);
        }

        // POST: Accommodations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _acccommodationService.DeleteById(id);

            return RedirectToAction(nameof(Index));
        }

        //private bool AccommodationExists(Guid id)
        //{
        //    return _context.Accommodations.Any(e => e.Id == id);
        //}
    }
}
