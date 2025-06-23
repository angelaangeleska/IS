using BookingSystem.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Service.Interface
{
    public interface IDataFetchService
    {
        Task<List<City>> FetchCitiesFromApi();
        Task<List<Country>> FetchCountriesFromApi();
    }
}
