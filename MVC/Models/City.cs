using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC.Models
{
    public class City
    {
        [Key]
        [Display(Name = "Id")]
        public virtual int Id { get; set; }

        [Display(Name = "Name")]
        public virtual string Name { get; set; }

        [ForeignKey("Country")]
        public virtual int CountryId { get; set; }

        [Display(Name = "Country")]
        public virtual Country Country { get; set; }

        [Display(Name = "People")]
        public virtual ICollection<Person> People { get; set; }

        public City()
        {
            People = new List<Person>();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
