using System;
using System.Net.Http;
using Xunit;

namespace Timesheets.Tests.Setup
{
    public class IntegrationTestsFixture<TStartup> : IClassFixture<TimesheetsApiFactory<TStartup>>, IDisposable where TStartup : class
    {
        private readonly TimesheetsApiFactory<TStartup> _factory;
        protected readonly HttpClient Client;

        public IntegrationTestsFixture()
        {
            _factory = new TimesheetsApiFactory<TStartup>();
            Client = _factory.CreateClient();
        }

        public void Dispose()
        {
            _factory?.Dispose();
            Client?.Dispose();
        }
    }
}
