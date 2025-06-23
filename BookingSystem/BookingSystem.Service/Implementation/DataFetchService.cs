using BookingSystem.Domain.DomainModels;
using BookingSystem.Domain.DomainModels.DTO;
using BookingSystem.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BookingSystem.Service.Implementation
{
    public class DataFetchService : IDataFetchService
    {
        private readonly HttpClient _httpClient;
        private readonly ICityService _cityService;
        private readonly ICountryService _countryService;

        public DataFetchService(IHttpClientFactory httpClientFactory, ICityService cityService, ICountryService countryService)
        {
            _httpClient = httpClientFactory.CreateClient();
            _cityService = cityService;
            _countryService = countryService;
        }

        public async Task<List<City>> FetchCitiesFromApi()
        {
            //    var client = new HttpClient();
            //    var request = new HttpRequestMessage
            //    {
            //        Method = HttpMethod.Get,
            //        RequestUri = new Uri("https://wft-geo-db.p.rapidapi.com/v1/geo/cities"),
            //        Headers =
            //        {
            //            { "x-rapidapi-key", "ae9cc4cc9emsh74fa90f1760528fp102f55jsn95a37886a0c3" },
            //            { "x-rapidapi-host", "wft-geo-db.p.rapidapi.com" },
            //        },
            //    };
            return new List<City>();
        }

        public async Task<List<Country>> FetchCountriesFromApi()
        {
            var existingCodes = _countryService.GetAll()
                                               .Select(c => c.Code)
                                               .ToHashSet(StringComparer.OrdinalIgnoreCase);

            var allCountries = new List<Country>();
            const int limit = 10;
            int maxOffset = 198;  // Set maximum offset to 198

            for (int offset = 0; offset <= maxOffset; offset += limit)
            {
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri($"https://wft-geo-db.p.rapidapi.com/v1/geo/countries?limit={limit}&offset={offset}"),
                    Headers =
                    {
                        { "x-rapidapi-key", "ae9cc4cc9emsh74fa90f1760528fp102f55jsn95a37886a0c3" },
                        { "x-rapidapi-host", "wft-geo-db.p.rapidapi.com" },
                    },
                };

                using (var response = await _httpClient.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    var body = await response.Content.ReadAsStringAsync();
                    var apiResponse = JsonSerializer.Deserialize<GeoApiResponse>(body);

                    if (apiResponse?.Data == null || !apiResponse.Data.Any())
                        break;

                    var newCountries = apiResponse.Data
                        .Where(dto => !existingCodes.Contains(dto.Code))
                        .Select(dto => new Country
                        {
                            Name = dto.Name,
                            CurrencyName = dto.CurrencyCodes?.FirstOrDefault(),
                            Code = dto.Code
                        })
                        .ToList();

                    // Update cache to prevent duplicates
                    foreach (var country in newCountries)
                    {
                        existingCodes.Add(country.Code);
                    }

                    allCountries.AddRange(newCountries);

                    if (newCountries.Any())
                    {
                        _countryService.InsertMany(newCountries);
                    }

                    // Respect API rate limits (1 request/second)
                    await Task.Delay(10000);
                }
            }

            return allCountries;
        }


    }
}
