using MediatR;

namespace WebShop.Application.Features.Orders.Queries
{
    public class GetOrdersListQuery : IRequest<List<OrdersListVM>>
    {
    }
}
