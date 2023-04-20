using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Net.Mail;
using System.Text.Json;
using WebShop.Application.Contracts.Infrastructure;
using WebShop.Application.Models.SuppliersService;

namespace WebShop.Infrastructure.SupplierStock
{
    internal class SupplierStockService : ISupplierStockService
    {
        public SupplierStockService(IOptions<SuppliersServiceSettings> suppliersServiceSettings)
        {
            _suppliersServiceSettings = suppliersServiceSettings.Value;
        }

        private SuppliersServiceSettings _suppliersServiceSettings { get; }
        public async Task<bool> ReserveOnSuppliersStockAsync(ReserveOnSuppliersStockRequest request)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_suppliersServiceSettings.BaseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                                
                HttpResponseMessage response = await client.PostAsJsonAsync(_suppliersServiceSettings.ReserveProductQuantityEndpoint, request);

                if (response.StatusCode == System.Net.HttpStatusCode.Accepted || response.StatusCode == System.Net.HttpStatusCode.OK)
                    return true;

                return false;
            }

        }
    }
}
