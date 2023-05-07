using WebShop.Application.Features.Orders.Commands;

namespace WebShop.Application.Contracts.Services
{
    public interface IDiscountCalculationService
    {
        double GetDiscountPercentage(CreateOrderCommand orderRequest);

    }
}
