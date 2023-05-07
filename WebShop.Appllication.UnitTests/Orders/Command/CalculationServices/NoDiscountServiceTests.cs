using WebShop.Application.Features.Orders.Commands;
using WebShop.Application.Features.Orders.Commands.CalculationServices;

namespace WebShop.Appllication.UnitTests.Orders.Command.CalculationServices
{
    public class NoDiscountServiceTests
    {
        [Fact]
        void GetDiscountPercentage_Calculation_ShouldReturn0()
        {
            var createOrderCommand = new CreateOrderCommand()
            {
                CustomerId = "Katarina",
                CustomerPhone = "0653575051",
                ShippingAddress = new ShippingAddressDTO()
                {
                    City = "Palic",
                    Street = "Marka Oreskovica",
                    HouseNumber = "3A"
                }
            };

            var noDiscountService = new NoDiscountService();

            var discountPercentage = noDiscountService.GetDiscountPercentage(createOrderCommand);

            Assert.Equal(0, discountPercentage);

        }
    }
}
