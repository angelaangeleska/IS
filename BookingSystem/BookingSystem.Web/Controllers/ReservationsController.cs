using BookingSystem.Domain.DomainModels;
using BookingSystem.Domain.DomainModels.Validation;
using BookingSystem.Repository;
using BookingSystem.Service.Implementation;
using BookingSystem.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace BookingSystem.Web.Controllers
{
    public class ReservationsController : Controller
    {
        private readonly IReservationService _reservationService;
        private readonly IAccommodationService _accommodationService;

        public ReservationsController(IReservationService reservationService, IAccommodationService accommodationService)
        {
            _reservationService = reservationService;
            _accommodationService = accommodationService;
        }

        // GET: Reservations
        [Authorize]
        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var reservations = _reservationService.GetAll()
                .Where(r => r.UserId == userId)
                .ToList();

            return View(reservations);
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
        [Authorize]
        public IActionResult Create(Guid accommodationId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var accommodation = _accommodationService.GetById(accommodationId);

            if (accommodation == null)
            {
                return NotFound();
            }

            return View(new Reservation
            {
                AccommodationId = accommodationId,
                UserId = userId,
                Accommodation = accommodation,
                CheckInDate = DateTime.Today.AddDays(1), // Tomorrow
                CheckOutDate = DateTime.Today.AddDays(2)  // Day after tomorrow
            });
        }

        // POST: Reservations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Create([Bind("CheckInDate,CheckOutDate,NumberOfGuests")] Reservation reservation, Guid accommodationId)
        {
            // Strip time components
            reservation.CheckInDate = reservation.CheckInDate.Date;
            reservation.CheckOutDate = reservation.CheckOutDate.Date;

            reservation.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            reservation.CreatedOn = DateTime.UtcNow;
            reservation.AccommodationId = accommodationId;

            var accommodation = _accommodationService.GetById(accommodationId);
            if (accommodation == null) return NotFound();

            if (reservation.NumberOfGuests > accommodation.Capacity)
            {
                ModelState.AddModelError("NumberOfGuests", "Exceeds accommodation capacity");
            }

            var validationContext = new ValidationContext(reservation, null, null);
            var validationResults = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(reservation, validationContext, validationResults, true);

            foreach (var result in validationResults)
            {
                foreach (var memberName in result.MemberNames)
                {
                    ModelState.AddModelError(memberName, result.ErrorMessage);
                }
            }

            if (ModelState.IsValid)
            {
                var nights = (reservation.CheckOutDate - reservation.CheckInDate).Days;
                reservation.TotalPrice = accommodation.PricePerNight * nights;

                _reservationService.Insert(reservation);

                accommodation.IsAvailable = false;
                _accommodationService.Update(accommodation);

                return RedirectToAction(nameof(Index));
            }

            reservation.Accommodation = accommodation;
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
        public IActionResult Edit(Guid id, DateTime CheckInDate, DateTime CheckOutDate, int NumberOfGuests)
        {
            try
            {
                // Load the existing reservation with all its data
                var existingReservation = _reservationService.GetById(id);
                if (existingReservation == null)
                {
                    return NotFound();
                }

                // Strip time components
                CheckInDate = CheckInDate.Date;
                CheckOutDate = CheckOutDate.Date;

                // Validate dates
                if (CheckInDate >= CheckOutDate)
                {
                    ModelState.AddModelError("CheckOutDate", "Check-out date must be after check-in date.");
                }

                if (CheckInDate < DateTime.Today)
                {
                    ModelState.AddModelError("CheckInDate", "Check-in date cannot be in the past.");
                }

                // Validate guest capacity
                if (NumberOfGuests > existingReservation.Accommodation.Capacity)
                {
                    ModelState.AddModelError("NumberOfGuests", $"Number of guests cannot exceed accommodation capacity of {existingReservation.Accommodation.Capacity}.");
                }

                if (NumberOfGuests <= 0)
                {
                    ModelState.AddModelError("NumberOfGuests", "Number of guests must be at least 1.");
                }

                if (ModelState.IsValid)
                {
                    // Update only the editable fields
                    existingReservation.CheckInDate = CheckInDate;
                    existingReservation.CheckOutDate = CheckOutDate;
                    existingReservation.NumberOfGuests = NumberOfGuests;

                    // Recalculate total price
                    var nights = (CheckOutDate - CheckInDate).Days;
                    existingReservation.TotalPrice = existingReservation.Accommodation.PricePerNight * nights;

                    // Update the reservation
                    _reservationService.Update(existingReservation);

                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Unable to save changes. Please try again.");
            }

            // If we get here, something went wrong, reload the reservation
            var reservation = _reservationService.GetById(id);
            return View(reservation);
        }


        public IActionResult Cancel(Guid id)
        {
            var reservation = _reservationService.GetById(id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Cancel")]
        [ValidateAntiForgeryToken]
        public IActionResult CancelConfirmed(Guid id)
        {
            _reservationService.CancelReservation(id);
            _reservationService.DeleteById(id);
            return RedirectToAction(nameof(Index));
        }

        //private bool ReservationExists(Guid id)
        //{
        //    return _context.Reservations.Any(e => e.Id == id);
        //}
    }
}
