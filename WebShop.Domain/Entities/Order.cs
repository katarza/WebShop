using WebShop.Domain.Common;

namespace WebShop.Domain.Entities
{
    public class Order : AuditableEntity
    {
        public Guid Id { get; set; }

        public string CustomerId { get; set; } = string.Empty;

        public Address CustomerAddress { get; set; } = new Address();

        public string CustomerPhone { get; set; } = string.Empty;

        public double TotalAmount { get; set; }

        public double AppliedDiscount { get; set; }

    }
}
