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
        Task<IReadOnlyCollection<UserTaskDto>> GetTodayUserTasksAsync(int activeAccountId);

        Task<IReadOnlyCollection<UserTaskDto>> GetImportantUserTasksAsync(int activeAccountId);

        Task<IReadOnlyCollection<UserTaskDto>> GetUpcomingUserTasksAsync(int activeAccountId);

        Task<IReadOnlyCollection<UserTaskDto>> GetOverdueUserTasksAsync(int activeAccountId);

        Task<IReadOnlyCollection<UserTaskDto>> GetSortedUserTasksByPriority(int activeAccountId);

        Task<IReadOnlyCollection<UserTaskDto>> GetUserTasksByNameAsync(int activeAccountId, string name);

        Task<UserTaskDto> GetUserTask(int userTaskId);

        //Task<IReadOnlyCollection<UserTaskDto>> GetAllByInclude();

        Task AddUserTask(UserTaskDto userTask);

        Task DeleteUserTask(int id);
    }
}
