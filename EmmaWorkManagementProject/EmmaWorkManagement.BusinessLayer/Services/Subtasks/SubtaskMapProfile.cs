using AutoMapper;
using EmmaWorkManagement.BusinessLayer.Dtos;
using EmmaWorkManagement.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmmaWorkManagement.BusinessLayer.Services.Subtasks
{
    public class SubtaskMapProfile : Profile
    {
        public SubtaskMapProfile()
        {
            CreateMap<SubtaskDto, Subtask>();
            CreateMap<Subtask, SubtaskDto>();
        }
    }
}
