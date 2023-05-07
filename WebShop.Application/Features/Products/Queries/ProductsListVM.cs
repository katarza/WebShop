namespace WebShop.Application.Features.Products.Queries
{
    public class ProductsListVM
    {
        public Guid Id { get; set; }

        public string ProductName { get; set; } = string.Empty;

        public int QuantityInStock { get; set; }

        public decimal UnitPrice { get; set; }
    }
}
