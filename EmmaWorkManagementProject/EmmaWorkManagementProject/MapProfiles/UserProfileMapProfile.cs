using AutoMapper;
using EmmaWorkManagement.BusinessLayer.Dtos;
using EmmaWorkManagementProject.Models;

namespace EmmaWorkManagementProject.MapProfiles
{
    public class UserProfileMapProfile : Profile
    {
        public UserProfileMapProfile()
        {
            CreateMap<UserProfileViewModel, UserProfileDto>();
            CreateMap<UserProfileDto, UserProfileViewModel>();
        }
    }
}
