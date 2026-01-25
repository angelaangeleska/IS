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
            string euroCodes = string.Join(",", countryByCode.Keys);

            var allCities = new List<City>();
            const int limit = 10;
            int maxOffset = 160;

            for (int offset = 0; offset <= maxOffset; offset += limit)
            {
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri($"https://wft-geo-db.p.rapidapi.com/v1/geo/cities?limit={limit}&offset={offset}&includeDeleted=NONE&minPopulation=400000&types=CITY&&countryIds={euroCodes}"),
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

                    await Task.Delay(1500);
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
            // Prevent double-seeding
            if (_countryService.GetAll().Any()) return _countryService.GetAll().ToList();

            // The list of currencies used across the entire European continent
            var euroCurrencies = new List<string>
            {
                "EUR", "GBP", "CHF", "PLN", "HUF", "CZK", "DKK", "SEK", "NOK", // EU & West
                "MKD", "RSD", "ALL", "BAM", "BGN", "RON", "MDL", "UAH", "BYN"  // Balkans & East
            };

            var allCountries = new List<Country>();
            var existingCodes = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            foreach (var currency in euroCurrencies)
            {
                const int limit = 10;
                int maxOffset = (currency == "EUR") ? 20 : 0;

                for (int offset = 0; offset <= maxOffset; offset += limit)
                {
                    var request = new HttpRequestMessage
                    {
                        Method = HttpMethod.Get,
                        RequestUri = new Uri($"https://wft-geo-db.p.rapidapi.com/v1/geo/countries?limit={limit}&offset={offset}&currencyCode={currency}"),
                        Headers =
                        {
                            { "x-rapidapi-key", "ae9cc4cc9emsh74fa90f1760528fp102f55jsn95a37886a0c3" },
                            { "x-rapidapi-host", "wft-geo-db.p.rapidapi.com" },
                        },
                    };

                    using var response = await _httpClient.SendAsync(request);
                    if (!response.IsSuccessStatusCode) break;

                    var body = await response.Content.ReadAsStringAsync();
                    var apiResponse = JsonSerializer.Deserialize<GeoApiResponse>(body);

                    if (apiResponse?.Data == null || !apiResponse.Data.Any()) break;

                    var newCountries = apiResponse.Data
                        .Where(dto => !existingCodes.Contains(dto.Code))
                        .Select(dto => new Country
                        {
                            Name = dto.Name,
                            Code = dto.Code,
                            CurrencyName = currency
                        }).ToList();

                    if (newCountries.Any())
                    {
                        _countryService.InsertMany(newCountries);
                        foreach (var c in newCountries) existingCodes.Add(c.Code);
                        allCountries.AddRange(newCountries);
                    }

                    // Respecting rate limits while keeping things moving
                    await Task.Delay(1500);
                }
            }
            return allCountries;
        }
    }
}
