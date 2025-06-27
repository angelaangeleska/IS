using BookingSystem.Domain.DomainModels;
using BookingSystem.Domain.DomainModels.Pagination;
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
            return _accommodationRepository.GetAll(selector: x => x, 
                                                   include: x => x.Include(y => y.City)).ToList();
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

        public PaginatedList<Accommodation> GetPaginated(int pageIndex, int pageSize, Guid? cityId = null)
        {
            var accommodations = _accommodationRepository.GetAll(
                selector: x => x,
                include: x => x.Include(y => y.City)
                               .Include(a => a.CreatedFromUser)
            );

            if (cityId.HasValue)
            {
                accommodations = accommodations.Where(a => a.CityId == cityId.Value);
            }

            var count = accommodations.Count();
            var items = accommodations
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return new PaginatedList<Accommodation>(items, count, pageIndex, pageSize);
        }

    }
}
