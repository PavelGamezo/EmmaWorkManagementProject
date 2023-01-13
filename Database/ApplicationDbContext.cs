using EmmaWorkManagementProject.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmmaWorkManagementProject.Database
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserTask> UserTasks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            const string connectionString = "Server=localhost;Database=WorkingWithEfCore;Trusted_Connection=True";
            if (optionsBuilder.IsConfigured == false)
            {
                optionsBuilder.UseSqlServer(connectionString);
            }
        }
    }
}
