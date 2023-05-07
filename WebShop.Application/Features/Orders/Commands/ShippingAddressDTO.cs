namespace WebShop.Application.Features.Orders.Commands
{
    public class ShippingAddressDTO
    {
        public string City { get; set; } = string.Empty;

        public string Street { get; set; } = string.Empty;

        public string HouseNumber { get; set; } = string.Empty;
    }
}
