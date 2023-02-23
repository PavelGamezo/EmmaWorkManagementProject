using AutoMapper;
using EmmaWorkManagement.BusinessLayer.Dtos;
using EmmaWorkManagement.BusinessLayer.Interfaces;
using EmmaWorkManagement.Data.Interaces;
using EmmaWorkManagement.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmmaWorkManagement.BusinessLayer.Services.Subtasks
{
    public class SubtaskService : ISubtaskService
    {
        private readonly ISubtaskRepository _subtaskRepository;
        private readonly IMapper _mapper;

        public SubtaskService(ISubtaskRepository subtaskRepository, IMapper mapper)
        {
            _mapper = mapper;
            _subtaskRepository = subtaskRepository;
        }

        public async Task<IReadOnlyCollection<Subtask>> GetAllActiveSubtasks(int id)
        {
            return await _subtaskRepository.GetAll()
                                           .Where(q => q.UserTaskId == id)
                                           .ToArrayAsync();
        }
        
        public async Task DeleteSubtask(int id)
        {
            var subtask = await _subtaskRepository.GetById(id);
            var mappedSubtask = _mapper.Map<Subtask>(subtask);
            await _subtaskRepository.Delete(subtask);
            await _subtaskRepository.Save();
        }
        
        public async Task CreateSubtask(SubtaskDto model)
        {
            var subtask = _mapper.Map<Subtask>(model);
            await _subtaskRepository.Create(subtask);
            await _subtaskRepository.Save();
        }
    }
}
