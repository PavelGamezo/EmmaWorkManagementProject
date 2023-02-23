using EmmaWorkManagement.Data.Interaces;
using EmmaWorkManagement.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmmaWorkManagement.Data.Repositories
{
    public class SubtaskRepository : ISubtaskRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public SubtaskRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task Create(Subtask entity)
        {
            await _applicationDbContext.Subtasks.AddAsync(entity);
        }

        public async Task Delete(Subtask entity)
        {
            _applicationDbContext.Subtasks.Remove(entity);
        }

        public IQueryable<Subtask> GetAll()
        {
            return _applicationDbContext.Subtasks.AsQueryable();
        }

        public async Task<Subtask> GetById(int id)
        {
            return await _applicationDbContext.Subtasks.FindAsync(id);
        }

        public Task Save()
        {
            return _applicationDbContext.SaveChangesAsync();
        }
    }
}
