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
        private static PeopleViewModel model = null;

        [HttpGet]
        public IActionResult Index()
        {
            if (model == null)
                model = new PeopleViewModel();

            return View(model);
        }

        [HttpGet]
        public IActionResult Remove(int id)
        {
            var person = Repository.People.Where(x => x.Id == id).FirstOrDefault();
            if (person != null) {
                Repository.People.Remove(person);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Search(string filter)
        {
            model.Filter = filter;
            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create(PeopleViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var person = new Person(viewModel.CreateViewModel.Name, viewModel.CreateViewModel.City, viewModel.CreateViewModel.PhoneNumber);
                Repository.People.Add(person);
                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }
    }
}