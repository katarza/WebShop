using WebShop.Domain.Entities;

namespace WebShop.Application.Contracts.Persistence
{
    public interface IShoppingCartItemRepository : IAsyncRepository<ShoppingCartItem>
    {
        Task<List<ShoppingCartItem>> ListCustomerCartContent(string customerId);
        void EmptyShoppingCartAsync(string customerId);
    }
}
