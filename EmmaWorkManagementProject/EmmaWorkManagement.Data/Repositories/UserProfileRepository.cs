using EmmaWorkManagement.Data.Interaces;
using EmmaWorkManagement.Entities;
using EmmaWorkManagement.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmmaWorkManagement.Data.Repositories
{
    public class UserProfileRepository : IUserProfileRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public UserProfileRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task Create(UserProfile entity)
        {
            await _applicationDbContext.UserProfiles.AddAsync(entity);
        }

        public async Task Delete(UserProfile entity)
        {
            _applicationDbContext.UserProfiles.Remove(entity);
        }

        public IQueryable<UserProfile> GetAll()
        {
            return _applicationDbContext.UserProfiles.Include(q=>q.Account).AsQueryable();
        }

        public async Task<UserProfile> GetById(int id)
        {
            return await _applicationDbContext.UserProfiles.Include(q => q.Account).FirstOrDefaultAsync(q => q.Id == id);
        }

        public Task Save()
        {
            return _applicationDbContext.SaveChangesAsync();
        }
    }
}
