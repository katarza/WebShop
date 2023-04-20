using WebShop.Application.Contracts.Services;

namespace WebShop.Application.Features.Orders.Commands.CalculationServices
{
    internal class NoDiscountService : IDiscountCalculationService
    {
        public double GetDiscountPercentage(CreateOrderCommand orderRequest)
        {
            return 0;
        }
    }
}