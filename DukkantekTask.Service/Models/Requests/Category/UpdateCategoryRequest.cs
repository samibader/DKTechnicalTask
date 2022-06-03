using System.ComponentModel.DataAnnotations;

namespace DukkantekTask.Service.Models.Requests
{
    public class UpdateCategoryRequest
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }
    }
}
