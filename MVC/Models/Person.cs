﻿using System;
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
        public virtual string Name { get; set; }

        [ForeignKey("City")]
        public virtual int CityId { get; set; }

        [Display(Name = "City")]
        public virtual City City { get; set; }

        [Display(Name = "Phone number")]
        public virtual string PhoneNumber { get; set; }

        public Person()
        {
        }

        public override string ToString()
        {
            return Name;
        }
    }
}