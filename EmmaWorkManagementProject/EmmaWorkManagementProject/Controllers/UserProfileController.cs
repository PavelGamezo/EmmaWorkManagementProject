using EmmaWorkManagement.BusinessLayer.Dtos;
using EmmaWorkManagement.BusinessLayer.Interfaces;
using EmmaWorkManagement.BusinessLayer.Services.UserTasks;
using EmmaWorkManagement.Data;
using EmmaWorkManagement.Entities;
using EmmaWorkManagement.Exceptions;
using EmmaWorkManagementProject.Models;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace EmmaWorkManagementProject.Controllers
{
    public class UserProfileController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly IUserProfileService _profileService;
        private readonly ApplicationDbContext _context;

        public UserProfileController(IAccountService accountService, IUserProfileService userProfileService, ApplicationDbContext context)
        {
            _accountService = accountService;
            _profileService = userProfileService;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetUserProfile()
        {
            var activeAccount = await _accountService.GetAccountByEmail(User.Identity.Name);
            try
            {
                if (activeAccount is null)
                {
                    RedirectToAction("Error");
                }
                var activeAccountId = activeAccount.Id;
                var activeProfile = await _profileService.GetUserProfile(activeAccountId);
                return View(new UserProfileViewModel()
                {
                    Id = activeProfile.Id,
                    Name = activeProfile.Name,
                    Surname = activeProfile.Surname,
                    About = activeProfile.About,
                    Email = activeProfile.Email,
                    Registered = activeProfile.Registered,
                    Image = activeProfile.Avatar
                });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> GetUserProfile(UserProfileViewModel profile)
        {
            var activeAccount = await _accountService.GetAccountByEmail(User.Identity.Name);
            try
            {
                if (activeAccount is null)
                {
                    RedirectToAction("Error");
                }
                var activeAccountId = activeAccount.Id;
                var activeProfile = await _profileService.GetUserProfile(activeAccountId);

                activeAccount.UserProfile = new EmmaWorkManagement.Entities.Entities.UserProfile()
                {
                    Id = activeProfile.Id,
                    About = profile.About,
                    Name = activeProfile.Name,
                    Surname = activeProfile.Surname,
                    Registered = activeProfile.Registered,
                    Email = activeProfile.Email,
                    Account = activeAccount,
                    AccountId = activeAccount.Id
                };

                _context.SaveChanges();

                return View(new UserProfileViewModel()
                {
                    Id = activeProfile.Id,
                    About = profile.About,
                    Name = profile.Name,
                    Surname = profile.Surname,
                    Registered = activeProfile.Registered,
                    Email = activeProfile.Email,
                });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> UpdateAccountName()
        {
            return View();
        }

        public async Task<IActionResult> UpdateAccountName(UserProfileViewModel profile)
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

                activeAccount.Name = profile.Name;
                activeAccount.Surname = profile.Surname;
                activeAccount.UserProfile = new EmmaWorkManagement.Entities.Entities.UserProfile()
                {
                    Id = activeProfile.Id,
                    About = activeProfile.About,
                    Name = profile.Name,
                    Surname = profile.Surname,
                    Registered = activeProfile.Registered,
                    Email = activeProfile.Email,
                    Account = activeAccount,
                    AccountId = activeAccount.Id,
                    Avatar = activeProfile.Avatar
                };

                _context.SaveChanges();

                return RedirectToAction("GetUserProfile");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> UpdateAccountEmail()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAccountEmail(UserProfileViewModel profile)
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

                activeAccount.Email = profile.Email;
                activeAccount.UserProfile = new EmmaWorkManagement.Entities.Entities.UserProfile()
                {
                    Id = activeProfile.Id,
                    About = activeProfile.About,
                    Name = activeProfile.Name,
                    Surname = activeProfile.Surname,
                    Registered = activeProfile.Registered,
                    Email = profile.Email,
                    Account = activeAccount,
                    AccountId = activeAccount.Id
                };

                _context.SaveChanges();

                return RedirectToAction("GetUserProfile");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpGet]
        public async Task UpdatePassword(AccountUpdateViewModel model)
        {
            var activeAccount = await _accountService.GetAccountByEmail(User.Identity.Name);
            try
            {
                if (activeAccount is null)
                {
                    RedirectToAction("Error");
                }
                activeAccount.Password = model.Password;
                _context.SaveChanges();

                var activeProfile = await _profileService.GetUserProfile(activeAccount.Id);
                RedirectToAction("GetUserProfile");
            }
            catch
            {
                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> UpdateProfileAvatar()
        {
            return View("UpdateProfileAvatar");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProfileAvatar(UserProfileViewModel profile)
        {
            var activeAccount = await _accountService.GetAccountByEmail(User.Identity.Name);
            var activeProfile = await _profileService.GetUserProfile(activeAccount.Id);

            if (profile.Avatar is not null)
            {
                byte[] imageData = null;
                using (var binaryReader = new BinaryReader(profile.Avatar.OpenReadStream()))
                {
                    imageData = binaryReader.ReadBytes((int)profile.Avatar.Length);
                }
                activeProfile.Avatar = imageData;
            }

            await _accountService.UpdateAccount(activeProfile);

            return RedirectToAction("GetUserProfile", "UserProfile");
        }
    }
}