using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC.Models
{
    public class Person
    {
        [Key]
        [Display(Name = "Id")]
        public virtual int Id { get; set; }

        [Display(Name = "Name")]
        [StringLength(50, MinimumLength = 3)]
        public virtual string Name { get; set; }

        [ForeignKey("City")]
        public virtual int CityId { get; set; }

        [Display(Name = "City")]
        public virtual City City { get; set; }

        [Display(Name = "Phone number")]
        public virtual string PhoneNumber { get; set; }

        [Display(Name = "Languages")]
        public virtual ICollection<PersonLanguage> PersonLanguages { get; set; }

        public Person()
        {
            PersonLanguages = new List<PersonLanguage>();
        }
        public override string ToString()
        {
            return string.Format($"{Name} (#{Id:D4})");
        }
    }
}