using BookingSystem.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Service.Interface
{
    public interface IReservationService
    {
        List<Reservation> GetAll();
        Reservation? GetById(Guid id);
        Reservation Insert(Reservation reservation);
        Reservation Update(Reservation reservation);
        Reservation DeleteById(Guid id);
    }
}
