using AutoFixture;
using AutoMapper;
using DukkantekTask.Domain.Entities;
using DukkantekTask.Domain.Enums;
using DukkantekTask.Domain.Repositories;
using DukkantekTask.Domain.UoW;
using DukkantekTask.Service.Implementation;
using DukkantekTask.Service.Interfaces;
using DukkantekTask.Service.Models.Requests;
using DukkantekTask.Service.Models.Responses;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DukkantekTask.Api.Tests.Services
{
    [TestFixture]
    public class ProductServiceTests
    {
        private IFixture _fixture;
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private Mock<IMapper> _mapperMock;
        private Mock<IProductRepository> _productRepositoryMock;

        private IProductService _sut;
        private Guid _productId = Guid.NewGuid();

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();

            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _unitOfWorkMock
                .Setup(x => x.SaveChangesAsync())
                .ReturnsAsync(1);

            _mapperMock = new Mock<IMapper>();

            _productRepositoryMock = new Mock<IProductRepository>();
            _productRepositoryMock
                .Setup(x => x.GetProductsCountByStatusAsync())
                .ReturnsAsync(new Dictionary<Domain.Enums.ProductStatusEnum, int>()
                {
                    { Domain.Enums.ProductStatusEnum.Sold, 10 },
                    { Domain.Enums.ProductStatusEnum.Damaged, 20 },
                    { Domain.Enums.ProductStatusEnum.InStock, 30 },
                });

            _sut = new ProductService(_unitOfWorkMock.Object, _mapperMock.Object, _productRepositoryMock.Object);
        }

        private Product GenerateProduct(Guid productId)
        {
            var product = _fixture
                .Build<Product>()
                .With(c => c.Id, productId)
                .With(c => c.Description, string.Empty)
                .With(c => c.Category, new Category { Id = 1 })
                .With(c => c.CategoryId, 1)
                .Create();

            return product;
        }

        [Test]
        public async Task ProductService_GetProductsCountByStatusAsync()
        {
            // arrange
            var expected = new GetProductsCountByStatusResponse
            {
                IsSuccessful = true,
                Message = "Get products count by status was successfully executed",
                Value = new Service.Models.Dtos.ProductsCountSummaryDto
                {
                    TotalSoldProductsCount = 10,
                    TotalDamagedProductsCount = 20,
                    TotalInStockProductsCount = 30
                }
            };

            // act
            var actual = await _sut.GetProductsCountByStatusAsync();

            // assert
            _productRepositoryMock.Verify(x => x.GetProductsCountByStatusAsync(),
                Times.Once());

            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public async Task ProductService_ChangeProductStatusAsync()
        {
            // arrange
            var request = new ChangeProductStatusRequest
            {
                Id = _productId,
                Status = Domain.Enums.ProductStatusEnum.Sold
            };

            _productRepositoryMock
                .Setup(x => x.FirstOrDefaultAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Guid productId) => GenerateProduct(productId));

            _productRepositoryMock
                .Setup(x => x.UpdateAsync(It.IsAny<Product>()))
                .ReturnsAsync((Product product) => product);

            // act
            var actual = await _sut.ChangeProductStatusAsync(request);

            // assert
            _productRepositoryMock.Verify(x => x.FirstOrDefaultAsync(It.IsAny<Guid>()),
                Times.Once());

            _productRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Product>()),
                Times.Once());

            Assert.IsTrue(actual.IsSuccessful);
        }

        [Test]
        public async Task ProductService_SellProductAsync_Successful_When_StatusInStock()
        {
            // arrange
            var product = GenerateProduct(_productId);
            product.Status = ProductStatusEnum.InStock;

            var request = new SellProductRequest
            {
                Id = _productId
            };

            _productRepositoryMock
                .Setup(x => x.FirstOrDefaultAsync(_productId))
                .ReturnsAsync((Guid productId) => product);

            _productRepositoryMock
                .Setup(x => x.UpdateAsync(It.IsAny<Product>()))
                .ReturnsAsync((Product product) => product);

            // act
            var actual = await _sut.SellProductAsync(request);

            // assert
            _productRepositoryMock.Verify(x => x.FirstOrDefaultAsync(_productId),
                Times.Once());

            _productRepositoryMock.Verify(x => x.UpdateAsync(product),
                Times.Once());

            Assert.IsTrue(product.Status== ProductStatusEnum.Sold);

            Assert.IsTrue(actual.IsSuccessful);
        }

        [Test]
        public async Task ProductService_SellProductAsync_NotSuccessful_When_StatusSold()
        {
            // arrange
            var product = GenerateProduct(_productId);
            product.Status = ProductStatusEnum.Sold;

            var request = new SellProductRequest
            {
                Id = _productId
            };

            _productRepositoryMock
                .Setup(x => x.FirstOrDefaultAsync(_productId))
                .ReturnsAsync((Guid productId) => product);

            _productRepositoryMock
                .Setup(x => x.UpdateAsync(It.IsAny<Product>()))
                .ReturnsAsync((Product product) => product);

            // act
            var actual = await _sut.SellProductAsync(request);

            // assert
            _productRepositoryMock.Verify(x => x.FirstOrDefaultAsync(_productId),
                Times.Once());

            _productRepositoryMock.Verify(x => x.UpdateAsync(product),
                Times.Never());

            Assert.IsFalse(actual.IsSuccessful);
            actual.Message.Should().BeEquivalentTo($"product with id:{product.Id} cannot be sold, status is Sold");
        }

        [Test]
        public async Task ProductService_SellProductAsync_NotSuccessful_When_StatusDamaged()
        {
            // arrange
            var product = GenerateProduct(_productId);
            product.Status = ProductStatusEnum.Damaged;

            var request = new SellProductRequest
            {
                Id = _productId
            };

            _productRepositoryMock
                .Setup(x => x.FirstOrDefaultAsync(_productId))
                .ReturnsAsync((Guid productId) => product);

            _productRepositoryMock
                .Setup(x => x.UpdateAsync(It.IsAny<Product>()))
                .ReturnsAsync((Product product) => product);

            // act
            var actual = await _sut.SellProductAsync(request);

            // assert
            _productRepositoryMock.Verify(x => x.FirstOrDefaultAsync(_productId),
                Times.Once());

            _productRepositoryMock.Verify(x => x.UpdateAsync(product),
                Times.Never());

            Assert.IsFalse(actual.IsSuccessful);
            actual.Message.Should().BeEquivalentTo($"product with id:{product.Id} cannot be sold, status is Damaged");
        }
    }
}
