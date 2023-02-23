using System.ComponentModel.DataAnnotations;

namespace EmmaWorkManagementProject.Models
{
    public class SubtaskViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [MinLength(1, ErrorMessage = "Name should have more than 1 symbol")]
        [MaxLength(50, ErrorMessage = "Name should have less than 50 symbols")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [MaxLength(1000, ErrorMessage = "Name should have less than 1000 symbols")]
        [Display(Name = "Comment")]
        public string? Comment { get; set; }

        public int UserTaskId { get; set; }
    }
}
