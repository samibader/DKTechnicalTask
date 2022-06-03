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
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoriesController(
            ICategoryService categoryService
            )
        {
            _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
        }

        [HttpPost("CreateCategoryAsync")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<ActionResult<CreateCategoryResponse>> CreateCategoryAsync([FromBody] CreateCategoryRequest request)
        {
            Log.Information("CreateCategoryAsync enpoint was called in CategoriesController.");
            return await _categoryService.CreateCategoryAsync(request);
        }

        [HttpPost("UpdateCategoryAsync")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<ActionResult<UpdateCategoryResponse>> UpdateCategoryAsync([FromBody] UpdateCategoryRequest request)
        {
            Log.Information("UpdateCategoryAsync enpoint was called in CategoriesController.");
            return await _categoryService.UpdateCategoryAsync(request);
        }

        [HttpPost("DeleteCategoryAsync")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<ActionResult<DeleteCategoryResponse>> DeleteCategoryAsync([FromBody] DeleteCategoryRequest request)
        {
            Log.Information("DeleteCategoryAsync enpoint was called in CategoriesController.");
            return await _categoryService.DeleteCategoryAsync(request);
        }
    }
}
