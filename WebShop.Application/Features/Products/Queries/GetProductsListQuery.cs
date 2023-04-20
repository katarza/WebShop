using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Application.Features.Products.Queries
{
    public class GetProductsListQuery : IRequest<List<ProductsListVM>>
    {
    }
}
