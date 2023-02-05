namespace EmmaWorkManagementProject.Models
{
    public class UserProfileViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? About { get; set; }
        public DateTime? Registered { get; set; }
        public string? Email { get; set; }
    }
}
