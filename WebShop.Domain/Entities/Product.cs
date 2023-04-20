using WebShop.Domain.Common;

namespace WebShop.Domain.Entities
{
    public class Product : AuditableEntity
    {
        public Guid Id { get; set; } 

        public string ProductName { get; set; } = string.Empty;

        public int QuantityInStock { get; set; }

        public double UnitPrice { get; set; }
    }
}
