namespace WebShop.Application.Models.SuppliersService
{
    public class ReserveOnSuppliersStockRequest
    {
        public ReserveOnSuppliersStockRequest(Guid productId, int quantity)
        {
            ProductId = productId;
            Quantity = quantity;
        }

        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
