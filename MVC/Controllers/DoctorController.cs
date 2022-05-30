using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Assignment1.Models;

namespace Assignment1.Controllers
{
    public class DoctorController : Controller
    {
        public IActionResult FeverCheck()
        {
            return View();
        }

        [HttpPost]
        public IActionResult FeverCheck(string temperature, string measurement)
        {
            ViewBag.Message = DoctorModel.CheckTemperature(temperature, measurement);
            return View();
        }
    }
}
