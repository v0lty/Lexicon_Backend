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

        public IEnumerable<Person> People
        {
            get
            {
                return string.IsNullOrEmpty(Filter)
                ? Repository.People
                : Repository.People.Where(x => Filter.Split(',').Any(y =>
                x.Name.Contains(y.Trim(), StringComparison.OrdinalIgnoreCase) ||
                x.City.Contains(y.Trim(), StringComparison.OrdinalIgnoreCase))).ToList();
            }
        }

        public PeopleViewModel()
        {
            CreateViewModel = new CreatePersonViewModel();
        }
    }
}