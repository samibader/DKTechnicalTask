using DukkantekTask.Domain.Entities;
using DukkantekTask.Domain.Enums;
using DukkantekTask.Domain.Repositories;
using DukkantekTask.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DukkantekTask.Infrastructure.Repositories
{
    /// <summary>
    /// Product repository implementation
    /// </summary>
    public class ProductRepository : EfCoreRepositoryBase<Product,Guid>, IProductRepository
    {
        public ProductRepository(EfCoreDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<Dictionary<ProductStatusEnum, int>> GetProductsCountByStatusAsync()
        {
            return await GetAll()
                .GroupBy(p => p.Status)
                .Select(_ => new { ProductStatus = _.Key, TotalCount = _.Count() })
                .ToDictionaryAsync(c => c.ProductStatus, c => c.TotalCount);
        }
    }
}
