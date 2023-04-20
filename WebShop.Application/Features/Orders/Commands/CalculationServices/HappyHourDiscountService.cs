using System;
using WebShop.Application.Contracts.CalculationServices;

namespace WebShop.Application.Features.Orders.Commands.CalculationServices
{
    public class HappyHourDiscountService : IDiscountCalculationService
    {
        public double GetDiscountPercentage(CreateOrderCommand orderRequest)
        {
            char lastCharacter = orderRequest.CustomerPhone[orderRequest.CustomerPhone.Length - 1];
            int lastDigit = Convert.ToInt32(lastCharacter);
            double discountPercentage = 0;
            if (lastDigit % 2 != 0)
            {
                discountPercentage = 10;
            }
            else
            {
                if (lastDigit == 0)
                {
                    discountPercentage = 30;
                }
                else
                {
                    discountPercentage = 20;
                }

            }

            return discountPercentage;
        }
    }
}