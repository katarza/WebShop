using System.Text.Json;
using WebShop.Application.Features.ShoppingCartItems.Queries.GetShoppingCartItemsList;
using Microsoft.AspNetCore.Mvc.Testing;

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
