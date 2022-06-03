using DukkantekTask.Domain.Entities.Base;
using DukkantekTask.Domain.Enums;
using System;

namespace DukkantekTask.Domain.Entities
{
    /// <summary>
    /// Domain model for "Product", with custom "Guid" type as "long"
    /// </summary>
    public class Product : Entity<Guid>
    {
        /// <summary>
        /// Name of the product
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Barcode of the product
        /// </summary>
        public string BarCode { get; set; }

        /// <summary>
        /// Description of the product
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Weight of the product
        /// </summary>
        public decimal Weight { get; set; }

        /// <summary>
        /// Status of the product
        /// </summary>
        public ProductStatusEnum Status { get; set; }

        /// <summary>
        /// Category Id of the product
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// Reference for the Category object related to the product
        /// </summary>
        public virtual Category Category { get; set; }
    }
}
