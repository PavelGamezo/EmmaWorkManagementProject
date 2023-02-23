using AutoMapper;
using EmmaWorkManagement.Entities.Entities;

namespace EmmaWorkManagement.Entities
{
    public class UserTask
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Summary { get; set; }
        public DateTime DateOfCreation { get; set; }
        public DateTime DateOfCompletion { get; set; }
        public string Priority { get; set; }
        public bool IsActive { get; set; }

        public int AccountId { get; set; }
        public Account Account { get; set; }

        public ICollection<Subtask> Subtasks { get; set; } = new List<Subtask>();
    }
}
