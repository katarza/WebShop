using WebShop.Domain.Entities;

namespace WebShop.Application.Contracts.Persistence
{
    public interface IOrderRepository : IAsyncRepository<Order>
    {
    }
}
