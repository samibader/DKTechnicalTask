using DukkantekTask.Service.Models.Requests;
using DukkantekTask.Service.Models.Responses;
using System.Threading.Tasks;

namespace DukkantekTask.Service.Interfaces
{
    /// <summary>
    /// Product Service Interface
    /// </summary>
    public interface IProductService
    {
        Task<GetProductsCountByStatusResponse> GetProductsCountByStatusAsync();
        Task<ChangeProductStatusResponse> ChangeProductStatusAsync(ChangeProductStatusRequest request);
        Task<SellProductResponse> SellProductAsync(SellProductRequest request);
    }
}
