using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Application.Contracts.Persistence;
using WebShop.Application.Features.Orders.Commands;
using WebShop.Application.Features.Products.Queries;

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
