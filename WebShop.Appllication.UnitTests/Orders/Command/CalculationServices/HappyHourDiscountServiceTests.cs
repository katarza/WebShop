using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Application.Features.Orders.Commands.CalculationServices;
using WebShop.Application.Features.Orders.Commands;

namespace WebShop.Appllication.UnitTests.Orders.Command.CalculationServices
{
    public class HappyHourDiscountServiceTests
    {
        [Fact]
        void GetDiscountPercentage_Calculation_OddShouldReturn10()
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

            var happyHourDiscountService = new HappyHourDiscountService();

            var discountPercentage = happyHourDiscountService.GetDiscountPercentage(createOrderCommand);

            Assert.Equal(10, discountPercentage);

        }

        [Fact]
        void GetDiscountPercentage_Calculation_EvenShouldReturn20()
        {
            var createOrderCommand = new CreateOrderCommand()
            {
                CustomerId = "Katarina",
                CustomerPhone = "0653575052",
                ShippingAddress = new ShippingAddressDTO()
                {
                    City = "Palic",
                    Street = "Marka Oreskovica",
                    HouseNumber = "3A"
                }
            };

            var happyHourDiscountService = new HappyHourDiscountService();

            var discountPercentage = happyHourDiscountService.GetDiscountPercentage(createOrderCommand);

            Assert.Equal(20, discountPercentage);

        }

        [Fact]
        void GetDiscountPercentage_Calculation_ZeroShouldReturn30()
        {
            var createOrderCommand = new CreateOrderCommand()
            {
                CustomerId = "Katarina",
                CustomerPhone = "0653575050",
                ShippingAddress = new ShippingAddressDTO()
                {
                    City = "Palic",
                    Street = "Marka Oreskovica",
                    HouseNumber = "3A"
                }
            };

            var happyHourDiscountService = new HappyHourDiscountService();

            var discountPercentage = happyHourDiscountService.GetDiscountPercentage(createOrderCommand);

            Assert.Equal(30, discountPercentage);

        }
    }
}
