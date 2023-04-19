using WebShop.Application.Contracts.Infrastructure;

namespace WebShop.Infrastructure.SupplierStock
{
    internal class SupplierStockService : ISupplierStockService
    {
        public Task<bool> IsQuantityAvailableOnSuppliersStock(int quantity, Guid productId)
        {
            throw new NotImplementedException();
        }
    }
}
