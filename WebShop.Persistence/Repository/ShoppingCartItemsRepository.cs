using Microsoft.EntityFrameworkCore;
using WebShop.Application.Contracts.Persistence;
using WebShop.Domain.Entities;

namespace WebShop.Persistence.Repository
{
    public class ShoppingCartItemsRepository : BaseRepository<ShoppingCartItem>, IShoppingCartItemRepository
    {
        public ShoppingCartItemsRepository(WebShopDbContext dbContext) : base(dbContext)
        {
            if (dbContext.ShoppingCartItems.Any() == false)
            {
                var product1Guid = Guid.Parse("{3fa85f64-5717-4562-b3fc-2c963f66afa6}");
                var product2Guid = Guid.Parse("{B0788D2F-8003-43C1-92A4-EDC76A7C5DDE}");
                var product3Guid = Guid.Parse("{BF3F3002-7E53-441E-8B76-F6280BE284AA}");

                var shoppingCartItem1Guid = Guid.Parse("{B0788D2F-8003-43C1-92A4-EDC76A7C5DDE}");
                var shoppingCartItem2Guid = Guid.Parse("{6313179F-7837-473A-A4D5-A5571B43E6A6}");
                var shoppingCartItem3Guid = Guid.Parse("{BF3F3002-7E53-441E-8B76-F6280BE284AA}");
                var shoppingCartItem4Guid = Guid.Parse("{FE98F549-E790-4E9F-AA16-18C2292A2EE9}");


                var shoppingCartItems = new List<ShoppingCartItem>
                {
                    new ShoppingCartItem
                    {
                        Id = shoppingCartItem1Guid,
                        CustomerId = "Katarina",
                        ProductId = product1Guid,
                        Quantity = 1
                    },
                    new ShoppingCartItem
                    {
                        Id = shoppingCartItem2Guid,
                        CustomerId = "Katarina",
                        ProductId = product2Guid,
                        Quantity = 5
                    },
                    new ShoppingCartItem
                    {
                        Id = shoppingCartItem3Guid,
                        CustomerId = "Katarina",
                        ProductId = product3Guid,
                        Quantity = 1
                    }
                };

                dbContext.ShoppingCartItems.AddRange(shoppingCartItems);

                dbContext.SaveChanges();
            }
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
