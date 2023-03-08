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
using EmmaWorkManagement.Data.Interaces;

namespace EmmaWorkManagement.BusinessLayer.Services.UserTasks
{
    public class UserTaskService : IUserTaskService
    {
        private readonly IMapper _mapper;
        private readonly IUserTaskRepository _userTaskRepository;
        private readonly IAccountRepository _accountRepository;

        public UserTaskService(IMapper mapper ,IUserTaskRepository userTaskRepository, IAccountRepository accountRepository)
        {
            _mapper = mapper;
            _userTaskRepository = userTaskRepository;
            _accountRepository = accountRepository;
        }

        #region IUserTaskService Members


        public async Task<IReadOnlyCollection<UserTaskDto>> GetSortedUserTasksByPriority(int activeAccountId)
        {
            return await _userTaskRepository.GetAll()
                                            .Where(q=>q.AccountId == activeAccountId)
                                            .OrderByDescending(x => x.Priority)
                                            .ProjectTo<UserTaskDto>(_mapper.ConfigurationProvider)
                                            .ToArrayAsync();
        }

        public async Task<IReadOnlyCollection<UserTaskDto>> GetTodayUserTasksAsync(int activeAccountId)
        {
            return await _userTaskRepository.GetAll()
                                            .Where(q=>q.AccountId == activeAccountId)
                                            .Where(q=>q.DateOfCompletion.Day == DateTime.Now.Day)
                                            .Where(q=>q.IsActive == false)
                                            .ProjectTo<UserTaskDto>(_mapper.ConfigurationProvider)
                                            .ToArrayAsync();
        }
        
        public async Task<IReadOnlyCollection<UserTaskDto>> GetImportantUserTasksAsync(int activeAccountId)
        {
            return await _userTaskRepository.GetAll()
                                            .Where(q => q.AccountId == activeAccountId)
                                            .Where(q => q.Priority == "High")
                                            .Where(q => q.IsActive == false)
                                            .ProjectTo<UserTaskDto>(_mapper.ConfigurationProvider)
                                            .ToArrayAsync();
            
        }
        
        public async Task<IReadOnlyCollection<UserTaskDto>> GetUpcomingUserTasksAsync(int activeAccountId)
        {
            return await _userTaskRepository.GetAll()
                                            .Where(q => q.AccountId == activeAccountId)
                                            .Where(q=>q.DateOfCompletion > DateTime.Now || q.DateOfCompletion.Day == DateTime.Now.Day)
                                            .Where(q => q.IsActive == false)
                                            .ProjectTo<UserTaskDto>(_mapper.ConfigurationProvider)
                                            .ToArrayAsync();
        } 
        
        public async Task<IReadOnlyCollection<UserTaskDto>> GetOverdueUserTasksAsync(int activeAccountId)
        {
            return await _userTaskRepository.GetAll()
                                            .Where(q => q.AccountId == activeAccountId)
                                            .Where(q=>q.DateOfCompletion < DateTime.Now && q.DateOfCompletion.Day != DateTime.Now.Day)
                                            .Where(q => q.IsActive == false)
                                            .ProjectTo<UserTaskDto>(_mapper.ConfigurationProvider)
                                            .ToArrayAsync();
        }
        
        public async Task<IReadOnlyCollection<UserTaskDto>> GetUserTasksByNameAsync(int activeAccountId, string name)
        {
            return await _userTaskRepository.GetAll()
                                            .Where(q => q.AccountId == activeAccountId)
                                            .Where(q => q.Name.Contains(name))
                                            .Where(q => q.IsActive == false)
                                            .ProjectTo<UserTaskDto>(_mapper.ConfigurationProvider)
                                            .ToArrayAsync();
        }

        public async Task<IReadOnlyCollection<UserTaskDto>> GetUserTasksByDayTimeAsync(int activeAccountId)
        {
            var date = DateTime.Now;
            return await _userTaskRepository.GetAll()
                                            .Where(q => q.AccountId == activeAccountId)
                                            .Where(q => q.DateOfCompletion.Day == date.Day && q.DateOfCompletion.Year == date.Year)
                                            .Where(q => q.IsActive == false)
                                            .ProjectTo<UserTaskDto>(_mapper.ConfigurationProvider)
                                            .ToArrayAsync();
        }

        public async Task<IReadOnlyCollection<UserTaskDto>> GetCompletedTasksAsync(int activeAccountId)
        {
            return await _userTaskRepository.GetAll()
                                            .Where(q => q.AccountId == activeAccountId)
                                            .Where(q => q.IsActive == true)
                                            .ProjectTo<UserTaskDto>(_mapper.ConfigurationProvider)
                                            .ToArrayAsync();
        }

        public async Task<UserTaskDto> GetUserTask(int userTaskId)
        {
            var userTask = await _userTaskRepository.GetById(userTaskId);
            var mappingUserTask = _mapper.Map<UserTaskDto>(userTask);
            return mappingUserTask;
        }

        public async Task UpdateUserTask(UserTaskDto userTaskDto)
        {
            var userTask = await _userTaskRepository.GetById(userTaskDto.Id);
            userTask.Name = userTaskDto.Name;
            userTask.Summary = userTaskDto.Summary;
            userTask.DateOfCompletion = userTaskDto.DateOfCompletion;
            userTask.Priority = userTaskDto.Priority;

            await _userTaskRepository.Save();
        }

        public async Task DeleteUserTask(int id)
        {
            var userTask = await _userTaskRepository.GetById(id);
            var mappingUserTask = _mapper.Map<UserTask>(userTask);
            await _userTaskRepository.Delete(mappingUserTask);
            await _userTaskRepository.Save();
        }

        public async Task AddUserTask(UserTaskDto userTaskModel, int accountModelId)
        {
            var mappingAccount = _accountRepository.GetAll().FirstOrDefault(q=>q.Id == accountModelId);
            var mappingUserTask = _mapper.Map<UserTask>(userTaskModel);

            mappingUserTask.Account = mappingAccount;
            mappingUserTask.AccountId = mappingAccount.Id;

            mappingAccount.UserTasks.Add(mappingUserTask);

            await _userTaskRepository.Save();
        }

        public async Task CompleteUserTask(int id)
        {
            var userTask = await _userTaskRepository.GetById(id);
            userTask.IsActive = !userTask.IsActive;

            _userTaskRepository.Save();
        }

        #endregion
    }
}
