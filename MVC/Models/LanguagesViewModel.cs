using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

using MVC.Data;

namespace MVC.Models
{
    public class LanguageCreateViewModel
    {
        [Required, Display(Name = "Name")]
        public virtual string Name { get; set; }
    }

    public class LanguageLinkViewModel
    {
        [Required, Display(Name = "Person")]
        public virtual string Person { get; set; }

        [Required, Display(Name = "Language")]
        public virtual string Language { get; set; }
    }

    public class LanguagesViewModel
    {
        public virtual LanguageCreateViewModel CreateModel { get; }

        public virtual LanguageLinkViewModel LinkModel { get; }

        public virtual string Filter { get; set; }

        public virtual ICollection<Language> Languages { get; set; }

        public LanguagesViewModel()
        {
            CreateModel = new LanguageCreateViewModel();
            LinkModel = new LanguageLinkViewModel();
        }
    }
}
