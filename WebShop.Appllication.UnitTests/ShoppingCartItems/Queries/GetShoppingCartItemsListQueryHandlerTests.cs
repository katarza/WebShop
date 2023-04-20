using AutoMapper;
using EmptyFiles;
using Moq;
using WebShop.Application.Contracts.Persistence;
using Shouldly;
using WebShop.Appllication.UnitTests.Mocks;
using WebShop.Application.Profiles;
using WebShop.Application.Features.ShoppingCartItems.Queries.GetShoppingCartItemsList;

namespace WebShop.Appllication.UnitTests.ShoppingCartItems.Queries
{
    public class GetShoppingCartItemsListQueryHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IShoppingCartItemRepository> _mockShoppingCartItemRepository;
        private readonly Mock<IProductRepository> _mockProductRepository;

        public GetShoppingCartItemsListQueryHandlerTests()
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
        public async Task Handle_GetShoppingCartItemsListTest_ShouldReturn3Items()
        {
            var handler = new GetShoppingCartItemsListQueryHandler(_mapper, _mockShoppingCartItemRepository.Object, _mockProductRepository.Object);

            var result = await handler.Handle(new GetShoppingCartItemsListQuery() { CustomerId = "Katarina" }, CancellationToken.None);

            result.ShouldBeOfType<List<ShoppingCardItemsListVM>>();

            result.Count.ShouldBe(3);
        }

        [Fact]
        public async Task Handle_GetShoppingCartItemsListTest_ShouldReturnShoppingCardItemsListVM()
        {
            var handler = new GetShoppingCartItemsListQueryHandler(_mapper, _mockShoppingCartItemRepository.Object, _mockProductRepository.Object);

            var result = await handler.Handle(new GetShoppingCartItemsListQuery() { CustomerId = "Katarina" }, CancellationToken.None);

            result.ShouldBeOfType<List<ShoppingCardItemsListVM>>();
        }
    }
}