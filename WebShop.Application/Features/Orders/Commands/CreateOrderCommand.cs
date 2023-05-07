using MediatR;

namespace WebShop.Application.Features.Orders.Commands
{
    public class CreateOrderCommand : IRequest<CreateOrderCommandResponse>
    {
        public string CustomerId { get; set; } = string.Empty;
        public string CustomerPhone { get; set; } = string.Empty;

        public ShippingAddressDTO ShippingAddress { get; set; } = new ShippingAddressDTO();
    }
}
