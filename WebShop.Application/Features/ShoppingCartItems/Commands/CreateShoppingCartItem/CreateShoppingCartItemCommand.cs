using MediatR;

namespace WebShop.Application.Features.ShoppingCartItems.Commands.CreateShoppingCartItem
{
    public class CreateShoppingCartItemCommand : IRequest
    {
        public string CustomerId { get; set; } = String.Empty;

        public Guid ProductId { get; set;}

        public int Quantity { get; set;}
    }
}
