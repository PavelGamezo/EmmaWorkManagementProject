using EmmaWorkManagement.BusinessLayer.Dtos;
using EmmaWorkManagement.BusinessLayer.Interfaces;
using EmmaWorkManagement.Data;
using EmmaWorkManagement.Data.Interaces;
using EmmaWorkManagement.Data.Interfaces;
using EmmaWorkManagement.Entities;
using EmmaWorkManagement.Exceptions;
using EmmaWorkManagementProject.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EmmaWorkManagementProject.Controllers
{
    public class UserTaskController : Controller
    {
        private readonly IUserTaskService _userTaskService;
        private readonly IAccountService _accountService;

        public UserTaskController(IUserTaskService userTaskService, IAccountService accountService, ApplicationDbContext applicationDbContext)
        {
            _userTaskService = userTaskService;
            _accountService = accountService;
        }

        public async Task<IActionResult> GetSortedUserTasksByPriority()
        {
            var activeAccountDto = await _accountService.GetAccountByEmailAsync(User.Identity.Name);
            try
            {
                if (activeAccountDto == null)
                {
                    throw new ObjectNotFoundException("Account");
                }
                var activeAccountId = activeAccountDto.Id;
                var response = await _userTaskService.GetSortedUserTasksByPriority(activeAccountId);
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
            catch (ObjectNotFoundException objNotFound)
            {
                return RedirectToAction("Error");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error");
            }
        }

        public async Task<IActionResult> GetTodayUserTasks()
        {
            var activeAccountDto = await _accountService.GetAccountByEmailAsync(User.Identity.Name);
            try
            {
                if(activeAccountDto == null)
                {
                    throw new ObjectNotFoundException("Account");
                }
                var activeAccountId = activeAccountDto.Id;
                var response = await _userTaskService.GetTodayUserTasksAsync(activeAccountId);
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
            catch (ObjectNotFoundException objNotFound)
            {
                return RedirectToAction("Error");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error");
            }
        }

        public async Task<IActionResult> GetImportantUserTasks()
        {
            var activeAccountDto = await _accountService.GetAccountByEmailAsync(User.Identity.Name);
            try
            {
                if (activeAccountDto == null)
                {
                    throw new ObjectNotFoundException("Account");
                }

                var activeAccountId = activeAccountDto.Id;
                var response = await _userTaskService.GetImportantUserTasksAsync(activeAccountId);
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
            catch (ObjectNotFoundException objNotFound)
            {
                return RedirectToAction("Error");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error");
            }
        }

        public async Task<IActionResult> GetUpcomingUserTasks()
        {
            var activeAccountDto = await _accountService.GetAccountByEmailAsync(User.Identity.Name);
            try
            {
                if (activeAccountDto == null)
                {
                    throw new ObjectNotFoundException("Account");
                }

                var activeAccountId = activeAccountDto.Id;
                var response = await _userTaskService.GetUpcomingUserTasksAsync(activeAccountId);
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
            catch (ObjectNotFoundException objNotFound)
            {
                return RedirectToAction("Error");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error");
            }
        }

        public async Task<IActionResult> GetOverdueUserTasks()
        {
            var activeAccountDto = await _accountService.GetAccountByEmailAsync(User.Identity.Name);
            try
            {
                if (activeAccountDto == null)
                {
                    throw new ObjectNotFoundException("Account");
                }

                var activeAccountId = activeAccountDto.Id;
                var response = await _userTaskService.GetOverdueUserTasksAsync(activeAccountId);
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
            catch (ObjectNotFoundException objNotFound)
            {
                return RedirectToAction("Error");
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
            var activeAccountDto = await _accountService.GetAccountByEmailAsync(User.Identity.Name);
            try
            {
                if (activeAccountDto == null)
                {
                    throw new ObjectNotFoundException("Account");
                }

                var activeAccountId = activeAccountDto.Id;
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
            catch (ObjectNotFoundException objNotFound)
            {
                return RedirectToAction("Error");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error");
            }
        }

        public async Task<IActionResult> GetCompletedTasks()
        {
            var accountDto = await _accountService.GetAccountByEmailAsync(User.Identity.Name);
            try
            {
                if(accountDto == null)
                {
                    throw new ObjectNotFoundException("Account");
                }

                var accountId = accountDto.Id;
                var response = await _userTaskService.GetCompletedTasksAsync(accountId);
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

        public async Task<IActionResult> GetUserTask(int id, bool isJson)
        {
            try
            {
                var userTaskDto = await _userTaskService.GetUserTask(id);
                if (userTaskDto == null)
                {
                    throw new ObjectNotFoundException("Task");
                }

                var response = new UserTaskViewModel()
                {
                    Id = userTaskDto.Id,
                    Name = userTaskDto.Name,
                    Summary = userTaskDto.Summary,
                    DateOfCreation = userTaskDto.DateOfCreation,
                    DateOfCompletion = userTaskDto.DateOfCompletion,
                    Priority = userTaskDto.Priority,
                    Subtasks = userTaskDto.Subtasks
                };

                if (isJson)
                {
                    return Json(response);
                }

                return View(response);
            }
            catch (ObjectNotFoundException objNotFound)
            {
                return RedirectToAction("Error");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error");
            }
        }

        [HttpGet]
        public async Task<IActionResult> AddUserTask()
        {
            return PartialView();
        }

        [HttpPost]
        public async Task<IActionResult> AddUserTask(UserTaskViewModel model)
        {
            var accountDto = await _accountService.GetAccountByEmailAsync(User.Identity.Name);
            try
            {
                if (accountDto == null)
                {
                    throw new ObjectNotFoundException("Task");
                }
                var accountId = accountDto.Id;

                var userTaskDto = new UserTaskDto()
                {
                    Name = model.Name,
                    Summary = model.Summary,
                    DateOfCompletion = model.DateOfCompletion,
                    DateOfCreation = DateTime.Now,
                    Priority = model.Priority,
                };
                await _userTaskService.AddUserTask(userTaskDto, accountId);

                return RedirectToAction("GetTodayUserTasks");
            }
            catch (ObjectNotFoundException objNotFound)
            {
                return RedirectToAction("Error");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error");
            }
        }

        [HttpGet]
        public async Task<IActionResult> UpdateUserTask(int id)
        {
            var userTask = await _userTaskService.GetUserTask(id);
            var model = new UserTaskViewModel()
            {
                Id = userTask.Id,
                Name = userTask.Name,
                Summary = userTask.Summary,
                DateOfCompletion = userTask.DateOfCompletion,
                DateOfCreation = userTask.DateOfCreation,
                Priority = userTask.Priority
            };

            return PartialView(model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUserTask(UserTaskViewModel model, int id)
        {
            var accountDto = await _accountService.GetAccountByEmailAsync(User.Identity.Name);
            try
            {
                if (accountDto == null)
                {
                    throw new ObjectNotFoundException("Account");
                }

                var userTask = await _userTaskService.GetUserTask(model.Id);
                var updatedUserTask = new UserTaskDto()
                {
                    Name = model.Name,
                    Summary = model.Summary,
                    DateOfCompletion = model.DateOfCompletion,
                    DateOfCreation = userTask.DateOfCreation,
                    Priority = model.Priority,
                    Id = id,
                };
                await _userTaskService.UpdateUserTask(updatedUserTask);
                return RedirectToAction("GetTodayUserTasks");
            }
            catch (ObjectNotFoundException objNotFound)
            {
                return RedirectToAction("Error");
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
                return RedirectToAction("GetTodayUserTasks");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error");
            }
        }
        
        public async Task<IActionResult> CompleteUserTask(int id)
        {
            var accountDto = await _accountService.GetAccountByEmailAsync(User.Identity.Name);
            try
            {
                if (accountDto == null)
                {
                    RedirectToAction("Error");
                }

                await _userTaskService.CompleteUserTask(id);
                return RedirectToAction("GetTodayUserTasks");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error");
            }
        }
    }
}
