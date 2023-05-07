using AutoMapper;
using WebShop.Application.Contracts.Services;
using WebShop.Application.Contracts.Persistence;
using WebShop.Application.Features.Orders.Commands.CalculationServices;
using WebShop.Domain.Entities;
using ValidationException = WebShop.Application.Exceptions.ValidationException;

namespace WebShop.Application.Features.Orders.Commands.Factory
{
    internal class OrderFactory : IOrderFactory
    {
        private readonly IMapper _mapper;
        public IDiscountCalculationService DiscountCalculationService { get; set; }
        private readonly IShoppingCartItemRepository _shoppingCartItemRepository;
        private readonly IProductRepository _productRepository;

        public OrderFactory(IMapper mapper, IShoppingCartItemRepository shoppingCartItemRepository, IProductRepository productRepository)
        {
            _mapper = mapper;
            _shoppingCartItemRepository = shoppingCartItemRepository;
            _productRepository = productRepository;
            DiscountCalculationService = new NoDiscountService();
        }

        public async Task<Order> CreateOrderAsync(CreateOrderCommand createOrderCommand)
        {
            var order = new Order();

            order = _mapper.Map<Order>(createOrderCommand);

            double totalAmount = await calculateOrderTotalAmountAsync(createOrderCommand);

            var discountPercentage = DiscountCalculationService.GetDiscountPercentage(createOrderCommand);
            
            order.AppliedDiscount = totalAmount * (discountPercentage) / 100;

            order.TotalAmount = totalAmount - order.AppliedDiscount;

            return order;
        }

        private async Task<double> calculateOrderTotalAmountAsync(CreateOrderCommand createOrderCommand)
        {           

            double totalAmount = 0;

            var allShoppingCartItems = (await _shoppingCartItemRepository.ListCustomerCartContent(createOrderCommand.CustomerId));

            foreach (var item in allShoppingCartItems)
            {
                var product = await _productRepository.GetByIdAsync(item.ProductId);
                if (product == null)
                {
                    var failure = new Dictionary<string, string[]>()
                    {
                        { "CustomerId", new string[1] {"Product from cart not found!"} }
                    };
                    throw new ValidationException(failure);
                }
                else
                {
                    totalAmount += item.Quantity * product.UnitPrice ;
                }

            }

            return totalAmount;
        }
    }
}
