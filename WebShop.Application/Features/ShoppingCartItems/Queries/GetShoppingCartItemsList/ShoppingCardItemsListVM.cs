namespace WebShop.Application.Features.ShoppingCartItems.Queries.GetShoppingCartItemsList
{
    public class ShoppingCardItemsListVM
    {
        public string CustomerId { get; set; } = string.Empty;

        public ProductDTO Product { get; set; } = default!;

        public int Quantity { get; set; }

    }
}