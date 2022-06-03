using System.ComponentModel.DataAnnotations;

namespace DukkantekTask.Domain.Enums
{
    /// <summary>
    /// Enumeration for Product Status
    /// </summary>
    public enum ProductStatusEnum
    {
        [Display(Name = "Sold")]
        Sold = 1,

        [Display(Name = "In Stock")]
        InStock = 2,

        [Display(Name = "Damaged")]
        Damaged = 3
    }
}
