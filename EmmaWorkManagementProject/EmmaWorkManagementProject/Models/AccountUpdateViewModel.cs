using System.ComponentModel.DataAnnotations;

namespace EmmaWorkManagementProject.Models
{
    public class AccountUpdateViewModel
    {
        [Required(ErrorMessage = "Enter your email")]
        [DataType(DataType.Password)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Enter your email")]
        [DataType(DataType.Password)]
        public string EmailConfirm { get; set; }

        [Required(ErrorMessage = "Enter your password")]
        [MinLength(8, ErrorMessage = "New password is too small")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Password confirm")]
        [Required(ErrorMessage = "Confirm your password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string PasswordConfirm { get; set; }
    }
}
