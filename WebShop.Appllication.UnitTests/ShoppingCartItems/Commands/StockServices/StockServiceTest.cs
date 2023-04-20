using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Application.Contracts.Infrastructure;
using WebShop.Application.Contracts.Persistence;
using WebShop.Application.Exceptions;
using WebShop.Application.Features.ShoppingCartItems.Commands.CreateShoppingCartItem.StockServices;
using WebShop.Application.Models.SuppliersService;
using WebShop.Appllication.UnitTests.Mocks;
using WebShop.Domain.Entities;

namespace WebShop.Appllication.UnitTests.ShoppingCartItems.Commands.StockServices
{
    public class StockServiceTest
    {
        private readonly Mock<ISupplierStockService> _supplierStockService;
        private readonly Mock<ILogger<StockService>> _logger;
        private readonly Mock<IProductRepository> _mockProductRepository;

        public StockServiceTest()
        {
            _mockProductRepository = RepositoryMocks.GetProductRepository();
            _supplierStockService = new Mock<ISupplierStockService>();
            _logger = new Mock<ILogger<StockService>>();
        }

        [Fact]
        async void ReserveProductQuantityAsync_Perform_ShouldUpdateProductLocalStock()
        {
            var shoppingCartItem = new ShoppingCartItem()
            {
                CustomerId = "Katarina",
                ProductId = Guid.Parse("{3fa85f64-5717-4562-b3fc-2c963f66afa6}"),
                Quantity = 1
            };


            var product = await _mockProductRepository.Object.GetByIdAsync(shoppingCartItem.ProductId);

            Assert.Equal(50, product.QuantityInStock);

            // external stock unabvailable
            _supplierStockService.Setup(s => s.ReserveOnSuppliersStockAsync(It.IsAny<ReserveOnSuppliersStockRequest>())).ReturnsAsync(false) ;

            var stockService = new StockService(_supplierStockService.Object, _logger.Object, _mockProductRepository.Object);

            await stockService.ReserveProductQuantityAsync(shoppingCartItem);

            product = await _mockProductRepository.Object.GetByIdAsync(shoppingCartItem.ProductId);

            Assert.Equal(49, product.QuantityInStock);


        }

        [Fact]
        async void ReserveProductQuantityAsync_Perform_ShouldEmptyProductLocalStock()
        {
            var shoppingCartItem = new ShoppingCartItem()
            {
                CustomerId = "Katarina",
                ProductId = Guid.Parse("{3fa85f64-5717-4562-b3fc-2c963f66afa6}"),
                Quantity = 100
            };

            var product = await _mockProductRepository.Object.GetByIdAsync(shoppingCartItem.ProductId);

            Assert.Equal(50, product.QuantityInStock);

            // external stock available
            _supplierStockService.Setup(s => s.ReserveOnSuppliersStockAsync(It.IsAny<ReserveOnSuppliersStockRequest>())).ReturnsAsync(true);

            var stockService = new StockService(_supplierStockService.Object, _logger.Object, _mockProductRepository.Object);

            await stockService.ReserveProductQuantityAsync(shoppingCartItem);

            Assert.Equal(0, product.QuantityInStock);


        }

        [Fact]
        async void ReserveProductQuantityAsync_Perform_ShouldThrowValidationExceptionExternalStorageReservationFailed()
        {
            var shoppingCartItem = new ShoppingCartItem()
            {
                CustomerId = "Katarina",
                ProductId = Guid.Parse("{3fa85f64-5717-4562-b3fc-2c963f66afa6}"),
                Quantity = 100
            };

            // external stock available
            _supplierStockService.Setup(s => s.ReserveOnSuppliersStockAsync(It.IsAny<ReserveOnSuppliersStockRequest>())).ReturnsAsync(false); ;

            var stockService = new StockService(_supplierStockService.Object, _logger.Object, _mockProductRepository.Object);

            await Assert.ThrowsAsync<ValidationException>(() =>stockService.ReserveProductQuantityAsync(shoppingCartItem));
        }

    }
    
}
