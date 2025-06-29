using BookingSystem.Domain.DomainModels;
using BookingSystem.Repository.Interface;
using BookingSystem.Service.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Service.Implementation
{
    public class ReservationService : IReservationService
    {
        private readonly IRepository<Reservation> _reservationRepository;
        private readonly IAccommodationService _accommodationService;

        public ReservationService(IRepository<Reservation> reservationRepository, IAccommodationService accommodationService)
        {
            _reservationRepository = reservationRepository;
            _accommodationService = accommodationService;
        }

        public Reservation CancelReservation(Guid id)
        {
            var reservation = GetById(id);
            if (reservation == null)
            {
                throw new Exception("Reservation not found");
            }

            if (reservation.Accommodation == null)
            {
                throw new Exception("Accommodation not loaded");
            }

            reservation.Accommodation.IsAvailable = true;
            _accommodationService.Update(reservation.Accommodation);

            return reservation;
        }

        public Reservation DeleteById(Guid id)
        {
            var reservation = GetById(id);
            if (reservation == null)
            {
                throw new Exception("Reservation not found");
            }
            _reservationRepository.Delete(reservation);
            return reservation;
        }

        public List<Reservation> GetAll()
        {
            return _reservationRepository.GetAll(selector: x => x, 
                                                 include: x => x.Include(y => y.User)
                                                                .Include(y => y.Accommodation))
                                         .ToList();
        }

        public Reservation? GetById(Guid id)
        {
            return _reservationRepository.Get(selector: x => x,
                                          predicate: x => x.Id.Equals(id),
                                          include: x => x.Include(y => y.Accommodation));
        }

        public Reservation Insert(Reservation reservation)
        {
            reservation.Id = Guid.NewGuid();
            return _reservationRepository.Insert(reservation);
        }

        public Reservation Update(Reservation reservation)
        {
            var existingReservation = GetById(reservation.Id);
            if (existingReservation == null)
            {
                throw new Exception("Reservation not found");
            }

            var accommodation = existingReservation.Accommodation
                ?? _accommodationService.GetById(existingReservation.AccommodationId);

            if (accommodation == null)
            {
                throw new Exception("Accommodation not found for this reservation.");
            }

            if (reservation.NumberOfGuests > accommodation.Capacity)
            {
                throw new Exception("Number of guests exceeds accommodation capacity.");
            }

            existingReservation.CheckInDate = reservation.CheckInDate;
            existingReservation.CheckOutDate = reservation.CheckOutDate;
            existingReservation.NumberOfGuests = reservation.NumberOfGuests;

            return _reservationRepository.Update(existingReservation);
        }
    }
}
