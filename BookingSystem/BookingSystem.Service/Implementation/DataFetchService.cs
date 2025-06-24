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
            var existingCityNames = _cityService.GetAll()
                                                .Select(c => c.Name)
                                                .ToHashSet(StringComparer.OrdinalIgnoreCase);

            var countryByCode = _countryService.GetAll()
                                               .ToDictionary(c => c.Code, StringComparer.OrdinalIgnoreCase);

            var allCities = new List<City>();
            const int limit = 10;
            int maxOffset = 1526;

            for (int offset = 0; offset <= maxOffset; offset += limit)
            {
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri($"https://wft-geo-db.p.rapidapi.com/v1/geo/cities?limit={limit}&offset={maxOffset}&includeDeleted=NONE&minPopulation=80000&types=CITY&&countryIds=AL,AD,AT,BY,BE,BA,BG,HR,CY,CZ,DK,EE,FI,FR,DE,GR,HU,IS,IE,IT,XK,LV,LI,LT,LU,MT,MD,MC,ME,NL,MK,NO,PL,PT,RO,RU,SM,RS,SK,SI,ES,SE,CH,UA,GB,VA"),
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

                    var apiResponse = JsonSerializer.Deserialize<GeoApiResponseCities>(body);

                    if (apiResponse?.Data == null || !apiResponse.Data.Any())
                        break;

                    var newCities = apiResponse.Data
                        .Where(dto => !existingCityNames.Contains(dto.Name))
                        .Where(dto => countryByCode.ContainsKey(dto.CountryCode))
                        .Select(dto =>
                        {
                            countryByCode.TryGetValue(dto.CountryCode, out var country);

                            return new City
                            {
                                Name = dto.Name,
                                CountryId = country?.Id ?? Guid.Empty,
                                Country = country,
                                Latitude = dto.Latitude,
                                Longitude = dto.Longitude,
                                Population = dto.Population,
                                SizeCategory = GetSizeCategory(dto.Population),
                                Accommodations = null
                            };
                        })
                        .ToList();

                    foreach (var city in newCities)
                    {
                        existingCityNames.Add(city.Name);
                    }

                    allCities.AddRange(newCities);

                    if (newCities.Any())
                    {
                        _cityService.InsertMany(newCities);
                    }

                    await Task.Delay(10000);
                }
            }

            return allCities;
        }

        private SizeCategory GetSizeCategory(int population)
        {
            if (population < 100_000) return SizeCategory.Small;
            if (population < 1_000_000) return SizeCategory.Medium;
            return SizeCategory.Large;
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
