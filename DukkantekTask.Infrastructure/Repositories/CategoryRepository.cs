using DukkantekTask.Domain.Entities;
using DukkantekTask.Domain.Repositories;
using DukkantekTask.Infrastructure.Context;

namespace DukkantekTask.Infrastructure.Repositories
{
    /// <summary>
    /// Category repository implementation
    /// </summary>
    public class CategoryRepository : EfCoreRepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(EfCoreDbContext dbContext) : base(dbContext)
        {

        }
    }
}
