using AutoMapper;
using EmmaWorkManagement.BusinessLayer.Dtos;
using EmmaWorkManagement.BusinessLayer.Interfaces;
using EmmaWorkManagement.Data.Interaces;
using EmmaWorkManagement.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmmaWorkManagement.BusinessLayer.Services.UserProfiles
{
    public class UserProfileService : IUserProfileService
    {
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly IMapper _mapper;

        public UserProfileService(IUserProfileRepository userProfileRepository, IMapper mapper)
        {
            _userProfileRepository = userProfileRepository;
            _mapper = mapper;  
        }

        public async Task<UserProfileDto> GetUserProfile(int accountId)
        {
            var activeProfile = await _userProfileRepository.GetAll()
                                                            .FirstOrDefaultAsync(q => q.AccountId == accountId);
            var mappedActiveProfile = _mapper.Map<UserProfileDto>(activeProfile);
            return mappedActiveProfile;
        }

        public async Task UpdateUserProfile(UserProfileDto model)
        {
            var activeProfile = await _userProfileRepository.GetById(model.Id);

            activeProfile.Name = model.Name;
            activeProfile.Surname = model.Surname;
            activeProfile.About = model.About;
            activeProfile.Registered = model.Registered;
            activeProfile.Avatar = model.Avatar;

            await _userProfileRepository.Save();
        }
    }
}
