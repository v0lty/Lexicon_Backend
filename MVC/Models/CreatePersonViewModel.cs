using System.ComponentModel.DataAnnotations;

namespace MVC.Models
{
    public class CreatePersonViewModel
    {
        [Required, Display(Name = "Name")]
        public string Name { get; set; }

        [Required, Display(Name = "City")]
        public string City { get; set; }

        [Required, Display(Name = "Phone number")]
        [RegularExpression("^(?!0+$)(\\+\\d{1,3}[- ]?)?(?!0+$)\\d{10,15}$", ErrorMessage = "Phone number is not valid.")]
        public string PhoneNumber { get; set; }
    }
}
