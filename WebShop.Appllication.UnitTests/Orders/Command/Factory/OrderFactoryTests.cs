using AutoMapper;
using Moq;
using WebShop.Application.Contracts.Persistence;
using WebShop.Application.Features.Orders.Commands;
using WebShop.Application.Features.Orders.Commands.CalculationServices;
using WebShop.Application.Features.Orders.Commands.Factory;
using WebShop.Application.Profiles;
using WebShop.Appllication.UnitTests.Mocks;

namespace WebShop.Appllication.UnitTests.Orders.Command.Factory
{
    public class OrderFactoryTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IShoppingCartItemRepository> _mockShoppingCartItemRepository;
        private readonly Mock<IProductRepository> _mockProductRepository;

        public OrderFactoryTests()
        {
            _mockShoppingCartItemRepository = RepositoryMocks.GetShoppingCartItemRepository();
            _mockProductRepository = RepositoryMocks.GetProductRepository();

            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async void CreateOrderAsync_ContructOrder_TotalAmountMustBe320()
        {

            var orderFactory = new OrderFactory(_mapper, _mockShoppingCartItemRepository.Object, _mockProductRepository.Object);
            var createOrderCommand = new CreateOrderCommand()
            {
                CustomerId = "Katarina",
                CustomerPhone = "0653575051",
                ShippingAddress = new ShippingAddressDTO()
                {
                    City = "Palic",
                    Street = "Marka Oreskovica",
                    HouseNumber = "3A"
                }
            };

            var order = await orderFactory.CreateOrderAsync(createOrderCommand);

            Assert.Equal(320, order.TotalAmount);

        }

        [Fact]
        public async void CreateOrderAsync_ContructNoDiscountOrder_AppliedDiscountMustBe0()
        {

            var orderFactory = new OrderFactory(_mapper, _mockShoppingCartItemRepository.Object, _mockProductRepository.Object);
            var createOrderCommand = new CreateOrderCommand()
            {
                CustomerId = "Katarina",
                CustomerPhone = "0653575051",
                ShippingAddress = new ShippingAddressDTO()
                {
                    City = "Palic",
                    Street = "Marka Oreskovica",
                    HouseNumber = "3A"
                }
            };

            var order = await orderFactory.CreateOrderAsync(createOrderCommand);

            Assert.Equal(0, order.AppliedDiscount);

        }

        [Fact]
        public async void CreateOrderAsync_ContructHappyHourDiscountOrder_AppliedDiscountMustBe32()
        {

            var orderFactory = new OrderFactory(_mapper, _mockShoppingCartItemRepository.Object, _mockProductRepository.Object);
            orderFactory.DiscountCalculationService = new HappyHourDiscountService();
            var createOrderCommand = new CreateOrderCommand()
            {
                CustomerId = "Katarina",
                CustomerPhone = "0653575051",
                ShippingAddress = new ShippingAddressDTO()
                {
                    City = "Palic",
                    Street = "Marka Oreskovica",
                    HouseNumber = "3A"
                }
            };

            var order = await orderFactory.CreateOrderAsync(createOrderCommand);

            Assert.Equal(32, order.AppliedDiscount);

        }

        [Fact]
        public async void CreateOrderAsync_ContructHappyHourDiscountOrder_TotalAmountMustBe288()
        {

            var orderFactory = new OrderFactory(_mapper, _mockShoppingCartItemRepository.Object, _mockProductRepository.Object);
            orderFactory.DiscountCalculationService = new HappyHourDiscountService();
            var createOrderCommand = new CreateOrderCommand()
            {
                CustomerId = "Katarina",
                CustomerPhone = "0653575051",
                ShippingAddress = new ShippingAddressDTO()
                {
                    City = "Palic",
                    Street = "Marka Oreskovica",
                    HouseNumber = "3A"
                }
            };

            var order = await orderFactory.CreateOrderAsync(createOrderCommand);

            Assert.Equal(288, order.TotalAmount);

        }
    }
}
