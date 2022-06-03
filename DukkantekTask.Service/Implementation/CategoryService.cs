using AutoMapper;
using DukkantekTask.Domain.Repositories;
using DukkantekTask.Domain.UoW;
using DukkantekTask.Service.Implementation.Base;
using DukkantekTask.Service.Interfaces;
using DukkantekTask.Service.Models.Requests;
using DukkantekTask.Service.Models.Responses;
using Serilog;
using System;
using System.Threading.Tasks;

namespace DukkantekTask.Service.Implementation
{
    /// <summary>
    /// Category Service Implementation
    /// </summary>
    public class CategoryService : AppService, ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ICategoryRepository categoryRepository
            ) : base(unitOfWork,mapper)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<CreateCategoryResponse> CreateCategoryAsync(CreateCategoryRequest request)
        {
            Log.Information($"CreateCategoryAsync method was called in CategoryService. Attempting to create category with name: {request.Name}");

            try
            {
                var result = await _categoryRepository.InsertAsync(new Domain.Entities.Category { Name = request.Name });

                var affectedRowsCount = await UnitOfWork.SaveChangesAsync();
                var isCreated = affectedRowsCount > 0;

                return new CreateCategoryResponse
                {
                    IsSuccessful = isCreated,
                    Message = isCreated ?
                        "Category created successfully" :
                        "Category has failed to being created"
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"There was an error when trying to create category with name {request.Name}. Error: ", ex.ToString());

                return new CreateCategoryResponse
                {
                    IsSuccessful = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<UpdateCategoryResponse> UpdateCategoryAsync(UpdateCategoryRequest request)
        {
            Log.Information($"UpdateCategoryAsync method was called in CategoryService. Attempting to update category with id: {request.Id}");

            try
            {
                UpdateCategoryResponse response = new UpdateCategoryResponse();
                var category = await _categoryRepository.FirstOrDefaultAsync(request.Id);
                if (category == null)
                {
                    // category not found
                    return new UpdateCategoryResponse
                    {
                        IsSuccessful = false,
                        Message = $"category with id:{request.Id} was not found!"
                    };
                }

                ObjectMapper.Map(request, category);
                await _categoryRepository.UpdateAsync(category);

                var affectedRowsCount = await UnitOfWork.SaveChangesAsync();
                var isUpdated = affectedRowsCount > 0;

                return new UpdateCategoryResponse
                {
                    IsSuccessful = true,
                    Message = isUpdated
                        ? "Category was successfully updated"
                        : "Category has failed to being updated"
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"There was an error when trying to update category with id {request.Id}. Error: ", ex.ToString());

                return new UpdateCategoryResponse
                {
                    IsSuccessful = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<DeleteCategoryResponse> DeleteCategoryAsync(DeleteCategoryRequest request)
        {
            Log.Information($"UpdateCategoryAsync method was called in CategoryService. Attempting to update category with id: {request.Id}");

            try
            {
                DeleteCategoryResponse response = new DeleteCategoryResponse();
                var category = await _categoryRepository.FirstOrDefaultAsync(request.Id);
                if (category == null)
                {
                    // category not found
                    return new DeleteCategoryResponse
                    {
                        IsSuccessful = false,
                        Message = $"category with id:{request.Id} was not found!"
                    };
                }

                await _categoryRepository.DeleteAsync(category);

                var affectedRowsCount = await UnitOfWork.SaveChangesAsync();
                var isDeleted = affectedRowsCount > 0;

                return new DeleteCategoryResponse
                {
                    IsSuccessful = true,
                    Message = isDeleted
                        ? "Category was successfully deleted"
                        : "Category has failed to being deleted"
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"There was an error when trying to delete category with id {request.Id}. Error: ", ex.ToString());

                return new DeleteCategoryResponse
                {
                    IsSuccessful = false,
                    Message = ex.Message
                };
            }
        }

        
    }
}
