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
using EmmaWorkManagement.Exceptions;

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
            return View(new LoginViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var accountDto = await _accountService.GetAccountByEmailAsync(model.Email);

                    if (accountDto != null)
                    {
                        if(accountDto.Password != model.Password)
                        {
                            ModelState.AddModelError("", "Incorrect username and/or password");
                            return View(model);
                        }

                        await Authenticate(accountDto.Email, $"{accountDto.Name} {accountDto.Surname}");

                        return RedirectToAction("GetTodayUserTasks", "UserTask");
                    }
                }
                ModelState.AddModelError("", "Incorrect username and/or password");

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
            return View(new RegisterViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel account)
        {
            if (ModelState.IsValid)
            {
                var searchingAccountDto = await _accountService.GetAccountByEmailAsync(account.Email);
                if (searchingAccountDto == null)
                {
                    await _accountService.Register(new AccountDto()
                    {
                        Name = account.Name,
                        Surname = account.Surname,
                        Password = account.Password,
                        Email = account.Email
                    });
                    await Authenticate(account.Email, $"{account.Name} {account.Surname}");

                    return RedirectToActionPermanent("GetTodayUserTasks", "UserTask");
                }
                ModelState.AddModelError("Email", "Account has already excist");

                return View(account);
            }
            else
            {
                ModelState.AddModelError("", "Incorrect input");
            }

            return View(account);
        }

        [HttpGet]
        public async Task<IActionResult> UpdatePassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePassword(AccountUpdateViewModel model)
        {
            var activeAccountDto = await _accountService.GetAccountByEmailAsync(User.Identity.Name);
            try
            {
                if (activeAccountDto is null)
                {
                    throw new ObjectNotFoundException(model.Email);
                }
                activeAccountDto.Password = model.Password;
                await _accountService.UpdateAccountPassword(activeAccountDto);

                return RedirectToAction("GetUserProfile", "UserProfile");
            }
            catch
            {
                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> PasswordRecovery()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PasswordRecovery(AccountUpdateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var accountDto = await _accountService.GetAccountByEmailAsync(model.Email);
                if (accountDto is null)
                {
                    ModelState.AddModelError("Email", "Incorrect email input");
                    return View(model);
                }
                accountDto.Password = model.Password;

                await _accountService.UpdateAccountPassword(accountDto);

                return RedirectToAction("Login");
            }
            else
            {
                return View(model);
            }
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

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Delete()
        {
            var activeAccount = await _accountService.GetAccountByEmail(User.Identity.Name);
            await _accountService.DeleteAccount(activeAccount.Id);
            return RedirectToAction("Index", "Home");
        }
    }
}
