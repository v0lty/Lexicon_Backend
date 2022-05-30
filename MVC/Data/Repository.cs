using System.Collections.Generic;
using MVC.Models;

namespace MVC.Data
{    
    public class Repository
    {
        public static readonly List<Person> People = new List<Person>()
        {
            new Person { Name = "Johan Svensson", City = "Göteborg", PhoneNumber = "+46725180302"},
            new Person { Name = "Nils Kristiansson", City = "Uddevalla", PhoneNumber = "+46737470353"},
            new Person { Name = "Christoffer Nilsson", City = "Trollhättan", PhoneNumber = "+46736395900"},
            new Person { Name = "Lars Nilsson", City = "Göteborg", PhoneNumber = "+46725180305"},
            new Person { Name = "Johanna Pettersson", City = "Kristianstad", PhoneNumber = "+46733080322"},
            new Person { Name = "Lisa Forsell", City = "Lund", PhoneNumber = "+46718180309"},
            new Person { Name = "Kristian Nilsson", City = "Uddevalla", PhoneNumber = "+46739470303"},
            new Person { Name = "Sara Johnsson", City = "Stockholm", PhoneNumber = "+46739165309"}
        };
    }
}