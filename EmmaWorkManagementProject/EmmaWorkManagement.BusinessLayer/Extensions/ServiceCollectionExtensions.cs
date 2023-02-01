using EmmaWorkManagement.BusinessLayer.Interfaces;
using EmmaWorkManagement.BusinessLayer.Services.Accounts;
using EmmaWorkManagement.BusinessLayer.Services.UserProfiles;
using EmmaWorkManagement.BusinessLayer.Services.UserTasks;
using EmmaWorkManagement.Data.Extensions;
using EmmaWorkManagement.Entities;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EmmaWorkManagement.BusinessLayer.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBusinessLayerServices(this IServiceCollection services, string connectionString)
        {
            services.AddScoped<IUserTaskService, UserTaskService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IUserProfileService, UserProfileService>();
            services.AddDatabase(connectionString);
            services.AddAutoMapper(Assembly.GetCallingAssembly(),
                       Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
