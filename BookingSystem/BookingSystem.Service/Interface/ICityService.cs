using BookingSystem.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Service.Interface
{
    public interface ICityService
    {
        List<City> GetAll();
        City? GetById(Guid id);
        City Insert(City city);
        void InsertMany(IEnumerable<City> cities);
        City Update(City city);
        City DeleteById(Guid id);
    }
}
