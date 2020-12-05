using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Timesheets.Tests.Setup
{
    public class TestingWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {

        protected override IHostBuilder CreateHostBuilder()
        {
            var builder = Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                        .UseStartup<TStartup>()
                        .UseTestServer()
                        .ConfigureAppConfiguration(conf => conf.AddJsonFile("appsettings.json", optional: false).AddEnvironmentVariables())
                        .ConfigureTestServices(services => services.AddMvc().AddApplicationPart(typeof(TStartup).Assembly));
                });

            return builder;
        }
    }
}
