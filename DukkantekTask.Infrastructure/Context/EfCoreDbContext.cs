using Microsoft.EntityFrameworkCore;

namespace DukkantekTask.Infrastructure.Context
{
    /// <summary>
    /// Database Context
    /// </summary>
    public class EfCoreDbContext : DbContext
    {
        public EfCoreDbContext(DbContextOptions<EfCoreDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EfCoreDbContext).Assembly);
        }
    }
}
