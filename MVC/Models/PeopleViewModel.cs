using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using MVC.Data;

namespace MVC.Models
{
    public class PeopleCreateViewModel
    {
        [Required, Display(Name = "Name")]
        public string Name { get; set; }

        [Required, Display(Name = "City")]
        public string City { get; set; }

        [Required, Display(Name = "Phone number")]
        [RegularExpression("^(?!0+$)(\\+\\d{1,3}[- ]?)?(?!0+$)\\d{10,15}$", ErrorMessage = "Phone number is not valid.")]
        public string PhoneNumber { get; set; }
    }

    public class PeopleViewModel
    {
        public PeopleCreateViewModel CreateModel { get; set; }

        public string Filter { get; set; }

        public IEnumerable<Person> People { get; set; }

        public PeopleViewModel()
        {
            CreateModel = new PeopleCreateViewModel();
        }
    }
}