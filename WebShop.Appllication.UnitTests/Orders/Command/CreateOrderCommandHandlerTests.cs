using AutoMapper;
using Moq;
using WebShop.Application.Common.Dates;
using WebShop.Application.Contracts.Persistence;
using WebShop.Application.Features.Orders.Commands;
using WebShop.Application.Profiles;
using WebShop.Appllication.UnitTests.Mocks;

namespace WebShop.Appllication.UnitTests.Orders.Command
{
    public class CreateOrderCommandHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IShoppingCartItemRepository> _mockShoppingCartItemRepository;
        private readonly Mock<IProductRepository> _mockProductRepository;
        private readonly Mock<IOrderRepository> _mockOrderRepository;

        public CreateOrderCommandHandlerTests()
        {
            _mockShoppingCartItemRepository = RepositoryMocks.GetShoppingCartItemRepository();
            _mockProductRepository = RepositoryMocks.GetProductRepository();
            _mockOrderRepository = RepositoryMocks.GetOrderRepository();


            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        async void Handle_CreateOrder_CheckIfOneOrderAdded()
        {
            var unitOfWork = new Mock<IUnitOfWork>();
            var dateService = new Mock<IDateService>();
            dateService.Setup(p => p.GetDate()).Returns(DateTime.ParseExact("2023-04-20 15:12 PM", "yyyy-MM-dd HH:mm tt", null));

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

            var handler = new CreateOrderCommandHandler(_mapper, _mockOrderRepository.Object, _mockProductRepository.Object,  _mockShoppingCartItemRepository.Object, unitOfWork.Object, dateService.Object);
            
            await handler.Handle(createOrderCommand, CancellationToken.None);

            var allOrders = await _mockOrderRepository.Object.ListAllAsync();
            
            Assert.Equal(1, allOrders.Count);
            
        }

        [Fact]
        async void Handle_CreateOrder_CheckIfCartEmptyAfterOrderCreation()
        {
            var unitOfWork = new Mock<IUnitOfWork>();
            var dateService = new Mock<IDateService>();
            dateService.Setup(p => p.GetDate()).Returns(DateTime.ParseExact("2023-04-20 15:12 PM", "yyyy-MM-dd HH:mm tt", null));

            

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

            var allShoppingCartItems = await _mockShoppingCartItemRepository.Object.ListCustomerCartContent(createOrderCommand.CustomerId);

            Assert.Equal(3, allShoppingCartItems.Count);

            var handler = new CreateOrderCommandHandler(_mapper, _mockOrderRepository.Object, _mockProductRepository.Object, _mockShoppingCartItemRepository.Object, unitOfWork.Object, dateService.Object);

            await handler.Handle(createOrderCommand, CancellationToken.None);

            allShoppingCartItems = await _mockShoppingCartItemRepository.Object.ListCustomerCartContent(createOrderCommand.CustomerId);


            Assert.Empty(allShoppingCartItems);

        }

        [Fact]
        async void Handle_CreateOrder_CheckIfNoDiscountOutsideHappyHour()
        {
            var unitOfWork = new Mock<IUnitOfWork>();
            var dateService = new Mock<IDateService>();
            dateService.Setup(p => p.GetDate()).Returns(DateTime.ParseExact("2023-04-20 15:12 PM", "yyyy-MM-dd HH:mm tt", null));

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

            var handler = new CreateOrderCommandHandler(_mapper, _mockOrderRepository.Object, _mockProductRepository.Object, _mockShoppingCartItemRepository.Object, unitOfWork.Object, dateService.Object);

            var response = await handler.Handle(createOrderCommand, CancellationToken.None);

            Assert.Equal(0, response.Order.AppliedDiscount);
            Assert.Equal(320, response.Order.TotalAmount);

        }

        [Fact]
        async void Handle_CreateOrder_CheckIfHappyHourDiscountApplied()
        {
            var unitOfWork = new Mock<IUnitOfWork>();
            var dateService = new Mock<IDateService>();
            dateService.Setup(p => p.GetDate()).Returns(DateTime.ParseExact("2023-04-20 16:12 PM", "yyyy-MM-dd HH:mm tt", null));

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

            var handler = new CreateOrderCommandHandler(_mapper, _mockOrderRepository.Object, _mockProductRepository.Object, _mockShoppingCartItemRepository.Object, unitOfWork.Object, dateService.Object);

            var response = await handler.Handle(createOrderCommand, CancellationToken.None);

            Assert.Equal(32, response.Order.AppliedDiscount);
            Assert.Equal(288, response.Order.TotalAmount);

        }
    }
}
