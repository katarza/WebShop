using Microsoft.EntityFrameworkCore;
using WebShop.Application.Contracts.Persistence;
using WebShop.Domain.Entities;

namespace WebShop.Persistence.Repository
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(WebShopDbContext dbContext) : base(dbContext)
        {
            if (dbContext.Products.Any() == false)
            {
                var product1Guid = Guid.Parse("{3fa85f64-5717-4562-b3fc-2c963f66afa6}");
                var product2Guid = Guid.Parse("{B0788D2F-8003-43C1-92A4-EDC76A7C5DDE}");
                var product3Guid = Guid.Parse("{BF3F3002-7E53-441E-8B76-F6280BE284AA}");
                //seed data
                var products = new List<Product>
                {
                    new Product
                    {
                        Id = product1Guid,
                        ProductName = "Test Product 1",
                        QuantityInStock = 50,
                        UnitPrice = 100
                    },

                    new Product
                    {
                        Id = product2Guid,
                        ProductName = "Test Product 2",
                        QuantityInStock = 1000,
                        UnitPrice = 20
                    },

                    new Product
                    {
                        Id = product3Guid,
                        ProductName = "Test Product 3",
                        QuantityInStock = 200,
                        UnitPrice = 120
                    }
                };
                dbContext.Products.AddRange(products);
                dbContext.SaveChanges();
            }
        }

        override public async Task<Product?> GetByIdAsync(Guid id)
        {
            Product? t = await _dbContext.Products
                            .Where(s => s.Id == id).FirstOrDefaultAsync();
            return t;
        }

        public Task<bool> DoesProductIdExist(Guid productId)
        {
            var matches = _dbContext.Products.Any(e => e.Id.Equals(productId));
            return Task.FromResult(matches);
        }

        public async Task<int> ReserveProductQuantityAsync(Guid ProductId, int Quantity)
        {
            Product? product = await GetByIdAsync(ProductId);

            int quantityToReserveLocally = 0;

            if (product is not null)
            {

                if (product.QuantityInStock >= Quantity)
                {
                    quantityToReserveLocally = Quantity;
                }
                else
                {
                    quantityToReserveLocally = product.QuantityInStock;
                }

                product.QuantityInStock -= quantityToReserveLocally;

                Update(product);
            }

            return quantityToReserveLocally;


        }

    }
}
