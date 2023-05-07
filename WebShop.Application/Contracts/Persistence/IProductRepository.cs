using WebShop.Domain.Entities;

namespace WebShop.Application.Contracts.Persistence
{
    public interface IProductRepository : IAsyncRepository<Product>
    {
        Task<bool> DoesProductIdExist(Guid productId);

        Task<int> ReserveProductQuantityAsync(Guid ProductId, int Quantity);

    }
}
