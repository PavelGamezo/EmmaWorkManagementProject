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


        public async Task<IReadOnlyCollection<UserTaskDto>> GetSortedUserTasksByPriority(int activeAccountId)
        {
            return await _userTaskRepository.GetAll()
                                            .Include(q=>q.Subtasks)
                                            .Include(q=>q.Account)
                                            .Where(q=>q.AccountId == activeAccountId)
                                            .OrderByDescending(x => x.Priority)
                                            .ProjectTo<UserTaskDto>(_mapper.ConfigurationProvider)
                                            .ToArrayAsync();
        }

        public async Task<IReadOnlyCollection<UserTaskDto>> GetTodayUserTasksAsync(int activeAccountId)
        {
            return await _userTaskRepository.GetAll()
                                            .Include(q => q.Subtasks)
                                            .Include(q=>q.Account)
                                            .Where(q=>q.AccountId == activeAccountId)
                                            .ProjectTo<UserTaskDto>(_mapper.ConfigurationProvider)
                                            .Where(q=>q.DateOfCompletion.Day == DateTime.Now.Day)
                                            .ToArrayAsync();
        }
        
        public async Task<IReadOnlyCollection<UserTaskDto>> GetImportantUserTasksAsync(int activeAccountId)
        {
            return await _userTaskRepository.GetAll()
                                            .Include(q => q.Subtasks)
                                            .Include(q => q.Account)
                                            .Where(q => q.AccountId == activeAccountId)
                                            .ProjectTo<UserTaskDto>(_mapper.ConfigurationProvider)
                                            .Where(q => q.Priority == "High")
                                            .ToArrayAsync();
            
        }
        
        public async Task<IReadOnlyCollection<UserTaskDto>> GetUpcomingUserTasksAsync(int activeAccountId)
        {
            return await _userTaskRepository.GetAll()
                                            .Include(q => q.Subtasks)
                                            .Include(q => q.Account)
                                            .Where(q => q.AccountId == activeAccountId)
                                            .ProjectTo<UserTaskDto>(_mapper.ConfigurationProvider)
                                            .Where(q=>q.DateOfCompletion > DateTime.Now || q.DateOfCompletion.Day == DateTime.Now.Day)
                                            .ToArrayAsync();
        } 
        
        public async Task<IReadOnlyCollection<UserTaskDto>> GetOverdueUserTasksAsync(int activeAccountId)
        {
            return await _userTaskRepository.GetAll()
                                            .Include(q => q.Subtasks)
                                            .Include(q => q.Account)
                                            .Where(q => q.AccountId == activeAccountId)
                                            .ProjectTo<UserTaskDto>(_mapper.ConfigurationProvider)
                                            .Where(q=>q.DateOfCompletion < DateTime.Now && q.DateOfCompletion.Day != DateTime.Now.Day)
                                            .ToArrayAsync();
        }
        
        public async Task<IReadOnlyCollection<UserTaskDto>> GetUserTasksByNameAsync(int activeAccountId, string name)
        {
            return await _userTaskRepository.GetAll()
                                            .Include(q => q.Subtasks)
                                            .Include(q => q.Account)
                                            .Where(q => q.AccountId == activeAccountId)
                                            .Where(q => q.Name.Contains(name))
                                            .ProjectTo<UserTaskDto>(_mapper.ConfigurationProvider)
                                            .ToArrayAsync();
        }

        public async Task<IReadOnlyCollection<UserTaskDto>> GetUserTasksByDayTimeAsync(int activeAccountId)
        {
            var date = DateTime.Now;
            return await _userTaskRepository.GetAll()
                                            .Include(q => q.Subtasks)
                                            .Include(q => q.Account)
                                            .Where(q => q.AccountId == activeAccountId)
                                            .Where(q => q.DateOfCompletion.Day == date.Day && q.DateOfCompletion.Year == date.Year)
                                            .ProjectTo<UserTaskDto>(_mapper.ConfigurationProvider)
                                            .ToArrayAsync();
        }

        /*public async Task<IReadOnlyCollection<UserTaskDto>> GetAllByIncludeAsync()
        {

            return await _userTaskRepository.GetAllByInclude().Where(q=>q.)
        }*/

        public async Task<UserTaskDto> GetUserTask(int userTaskId)
        {
            var userTask = await _userTaskRepository.GetById(userTaskId);
            var mappingUserTask = _mapper.Map<UserTaskDto>(userTask);
            return mappingUserTask;
        }

        public async Task UpdateUserTask(UserTaskDto userTask, Account activeAccount, int id)
        {
            var task = await _userTaskRepository.GetById(id);
            task.DateOfCompletion = userTask.DateOfCompletion;
            task.Summary = userTask.Summary;
            task.Name = userTask.Name;
            task.Priority = userTask.Priority;
            
            await _userTaskRepository.Save();
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
