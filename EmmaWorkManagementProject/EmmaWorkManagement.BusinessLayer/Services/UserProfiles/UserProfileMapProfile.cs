using EmmaWorkManagement.BusinessLayer.Dtos;
using EmmaWorkManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EmmaWorkManagement.Entities.Entities;

namespace EmmaWorkManagement.BusinessLayer.Services.UserTasks
{
    public class UserProfileMapProfile : Profile
    {
        public UserProfileMapProfile()
        {
            CreateMap<UserProfileDto, UserProfile>();
            CreateMap<UserProfile, UserProfileDto>();
        }
    }
}
