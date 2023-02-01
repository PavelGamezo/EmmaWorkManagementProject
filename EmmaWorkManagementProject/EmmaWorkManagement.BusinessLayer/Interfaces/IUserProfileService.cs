using EmmaWorkManagement.BusinessLayer.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmmaWorkManagement.BusinessLayer.Interfaces
{
    public interface IUserProfileService
    {
        Task<UserProfileDto> GetUserProfile(int id);
        Task UpdateUserProfile(UserProfileDto activeProfile);
    }
}
