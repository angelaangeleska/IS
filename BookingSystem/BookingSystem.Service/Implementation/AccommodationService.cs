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
    public class AccommodationService : IAccommodationService
    {
        private readonly IRepository<Accommodation> _accommodationRepository;

        public AccommodationService(IRepository<Accommodation> accommodationRepository)
        {
            _accommodationRepository = accommodationRepository;
        }

        public Accommodation DeleteById(Guid id)
        {
            var accommodation = GetById(id);
            if (accommodation == null)
            {
                throw new Exception("Accommodation not found");
            }
            _accommodationRepository.Delete(accommodation);
            return accommodation;
        }

        public List<Accommodation> GetAll()
        {
            return _accommodationRepository.GetAll(selector: x => x).ToList();
        }

        public Accommodation? GetById(Guid id)
        {
            return _accommodationRepository.Get(selector: x => x,
                                         predicate: x => x.Id.Equals(id));
        }

        public Accommodation Insert(Accommodation accommodation)
        {
            accommodation.Id = Guid.NewGuid();
            return _accommodationRepository.Insert(accommodation);
        }

        public Accommodation Update(Accommodation accommodation)
        {
            return _accommodationRepository.Update(accommodation);
        }
    }
}
