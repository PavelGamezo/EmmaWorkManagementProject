using EmmaWorkManagement.BusinessLayer.Dtos;
using EmmaWorkManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace EmmaWorkManagement.BusinessLayer.Services.UserTasks
{
    public class UserTaskMapProfile : Profile
    {
        public UserTaskMapProfile()
        {
            CreateMap<UserTaskDto, UserTask>();
            CreateMap<UserTask, UserTaskDto>();
        }
    }
}
