using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Application.Common.Dates;
using WebShop.Application.Contracts.Services;
using WebShop.Application.Contracts.Persistence;
using WebShop.Application.Features.Orders.Commands.CalculationServices;
using WebShop.Application.Features.Orders.Commands.Factory;
using WebShop.Application.Features.ShoppingCartItems.Commands.CreateShoppingCartItem;
using WebShop.Application.Features.ShoppingCartItems.Queries.GetShoppingCartItemsList;
using WebShop.Domain.Entities;

namespace WebShop.Application.Features.Orders.Commands
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, CreateOrderCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly IShoppingCartItemRepository _shoppingCartItemRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDateService _dateService;
        private readonly OrderFactory factory;

        public CreateOrderCommandHandler(IMapper mapper, IOrderRepository orderRepository, IProductRepository productRepository, IShoppingCartItemRepository shoppingCartItemRepository, IUnitOfWork unitOfWork, IDateService dateService)
        {
            _mapper = mapper;
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _shoppingCartItemRepository = shoppingCartItemRepository;
            _unitOfWork = unitOfWork;
            _dateService = dateService;
            factory = new OrderFactory(_mapper, _shoppingCartItemRepository, _productRepository);
        }

        public async Task<CreateOrderCommandResponse> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var createOrderCommandResponse = new CreateOrderCommandResponse();

            TimeSpan start = new(16, 0, 0);
            TimeSpan end = new(17, 0, 0);
            TimeSpan now = _dateService.GetDate().TimeOfDay;

            if ((now > start) && (now < end))
            {
                factory.DiscountCalculationService = new HappyHourDiscountService();
            }

            Order order = await factory.CreateOrderAsync(request);

            await _orderRepository.AddAsync(order);

            _shoppingCartItemRepository.EmptyShoppingCartAsync(order.CustomerId);

            await _unitOfWork.SaveChangesAsync();

            OrderDTO orderDTO = _mapper.Map<OrderDTO>(order);

            createOrderCommandResponse.Order = orderDTO;

            return createOrderCommandResponse;

        }
    }
}


