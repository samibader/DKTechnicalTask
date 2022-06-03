using DukkantekTask.Domain.Entities.Base;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DukkantekTask.Domain.Repositories.Base
{
    /// <summary>
    /// Generic Repository Interface
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    /// <typeparam name="TKey">Primary key</typeparam>
    public interface IRepository<TEntity, TKey>  where TEntity : class, IEntity<TKey>
    {
        Task<TEntity> InsertAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate);
        Task<IQueryable<TEntity>> GetAllAsync();
        IQueryable<TEntity> GetAll();
        Task<TEntity> FirstOrDefaultAsync(TKey id);
        Task<IQueryable<TEntity>> GetAllIncludingAsync(
            params Expression<Func<TEntity, object>>[] propertySelectors);
    }

    /// <summary>
    /// Shortcut to create generic repository with "int" as primary key
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    public interface IRepository<TEntity> : IRepository<TEntity, int> where TEntity : class, IEntity<int>
    {

    }
}
