using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Application.Features.Orders.Commands;
using WebShop.Application.Features.ShoppingCartItems.Queries.GetShoppingCartItemsList;

namespace WebShop.Application.Features.Orders.Queries
{
    public class GetOrdersListQuery : IRequest<List<OrdersListVM>>
    {
    }
}
