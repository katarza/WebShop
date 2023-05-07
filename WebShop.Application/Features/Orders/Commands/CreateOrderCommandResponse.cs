namespace WebShop.Application.Features.Orders.Commands
{
    public class CreateOrderCommandResponse
    {
        public OrderDTO Order { get; set; } = new OrderDTO();
    }
}
