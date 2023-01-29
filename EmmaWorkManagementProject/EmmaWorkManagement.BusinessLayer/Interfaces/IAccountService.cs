using EmmaWorkManagement.BusinessLayer.Dtos;
using EmmaWorkManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmmaWorkManagement.BusinessLayer.Interfaces
{
    public interface IAccountService
    {
        Task Register(AccountDto account);

        Task DeleteAccount(int id);

        Task<AccountDto> GetAccount(int id);
    }
}
