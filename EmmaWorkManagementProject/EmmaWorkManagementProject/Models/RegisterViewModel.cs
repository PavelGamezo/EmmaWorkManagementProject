using System.ComponentModel.DataAnnotations;

namespace EmmaWorkManagementProject.Models
{
    public class RegisterViewModel
    {
        [Display(Name = "Name")]
        [Required(ErrorMessage = "Enter your name")]
        [MinLength(1, ErrorMessage = "The minimum length must be greater than one character")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Enter surname")]
        [Display(Name = "Surname")]
        [MinLength(1, ErrorMessage = "The minimum length must be greater than one character")]
        public string? Surname { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Enter Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Password confirm")]
        [Required(ErrorMessage = "Confirm your password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string PasswordConfirm { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Enter your email")]
        public string Email { get; set; }
    }
}
