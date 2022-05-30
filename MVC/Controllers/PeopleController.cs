using System;
using System.Net;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using MVC.Models;
using MVC.Data;

namespace MVC.Controllers
{
    public class PeopleController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public PeopleController(ApplicationDbContext context)
        {
            dbContext = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.Cities = new SelectList(dbContext.Cities.ToList());
            return View(new PeopleViewModel() { People = dbContext.People.ToList() } );
        }

        [HttpGet]
        public IActionResult List()
        {
            return PartialView("_PeopleView", dbContext.People.ToList());
        }

        [HttpGet]
        public IActionResult Remove(int id)
        {
            var person = dbContext.People.Find(id);
            if (person != null) {
                dbContext.People.Remove(person);
                dbContext.SaveChanges();
            }

            return PartialView("_PeopleView", dbContext.People.ToList());
        }

        [HttpPost]
        public IActionResult RemoveWithConfirmation(int id)
        {
            var person = dbContext.People.Find(id);
            if (person != null) {
                dbContext.People.Remove(person);
                dbContext.SaveChanges();
                return Ok("Person was removed!");
            }

            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return BadRequest($"Person with id={id} does not exist.");
        }

        [HttpPost]
        public IActionResult Details(int id)
        {
            var person = dbContext.People.Find(id);
            if (person != null) {
                return PartialView("_PersonView", person);
            }

            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return BadRequest($"Person with id={id} does not exist.");
        }

        [HttpPost]
        public IActionResult Search(string search)
        {
            if (!string.IsNullOrEmpty(search))
            {
                var list = dbContext.People.ToList().Where(
                    s => search.Split(',').Any(
                    t => s.Name.ToString().Contains(t.Trim(), StringComparison.OrdinalIgnoreCase)
                      || s.City.ToString().Contains(t.Trim(), StringComparison.OrdinalIgnoreCase))).ToList();

                return PartialView("_PeopleView", list);            
            }

            return PartialView("_PeopleView", dbContext.People.ToList());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create(PeopleCreateViewModel createModel)
        {
            if (ModelState.IsValid) {
                var person = new Person() {
                    Name = createModel.Name,
                    City = dbContext.Cities.Where(c => c.Name == createModel.City).FirstOrDefault(),
                    PhoneNumber = createModel.PhoneNumber
                };

                dbContext.People.Add(person);
                dbContext.SaveChanges();

                return PartialView("_PeopleView", dbContext.People.ToList());
            }

            return BadRequest(ModelState);
        }
    }
}