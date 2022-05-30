using System;
using System.Collections.Generic;
using System.Linq;
using MVC.Data;

namespace MVC.Models
{
    public class PeopleViewModel
    {
        public CreatePersonViewModel CreateViewModel { get; set; }

        public string Filter { get; set; }

        public IEnumerable<Person> People { get; set; }

        public PeopleViewModel()
        {
            CreateViewModel = new CreatePersonViewModel();
        }
    }
}