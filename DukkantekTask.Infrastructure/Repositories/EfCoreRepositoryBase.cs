using DukkantekTask.Domain.Entities.Base;
using DukkantekTask.Domain.Repositories.Base;
using DukkantekTask.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DukkantekTask.Infrastructure.Repositories
{
    /// <summary>
    /// Generic Repository Implementation
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    /// <typeparam name="TKey">Primary key</typeparam>
    public class EfCoreRepositoryBase<TEntity,TKey> : IRepository<TEntity, TKey> where TEntity : class, IEntity<TKey>
    {
        protected readonly EfCoreDbContext _dbContext;
        protected virtual DbSet<TEntity> Table => _dbContext.Set<TEntity>();
        public EfCoreRepositoryBase(EfCoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<TEntity> InsertAsync(TEntity entity)
        {
            await Table.AddAsync(entity);
            return entity;
        }

        public Task<TEntity> UpdateAsync(TEntity entity)
        {
            AttachIfNot(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
            return Task.FromResult(entity);
        }

        public Task DeleteAsync(TEntity entity)
        {
            AttachIfNot(entity);
            return Task.FromResult(Table.Remove(entity));
        }

        public Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Table.CountAsync(predicate);
        }

        public IQueryable<TEntity> GetAll()
        {
            return Table.AsQueryable();
        }

        public async Task<IQueryable<TEntity>> GetAllAsync()
        {
            return await GetAllIncludingAsync();
        }

        public async Task<TEntity> FirstOrDefaultAsync(TKey id)
        {
            return await (await GetAllAsync()).FirstOrDefaultAsync(
                CreateEqualityExpressionForId(id)
            );
        }

        public Task<IQueryable<TEntity>> GetAllIncludingAsync(
            params Expression<Func<TEntity, object>>[] propertySelectors)
        {
            IQueryable<TEntity> query = Table.AsQueryable();

            if (propertySelectors != null && propertySelectors.Count()>0)
            {
                foreach (var propertySelector in propertySelectors)
                {
                    query = query.Include(propertySelector);
                }
            }

            return Task.FromResult(query);
        }

        protected virtual void AttachIfNot(TEntity entity)
        {
            var entry = _dbContext.ChangeTracker.Entries().FirstOrDefault(ent => ent.Entity == entity);
            if (entry != null)
            {
                return;
            }

            Table.Attach(entity);
        }

        protected virtual Expression<Func<TEntity, bool>> CreateEqualityExpressionForId(TKey id)
        {
            var lambdaParam = Expression.Parameter(typeof(TEntity));

            var leftExpression = Expression.PropertyOrField(lambdaParam, "Id");

            var idValue = Convert.ChangeType(id, typeof(TKey));

            Expression<Func<object>> closure = () => idValue;
            var rightExpression = Expression.Convert(closure.Body, leftExpression.Type);

            var lambdaBody = Expression.Equal(leftExpression, rightExpression);

            return Expression.Lambda<Func<TEntity, bool>>(lambdaBody, lambdaParam);
        }
    }

    public class EfCoreRepositoryBase<TEntity> : EfCoreRepositoryBase<TEntity, int>, IRepository<TEntity>
        where TEntity : class, IEntity<int>
    {
        public EfCoreRepositoryBase(EfCoreDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
