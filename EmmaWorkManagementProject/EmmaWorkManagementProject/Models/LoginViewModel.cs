using System.ComponentModel.DataAnnotations;

namespace EmmaWorkManagementProject.Models
{
    public class LoginViewModel
    {

        [Required(ErrorMessage = "Enter your email")]
        [DataType(DataType.Password)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Enter your password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
