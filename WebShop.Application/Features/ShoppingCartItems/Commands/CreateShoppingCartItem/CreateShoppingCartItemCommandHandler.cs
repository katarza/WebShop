using AutoMapper;
using MediatR;
using WebShop.Application.Contracts.Services;
using WebShop.Application.Contracts.Persistence;
using WebShop.Domain.Entities;


namespace WebShop.Application.Features.ShoppingCartItems.Commands.CreateShoppingCartItem
{
    public class CreateShoppingCartItemCommandHandler : IRequestHandler<CreateShoppingCartItemCommand>
    {
        private readonly IShoppingCartItemRepository _shoppingCartItemRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStockService _stockService;

        private readonly IMapper _mapper;

        public CreateShoppingCartItemCommandHandler(IMapper mapper, IShoppingCartItemRepository shoppingCartItemRepository,
            IStockService stockService, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _shoppingCartItemRepository = shoppingCartItemRepository;
            _unitOfWork = unitOfWork;
            _stockService = stockService;
        }

        public async Task<Unit> Handle(CreateShoppingCartItemCommand request, CancellationToken cancellationToken)
        {
            var shoppingCartItem = _mapper.Map<ShoppingCartItem>(request);

            await _stockService.ReserveProductQuantityAsync(shoppingCartItem);

            await _shoppingCartItemRepository.AddAsync(shoppingCartItem);

            await _unitOfWork.SaveChangesAsync();

            return Unit.Value;
                        
        }

        
    }
}
 