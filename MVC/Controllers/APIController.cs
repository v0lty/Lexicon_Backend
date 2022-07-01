using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC.Data;
using MVC.Models;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MVC.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class APIController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public APIController(ApplicationDbContext context)
        {
            dbContext = context;
        }
                
        [HttpGet]
        [Route("GetPeople")]
        public JsonResult GetPeople()
        {
            var people = dbContext.People
                .Include(person => person.City)
                .ThenInclude(city => city.Country).AsNoTracking()
                .Include(person => person.PersonLanguages)
                .ThenInclude(personLanguage => personLanguage.Language).AsNoTracking()
                .Select(person => 
                  new { 
                    person.Id,
                    person.Name,
                    Phone = person.PhoneNumber,
                    City = person.City.Name,
                    CityId = person.City.Id,
                    Country = person.City.Country.Name,
                    CountryId = person.City.Country.Id,
                    Languages = person.PersonLanguages.Select(
                        personLanguage => new {
                        personLanguage.Language.Name
                    }).ToArray()
                }
            ).ToArray();

            var peopleData = JsonSerializer.Serialize(people, new JsonSerializerOptions() 
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                WriteIndented = true
            });

            return new JsonResult(peopleData);
        }

        [HttpGet]
        [Route("GetCountries")]
        public JsonResult GetCountries()
        {
            var countries = dbContext.Countries
                .Select(country =>
                  new {
                      country.Id,
                      country.Name,
                      Cities = country.Cities.Select(
                        city => new {
                            city.Id,
                            city.Name
                        }).ToArray()
                  }
            ).ToArray();

            var countriesData = JsonSerializer.Serialize(countries, new JsonSerializerOptions()
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                WriteIndented = true
            });

            return new JsonResult(countriesData);
        }

        [HttpGet]
        [Route("GetCities")]
        public JsonResult GetCities()
        {
            var cities = dbContext.Cities
                .Select(city =>
                  new {
                      city.Id,
                      city.Name,
                      Country = city.Country.Name,
                      CountryId = city.Country.Id,
                  }
            ).ToArray();

            var citiesData = JsonSerializer.Serialize(cities, new JsonSerializerOptions()
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                WriteIndented = true
            });

            return new JsonResult(citiesData);
        }

        [HttpPost]
        [Route("EditPerson")]
        public JsonResult EditPerson(string personId, string personName, string personPhone, string personCityId)
        {
            var person = dbContext.People.Find(int.Parse(personId));
            if (person != null) {
                person.Name = personName;
                person.PhoneNumber = personPhone;
                person.City = dbContext.Cities.Find(int.Parse(personCityId));
                dbContext.People.Update(person);
                dbContext.SaveChanges();
                return new JsonResult(null);
            }
            else {
                Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;
                return new JsonResult(new { Success = "False", responseText = $"Person with id {personId} does not exist." });
            }
        }

        [HttpPost]
        [Route("CreatePerson")]
        public JsonResult CreatePerson(string personName, string personPhone, string personCityId)
        {
            var person = new Person() {
                Name = personName,
                PhoneNumber = personPhone,
                City = dbContext.Cities.Find(int.Parse(personCityId))               
            };

            dbContext.People.Add(person);
            dbContext.SaveChanges();
            return new JsonResult(null);
        }

        [HttpPost]
        [Route("RemovePerson")]
        public JsonResult RemovePerson(string personId)
        {
            var person = dbContext.People.Find(int.Parse(personId));
            if (person != null) {
                dbContext.People.Remove(person);
                dbContext.SaveChanges();
                return new JsonResult(null);
            }
            else {
                Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;
                return new JsonResult(new { Success = "False", responseText = $"Person with id {personId} does not exist." });
            }
        }

        [HttpPost]
        [Route("EditCountry")]
        public JsonResult EditCountry(string countryId, string countryName)
        {
            var country = dbContext.Countries.Find(int.Parse(countryId));
            if (country != null) {
                country.Name = countryName;
                dbContext.Countries.Update(country);
                dbContext.SaveChanges();
                return new JsonResult(null);
            }
            else {
                Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;
                return new JsonResult(new { Success = "False", responseText = $"Country with id {countryId} does not exist." });
            }
        }

        [HttpPost]
        [Route("CreateCountry")]
        public JsonResult CreateCountry(string countryName)
        {
            if (dbContext.Countries.Any(c => c.Name == countryName)) {
                Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;
                return new JsonResult(new { Success = "False", responseText = $"A contry with name {countryName} does already exist." });
            }

            dbContext.Countries.Add(new Country() { Name = countryName });
            dbContext.SaveChanges();
            return new JsonResult(null);
        }

        [HttpPost]
        [Route("RemoveCountry")]
        public JsonResult RemoveCountry(string countryId)
        {
            var country = dbContext.Countries.Find(int.Parse(countryId));
            if (country != null)  {
                dbContext.Countries.Remove(country);
                dbContext.SaveChanges();
                return new JsonResult(null);
            }
            else {
                Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;
                return new JsonResult(new { Success = "False", responseText = $"Country with id {countryId} does not exist." });
            }
        }

        [HttpPost]
        [Route("EditCity")]
        public JsonResult EditCity(string cityId, string cityName, string countryId)
        {
            var city = dbContext.Cities.Find(int.Parse(cityId));
            if (city != null) {
                city.Name = cityName;
                city.Country = dbContext.Countries.Find(int.Parse(countryId));
                city.CountryId = city.Country.Id;
                dbContext.Cities.Update(city);
                dbContext.SaveChanges();
                return new JsonResult(null);
            }
            else {
                Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;
                return new JsonResult(new { Success = "False", responseText = $"City with id {cityId} does not exist." });
            }
        }

        [HttpPost]
        [Route("CreateCity")]
        public JsonResult CreateCity(string cityName, string countryId)
        {
            if (dbContext.Cities.Any(c => c.Name == cityName)) {
                Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;
                return new JsonResult(new { Success = "False", responseText = $"A city with name {cityName} does already exist." });
            }

            var country = dbContext.Countries.Find(int.Parse(countryId)); 
            if (country == null) {
                Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;
                return new JsonResult(new { Success = "False", responseText = $"A country with id {countryId} does not exist." });
            }

            dbContext.Cities.Add(new City() { Name = cityName, CountryId = int.Parse(countryId) });
            dbContext.SaveChanges();
            return new JsonResult(null);
        }

        [HttpPost]
        [Route("RemoveCity")]
        public JsonResult RemoveCity(string cityId)
        {
            var city = dbContext.Cities.Find(int.Parse(cityId));
            if (city != null)
            {
                dbContext.Cities.Remove(city);
                dbContext.SaveChanges();
                return new JsonResult(null);
            }
            else {
                Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;
                return new JsonResult(new { Success = "False", responseText = $"City with id {cityId} does not exist." });
            }
        }
    }
}