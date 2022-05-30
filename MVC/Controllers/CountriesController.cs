using System;
using System.Net;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using MVC.Data;

namespace MVC.Controllers
{
    public class CountriesController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public CountriesController(ApplicationDbContext context)
        {
            dbContext = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new CountriesViewModel() { Countries = dbContext.Countries.ToList() });
        }

        [HttpGet]
        public IActionResult Remove(int id)
        {
            var country = dbContext.Countries.Find(id);
            if (country != null) {
                dbContext.Countries.Remove(country);
                dbContext.SaveChanges();
            }

            return PartialView("_CountriesView", dbContext.Countries.ToList());
        }

        [HttpPost]
        public IActionResult Search(string search)
        {
            if (!string.IsNullOrEmpty(search))
            {
                var list = dbContext.Countries.ToList().Where(
                  s => search.Split(',').Any(
                  t => s.Name.Contains(t.Trim(), StringComparison.OrdinalIgnoreCase)
               || s.Cities.Any(
                  c => c.Name.Contains(t.Trim(), StringComparison.OrdinalIgnoreCase)))).ToList();

                return PartialView("_CountriesView", list);
            }

            return PartialView("_CountriesView", dbContext.Countries.ToList());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create(CountryCreateViewModel createModel)
        {
            if (ModelState.IsValid) {

                if (dbContext.Countries.Any(c => c.Name == createModel.Name)) {
                    Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    return BadRequest($"A contry with name {createModel.Name} does already exist.");
                }

                dbContext.Countries.Add(new Country() { Name = createModel.Name });
                dbContext.SaveChanges();

                return PartialView("_CountriesView", dbContext.Countries.ToList());
            }

            return BadRequest(ModelState);
        }
    }
}