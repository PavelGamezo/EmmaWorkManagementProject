using Microsoft.AspNetCore.Mvc;
using EmmaWorkManagement.Data.Interfaces;
using EmmaWorkManagementProject.Models;
using EmmaWorkManagement.BusinessLayer.Interfaces;

namespace EmmaWorkManagementProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserTaskService _userTaskService;

        public HomeController(ILogger<HomeController> logger, IUserTaskService userTaskService)
        {
            _logger = logger;
            _userTaskService = userTaskService;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}