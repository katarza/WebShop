using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Application.Models.SuppliersService
{
    public class SuppliersServiceSettings
    {
        public string BaseUrl { get; set; } = string.Empty;
        public string ReserveProductQuantityEndpoint { get; set; } = string.Empty;
    }
}
