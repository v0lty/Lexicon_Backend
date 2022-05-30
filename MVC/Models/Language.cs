using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MVC.Models
{
    public class Language
    {
        [Key]
        [Display(Name = "Id")]
        public virtual int Id { get; set; }

        [Display(Name = "Name")]
        public virtual string Name { get; set; }

        [Display(Name = "Persons")]
        public virtual ICollection<PersonLanguage> PersonLanguages { get; set; }

        public Language()
        {
            PersonLanguages = new List<PersonLanguage>();
        }

        public override string ToString()
        {
            return string.Format($"{Name} (#{Id:D4})");
        }
    }
}
