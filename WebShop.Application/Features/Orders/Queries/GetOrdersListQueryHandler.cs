using AutoMapper;
using MediatR;
using WebShop.Application.Contracts.Persistence;

namespace WebShop.Application.Features.Orders.Queries
{
    public class GetOrdersListQueryHandler : IRequestHandler<GetOrdersListQuery, List<OrdersListVM>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public GetOrdersListQueryHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<List<OrdersListVM>> Handle(GetOrdersListQuery request, CancellationToken cancellationToken)
        {
            var allOrders = (await _orderRepository.ListAllAsync());
            return _mapper.Map<List<OrdersListVM>>(allOrders);
        }
    }
}
