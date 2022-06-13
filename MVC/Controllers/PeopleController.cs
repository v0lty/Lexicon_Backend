using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVC.Data;
using MVC.Models;
using System;
using System.Linq;
using System.Net;

namespace MVC.Controllers
{
    [Authorize(Roles = "Admin, User")]
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

        [HttpGet]
        public IActionResult EditPerson(int id)
        {
            var person = dbContext.People.Find(id);
            if (person != null)
            {
                ViewBag.Cities = new SelectList(dbContext.Cities.ToList());
                return PartialView("_PersonView", new PeopleCreateViewModel()
                {
                    Id = person.Id,
                    Name = person.Name,
                    City = person.City.Name,
                    PhoneNumber = person.PhoneNumber
                });
            }

            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return BadRequest($"Person with id={id} does not exist.");
        }

        [HttpPost]
        public IActionResult EditPerson(PeopleCreateViewModel createModel)
        {
            if (ModelState.IsValid)
            {
                var person = dbContext.People.Find(createModel.Id);
                if (person != null) {
                    person.Name = createModel.Name;
                    person.City = dbContext.Cities.Where(c => c.Name == createModel.City).FirstOrDefault();
                    person.PhoneNumber = createModel.PhoneNumber;
                    dbContext.People.Update(person);
                    dbContext.SaveChanges();
                }

                return PartialView("_PeopleView", dbContext.People.ToList());
            }

            return BadRequest(ModelState);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult RemovePerson(int id)
        {
            var person = dbContext.People.Find(id);
            if (person != null)
            {
                dbContext.People.Remove(person);
                dbContext.SaveChanges();
            }

            return PartialView("_PeopleView", dbContext.People.ToList());
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
    }
}