using EmmaWorkManagement.Entities;

namespace EmmaWorkManagement.Data.Interfaces
{
    public interface IBaseRepository<TEntity>
        where TEntity : class
    {
        Task Create(TEntity entity);

        Task<TEntity> GetById(int id);

        IQueryable<TEntity> GetAll();

        Task Delete(TEntity entity);

        Task Save();
    }
}
