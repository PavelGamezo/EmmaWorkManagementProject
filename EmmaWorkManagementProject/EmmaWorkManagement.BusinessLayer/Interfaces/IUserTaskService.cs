using EmmaWorkManagement.BusinessLayer.Dtos;
using EmmaWorkManagement.Data.Interfaces;
using EmmaWorkManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmmaWorkManagement.BusinessLayer.Interfaces
{
    public interface IUserTaskService
    {
        Task<IReadOnlyCollection<UserTaskDto>> GetTodayUserTasksAsync();

        Task<IReadOnlyCollection<UserTaskDto>> GetImportantUserTasksAsync();

        Task<IReadOnlyCollection<UserTaskDto>> GetUpcomingUserTasksAsync();

        Task<IReadOnlyCollection<UserTaskDto>> GetOverdueUserTasksAsync();

        Task<IReadOnlyCollection<UserTaskDto>> GetUserTasksByNameAsync(string name);

        Task AddUserTask(UserTaskDto userTask);

        Task DeleteUserTask(int id);
    }
}
