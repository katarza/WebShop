using AutoMapper;
using MediatR;
using System.ComponentModel.DataAnnotations;
using WebShop.Application.Contracts.Infrastructure;
using WebShop.Application.Contracts.Persistence;
using WebShop.Application.Models.SuppliersService;
using WebShop.Domain.Entities;
using ValidationException = WebShop.Application.Exceptions.ValidationException;

namespace WebShop.Application.Features.ShoppingCartItems.Commands.CreateShoppingCartItem
{
    public class CreateShoppingCartItemCommandHandler : IRequestHandler<CreateShoppingCartItemCommand>
    {
        private readonly IProductRepository _productRepository;
        private readonly IShoppingCartItemRepository _shoppingCartItemRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISupplierStockService _supplierStockService;

        private readonly IMapper _mapper;

        public CreateShoppingCartItemCommandHandler(IMapper mapper, IShoppingCartItemRepository shoppingCartItemRepository, 
            IProductRepository productRepository, IUnitOfWork unitOfWork, ISupplierStockService supplierStockService)
        {
            _mapper = mapper;
            _shoppingCartItemRepository = shoppingCartItemRepository;
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
            _supplierStockService = supplierStockService;
        }

        public async Task<Unit> Handle(CreateShoppingCartItemCommand request, CancellationToken cancellationToken)
        {
            var shoppingCartItem = _mapper.Map<ShoppingCartItem>(request);
            await reserveProductQuantity(shoppingCartItem);

            await _shoppingCartItemRepository.AddAsync(shoppingCartItem);

            await _unitOfWork.SaveChangesAsync();

            return Unit.Value;
                        
        }

        private async Task reserveProductQuantity(ShoppingCartItem shoppingCartItem)
        {
            int reservedOnLocalStock = await _productRepository.ReserveProductQuantityAsync(shoppingCartItem.ProductId, shoppingCartItem.Quantity);

            if (reservedOnLocalStock < shoppingCartItem.Quantity)
            {
                int quantityToReserveExternaly = shoppingCartItem.Quantity - reservedOnLocalStock;
                ReserveOnSuppliersStockRequest reserveOnSuppliersStockRequest = new ReserveOnSuppliersStockRequest(shoppingCartItem.ProductId, quantityToReserveExternaly);
                var reservationSucceeded = await _supplierStockService.ReserveOnSuppliersStockAsync(reserveOnSuppliersStockRequest);

                if (!reservationSucceeded)
                {
                    var failure = new Dictionary<string, string[]>()
                    {
                        { "Quantity", new string[1] {"Not enough items available on stock"} }
                    };
                    throw new ValidationException(failure);
                }
            }
        }
    }
}
 