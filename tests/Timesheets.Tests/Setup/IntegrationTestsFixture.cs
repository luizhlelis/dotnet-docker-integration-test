using System;
using System.Net.Http;
using Xunit;

namespace Timesheets.Tests.Setup
{
    [CollectionDefinition(nameof(IntegrationTestsFixtureCollection))]
    public class IntegrationTestsFixtureCollection : ICollectionFixture<IntegrationTestsFixture<StartupTests>>
    { }

    public class IntegrationTestsFixture<TStartup> : IDisposable where TStartup : class
    {
        private readonly TimesheetsApiFactory<TStartup> _factory;
        public HttpClient Client;

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
