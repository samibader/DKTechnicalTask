using DukkantekTask.Domain.Entities.Base;
using System.Collections.Generic;

namespace DukkantekTask.Domain.Entities
{
    /// <summary>
    /// Domain model for "Category", with default "Id" type as "int"
    /// </summary>
    public class Category : Entity
    {
        /// <summary>
        /// Name of the category
        /// </summary>
        public string Name { get; set; }


        /// <summary>
        /// Navigational property for category products
        /// </summary>
        public ICollection<Product> Products { get; set; }
    }
}
