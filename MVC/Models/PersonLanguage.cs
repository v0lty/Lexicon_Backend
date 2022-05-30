using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC.Models
{
    public class PersonLanguage
    {
        public virtual int PersonId { get; set; }

        public virtual int LanguageId { get; set; }

        public virtual Person Person { get; set; }

        public virtual Language Language { get; set; }

        public override string ToString()
        {
            return string.Format($"{Person.Name} {Language.Name}");
        }
    }
}
