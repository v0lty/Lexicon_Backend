using System;
using System.Net;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using MVC.Data;

namespace MVC.Controllers
{
    public class PeopleController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View(new PeopleViewModel());
        }

        [HttpGet]
        public IActionResult List()
        {
            return PartialView("_PeopleView", Repository.People);
        }

        [HttpGet]
        public IActionResult Remove(int id)
        {
            var person = Repository.People.Where(x => x.Id == id).FirstOrDefault();
            if (person != null)
            {
                Repository.People.Remove(person);
            }
            return PartialView("_PeopleView", Repository.People);
        }

        [HttpPost]
        public IActionResult RemoveWithConfirmation(int id)
        {
            var person = Repository.People.Where(x => x.Id == id).FirstOrDefault();
            if (person != null)
            {
                Repository.People.Remove(person);
                return Ok("Person was removed!");
            }

            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return BadRequest($"Person with id={id} does not exist.");
        }

        [HttpPost]
        public IActionResult Details(int id)
        {
            var person = Repository.People.Where(x => x.Id == id).FirstOrDefault();
            if (person != null)
            {
                return PartialView("_PersonView", person);
            }

            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return BadRequest($"Person with id={id} does not exist.");
        }

        [HttpPost]
        public IActionResult Search(string search)
        {
            if (ModelState.IsValid)
            {
                var model = new PeopleViewModel { Filter = search };
                return PartialView("_PeopleView", model.People);
            }

            return BadRequest(ModelState);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create(CreatePersonViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var person = new Person(viewModel.Name, viewModel.City, viewModel.PhoneNumber);
                Repository.People.Add(person);
                return PartialView("_PeopleView", Repository.People);
            }

            return BadRequest(ModelState);
        }
    }
}