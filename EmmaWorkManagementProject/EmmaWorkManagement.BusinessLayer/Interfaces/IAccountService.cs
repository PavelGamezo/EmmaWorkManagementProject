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

        Task<Account> GetAccountByEmail(string email);

        Task<AccountDto> GetAccountByEmailAsync(string email);

        Task UpdateAccountPassword(AccountDto accountDto);

        Task UpdateAccountName(AccountDto accountDto);

        Task UpdateAccountEmail(AccountDto accountDto);

        Task UpdateAccount(UserProfileDto activeProfile);
    }
}
