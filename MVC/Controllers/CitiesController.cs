using System;
using System.Net;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using MVC.Models;
using MVC.Data;

namespace MVC.Controllers
{
    public class CitiesController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public CitiesController(ApplicationDbContext context)
        {
            dbContext = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.Countries = new SelectList(dbContext.Countries.ToList());

            return View(new CitiesViewModel() { Cities = dbContext.Cities.ToList() });
        }

        [HttpGet]
        public IActionResult Remove(int id)
        {
            var city = dbContext.Cities.Find(id);
            if (city != null) {
                dbContext.Cities.Remove(city);
                dbContext.SaveChanges();
            }

            return PartialView("_CitiesView", dbContext.Cities.ToList());
        }

        [HttpPost]
        public IActionResult Search(string search)
        {
            if (!string.IsNullOrEmpty(search))
            {
                var list = dbContext.Cities.ToList().Where(
                  s => search.Split(',').Any(
                  t => s.Name.Contains(t.Trim(), StringComparison.OrdinalIgnoreCase) 
            || s.Country.Name.Contains(t.Trim(), StringComparison.OrdinalIgnoreCase))).ToList();

                return PartialView("_CitiesView", list);
            }

            return PartialView("_CitiesView", dbContext.Cities.ToList());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create(CityCreateViewModel createModel)
        {
            if (ModelState.IsValid) {

                if (dbContext.Cities.Any(c => c.Name == createModel.Name)) {
                    Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    return BadRequest($"A city with name {createModel.Name} does already exist.");
                }

                dbContext.Cities.Add(new City() { Name = createModel.Name, Country = dbContext.Countries.Where(c => c.Name == createModel.Country).FirstOrDefault() });
                dbContext.SaveChanges();

                return PartialView("_CitiesView", dbContext.Cities.ToList());
            }

            return BadRequest(ModelState);
        }
    }
}