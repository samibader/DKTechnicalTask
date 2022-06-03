using DukkantekTask.Api.Filters;
using DukkantekTask.Service.Interfaces;
using DukkantekTask.Service.Models.Requests;
using DukkantekTask.Service.Models.Responses;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Net;
using System.Threading.Tasks;

namespace DukkantekTask.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductsController(
            IProductService productService
            )
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
        }

        [HttpGet("GetProductsCountByStatusAsync")]
        public async Task<ActionResult<GetProductsCountByStatusResponse>> GetProductsCountByStatusAsync()
        {
            Log.Information("GetProductsCountByStatusAsync enpoint was called in ProductsController.");
            return await _productService.GetProductsCountByStatusAsync();
        }

        [HttpPost("ChangeProductStatusAsync")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<ActionResult<ChangeProductStatusResponse>> ChangeProductStatusAsync([FromForm] ChangeProductStatusRequest request)
        {
            Log.Information("ChangeProductStatusAsync enpoint was called in ProductsController.");
            return await _productService.ChangeProductStatusAsync(request);
        }

        [HttpPost("SellProductAsync")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<ActionResult<SellProductResponse>> SellProductAsync([FromForm] SellProductRequest request)
        {
            Log.Information("SellProductAsync enpoint was called in ProductsController.");
            return await _productService.SellProductAsync(request);
        }
    }
}
