namespace WebShop.Domain.Entities
{
    public class Customer
    {
         public string Id { get; set; } = string.Empty;

         public Address Address { get; set; } = new Address();

         public string PhoneNumber { get; set; } = string.Empty;
    }
}
