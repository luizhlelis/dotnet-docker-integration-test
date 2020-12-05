using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;

namespace Timesheets.Tests.Setup
{
    public class TestingCaseFixture<TStartup> : IDisposable where TStartup : class
    {
        protected readonly HttpClient Client;
        protected TimeSheetContext DbContext { get; }
        private readonly IDbContextTransaction _transaction;

        public TestingCaseFixture()
        {
            // constructs the testing server with the HostBuilder configuration
            var testingWebApp = new TestingWebApplicationFactory<TestingStartup>();

            // Create an HttpClient to send requests to the TestServer
            Client = testingWebApp.CreateClient();

            DbContext = testingWebApp.Services.GetRequiredService<TimeSheetContext>();

            // Open a transaction to not commit tests changes to db
            _transaction = DbContext.Database.BeginTransaction();
        }

        public void Dispose()
        {
            Client?.Dispose();

            if (_transaction == null) return;

            _transaction.Rollback();
            _transaction.Dispose();
        }
    }
}
