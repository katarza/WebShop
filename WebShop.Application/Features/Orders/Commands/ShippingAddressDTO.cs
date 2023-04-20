using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Application.Features.Orders.Commands
{
    public class ShippingAddressDTO
    {
        public string City { get; set; } = string.Empty;

        public string Street { get; set; } = string.Empty;

        public string HouseNumber { get; set; } = string.Empty;
    }
}
