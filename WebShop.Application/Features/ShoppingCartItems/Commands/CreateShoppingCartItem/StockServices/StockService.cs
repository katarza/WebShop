using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Application.Contracts.Services;
using WebShop.Application.Contracts.Infrastructure;
using WebShop.Application.Contracts.Persistence;
using WebShop.Application.Models.SuppliersService;
using WebShop.Domain.Entities;
using ValidationException = WebShop.Application.Exceptions.ValidationException;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2, PublicKey=0024000004800000940000000602000000240000525341310004000001000100c547cac37abd99c8db225ef2f6c8a3602f3b3606cc9891605d02baa56104f4cfc0734aa39b93bf7852f7d9266654753cc297e7d2edfe0bac1cdcf9f717241550e0a7b191195b7667bb4f64bcb8e2121380fd1d9d46ad2d92d2d15605093924cceaf74c4861eff62abf69b9291ed0a340e113be11e6a7d3113e92484cf7045cc7")]
namespace WebShop.Application.Features.ShoppingCartItems.Commands.CreateShoppingCartItem.StockServices
{
 
    internal class StockService : IStockService
    {
        private readonly ISupplierStockService _supplierStockService;
        private readonly ILogger<StockService> _logger;
        private readonly IProductRepository _productRepository;

        public StockService(ISupplierStockService supplierStockService, ILogger<StockService> logger, IProductRepository productRepository)
        {
            _supplierStockService = supplierStockService;
            _logger = logger;
            _productRepository = productRepository;
        }

        public async Task ReserveProductQuantityAsync(ShoppingCartItem shoppingCartItem)
        {
            int reservedOnLocalStock = await reserveProductQuamtityOnLocalStock(shoppingCartItem);

            if (reservedOnLocalStock < shoppingCartItem.Quantity)
            {
                await reserveProductQuantityOnExternalStock(shoppingCartItem, reservedOnLocalStock);
            }
        }

        private async Task reserveProductQuantityOnExternalStock(ShoppingCartItem shoppingCartItem, int reservedOnLocalStock)
        {
            int quantityToReserveExternaly = shoppingCartItem.Quantity - reservedOnLocalStock;
            ReserveOnSuppliersStockRequest reserveOnSuppliersStockRequest = new ReserveOnSuppliersStockRequest(shoppingCartItem.ProductId, quantityToReserveExternaly);
            bool reservationSucceeded = false;
            try
            {
                reservationSucceeded = await _supplierStockService.ReserveOnSuppliersStockAsync(reserveOnSuppliersStockRequest);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Reservation of quantity ({reserveOnSuppliersStockRequest.Quantity}) of  product ID {reserveOnSuppliersStockRequest.ProductId} on external suppliers stock failed due to an error with the mail service: {ex.Message}");
            }

            if (!reservationSucceeded)
            {
                var failure = new Dictionary<string, string[]>()
                    {
                        { "Quantity", new string[1] {"Not enough items available on stock"} }
                    };
                throw new ValidationException(failure);
            }
        }

        private async Task<int> reserveProductQuamtityOnLocalStock(ShoppingCartItem shoppingCartItem)
        {
            return await _productRepository.ReserveProductQuantityAsync(shoppingCartItem.ProductId, shoppingCartItem.Quantity);
        }
    }
}
