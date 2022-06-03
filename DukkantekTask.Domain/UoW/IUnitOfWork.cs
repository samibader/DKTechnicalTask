using System.Threading.Tasks;

namespace DukkantekTask.Domain.UoW
{
    /// <summary>
    /// Interface for the unit of work
    /// </summary>
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync();
    }
}
