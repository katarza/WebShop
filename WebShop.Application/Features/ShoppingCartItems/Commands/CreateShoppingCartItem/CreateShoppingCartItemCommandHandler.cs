using AutoMapper;
using MediatR;
using WebShop.Application.Contracts.Persistence;
using WebShop.Domain.Entities;

namespace WebShop.Application.Features.ShoppingCartItems.Commands.CreateShoppingCartItem
{
    public class CreateShoppingCartItemCommandHandler : IRequestHandler<CreateShoppingCartItemCommand>
    {
        private readonly IProductRepository _productRepository;
        private readonly IShoppingCartItemRepository _shoppingCartItemRepository;

        private readonly IMapper _mapper;

        public CreateShoppingCartItemCommandHandler(IMapper mapper, IShoppingCartItemRepository shoppingCartItemRepository, IProductRepository productRepository)
        {
            _mapper = mapper;
            _shoppingCartItemRepository = shoppingCartItemRepository;
            _productRepository = productRepository;
        }

        public async Task<Unit> Handle(CreateShoppingCartItemCommand request, CancellationToken cancellationToken)
        {
            var shoppingCartItem = _mapper.Map<ShoppingCartItem>(request);

            var validator = new CreateShoppingCartItemCommandValidator(_productRepository);

            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0) 
            {
                throw new Exceptions.ValidationException(validationResult);
            }

            await _shoppingCartItemRepository.AddAsync(shoppingCartItem);

            return Unit.Value;

        }
    }
}
 