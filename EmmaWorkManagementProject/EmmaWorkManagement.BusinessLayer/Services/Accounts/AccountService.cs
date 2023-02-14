using AutoMapper;
using EmmaWorkManagement.BusinessLayer.Dtos;
using EmmaWorkManagement.BusinessLayer.Interfaces;
using EmmaWorkManagement.Data.Interaces;
using EmmaWorkManagement.Data.Interfaces;
using EmmaWorkManagement.Data.Repositories;
using EmmaWorkManagement.Entities;
using EmmaWorkManagement.Entities.Entities;
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
        private readonly IUserProfileRepository _userProfileRepository;

        public AccountService(IMapper mapper, IAccountRepository accountRepository, IUserProfileRepository userProfileRepository)
        {
            _mapper = mapper;
            _accountRepository = accountRepository;
            _userProfileRepository = userProfileRepository;
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
            var newProfile = new UserProfile()
            {
                Account = mappingAccount,
                Name = mappingAccount.Name,
                About = "Default",
                Surname = mappingAccount.Surname,
                Registered = DateTime.Now,
                Email = mappingAccount.Email,
            };
            await _userProfileRepository.Create(newProfile);
            await _accountRepository.Create(mappingAccount);
            await _accountRepository.Save();
        }

        public async Task<AccountDto> GetAccount(int id)
        {
            var account = await _accountRepository.GetById(id);
            var mappedAccount = _mapper.Map<AccountDto>(account);
            return mappedAccount;
        }

        public async Task<Account> GetAccountByEmail(string email)
        {
            var account = _accountRepository.GetAll().FirstOrDefault(q => q.Email == email);
            return account;
        }

        public async Task UpdateAccountByProfile(UserProfileDto userProfile)
        {
            var account = _accountRepository.GetAll().FirstOrDefault(q => q.Id == userProfile.Id);
            account.Name = userProfile.Name;
            account.Surname = userProfile.Surname;
            await _accountRepository.Save();
        }

        public async Task UpdateAccount(UserProfileDto activeProfile)
        {
            var activeAccount = _accountRepository.GetAll().FirstOrDefault(q => q.Id == activeProfile.Id);
            activeAccount.UserProfile = new UserProfile()
            {
                Id = activeProfile.Id,
                About = activeProfile.About,
                Name = activeProfile.Name,
                Surname = activeProfile.Surname,
                Registered = activeProfile.Registered,
                Email = activeProfile.Email,
                Account = activeAccount,
                AccountId = activeAccount.Id,
                Avatar = activeProfile.Avatar
            };

            await _accountRepository.Save();
        }
        #endregion
    }
}
