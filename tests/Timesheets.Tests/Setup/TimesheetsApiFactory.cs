using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Timesheets.Tests.Setup
{
    public class TimesheetsApiFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseStartup<TStartup>();
            //builder.UseEnvironment("Testing");
        }
    }
}
