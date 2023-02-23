using EmmaWorkManagement.Data.Interaces;
using EmmaWorkManagement.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmmaWorkManagement.Data.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public AccountRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task Create(Account entity)
        {
            await _applicationDbContext.Accounts.AddAsync(entity);
        }

        public async Task Delete(Account entity)
        {
            _applicationDbContext.Accounts.Remove(entity);
        }

        public async Task<Account> GetById(int id)
        {
            return await _applicationDbContext.Accounts.Include(q => q.UserTasks)
                                                       .Include(q => q.UserProfile)
                                                       .FirstOrDefaultAsync(q => q.Id == id);
        }

        public Task Save()
        {
            return _applicationDbContext.SaveChangesAsync();
        }

        public IQueryable<Account> GetAll()
        {
            return _applicationDbContext.Accounts.Include(q=>q.UserTasks).Include(q=>q.UserProfile).AsQueryable();
        }
    }
}
