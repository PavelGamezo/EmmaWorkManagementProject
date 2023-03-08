using AutoMapper;
using EmmaWorkManagement.BusinessLayer.Dtos;
using EmmaWorkManagement.BusinessLayer.Interfaces;
using EmmaWorkManagement.Data.Interaces;
using EmmaWorkManagement.Data.Interfaces;
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
        private readonly IUserTaskRepository _userTaskRepository;
        private readonly IMapper _mapper;

        public SubtaskService(ISubtaskRepository subtaskRepository, IUserTaskRepository userTaskRepository, IMapper mapper)
        {
            _mapper = mapper;
            _subtaskRepository = subtaskRepository;
            _userTaskRepository = userTaskRepository;
        }

        public async Task<IReadOnlyCollection<Subtask>> GetAllActiveSubtasks(int id)
        {
            return await _subtaskRepository.GetAll()
                                           .Where(q => q.UserTaskId == id)
                                           .ToArrayAsync();
        }

        public async Task<SubtaskDto> GetSubtask(int id)
        {
            var subtask = await _subtaskRepository.GetById(id);
            var mappedSubtask = _mapper.Map<SubtaskDto>(subtask);

            return mappedSubtask;
        }
        
        public async Task DeleteSubtask(int id)
        {
            var subtask = await _subtaskRepository.GetById(id);
            var mappedSubtask = _mapper.Map<Subtask>(subtask);
            await _subtaskRepository.Delete(subtask);
            await _subtaskRepository.Save();
        }
        
        public async Task CreateSubtask(SubtaskDto model, int userTaskId)
        {
            var mappingUserTask = await _userTaskRepository.GetById(userTaskId);
            var mappingSubtask = _mapper.Map<Subtask>(model);

            mappingSubtask.UserTask = mappingUserTask;
            mappingSubtask.UserTaskId = mappingUserTask.Id;

            mappingUserTask.Subtasks.Add(mappingSubtask);
            await _subtaskRepository.Save();
        }

        public async Task CompleteSubtask(int id)
        {
            var subtask = await _subtaskRepository.GetById(id);
            subtask.isActive = !subtask.isActive;

            await _subtaskRepository.Save();
        }

        public async Task UpdateSubtask(SubtaskDto model)
        {
            var subtask = await _subtaskRepository.GetById(model.Id);
            subtask.Name = model.Name;
            subtask.Comment = model.Comment;

            await _subtaskRepository.Save();
        }
    }
}
