using WebShop.Application.Contracts.CalculationServices;

namespace WebShop.Application.Features.Orders.Commands.CalculationServices
{
    public class NoDiscountService : IDiscountCalculationService
    {
        public double GetDiscountPercentage(CreateOrderCommand orderRequest)
        {
            return 0;
        }
    }
}