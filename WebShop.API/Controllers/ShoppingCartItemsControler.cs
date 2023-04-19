using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebShop.Application.Features.ShoppingCartItems.Commands.CreateShoppingCartItem;
using WebShop.Application.Features.ShoppingCartItems.Queries.GetShoppingCartItemsList;

namespace WebShop.API.Controllers
{
    [Route("API/ShoppingCart/Items")]
    public class ShoppingCartItemsControler : ControllerBase
    {
        private readonly IMediator _mediator;

        public ShoppingCartItemsControler(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<ShoppingCardItemsListVM>>> GetShoppingCartItems(string customerId)
        {
            var dtos = await _mediator.Send(new GetShoppingCartItemsListQuery() { CustomerId = customerId});
            return Ok(dtos);
        }

        [HttpPost]
        public async Task<ActionResult<CreateShoppingCartItemCommand>> AddShoppingCartItem
            ([FromBody] CreateShoppingCartItemCommand createShoppingCartItemCommand)
        {
            var response = await _mediator.Send(createShoppingCartItemCommand);
            return Ok(response);
        }


    }
}
