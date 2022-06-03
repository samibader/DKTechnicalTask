using AutoMapper;
using DukkantekTask.Domain.Enums;
using DukkantekTask.Domain.Repositories;
using DukkantekTask.Domain.UoW;
using DukkantekTask.Service.Implementation.Base;
using DukkantekTask.Service.Interfaces;
using DukkantekTask.Service.Models.Dtos;
using DukkantekTask.Service.Models.Requests;
using DukkantekTask.Service.Models.Responses;
using Serilog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DukkantekTask.Service.Implementation
{
    /// <summary>
    /// Product Service Implementation
    /// </summary>
    public class ProductService : AppService, IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IProductRepository productRepository
            ) : base(unitOfWork,mapper)
        {
            _productRepository = productRepository;
        }

        public async Task<ChangeProductStatusResponse> ChangeProductStatusAsync(ChangeProductStatusRequest request)
        {
            Log.Information($"ChangeProductStatusAsync method was called in ProductService. Attempting to change product status for id: {request.Id}");

            try
            {
                ChangeProductStatusResponse response = new ChangeProductStatusResponse();
                var product = await _productRepository.FirstOrDefaultAsync(request.Id);
                if (product == null)
                {
                    // product not found
                    return new ChangeProductStatusResponse
                    {
                        IsSuccessful = false,
                        Message = $"product with id:{request.Id} was not found!"
                    };
                }

                ObjectMapper.Map(request, product);
                await _productRepository.UpdateAsync(product);

                var affectedRowsCount = await UnitOfWork.SaveChangesAsync();
                var isUpdated = affectedRowsCount > 0;

                return new ChangeProductStatusResponse
                {
                    IsSuccessful = true,
                    Message = isUpdated
                        ? "Product status was successfully changes"
                        : "Product status has failed to being changes"
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"There was an error when trying to change product status with id {request.Id}. Error: ", ex.ToString());

                return new ChangeProductStatusResponse
                {
                    IsSuccessful = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<GetProductsCountByStatusResponse> GetProductsCountByStatusAsync()
        {
            Log.Information($"GetProductsCountByStatusAsync method was called in ProductService.");
            try
            {
                var result = await _productRepository.GetProductsCountByStatusAsync();
                return new GetProductsCountByStatusResponse
                {
                    IsSuccessful = true,
                    Message = "Get products count by status was successfully executed",
                    Value = new ProductsCountSummaryDto
                    {
                        TotalSoldProductsCount = result.GetValueOrDefault(ProductStatusEnum.Sold, 0),
                        TotalInStockProductsCount = result.GetValueOrDefault(ProductStatusEnum.InStock, 0),
                        TotalDamagedProductsCount = result.GetValueOrDefault(ProductStatusEnum.Damaged, 0)
                    }
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"There was an error when trying to get products count by status. Error: ", ex.ToString());

                return new GetProductsCountByStatusResponse
                {
                    IsSuccessful = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<SellProductResponse> SellProductAsync(SellProductRequest request)
        {
            Log.Information($"SellProductAsync method was called in ProductService. Attempting to sell product id: {request.Id}");

            try
            {
                SellProductResponse response = new SellProductResponse();
                var product = await _productRepository.FirstOrDefaultAsync(request.Id);
                if (product == null)
                {
                    // product not found
                    return new SellProductResponse
                    {
                        IsSuccessful = false,
                        Message = $"product with id:{request.Id} was not found!"
                    };
                }

                if(product.Status != ProductStatusEnum.InStock)
                {
                    // we cannot sell this product, it's either "Damaged" or "Sold"
                    return new SellProductResponse
                    {
                        IsSuccessful = false,
                        Message = $"product with id:{request.Id} cannot be sold, status is {product.Status}"
                    };
                }

                // to sell the product, change it's status from "InStock to "Sold"
                product.Status = ProductStatusEnum.Sold;
                await _productRepository.UpdateAsync(product);

                var affectedRowsCount = await UnitOfWork.SaveChangesAsync();
                var isUpdated = affectedRowsCount > 0;

                return new SellProductResponse
                {
                    IsSuccessful = true,
                    Message = isUpdated
                        ? "Product was successfully sold"
                        : "Product has failed to being sold"
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"There was an error when trying to sell product with id {request.Id}. Error: ", ex.ToString());

                return new SellProductResponse
                {
                    IsSuccessful = false,
                    Message = ex.Message
                };
            }
        }
    }
}
