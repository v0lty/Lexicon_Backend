using MVC.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MVC.Models
{    
    public class CityCreateViewModel
    {
        [Required, Display(Name = "Name")]
        public virtual string Name { get; set; }

        [Required, Display(Name = "Country")]
        public virtual string Country { get; set; }
    }

    public class CitiesViewModel
    {
        public virtual CityCreateViewModel CreateModel { get; }

        public virtual string Filter { get; set; }

        public virtual ICollection<City> Cities { get; set; }        

        public CitiesViewModel()
        {
            CreateModel = new CityCreateViewModel();
        }
    }
}