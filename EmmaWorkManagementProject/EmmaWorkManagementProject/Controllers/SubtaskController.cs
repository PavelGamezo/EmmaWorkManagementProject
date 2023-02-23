using EmmaWorkManagement.BusinessLayer.Dtos;
using EmmaWorkManagement.BusinessLayer.Interfaces;
using EmmaWorkManagement.Data;
using EmmaWorkManagement.Entities.Entities;
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

        public async Task<IActionResult> GetAllSubtasks(int id)
        {
            var mappedSubtasks = (await _subtaskService.GetAllActiveSubtasks(id))
                .Select(q => new SubtaskViewModel
                {
                    Id = q.Id,
                    Name = q.Name,
                    Comment = q.Comment,
                    UserTaskId = q.UserTaskId
                })
                .ToArray();

            return View(mappedSubtasks);
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
            var activeUserTask = _applicationDbContext.UserTasks.FirstOrDefault(q=>q.Id == model.UserTaskId);
            var subtaskDto = new SubtaskDto()
            {
                Name = model.Name,
                Comment = model.Comment,
                UserTaskId = model.UserTaskId
            };

            activeUserTask.Subtasks.Add(new Subtask()
            {
                Name = subtaskDto.Name,
                Comment = subtaskDto.Comment,
                UserTaskId = subtaskDto.UserTaskId
            });
            _applicationDbContext.SaveChanges();

            return RedirectToAction("GetTodayUserTasks", "UserTask");
        }
    }
}
