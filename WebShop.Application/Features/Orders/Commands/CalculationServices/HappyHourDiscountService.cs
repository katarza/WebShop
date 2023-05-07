using System.Runtime.CompilerServices;
using WebShop.Application.Contracts.Services;

[assembly: InternalsVisibleTo("WebShop.Appllication.UnitTests")]
namespace WebShop.Application.Features.Orders.Commands.CalculationServices
{
    internal class HappyHourDiscountService : IDiscountCalculationService
    {
        public struct HappyHourDiscountPercentages
        {
            public const double None = 0;
            public const double LastPhoneNumberDigit0 = 30;
            public const double LastPhoneNumberDigitOdd = 10;
            public const double LastPhoneNumberDigitEven = 20;
        }

        public double GetDiscountPercentage(CreateOrderCommand orderRequest)
        {
            char lastCharacter = orderRequest.CustomerPhone[orderRequest.CustomerPhone.Length - 1];
            
            int lastDigit = lastCharacter - '0';

            if (lastDigit % 2 != 0) return HappyHourDiscountPercentages.LastPhoneNumberDigitOdd;

            if (lastDigit == 0) return HappyHourDiscountPercentages.LastPhoneNumberDigit0;
            
            return HappyHourDiscountPercentages.LastPhoneNumberDigitEven;
        }
    }
}