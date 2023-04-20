namespace WebShop.Application.Features.ShoppingCartItems.Queries.GetShoppingCartItemsList
{
    public class ProductDTO
    {
        public Guid Id { get; set; }

        public string ProductName { get; set; } = string.Empty;

        public double UnitPrice { get; set; }
    }
}
