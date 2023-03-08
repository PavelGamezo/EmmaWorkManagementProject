using AutoMapper;
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
        private readonly IMapper _mapper;

        public UserProfileController(IAccountService accountService, IUserProfileService userProfileService, IMapper mapper)
        {
            _accountService = accountService;
            _profileService = userProfileService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetUserProfile()
        {
            var accountDto = await _accountService.GetAccountByEmailAsync(User.Identity.Name);
            try
            {
                if (accountDto is null)
                {
                    throw new ObjectNotFoundException("Account");
                }
                var accountId = accountDto.Id;
                var activeProfile = await _profileService.GetUserProfile(accountId);

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
            catch (ObjectNotFoundException notFoundException)
            {
                return RedirectToAction("Error", notFoundException);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> GetUserProfile(UserProfileViewModel profile)
        {
            var accountDto = await _accountService.GetAccountByEmailAsync(User.Identity.Name);
            try
            {
                if (accountDto is null)
                {
                    throw new ObjectNotFoundException(profile.Name);
                }

                var activeAccountId = accountDto.Id;
                var activeProfile = await _profileService.GetUserProfile(activeAccountId);
                activeProfile.About = profile.About;

                await _profileService.UpdateUserProfile(activeProfile);

                return RedirectToAction("GetUserProfile");
            }
            catch (ObjectNotFoundException notFoundException)
            {
                return RedirectToAction("Error", notFoundException);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", ex);
            }
        }

        [HttpGet]
        public async Task<IActionResult> UpdateAccountName()
        {
            return View();
        }

        public async Task<IActionResult> UpdateAccountName(UserProfileViewModel model)
        {
            var accountDto = await _accountService.GetAccountByEmailAsync(User.Identity.Name);
            try
            {
                if (accountDto is null)
                {
                    throw new ObjectNotFoundException(model.Name);
                }

                var activeAccountId = accountDto.Id;
                var profileDto = await _profileService.GetUserProfile(activeAccountId);
                profileDto.Name = model.Name;
                profileDto.Surname = model.Surname;
                accountDto.Name = model.Name;
                accountDto.Surname = model.Surname;

                await _accountService.UpdateAccountName(accountDto);
                await _profileService.UpdateUserProfile(profileDto);

                return RedirectToAction("GetUserProfile");
            }
            catch (ObjectNotFoundException notFoundException)
            {
                return RedirectToAction("Error", notFoundException);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", ex);
            }
        }

        [HttpGet]
        public async Task<IActionResult> UpdateAccountEmail()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAccountEmail(UserProfileViewModel model)
        {
            var accountDto = await _accountService.GetAccountByEmailAsync(User.Identity.Name);
            try
            {
                if (accountDto is null)
                {
                    throw new ObjectNotFoundException(model.Name);
                }

                var activeAccountId = accountDto.Id;
                var profileDto = await _profileService.GetUserProfile(activeAccountId);
                profileDto.Email = model.Email;
                accountDto.Email = model.Email;

                await _accountService.UpdateAccountName(accountDto);
                await _profileService.UpdateUserProfile(profileDto);

                return RedirectToAction("GetUserProfile");
            }
            catch (ObjectNotFoundException notFoundException)
            {
                return RedirectToAction("Error", notFoundException);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", ex);
            }
        }

        [HttpGet]
        public async Task<IActionResult> UpdateProfileAvatar()
        {
            return PartialView("UpdateProfileAvatar");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProfileAvatar(UserProfileViewModel model)
        {
            var accountDto = await _accountService.GetAccountByEmailAsync(User.Identity.Name);
            var profileDto = await _profileService.GetUserProfile(accountDto.Id);

            if (model.Avatar is not null)
            {
                byte[] imageData = null;
                using (var binaryReader = new BinaryReader(model.Avatar.OpenReadStream()))
                {
                    imageData = binaryReader.ReadBytes((int)model.Avatar.Length);
                }
                profileDto.Avatar = imageData;
            }

            await _accountService.UpdateAccount(profileDto);

            return RedirectToAction("GetUserProfile", "UserProfile");
        }
    }
}