using AutoMapper;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Application.Common.Dates;
using WebShop.Application.Contracts.Persistence;
using WebShop.Application.Contracts.Services;
using WebShop.Application.Features.Orders.Commands;
using WebShop.Application.Features.ShoppingCartItems.Commands.CreateShoppingCartItem;
using WebShop.Application.Features.ShoppingCartItems.Commands.CreateShoppingCartItem.StockServices;
using WebShop.Application.Profiles;
using WebShop.Appllication.UnitTests.Mocks;
using WebShop.Domain.Entities;

namespace WebShop.Appllication.UnitTests.ShoppingCartItems.Commands
{
    public class CreateShoppingCartItemCommandHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IShoppingCartItemRepository> _mockShoppingCartItemRepository;
        private readonly Mock<IStockService> _mockStockService;

        public CreateShoppingCartItemCommandHandlerTests()
        {
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = configurationProvider.CreateMapper();
            _mockShoppingCartItemRepository = RepositoryMocks.GetShoppingCartItemRepository();
            _mockStockService = new Mock<IStockService>();
            _mockStockService.Setup(s => s.ReserveProductQuantityAsync(It.IsAny<ShoppingCartItem>())).Returns(Task.CompletedTask);
        }

        [Fact]
        async void Handle_CreateShoppingCartItem_CheckIfOneShoppingCartItemAdded()
        {
            var unitOfWork = new Mock<IUnitOfWork>();

            var createShoppingCartItemCommand = new CreateShoppingCartItemCommand()
            {
                CustomerId = "Katarina",
                ProductId = Guid.Parse("{3fa85f64-5717-4562-b3fc-2c963f66afa6}"),
                Quantity = 10
            };

            var handler = new CreateShoppingCartItemCommandHandler(_mapper, _mockShoppingCartItemRepository.Object, _mockStockService.Object, unitOfWork.Object);

            await handler.Handle(createShoppingCartItemCommand, CancellationToken.None);

            var allShoppingCartItems = await _mockShoppingCartItemRepository.Object.ListCustomerCartContent(createShoppingCartItemCommand.CustomerId);

            Assert.Equal(4, allShoppingCartItems.Count);

        }
    }
}
