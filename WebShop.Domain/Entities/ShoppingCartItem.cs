using WebShop.Domain.Common;

namespace WebShop.Domain.Entities
{
    public class ShoppingCartItem : AuditableEntity
    {
        public Guid Id { get; set; }
        public string CustomerId { get; set; } = String.Empty;

        public Guid ProductId { get; set; }

        public int Quantity { get; set; }

    }

}
