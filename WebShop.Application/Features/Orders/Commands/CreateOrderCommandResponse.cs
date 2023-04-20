using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Application.Features.Orders.Commands
{
    public class CreateOrderCommandResponse
    {
        public OrderDTO Order { get; set; } = new OrderDTO();
    }
}
