using DukkantekTask.Service.Models.Requests;
using DukkantekTask.Service.Models.Responses;
using System.Threading.Tasks;

namespace DukkantekTask.Service.Interfaces
{
    /// <summary>
    /// Category Service Interface
    /// </summary>
    public interface ICategoryService
    {
        Task<CreateCategoryResponse> CreateCategoryAsync(CreateCategoryRequest request);
        Task<UpdateCategoryResponse> UpdateCategoryAsync(UpdateCategoryRequest request);
        Task<DeleteCategoryResponse> DeleteCategoryAsync(DeleteCategoryRequest request);
    }
}
