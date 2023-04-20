using EmptyFiles;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Application.Contracts.Persistence;
using WebShop.Domain.Entities;

namespace WebShop.Appllication.UnitTests.Mocks
{
    public class RepositoryMocks
    {
        public static Mock<IShoppingCartItemRepository> GetShoppingCartItemRepository()
        {
            var shoppingCartItem1Guid = Guid.Parse("{B0788D2F-8003-43C1-92A4-EDC76A7C5DDE}");
            var shoppingCartItem2Guid = Guid.Parse("{6313179F-7837-473A-A4D5-A5571B43E6A6}");
            var shoppingCartItem3Guid = Guid.Parse("{BF3F3002-7E53-441E-8B76-F6280BE284AA}");
            var shoppingCartItem4Guid = Guid.Parse("{FE98F549-E790-4E9F-AA16-18C2292A2EE9}");

            var product1Guid = Guid.Parse("{3fa85f64-5717-4562-b3fc-2c963f66afa6}");
            var product2Guid = Guid.Parse("{B0788D2F-8003-43C1-92A4-EDC76A7C5DDE}");
            var product3Guid = Guid.Parse("{BF3F3002-7E53-441E-8B76-F6280BE284AA}");

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

            var mockShoppingCartItemRepository = new Mock<IShoppingCartItemRepository>();
            mockShoppingCartItemRepository.Setup(repo => repo.ListCustomerCartContent(It.IsAny<string>())).ReturnsAsync(shoppingCartItems);
            mockShoppingCartItemRepository.Setup(repo => repo.EmptyShoppingCartAsync(It.IsAny<string>())).Callback(new InvocationAction((x) => { shoppingCartItems.Clear(); })).Verifiable() ;

            mockShoppingCartItemRepository.Setup(repo => repo.AddAsync(It.IsAny<ShoppingCartItem>())).ReturnsAsync(
                (ShoppingCartItem shoppingCartItem) =>
                {
                    shoppingCartItems.Add(shoppingCartItem);
                    return shoppingCartItem;
                });

            return mockShoppingCartItemRepository;
        }

        public static Mock<IOrderRepository> GetOrderRepository()
        {
            var orders = new List<Order>();
            var mockOrderRepository = new Mock<IOrderRepository>();

            mockOrderRepository.Setup(repo => repo.AddAsync(It.IsAny<Order>())).ReturnsAsync(
                (Order order) =>
                {
                    orders.Add(order);
                    return order;
                });

            mockOrderRepository.Setup(repo => repo.ListAllAsync()).ReturnsAsync(orders);

            return mockOrderRepository;
        }

        public static Mock<IProductRepository> GetProductRepository()
        {

            var product1Guid = Guid.Parse("{3fa85f64-5717-4562-b3fc-2c963f66afa6}");
            var product2Guid = Guid.Parse("{B0788D2F-8003-43C1-92A4-EDC76A7C5DDE}");
            var product3Guid = Guid.Parse("{BF3F3002-7E53-441E-8B76-F6280BE284AA}");

            var singleProduct1 = new Product
            {
                Id = product1Guid,
                ProductName = "Test Product 1",
                QuantityInStock = 50,
                UnitPrice = 100
            };
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

            var mockProductRepository = new Mock<IProductRepository>();
            mockProductRepository.Setup(repo => repo.GetByIdAsync(It.Is<Guid>(x => x == product1Guid))).ReturnsAsync(singleProduct1);
            mockProductRepository.Setup(repo => repo.GetByIdAsync(It.Is<Guid>(x => x == product2Guid))).ReturnsAsync(products.Where(s => s.Id == product2Guid).FirstOrDefault());
            mockProductRepository.Setup(repo => repo.GetByIdAsync(It.Is<Guid>(x => x == product3Guid))).ReturnsAsync(products.Where(s => s.Id == product3Guid).FirstOrDefault());
            mockProductRepository.Setup(repo => repo.ReserveProductQuantityAsync(It.IsAny<Guid>(), It.IsAny<int>()))
                .Callback((Guid x, int y) => { singleProduct1.QuantityInStock -= Math.Min(y, singleProduct1.QuantityInStock); })
                .ReturnsAsync((Guid x, int y) => Math.Min(y, singleProduct1.QuantityInStock));

            return mockProductRepository;
        }
    }
}
