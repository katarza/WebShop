using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Domain.Entities;
using WebShop.Persistence;

namespace WebShop.API.IntegrationTests.Base
{
    public class Utilities
    {
        public static void InitializeDbForTests(WebShopDbContext context)
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
            context.Products.AddRange(products);
            context.ShoppingCartItems.AddRange(shoppingCartItems);

            context.SaveChanges();
        }
    }
}
