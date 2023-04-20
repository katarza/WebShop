using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Application.Features.Orders.Commands;

namespace WebShop.Application.Contracts.Services
{
    public interface IDiscountCalculationService
    {
        double GetDiscountPercentage(CreateOrderCommand orderRequest);

    }
}
