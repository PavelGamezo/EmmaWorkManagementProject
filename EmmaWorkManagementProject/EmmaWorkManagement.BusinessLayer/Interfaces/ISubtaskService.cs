using EmmaWorkManagement.BusinessLayer.Dtos;
using EmmaWorkManagement.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmmaWorkManagement.BusinessLayer.Interfaces
{
    public interface ISubtaskService
    {
        Task<SubtaskDto> GetSubtask(int id);
        Task CreateSubtask(SubtaskDto model, int userTaskId);
        Task DeleteSubtask(int id);
        Task<IReadOnlyCollection<Subtask>> GetAllActiveSubtasks(int id);
        Task CompleteSubtask(int id);
        Task UpdateSubtask(SubtaskDto model);
    }
}
