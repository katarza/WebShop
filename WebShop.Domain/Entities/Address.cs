namespace WebShop.Domain.Entities
{
    public class Address
    {
        public Guid Id { get; set; }

        public string City { get; set; } = string.Empty;

        public string Street { get; set; } = string.Empty;

        public string HouseNumber { get; set; } = string.Empty;

    }
}