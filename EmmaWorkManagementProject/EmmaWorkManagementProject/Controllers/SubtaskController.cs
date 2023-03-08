using EmmaWorkManagement.BusinessLayer.Dtos;
using EmmaWorkManagement.BusinessLayer.Interfaces;
using EmmaWorkManagement.BusinessLayer.Services.Accounts;
using EmmaWorkManagement.Data;
using EmmaWorkManagement.Entities.Entities;
using EmmaWorkManagement.Exceptions;
using EmmaWorkManagementProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmmaWorkManagementProject.Controllers
{
    public class SubtaskController : Controller
    {
        private readonly ISubtaskService _subtaskService;
        private readonly IUserTaskService _userTaskService;
        private readonly ApplicationDbContext _applicationDbContext;

        public SubtaskController(ISubtaskService subtaskService, IUserTaskService userTaskService, ApplicationDbContext applicationDbContext)
        {
            _subtaskService = subtaskService;
            _userTaskService = userTaskService;
            _applicationDbContext = applicationDbContext;
        }

        public async Task DeleteSubtask(int id)
        {
            await _subtaskService.DeleteSubtask(id);
            RedirectToAction("GetTodayUserTasks", "UserTask");
        }

        [HttpGet]
        public async Task<IActionResult> CreateSubtask(int id, bool isJson)
        {
            try
            {
                var userTask = await _userTaskService.GetUserTask(id);
                var model = new SubtaskViewModel()
                {
                    UserTaskId = userTask.Id
                };

                return PartialView(model);
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateSubtask(SubtaskViewModel model)
        {
            var userTaskDto = await _userTaskService.GetUserTask(model.UserTaskId);
            try
            {
                if(userTaskDto is null)
                {
                    throw new ObjectNotFoundException("UserTask");
                }
                var userTaskId = userTaskDto.Id;
                var subtaskDto = new SubtaskDto()
                {
                    Name = model.Name,
                    Comment = model.Comment,
                    UserTaskId = model.UserTaskId
                };

                await _subtaskService.CreateSubtask(subtaskDto, userTaskId);

                return RedirectToAction("GetTodayUserTasks", "UserTask");
            }
            catch (ObjectNotFoundException ex)
            {
                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> UpdateSubtask(int id)
        {
            var subtaskDto = await _subtaskService.GetSubtask(id);
            var model = new SubtaskViewModel()
            {
                Name = subtaskDto.Name,
                Comment = subtaskDto.Comment,
                Id = id,
                UserTaskId = subtaskDto.UserTaskId
            };

            return PartialView(model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateSubtask(SubtaskViewModel model)
        {
            var subtaskDto = new SubtaskDto()
            {
                Id = model.Id,
                UserTaskId = model.UserTaskId,
                Name = model.Name,
                Comment = model.Comment
            };

            await _subtaskService.UpdateSubtask(subtaskDto);

            return RedirectToAction("GetTodayUserTasks", "UserTask");
        }

        public async Task CompleteSubtask(int id)
        {
            try
            {
                await _subtaskService.CompleteSubtask(id);
            }
            catch (Exception ex)
            {
                RedirectToAction("Error");
            }
        }
    }
}
