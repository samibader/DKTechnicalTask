using DukkantekTask.Api.Controllers;
using DukkantekTask.Service.Interfaces;
using DukkantekTask.Service.Models.Requests;
using DukkantekTask.Service.Models.Responses;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace DukkantekTask.Api.Tests.Controllers
{
    [TestFixture]
    public class ProductControllerTests
    {
        private Mock<IProductService> _productServiceMock;
        private ProductsController _sut;

        [SetUp]
        public void Setup()
        {
            _productServiceMock = new Mock<IProductService>();
            _productServiceMock
                .Setup(x => x.ChangeProductStatusAsync(It.IsAny<ChangeProductStatusRequest>()))
                .ReturnsAsync(new ChangeProductStatusResponse());

            _productServiceMock
                .Setup(x => x.GetProductsCountByStatusAsync())
                .ReturnsAsync(new GetProductsCountByStatusResponse());

            _productServiceMock
                .Setup(x => x.SellProductAsync(It.IsAny<SellProductRequest>()))
                .ReturnsAsync(new SellProductResponse());

            _sut = new ProductsController(_productServiceMock.Object);
        }

        [Test]
        public async Task ProductsController_GetProductsCountByStatusAsync()
        {
            // act
            await _sut.GetProductsCountByStatusAsync();

            // assert
            _productServiceMock.Verify(x => x.GetProductsCountByStatusAsync(),
                Times.Once());
        }

        [Test]
        public async Task ProductsController_ChangeProductStatusAsync()
        {
            // act
            await _sut.ChangeProductStatusAsync(It.IsAny<ChangeProductStatusRequest>());

            // assert
            _productServiceMock.Verify(x => x.ChangeProductStatusAsync(
                    It.IsAny<ChangeProductStatusRequest>()),
                Times.Once());
        }

        [Test]
        public async Task ProductsController_SellProductAsync()
        {
            // act
            await _sut.SellProductAsync(It.IsAny<SellProductRequest>());

            // assert
            _productServiceMock.Verify(x => x.SellProductAsync(
                    It.IsAny<SellProductRequest>()),
                Times.Once());
        }
    }
}
