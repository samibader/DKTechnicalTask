using DukkantekTask.Domain.UoW;
using DukkantekTask.Infrastructure.Context;
using System.Threading.Tasks;

namespace DukkantekTask.Infrastructure.UoW
{
    /// <summary>
    /// Unit of Work implementation
    /// </summary>
    public class EfCoreUnitOfWork : IUnitOfWork
    {
        private readonly EfCoreDbContext _dbContext;
        public EfCoreUnitOfWork(EfCoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
