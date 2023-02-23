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
        Task CreateSubtask(SubtaskDto model);
        Task DeleteSubtask(int id);
        Task<IReadOnlyCollection<Subtask>> GetAllActiveSubtasks(int id);
    }
}
