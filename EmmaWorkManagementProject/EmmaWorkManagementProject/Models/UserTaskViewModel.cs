using EmmaWorkManagement.Entities.Entities;
using System.ComponentModel.DataAnnotations;

namespace EmmaWorkManagementProject.Models
{
    public class UserTaskViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "Enter task name")]
        [MinLength(1, ErrorMessage = "The minimum length must be greater than one character")]
        public string Name { get; set; }

        [Display(Name = "Summary")]
        [Required(ErrorMessage = "Enter task summary")]
        [Range(0, 100, ErrorMessage = "Length must be between 0 and 1000")]
        public string? Summary { get; set; }

        public DateTime DateOfCreation { get; set; }

        [Display(Name = "Date of complition")]
        [Required(ErrorMessage = "Enter completion date of new task")]
        public DateTime DateOfCompletion { get; set; }

        [Display(Name = "Priority")]
        [Required(ErrorMessage = "Enter completion date of new task")]
        public string Priority { get; set; }

        public ICollection<Subtask> Subtasks { get; set; } = null!;
    }
}
