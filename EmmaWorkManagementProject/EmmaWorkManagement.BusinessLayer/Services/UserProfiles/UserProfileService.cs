using AutoMapper;
using EmmaWorkManagement.BusinessLayer.Dtos;
using EmmaWorkManagement.BusinessLayer.Interfaces;
using EmmaWorkManagement.Data.Interaces;
using EmmaWorkManagement.Entities.Entities;
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

        public async Task<UserProfileDto> GetUserProfile(int id)
        {
            var activeProfile = _userProfileRepository.GetById(id).Result;
            var mappedActiveProfile = _mapper.Map<UserProfileDto>(activeProfile);
            return mappedActiveProfile;
        }

        public async Task UpdateUserProfile(UserProfileDto activeProfile)
        {
            var mappedActiveProfile = _mapper.Map<UserProfile>(activeProfile);
            await _userProfileRepository.Save();
        }
    }
}
