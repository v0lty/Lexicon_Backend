using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MVC.Models
{
    public class Country
    {
        [Key]
        [Display(Name = "Id")]
        public virtual int Id { get; set; }

        [Display(Name = "Name")]
        public virtual string Name { get; set; }

        [Display(Name = "Citys")]
        public virtual ICollection<City> Cities { get; set; }

        public Country()
        {
            Cities = new List<City>();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
