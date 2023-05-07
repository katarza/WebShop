namespace WebShop.Application.Features.Orders.Queries
{
    public class OrdersListVM
    {
        public string CustomerId { get; set; } = string.Empty;

        public ShippingAddressListDTO ShippingAddress { get; set; } = new ShippingAddressListDTO();

        public string CustomerPhone { get; set; } = string.Empty;

        public double TotalAmount { get; set; }

        public DateTime CreatedDate { get; set; }

        public double AppliedDiscount { get; set; }
    }
}
