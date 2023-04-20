using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Application.Models.SuppliersService;

namespace WebShop.Application.Contracts.Infrastructure
{
    public interface ISupplierStockService
    {
        Task<bool> ReserveOnSuppliersStockAsync(ReserveOnSuppliersStockRequest request);
    }
}
