using AutoMapper;
using MediatR;
using WebShop.Application.Contracts.Persistence;

namespace WebShop.Application.Features.Products.Queries
{
    public class GetProductsListQueryHandler : IRequestHandler<GetProductsListQuery, List<ProductsListVM>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetProductsListQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<List<ProductsListVM>> Handle(GetProductsListQuery request, CancellationToken cancellationToken)
        {
            var allProducts = (await _productRepository.ListAllAsync());
            return _mapper.Map<List<ProductsListVM>>(allProducts);
        }
    }
}
