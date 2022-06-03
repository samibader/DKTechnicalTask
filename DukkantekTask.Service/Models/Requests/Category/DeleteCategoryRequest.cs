using System.ComponentModel.DataAnnotations;

namespace DukkantekTask.Service.Models.Requests
{
    public class DeleteCategoryRequest
    {
        [Required]
        public int Id { get; set; }
    }
}
