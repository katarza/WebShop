using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using WebShop.Application.Contracts.Infrastructure;
using WebShop.Application.Models.SuppliersService;

namespace WebShop.Infrastructure.SupplierStock
{
    internal class SupplierStockService : ISupplierStockService
    {
        private SuppliersServiceSettings _suppliersServiceSettings { get; }
        public ILogger<SupplierStockService> _logger { get; }
        private readonly HttpClient _httpClient;

        public SupplierStockService(IOptions<SuppliersServiceSettings> suppliersServiceSettings, ILogger<SupplierStockService> logger, HttpClient httpClient)
        {
            _suppliersServiceSettings = suppliersServiceSettings.Value;
            _logger = logger;
            _httpClient = httpClient;
        }

        
        public async Task<bool> ReserveOnSuppliersStockAsync(ReserveOnSuppliersStockRequest request)
        {
            _httpClient.BaseAddress = new Uri(_suppliersServiceSettings.BaseUrl);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                                
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync(_suppliersServiceSettings.ReserveProductQuantityEndpoint, request);

            _logger.LogInformation("Reserve product quantity on external stock");

            if (response.StatusCode == System.Net.HttpStatusCode.Accepted || response.StatusCode == System.Net.HttpStatusCode.OK)
                return true;

            _logger.LogInformation("External product quantity stock reservation failed");

            return false;

        }
    }
}
