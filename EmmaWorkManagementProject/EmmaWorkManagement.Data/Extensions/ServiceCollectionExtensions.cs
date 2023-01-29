using EmmaWorkManagement.Data.Interaces;
using EmmaWorkManagement.Data.Interfaces;
using EmmaWorkManagement.Data.Repositories;
using EmmaWorkManagement.Entities;
using EmmaWorkManagementProject.Database.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EmmaWorkManagement.Data.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IUserTaskRepository, UserTaskRepository>();

            return services;
        }
    }
}
