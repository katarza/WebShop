using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebShop.Application.Features.Orders.Commands;
using WebShop.Application.Features.Orders.Queries;
using WebShop.Application.Features.ShoppingCartItems.Commands.CreateShoppingCartItem;
using WebShop.Application.Features.ShoppingCartItems.Queries.GetShoppingCartItemsList;

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

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<OrdersListVM>>> GetShoppingOrders()
        {
            var dtos = await _mediator.Send(new GetOrdersListQuery());
            return Ok(dtos);
        }

        [HttpPost]
        public async Task<ActionResult<CreateOrderCommandResponse>> CreateOrder
            ([FromBody] CreateOrderCommand createOrderCommand)
        {
            var response = await _mediator.Send(createOrderCommand);
            return Ok(response);
        }
    }
}
