using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;

namespace Timesheets.Tests.Setup
{
    public class TestingCaseFixture<TStartup> : IDisposable where TStartup : class
    {
        private readonly TestingWebApplicationFactory<TStartup> _factory;
        protected readonly HttpClient Client;
        
        protected TimeSheetContext DbContext { get; }
        private readonly IDbContextTransaction _transaction;

        public TestingCaseFixture()
        {
            // constructs the testing server with the HostBuilder configuration
            _factory = new TestingWebApplicationFactory<TStartup>();

            // Create an HttpClient to send requests to the TestServer
            Client = _factory.CreateClient();

            DbContext = _factory.Services.GetRequiredService<TimeSheetContext>();

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
