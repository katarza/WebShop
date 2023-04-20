using System;
using System.Runtime.CompilerServices;
using WebShop.Application.Contracts.Services;

[assembly: InternalsVisibleTo("WebShop.Appllication.UnitTests")]
namespace WebShop.Application.Features.Orders.Commands.CalculationServices
{
    internal class HappyHourDiscountService : IDiscountCalculationService
    {
        public double GetDiscountPercentage(CreateOrderCommand orderRequest)
        {
            char lastCharacter = orderRequest.CustomerPhone[orderRequest.CustomerPhone.Length - 1];
            int lastDigit = lastCharacter - '0';
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