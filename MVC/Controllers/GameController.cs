using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Assignment1.Controllers
{
    public class GameController : Controller
    {
        public int? SecretNumber
        {
            get { return HttpContext.Session.GetInt32("SecretNumber"); }
            set { HttpContext.Session.SetInt32("SecretNumber", value.Value); }
        }

        public int? NumGuesses
        {
            get { return HttpContext.Session.GetInt32("NumGuesses").Value; }
            set { HttpContext.Session.SetInt32("NumGuesses", value.Value); }
        }

        private void CreateSecretNumber()
        {
            SecretNumber = (new Random()).Next(0, 101);
            NumGuesses = 0;
        }

        [HttpGet]
        public IActionResult GuessingGame()
        {
            if (SecretNumber == null)
                CreateSecretNumber();

            return View();
        }

        [HttpPost]
        public IActionResult GuessingGame(int number)
        {
            if (number == SecretNumber) {                
                ViewBag.Message = string.Format($"Congratulation your guess was correct! Number of guesses: {NumGuesses}.");
                CreateSecretNumber();
            }
            else {
                ViewBag.NumGuesses = ++NumGuesses;
                ViewBag.Message = string.Format($"Your guess was too {(number < SecretNumber ? "low" : "high")}!");
            }
            return View();
        }
    }
}