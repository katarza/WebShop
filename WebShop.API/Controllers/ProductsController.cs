using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebShop.Application.Features.Products.Queries;

namespace WebShop.API.Controllers
{
    [Route("API/Products")]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<ProductsListVM>>> GetShoppingProducts()
        {
            var dtos = await _mediator.Send(new GetProductsListQuery());
            return Ok(dtos);
        }
    }
}
