using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using WebShop.Application.Contracts.Persistence;
using WebShop.Domain.Entities;

namespace WebShop.Persistence.Repository
{
    public class ShoppingCartItemsRepository : BaseRepository<ShoppingCartItem>, IShoppingCartItemRepository
    {
        public ShoppingCartItemsRepository(WebShopDbContext dbContext) : base(dbContext)
        { 
        }


        public Task<List<ShoppingCartItem>> ListCustomerCartContent(string customerId)
        {
           return _dbContext.ShoppingCartItems
                            .Where(s => s.CustomerId == customerId).ToListAsync();
        }

        override public async Task<ShoppingCartItem> AddAsync(ShoppingCartItem entity)
        {                    
            await _dbContext.Set<ShoppingCartItem>().AddAsync(entity);
            return entity;            
        }

        public void EmptyShoppingCartAsync(string customerId)
        {
           var shoppingCartItems = _dbContext.ShoppingCartItems
                            .Where(s => s.CustomerId == customerId).ToListAsync();

            _dbContext.ShoppingCartItems.RemoveRange(shoppingCartItems.Result);
        }
    }
}
