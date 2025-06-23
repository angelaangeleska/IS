using BookingSystem.Domain.DomainModels;
using BookingSystem.Repository.Implementation;
using BookingSystem.Repository.Interface;
using BookingSystem.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Service.Implementation
{
    public class CountryService : ICountryService
    {
        private readonly IRepository<Country> _countryRepository;

        public CountryService(IRepository<Country> countryRepository)
        {
            _countryRepository = countryRepository;
        }

        public Country DeleteById(Guid id)
        {
            var country = GetById(id);
            if (country == null)
            {
                throw new Exception("Country not found");
            }
            _countryRepository.Delete(country);
            return country;
        }

        public List<Country> GetAll()
        {
            return _countryRepository.GetAll(selector: x => x).ToList();
        }

        public Country? GetById(Guid id)
        {
            return _countryRepository.Get(selector: x => x,
                                          predicate: x => x.Id.Equals(id));
        }

        public Country Insert(Country country)
        {
            country.Id = Guid.NewGuid();
            return _countryRepository.Insert(country);
        }

        public void InsertMany(IEnumerable<Country> countries)
        {
            foreach (var country in countries)
            {
                _countryRepository.Insert(country);
            }
        }

        public Country Update(Country country)
        {
            return _countryRepository.Update(country);
        }
    }
}
