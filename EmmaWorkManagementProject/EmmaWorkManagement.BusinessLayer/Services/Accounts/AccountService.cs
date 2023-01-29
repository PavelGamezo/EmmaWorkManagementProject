using AutoMapper;
using EmmaWorkManagement.BusinessLayer.Dtos;
using EmmaWorkManagement.BusinessLayer.Interfaces;
using EmmaWorkManagement.Data.Interaces;
using EmmaWorkManagement.Data.Interfaces;
using EmmaWorkManagement.Entities;
using EmmaWorkManagementProject.Database.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmmaWorkManagement.BusinessLayer.Services.Accounts
{
    public class AccountService : IAccountService
    {
        private readonly IMapper _mapper;
        private readonly IAccountRepository _accountRepository;

        public AccountService(IMapper mapper, IAccountRepository accountRepository)
        {
            _mapper = mapper;
            _accountRepository = accountRepository;
        }

        #region IAccountService Members

        public async Task DeleteAccount(int id)
        {
            var account = await _accountRepository.GetById(id);
            await _accountRepository.Delete(account);
            await _accountRepository.Save();
        }

        public async Task Register(AccountDto account)
        {
            var mappingAccount = _mapper.Map<Account>(account);
            await _accountRepository.Create(mappingAccount);
            await _accountRepository.Save();
        }

        public async Task<AccountDto> GetAccount(int id)
        {
            var account = await _accountRepository.GetById(id);
            var mappedAccount = _mapper.Map<AccountDto>(account);
            return mappedAccount;
        }

        public async Task<Account> GetActiveAccount(string email)
        {
            var account = _accountRepository.GetAll().FirstOrDefault(q => q.Email == email);
            return account;
        }
        #endregion
    }
}
