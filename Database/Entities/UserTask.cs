namespace EmmaWorkManagementProject.Database.Entities
{
    public class UserTask
    {
        public int Id { get; set; }

        public string Summary { get; set; }

        public DateTime DateOfCreation { get; set; }

        public DateTime DateOfCompletion { get; set; }
        public string Priority { get; set; }
        public string Key { get; set; }
    }
}
