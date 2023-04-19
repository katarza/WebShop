using MediatR;

namespace WebShop.Application.Features.ShoppingCartItems.Queries.GetShoppingCartItemsList
{
    public class GetShoppingCartItemsListQuery : IRequest<List<ShoppingCardItemsListVM>>
    {
        public string CustomerId { get; set; } = String.Empty;
    }
}
