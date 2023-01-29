using EmmaWorkManagement.BusinessLayer.Dtos;
using EmmaWorkManagement.BusinessLayer.Interfaces;
using EmmaWorkManagement.Data;
using EmmaWorkManagementProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using EmmaWorkManagement.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmmaWorkManagementProject.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly ApplicationDbContext _applicationDbContext;

        public AccountController(IAccountService accountService, ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            _accountService = accountService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Account account = await _applicationDbContext.Accounts.FirstOrDefaultAsync(q => q.Name == model.Name && q.Surname == model.Surname && q.Password == model.Password);

                    if (account != null)
                    {
                        await Authenticate(account.Email, $"{account.Name} {account.Surname}");

                        return RedirectToAction("GetTodayUserTasks", "UserTask");
                    }
                    ModelState.AddModelError("", "Incorrect username and/or password");
                }
                return View(model);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel account)
        {
            if (ModelState.IsValid)
            {
                var searchingAccount = _applicationDbContext.Accounts.FirstOrDefault(q => q.Name == account.Name && q.Surname == account.Surname);
                if (searchingAccount == null)
                {
                    var registeredAccount = new AccountDto()
                    {
                        Name = account.Name,
                        Surname = account.Surname,
                        Password = account.Password,
                        Email = account.Email
                    };

                    await _accountService.Register(registeredAccount);
                    await Authenticate(account.Email, $"{account.Name} {account.Surname}");

                    return RedirectToActionPermanent("GetTodayUserTasks", "UserTask");
                }
            }
            else
            {
                ModelState.AddModelError("", "Account is already excist");
            }

            return View(account);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }



        private async Task Authenticate(string userEmail, string userFullName)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, userEmail),
                new Claim(ClaimTypes.UserData, userFullName)
            };
            var id = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(id);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
    }
}
