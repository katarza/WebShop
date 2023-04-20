using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Domain.Entities;

namespace WebShop.Application.Features.Orders.Commands
{
    public class OrderDTO
    {
        public string CustomerId { get; set; } = string.Empty;

        public ShippingAddressDTO ShippingAddress { get; set; } = new ShippingAddressDTO();

        public string CustomerPhone { get; set; } = string.Empty;

        public double TotalAmount { get; set; }

        public double AppliedDiscount
        {
            get; set;
        }
    }
}
