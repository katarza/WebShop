using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
