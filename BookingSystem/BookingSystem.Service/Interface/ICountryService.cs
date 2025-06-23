using BookingSystem.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Service.Interface
{
    public interface ICountryService
    {
        List<Country> GetAll();
        Country? GetById(Guid id);
        Country Insert(Country country);
        Country Update(Country country);
        Country DeleteById(Guid id);
    }
}
