using AutoMapper;
using AutoMapper.QueryableExtensions;
using EmmaWorkManagementProject;
using EmmaWorkManagement.BusinessLayer.Dtos;
using EmmaWorkManagement.BusinessLayer.Interfaces;
using EmmaWorkManagement.Data;
using EmmaWorkManagement.Data.Interfaces;
using EmmaWorkManagement.Entities;
using EmmaWorkManagement.Entities.Enums;
using EmmaWorkManagement.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmmaWorkManagement.BusinessLayer.Services.UserTasks
{
    public class UserTaskService : IUserTaskService
    {
        private readonly IMapper _mapper;
        private readonly IUserTaskRepository _userTaskRepository;

        public UserTaskService(IMapper mapper ,IUserTaskRepository userTaskRepository)
        {
            _mapper = mapper;
            _userTaskRepository = userTaskRepository;
        }

        #region IUserTaskService Members

        /*
        public async Task<IReadOnlyCollection<UserTaskDto>> GetSortedUserTasksByPriority()
        {
            return await _userTaskRepository.Select()
                                            .Where(q => q.Summary.Contains(summary))
                                            .ProjectTo<UserTaskDto>(_mapper.ConfigurationProvider)
                                            .ToArrayAsync();
        }
        
        public async Task<IReadOnlyCollection<UserTaskDto>> GetSortedUserTasksByDateOfCompletion()
        {

        }

        public async Task<IReadOnlyCollection<UserTaskDto>> GetSortedUserTasksByAlphabet()
        {

        }
        
        public async Task<IReadOnlyCollection<UserTaskDto>> GetSortedUserTasksByDateOfCreation()
        {

        }
        */

        public async Task<IReadOnlyCollection<UserTaskDto>> GetTodayUserTasksAsync()
        {
            return await _userTaskRepository.GetAll()
                                            .ProjectTo<UserTaskDto>(_mapper.ConfigurationProvider)
                                            .Where(q=>q.DateOfCompletion.Day == DateTime.Now.Day)
                                            .ToArrayAsync();
        }
        
        public async Task<IReadOnlyCollection<UserTaskDto>> GetImportantUserTasksAsync()
        {
            return await _userTaskRepository.GetAll()
                                            .ProjectTo<UserTaskDto>(_mapper.ConfigurationProvider)
                                            .Where(q => q.Priority == "High")
                                            .ToArrayAsync();
            
        }
        
        public async Task<IReadOnlyCollection<UserTaskDto>> GetUpcomingUserTasksAsync()
        {
            return await _userTaskRepository.GetAll()
                                            .ProjectTo<UserTaskDto>(_mapper.ConfigurationProvider)
                                            .Where(q=>q.DateOfCompletion > DateTime.Now || q.DateOfCompletion.Day == DateTime.Now.Day)
                                            .ToArrayAsync();
        } 
        
        public async Task<IReadOnlyCollection<UserTaskDto>> GetOverdueUserTasksAsync()
        {
            return await _userTaskRepository.GetAll()
                                            .ProjectTo<UserTaskDto>(_mapper.ConfigurationProvider)
                                            .Where(q=>q.DateOfCompletion < DateTime.Now && q.DateOfCompletion.Day != DateTime.Now.Day)
                                            .ToArrayAsync();
        }
        
        public async Task<IReadOnlyCollection<UserTaskDto>> GetUserTasksByNameAsync(string name)
        {
            return await _userTaskRepository.GetAll()
                                            .Where(q => q.Name.Contains(name))
                                            .ProjectTo<UserTaskDto>(_mapper.ConfigurationProvider)
                                            .ToArrayAsync();
        }

        public async Task<IReadOnlyCollection<UserTaskDto>> GetUserTasksByDayTimeAsync()
        {
            var date = DateTime.Now;
            return await _userTaskRepository.GetAll()
                                            .Where(q => q.DateOfCompletion.Day == date.Day && q.DateOfCompletion.Year == date.Year)
                                            .ProjectTo<UserTaskDto>(_mapper.ConfigurationProvider)
                                            .ToArrayAsync();

            // if (IReadOnlyCollection<UserTaskDto> is null || IReadOnlyCollection<UserTaskDto> == 0) throw new ObjectNotFoundException;
        }

        public async Task DeleteUserTask(int id)
        {
            var userTask = await _userTaskRepository.GetById(id);
            var mappingUserTask = _mapper.Map<UserTask>(userTask);
            await _userTaskRepository.Delete(mappingUserTask);
            await _userTaskRepository.Save();
        }

        public async Task AddUserTask(UserTaskDto userTask)
        {
            var mappingUserTask = _mapper.Map<UserTask>(userTask);
            await _userTaskRepository.Create(mappingUserTask);
            await _userTaskRepository.Save();
        }

        #endregion
    }
}
