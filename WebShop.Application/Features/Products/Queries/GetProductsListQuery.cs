using MediatR;

namespace WebShop.Application.Features.Products.Queries
{
    public class GetProductsListQuery : IRequest<List<ProductsListVM>>
    {
    }
}
