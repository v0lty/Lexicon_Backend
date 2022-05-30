using System.ComponentModel.DataAnnotations;

namespace MVC.Models
{
    public class Person
    {
        static int index = 0;

        [Key]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "City")]
        public string City { get; set; }

        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }

        public Person() : this(null, null, null) { }

        public Person(string name, string city, string phoneNumber)
        {
            Id = index++;
            Name = name;
            City = city;
            PhoneNumber = phoneNumber;
        }
    }
}