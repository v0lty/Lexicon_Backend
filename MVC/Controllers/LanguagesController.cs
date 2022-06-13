using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVC.Data;
using MVC.Models;
using System;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;

namespace MVC.Controllers
{
    [Authorize(Roles ="Admin")]
    public class LanguagesController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public LanguagesController(ApplicationDbContext context)
        {
            dbContext = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.People = new SelectList(dbContext.People.ToList());
            ViewBag.Languages = new SelectList(dbContext.Languages.ToList());

            return View(new LanguagesViewModel() { Languages = dbContext.Languages.ToList() });
        }

        [HttpGet]
        public IActionResult Remove(int id)
        {
            var language = dbContext.Languages.Find(id);
            if (language != null) {
                dbContext.Languages.Remove(language);
                dbContext.SaveChanges();
            }

            return PartialView("_LanguagesView", dbContext.Languages.ToList());
        }

        [HttpPost]
        public IActionResult Search(string search)
        {
            if (!string.IsNullOrEmpty(search))
            {
                var list = dbContext.Languages.ToList().Where(
                  s => search.Split(',').Any(
                  t => s.Name.Contains(t.Trim(), StringComparison.OrdinalIgnoreCase))).ToList();

                return PartialView("_LanguagesView", list);
            }

            return PartialView("_LanguagesView", dbContext.Languages.ToList());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create(LanguageCreateViewModel createModel)
        {
            if (ModelState.IsValid) {

                if (dbContext.Languages.Any(l => l.Name == createModel.Name)) {
                    Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    return BadRequest($"Language  with name {createModel.Name} does already exist.");
                }

                dbContext.Languages.Add(new Language() { Name = createModel.Name });
                dbContext.SaveChanges();
                // refresh
                ViewBag.Languages = new SelectList(dbContext.Languages.ToList());

                return PartialView("_LanguagesView", dbContext.Languages.ToList());
            }

            return BadRequest(ModelState);
        }

        private bool PersonLanguageExists(PersonLanguage personLanguages)
        {
            return dbContext.PersonLanguages.Any(pl => pl.PersonId == personLanguages.PersonId && pl.LanguageId == personLanguages.LanguageId);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Link(LanguageLinkViewModel linkModel)
        {
            if (ModelState.IsValid)
            {
                var person = dbContext.People.Find(int.Parse(Regex.Match(linkModel.Person, @"\d+").Value));
                var language = dbContext.Languages.Find(int.Parse(Regex.Match(linkModel.Language, @"\d+").Value));

                if (person == null || language == null) {
                    Response.StatusCode = (int)HttpStatusCode.NotFound;
                    return NotFound();
                }

                if (dbContext.PersonLanguages.Any(pl => pl.PersonId == person.Id && pl.LanguageId == language.Id)) {
                    Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    return BadRequest("Person and Language are already linked.");
                }

                dbContext.PersonLanguages.Add(new PersonLanguage() { PersonId = person.Id, LanguageId = language.Id });
                dbContext.SaveChanges();

                return PartialView($"_LanguagesView", dbContext.Languages.ToList());
            }

            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return BadRequest(ModelState);
        }
    }
}