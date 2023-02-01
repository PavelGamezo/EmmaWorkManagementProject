using EmmaWorkManagement.BusinessLayer.Dtos;
using EmmaWorkManagement.BusinessLayer.Interfaces;
using EmmaWorkManagement.Entities;
using EmmaWorkManagement.Exceptions;
using EmmaWorkManagementProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmmaWorkManagementProject.Controllers
{
    public class UserProfileController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly IUserProfileService _profileService;

        public UserProfileController(IAccountService accountService, IUserProfileService userProfileService)
        {
            _accountService = accountService;
            _profileService = userProfileService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUserProfile()
        {
            var activeAccount = _accountService.GetAccountByEmail(User.Identity.Name).Result;
            try
            {
                if (activeAccount is null)
                {
                    RedirectToAction("Error");
                }
                var activeAccountId = activeAccount.Id;
                var activeProfile = _profileService.GetUserProfile(activeAccountId).Result;
                return View(new UserProfileViewModel()
                {
                    Id = activeProfile.Id,
                    Name = activeProfile.Name,
                    Surname = activeProfile.Surname,
                    About = activeProfile.About,
                    Email = activeProfile.Email,
                    Registered = activeProfile.Registered,
                });
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> GetUserProfile(UserProfileViewModel profile)
        {
            var activeAccount = _accountService.GetAccountByEmail(User.Identity.Name).Result;
            try
            {
                if (activeAccount is null)
                {
                    RedirectToAction("Error");
                }
                var activeAccountId = activeAccount.Id;
                var activeProfile = _profileService.GetUserProfile(activeAccountId).Result;
                var changedProfile = new UserProfileDto()
                {
                    Id = activeProfile.Id,
                    Name = profile.Name,
                    Surname = profile.Surname,
                    About = profile.About,
                    Email = activeProfile.Email,
                    Registered = activeProfile.Registered
                };

                _profileService.UpdateUserProfile(changedProfile);

                activeAccount.Name = changedProfile.Name;
                activeAccount.Surname = changedProfile.Surname;
                await _accountService.UpdateAccountByProfile(changedProfile);

                return View(new UserProfileViewModel()
                {
                    Id = changedProfile.Id,
                    Name = changedProfile.Name,
                    Surname = changedProfile.Surname,
                    About = changedProfile.About,
                    Email = changedProfile.Email,
                    Registered=changedProfile.Registered
                });
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
