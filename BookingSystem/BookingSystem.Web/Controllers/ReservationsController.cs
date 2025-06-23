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
    public class ReservationsController : Controller
    {
        private readonly IReservationService _reservationService;

        public ReservationsController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        // GET: Reservations
        public IActionResult Index()
        {
            return View(_reservationService.GetAll());
        }

        // GET: Reservations/Details/5
        public IActionResult Details(Guid id)
        {
            var reservation = _reservationService.GetById(id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // GET: Reservations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Reservations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("UserId,AccommodationId,CheckInDate,CheckOutDate,NumberOfGuests,CreatedOn,TotalPrice")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                _reservationService.Insert(reservation);
                return RedirectToAction(nameof(Index));
            }
            return View(reservation);
        }

        // GET: Reservations/Edit/5
        public IActionResult Edit(Guid id)
        {
            var reservation = _reservationService.GetById(id);
            if (reservation == null)
            {
                return NotFound();
            }
            return View(reservation);
        }

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("UserId,AccommodationId,CheckInDate,CheckOutDate,NumberOfGuests,CreatedOn,TotalPrice,Id")] Reservation reservation)
        {
            if (id != reservation.Id)
            {
                return NotFound();
            }

            _reservationService.Update(reservation);
            return RedirectToAction(nameof(Index));
        }

        // GET: Reservations/Delete/5
        public IActionResult Delete(Guid id)
        {
            var reservation = _reservationService.GetById(id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _reservationService.DeleteById(id);
            return RedirectToAction(nameof(Index));
        }

        //private bool ReservationExists(Guid id)
        //{
        //    return _context.Reservations.Any(e => e.Id == id);
        //}
    }
}
