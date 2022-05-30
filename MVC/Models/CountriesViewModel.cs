using MVC.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MVC.Models
{
    public class CountryCreateViewModel
    {
        [Required, Display(Name = "Name")]
        public virtual string Name { get; set; }
    }

    public class CountriesViewModel
    {        
        public virtual CountryCreateViewModel CreateModel { get; }

        public virtual string Filter { get; set; }

        public virtual ICollection<Country> Countries { get; set; }

        public CountriesViewModel()
        {
            CreateModel = new CountryCreateViewModel();
        }
    }
}