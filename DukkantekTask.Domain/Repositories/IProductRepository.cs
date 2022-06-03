using DukkantekTask.Domain.Entities;
using DukkantekTask.Domain.Enums;
using DukkantekTask.Domain.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DukkantekTask.Domain.Repositories
{
    /// <summary>
    /// Interface for Product Repository
    /// </summary>
    public interface IProductRepository : IRepository<Product,Guid>
    {
        /// <summary>
        /// Custom repository method to count products by status
        /// </summary>
        /// <returns>dictionary with product status as key, and total count as value</returns>
        Task<Dictionary<ProductStatusEnum, int>> GetProductsCountByStatusAsync();
    }
}
