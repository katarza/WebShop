using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Application.Contracts.Infrastructure
{
    public interface ISupplierStockService
    {
        Task<bool> IsQuantityAvailableOnSuppliersStock(int quantity, Guid productId);
    }
}
