using System.ComponentModel.DataAnnotations;

namespace DukkantekTask.Service.Models.Requests
{
    public class CreateCategoryRequest
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
    }
}
