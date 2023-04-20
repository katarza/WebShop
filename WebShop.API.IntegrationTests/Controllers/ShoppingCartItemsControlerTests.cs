using Microsoft.VisualStudio.TestPlatform.TestHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.API.IntegrationTests.Base;
using System.Text.Json;
using WebShop.Application.Features.ShoppingCartItems.Queries.GetShoppingCartItemsList;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;

namespace WebShop.API.IntegrationTests.Controllers
{
    public class ShoppingCartItemsControlerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        
        private HttpClient client;
        public ShoppingCartItemsControlerTests()
        {
            var webApplicationFactory = new WebApplicationFactory<Program>();
            client = webApplicationFactory.CreateDefaultClient();
        }
        

        [Fact]
        public async Task GetShoppingCartItems_Invoke_ReturnsSuccessResult()
        {
            
            var response = await client.GetAsync("API/ShoppingCart/Items");

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<List<ShoppingCardItemsListVM>>(responseString);

            Assert.IsType<List<ShoppingCardItemsListVM>>(result);
            Assert.NotEmpty(result);
        }
    }
}
