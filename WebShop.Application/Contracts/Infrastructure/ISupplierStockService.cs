using WebShop.Application.Models.SuppliersService;

namespace WebShop.Application.Contracts.Infrastructure
{
    public interface ISupplierStockService
    {
        Task<bool> ReserveOnSuppliersStockAsync(ReserveOnSuppliersStockRequest request);
    }
}
