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
    using System.Security.Claims;
    using System.Security.Cryptography;
    using System.Threading.Tasks;
    using System.Security.Claims;

    namespace BookingSystem.Web.Controllers
    {
        public class AccommodationsController : Controller
        {
            private readonly IAccommodationService _accommodationService;
            private readonly ICityService _cityService;

            public AccommodationsController(IAccommodationService accommodationService, ICityService cityService)
            {
                _accommodationService = accommodationService;
                _cityService = cityService;
            }

            // GET: Accommodations
            public IActionResult Index(int? pageIndex, int pageSize = 6, Guid? cityId = null)
            {
                var cities = _cityService.GetAll();
                ViewBag.CityId = new SelectList(cities, "Id", "Name", cityId);

                var paginatedList = _accommodationService.GetPaginated(pageIndex ?? 1, pageSize, cityId);
                return View(paginatedList);
            }



            // GET: Accommodations/Details/5
            public IActionResult Details(Guid id)
            {
                var accommodation = _accommodationService.GetById(id);
                if (accommodation == null)
                {
                    return NotFound();
                }

                return View(accommodation);
            }

            // GET: Accommodations/Create
            public IActionResult Create()
            {
                ViewBag.CityId = new SelectList(_cityService.GetAll(), "Id", "Name");
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
                    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    accommodation.CreatedFromUserId = userId;
                    _accommodationService.Insert(accommodation);
                    return RedirectToAction(nameof(Index));
                }
                var cities = _cityService.GetAll();
                ViewBag.CityId = new SelectList(cities, "Id", "Name", accommodation.CityId);
                return View(accommodation);
            }

            // GET: Accommodations/Edit/5
            public IActionResult Edit(Guid id)
            {
                var accommodation = _accommodationService.GetById(id);
                if (accommodation == null)
                {
                    return NotFound();
                }
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (accommodation.CreatedFromUserId != userId)
                {
                    return Forbid(); 
                }
                var cities = _cityService.GetAll();
                ViewBag.CityId = new SelectList(cities, "Id", "Name", accommodation.CityId);
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

                var existingAccommodation = _accommodationService.GetById(id);
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (existingAccommodation?.CreatedFromUserId != userId)
                {
                    return Forbid();
                }

                _accommodationService.Update(accommodation);
                return RedirectToAction(nameof(Index));
            }

            // GET: Accommodations/Delete/5
            public IActionResult Delete(Guid id)
            {
                var accommodation = _accommodationService.GetById(id);
                if (accommodation == null)
                {
                    return NotFound();
                }

                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (accommodation.CreatedFromUserId != userId)
                {
                    return Forbid();
                }

                return View(accommodation);
            }

            // POST: Accommodations/Delete/5
            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public IActionResult DeleteConfirmed(Guid id)
            {
                var accommodation = _accommodationService.GetById(id);
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (accommodation != null && accommodation.CreatedFromUserId != userId)
                {
                    return Forbid();
                }

                _accommodationService.DeleteById(id);

                return RedirectToAction(nameof(Index));
            }

            public IActionResult MakeReservation(Guid id)
            {
                return RedirectToAction("Create", "Reservations", new { accommodationId = id });
            }

            //private bool AccommodationExists(Guid id)
            //{
            //    return _context.Accommodations.Any(e => e.Id == id);
            //}
        }
    }
