﻿using EmmaWorkManagement.BusinessLayer.Dtos;
using EmmaWorkManagement.BusinessLayer.Interfaces;
using EmmaWorkManagement.Data;
using EmmaWorkManagement.Data.Interaces;
using EmmaWorkManagement.Data.Interfaces;
using EmmaWorkManagement.Entities;
using EmmaWorkManagementProject.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EmmaWorkManagementProject.Controllers
{
    public class UserTaskController : Controller
    {
        private readonly IUserTaskService _userTaskService;
        private readonly IAccountService _accountService;
        private readonly ApplicationDbContext _applicationDbContext;

        public UserTaskController(IUserTaskService userTaskService, IAccountService accountService, ApplicationDbContext applicationDbContext)
        {
            _userTaskService = userTaskService;
            _accountService = accountService;
            _applicationDbContext = applicationDbContext;
        }

        public async Task<IActionResult> GetTodayUserTasks()
        {
            var activeAccount = await _accountService.GetAccountByEmail(User.Identity.Name);
            var activeAccountId = activeAccount.Id;
            var response = await _userTaskService.GetTodayUserTasksAsync(activeAccountId);
            try
            {
                    var result = response.Select(q => new UserTaskViewModel
                    {
                        Id = q.Id,
                        Name = q.Name,
                        Summary = q.Summary,
                        DateOfCreation = q.DateOfCreation,
                        DateOfCompletion = q.DateOfCompletion,
                        Priority = q.Priority,
                    }).ToArray();

                return View(result);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error");
            }
        }

        public async Task<IActionResult> GetImportantUserTasks()
        {
            var activeAccountId = (await _accountService.GetAccountByEmail(User.Identity.Name)).Id;
            var response = await _userTaskService.GetImportantUserTasksAsync(activeAccountId);
            try
            {
                var result = response.Select(q => new UserTaskViewModel
                {
                    Id = q.Id,
                    Name = q.Name,
                    Summary = q.Summary,
                    DateOfCreation = q.DateOfCreation,
                    DateOfCompletion = q.DateOfCompletion,
                    Priority = q.Priority,
                }).ToArray();

                return View(result);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error");
            }
        }

        public async Task<IActionResult> GetUpcomingUserTasks()
        {
            var activeAccount = await _accountService.GetAccountByEmail(User.Identity.Name);
            var activeAccountId = activeAccount.Id;
            var response = await _userTaskService.GetUpcomingUserTasksAsync(activeAccountId);
            try
            {
                var result = response.Select(q => new UserTaskViewModel
                {
                    Id = q.Id,
                    Name = q.Name,
                    Summary = q.Summary,
                    DateOfCreation = q.DateOfCreation,
                    DateOfCompletion = q.DateOfCompletion,
                    Priority = q.Priority,
                }).ToArray();

                return View(result);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error");
            }
        }

        public async Task<IActionResult> GetOverdueUserTasks()
        {
            var activeAccount = await _accountService.GetAccountByEmail(User.Identity.Name);
            var activeAccountId = activeAccount.Id;
            var response = await _userTaskService.GetOverdueUserTasksAsync(activeAccountId);
            try
            {
                var result = response.Select(q => new UserTaskViewModel
                {
                    Id = q.Id,
                    Name = q.Name,
                    Summary = q.Summary,
                    DateOfCreation = q.DateOfCreation,
                    DateOfCompletion = q.DateOfCompletion,
                    Priority = q.Priority,
                }).ToArray();

                return View(result);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetUserTasksByName()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetUserTasksByName(string name)
        {
            try
            {
                var activeAccount = await _accountService.GetAccountByEmail(User.Identity.Name);
                var activeAccountId = activeAccount.Id;
                var response = await _userTaskService.GetUserTasksByNameAsync(activeAccountId, name);
                var result = response.Select(q => new UserTaskViewModel
                {
                    Id = q.Id,
                    Name = q.Name,
                    Summary = q.Summary,
                    DateOfCreation = q.DateOfCreation,
                    DateOfCompletion = q.DateOfCompletion,
                    Priority = q.Priority,
                }).ToArray();

                return View(result);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error");
            }
        }

        [HttpGet]
        public async Task<IActionResult> AddUserTask()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddUserTask(string name, string summary, string priority, DateTime dateOfCompletion)
        {
            try
            {
                var activeAccount = await _accountService.GetAccountByEmail(User.Identity.Name);
                var activeAccountId = activeAccount.Id;

                activeAccount.UserTasks.Add(new UserTask()
                {
                    Name = name,
                    DateOfCreation = DateTime.Now,
                    Priority = priority,
                    DateOfCompletion = dateOfCompletion,
                    Summary = summary,
                    AccountId = activeAccount.Id,
                });
                _applicationDbContext.SaveChanges();

                return RedirectToAction("GetTodayUserTasks");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error");
            }
        }

        public async Task<IActionResult> DeleteUserTask(int id)
        {
            try
            {
                await _userTaskService.DeleteUserTask(id);
                return RedirectToActionPermanent("GetTodayUserTasks");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error");
            }
        }
    }
}
