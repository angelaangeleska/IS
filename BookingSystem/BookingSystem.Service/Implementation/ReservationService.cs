using BookingSystem.Domain.DomainModels;
using BookingSystem.Repository.Interface;
using BookingSystem.Service.Interface;
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

        public ReservationService(IRepository<Reservation> reservationRepository)
        {
            _reservationRepository = reservationRepository;
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
            return _reservationRepository.GetAll(selector: x => x).ToList();
        }

        public Reservation? GetById(Guid id)
        {
            return _reservationRepository.Get(selector: x => x,
                                          predicate: x => x.Id.Equals(id));
        }

        public Reservation Insert(Reservation reservation)
        {
            reservation.Id = Guid.NewGuid();
            return _reservationRepository.Insert(reservation);
        }

        public Reservation Update(Reservation reservation)
        {
            return _reservationRepository.Update(reservation);
        }
    }
}
