using AutoMapper;
using MediatR;
using WebShop.Application.Contracts.Persistence;
using WebShop.Domain.Entities;

namespace WebShop.Application.Features.ShoppingCartItems.Queries.GetShoppingCartItemsList
{
    public class GetShoppingCartItemsListQueryHandler : IRequestHandler<GetShoppingCartItemsListQuery, List<ShoppingCardItemsListVM>>
    {

        private readonly IShoppingCartItemRepository _shoppingCartItemRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetShoppingCartItemsListQueryHandler(IMapper mapper, IShoppingCartItemRepository shoppingCartItemRepository, IProductRepository productRepository)
        {
            _mapper = mapper;
            _shoppingCartItemRepository = shoppingCartItemRepository;
            _productRepository = productRepository;
        }

        public async Task<List<ShoppingCardItemsListVM>> Handle(GetShoppingCartItemsListQuery request, CancellationToken cancellationToken)
        {
            var allShoppingCartItems = (await _shoppingCartItemRepository.ListCustomerCartContent(request.CustomerId)).OrderBy(x => x.CreatedDate);

            var shoppingCardItemsListDTO = new List<ShoppingCardItemsListVM>();

            foreach (var item in allShoppingCartItems)
            {
                var product = await _productRepository.GetByIdAsync(item.ProductId);
                var itemVm = _mapper.Map<ShoppingCardItemsListVM>(item);
                itemVm.Product = _mapper.Map<ProductDTO>(product);
                shoppingCardItemsListDTO.Add(itemVm);
            }
            return shoppingCardItemsListDTO;
        }
    }
}
