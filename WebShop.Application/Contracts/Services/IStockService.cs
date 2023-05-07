using WebShop.Domain.Entities;

namespace WebShop.Application.Contracts.Services
{
    public interface IStockService
    {
        Task ReserveProductQuantityAsync(ShoppingCartItem shoppingCartItem);
    }
}
