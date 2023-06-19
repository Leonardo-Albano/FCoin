using System.Linq.Expressions;

namespace FCoin.Repositories.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<List<TEntity>> GetAllAsync();
        IQueryable<TEntity> GetAllAsync(Expression<Func<TEntity, bool>> predicate);
        TEntity GetById(int id);
        Task<TEntity> GetByIdAsync(int id);
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Remove(TEntity entity);
        void RemoveRange(List<TEntity> entity);
    }
}
