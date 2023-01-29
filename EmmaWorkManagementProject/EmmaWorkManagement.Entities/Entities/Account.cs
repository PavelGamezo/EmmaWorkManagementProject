using EmmaWorkManagement.Entities.Entities;

namespace EmmaWorkManagement.Entities
{
    public class Account
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public ICollection<UserTask> UserTasks { get; set; }
    }
}
