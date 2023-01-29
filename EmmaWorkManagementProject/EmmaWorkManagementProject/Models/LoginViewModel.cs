using System.ComponentModel.DataAnnotations;

namespace EmmaWorkManagementProject.Models
{
    public class LoginViewModel
    {

        [Required(ErrorMessage = "Enter your name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Enter your surname")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Enter your Password")]
        [DataType(DataType.Password)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Enter your Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
