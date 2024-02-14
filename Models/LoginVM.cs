using System.ComponentModel.DataAnnotations;

namespace TravelBookingFrance.FrontMVC.Models
{
    public class LoginVM
    {
        [Display(Name = "Username")]
        [Required(ErrorMessage = "Username is required")]
        public string username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string password { get; set; }
    }
}
