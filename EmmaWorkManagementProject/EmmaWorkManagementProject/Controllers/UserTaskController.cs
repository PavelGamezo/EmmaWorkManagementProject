using EmmaWorkManagement.BusinessLayer.Dtos;
using EmmaWorkManagement.BusinessLayer.Interfaces;
using EmmaWorkManagement.Data.Interfaces;
using EmmaWorkManagementProject.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EmmaWorkManagementProject.Controllers
{
    public class UserTaskController : Controller
    {
        private readonly IUserTaskService _userTaskService;
        private readonly IAccountService _accountService;

        public UserTaskController(IUserTaskService userTaskService, IAccountService accountService)
        {
            _userTaskService = userTaskService;
            _accountService = accountService;
        }

        public async Task<IActionResult> GetTodayUserTasks()
        {
            var response = await _userTaskService.GetTodayUserTasksAsync();
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
            var response = await _userTaskService.GetImportantUserTasksAsync();
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
            var response = await _userTaskService.GetUpcomingUserTasksAsync();
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
            var response = await _userTaskService.GetOverdueUserTasksAsync();
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
                var response = await _userTaskService.GetUserTasksByNameAsync(name);
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
                var accountName = User.Identity.Name;
                var newUserTask = new UserTaskDto()
                {
                    Name = name,
                    DateOfCreation = DateTime.Now,
                    Priority = priority,
                    DateOfCompletion = dateOfCompletion,
                    Summary = summary,
                };
                await _userTaskService.AddUserTask(newUserTask);
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
