using EmmaWorkManagementProject.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using EmmaWorkManagementProject.Database.Entities;

namespace EmmaWorkManagementProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            string dateInput = "Jan 20, 2023";
            var parsedDate = DateTime.Parse(dateInput);
            var tasks = new List<UserTask>()
            {
                new UserTask
                {
                    Summary = "Добавить сущности в проект",
                    DateOfCompletion = parsedDate,
                    DateOfCreation = DateTime.Now,
                    Priority = "Highest"
                },
                new UserTask
                {
                    Summary = "Добавить DAL слой в проект",
                    DateOfCompletion = parsedDate,
                    DateOfCreation = DateTime.Now,
                    Priority = "Highest"
                },
                new UserTask
                {
                    Summary = "Изменить вид проекта",
                    DateOfCompletion = parsedDate,
                    DateOfCreation = DateTime.Now,
                    Priority = "Highest"
                },

            };
            return View(tasks);
        }
    }
}