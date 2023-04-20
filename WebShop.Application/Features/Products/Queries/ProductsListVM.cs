using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
