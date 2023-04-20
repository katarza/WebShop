using WebShop.Domain.Entities;

namespace WebShop.Application.Features.Orders.Commands.Factory
{
    public interface IOrderFactory
    {
        Task<Order> CreateOrderAsync(CreateOrderCommand createOrderCommand);
    }
}