using EmmaWorkManagement.Data;
using EmmaWorkManagement.Data.Interfaces;
using EmmaWorkManagement.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmmaWorkManagementProject.Database.Repositories
{
    public class UserTaskRepository : IUserTaskRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public UserTaskRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task Create(UserTask entity)
        {
            await _applicationDbContext.UserTasks.AddAsync(entity);
        }

        public async Task Delete(UserTask entity)
        {
            _applicationDbContext.Remove(entity);
        }

        public async Task<UserTask> GetById(int id)
        {
            return await _applicationDbContext.UserTasks.Include(q => q.Subtasks)
                                                        .Include(q => q.Account)
                                                        .FirstOrDefaultAsync(q => q.Id == id);
        }

        public Task Save()
        {
            return _applicationDbContext.SaveChangesAsync();
        }

        public IQueryable<UserTask> GetAll()
        {
            return _applicationDbContext.UserTasks.Include(q => q.Account)
                                                  .Include(q => q.Subtasks)
                                                  .AsQueryable();
        }
        
        /*public IQueryable<UserTask> GetAllByInclude()
        {
            return _applicationDbContext.UserTasks.Include(q => q.Account).AsQueryable();
        }*/
    }
}
