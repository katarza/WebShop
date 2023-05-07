using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

namespace WebShop.API.IntegrationTests.Base
{
    public class CustomWebApplicationFactory<TStartup>
            : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            //builder.ConfigureServices(services =>
            //{

            //    services.AddDbContext<WebShopDbContext>(options =>
            //    {
            //        options.UseInMemoryDatabase("WebShopDbContextInMemoryTest");
            //    });

            //    var sp = services.BuildServiceProvider();

            //    using (var scope = sp.CreateScope())
            //    {
            //        var scopedServices = scope.ServiceProvider;
            //        var context = scopedServices.GetRequiredService<WebShopDbContext>();                   
                    
            //        context.Database.EnsureCreated();
                                      
            //        Utilities.InitializeDbForTests(context);
                                       
            //    };
            //});
        }

        public HttpClient GetAnonymousClient()
        {
            return CreateClient();
        }

    }
}
