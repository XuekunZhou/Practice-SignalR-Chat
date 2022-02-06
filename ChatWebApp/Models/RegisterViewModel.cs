#nullable disable
using System.ComponentModel.DataAnnotations;

namespace ChatWebApp.Models
{
    public class RegisterViewModel
    {
        [Required]
        [MaxLength(32)]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(32)]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date of birth")]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage ="Passwords do not match")]
        [Display(Name = "Confirm password")]
        public string ConfirmPassword { get; set; }
    }
}