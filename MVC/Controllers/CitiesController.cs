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
        public IActionResult Create(CitiesViewModel viewModel)
        {
            if (ModelState.IsValid) {
                var city = new City() { 
                    Name = viewModel.CreateModel.Name, 
                    Country = dbContext.Countries.Where(c => c.Name == viewModel.CreateModel.Country).FirstOrDefault() 
                };
                dbContext.Cities.Add(city);
                dbContext.SaveChanges();

                return PartialView("_CitiesView", dbContext.Cities.ToList());
            }

            return BadRequest(ModelState);
        }
    }
}