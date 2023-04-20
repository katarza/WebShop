using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Domain.Entities;

namespace WebShop.Application.Contracts.Services
{
    public interface IStockService
    {
        Task ReserveProductQuantityAsync(ShoppingCartItem shoppingCartItem);
    }
}
