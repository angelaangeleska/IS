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
    public class CityService : ICityService
    {
        private readonly IRepository<City> _cityRepository;

        public CityService(IRepository<City> cityRepository)
        {
            _cityRepository = cityRepository;
        }

        public City DeleteById(Guid id)
        {
            var city = GetById(id);
            if (city == null)
            {
                throw new Exception("City not found");
            }
            _cityRepository.Delete(city);
            return city;
        }

        public List<City> GetAll()
        {
            return _cityRepository.GetAll(selector: x => x, include: query => query.Include(y => y.Country)).ToList();
        }

        public City? GetById(Guid id)
        {
            return _cityRepository.Get(selector: x => x,
                                          predicate: x => x.Id.Equals(id));
        }

        public City Insert(City city)
        {
            city.Id = Guid.NewGuid();
            return _cityRepository.Insert(city);
        }

        public void InsertMany(IEnumerable<City> cities)
        {
            foreach (var city in cities)
            {
                _cityRepository.Insert(city);
            }
        }

        public City Update(City city)
        {
            return _cityRepository.Update(city);
        }
    }
}
