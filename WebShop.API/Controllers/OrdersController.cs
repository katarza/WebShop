using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebShop.Application.Features.ShoppingCartItems.Commands.CreateShoppingCartItem;

namespace WebShop.API.Controllers
{

    [Route("api/orders")]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

    }
}
