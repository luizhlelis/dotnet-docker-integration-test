using System;
using System.Net.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Hosting;
using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using System.IO;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.Configuration;

namespace Timesheets.Tests.Setup
{
    public class TestingCaseFixture<TStartup> : IDisposable where TStartup : class
    {
        private readonly TestingWebApplicationFactory<TStartup> _factory;
        protected readonly HttpClient Client;

        protected TimeSheetContext DbContext { get; }
        //private readonly IDbContextTransaction _transaction;

        public TestingCaseFixture()
        {
            // constructs the testing server with the HostBuilder configuration
            var testingWebApp = new TestingWebApplicationFactory<TestingStartup>();

            // Create an HttpClient to send requests to the TestServer
            Client = testingWebApp.CreateClient();

            //_transaction = DbContext.Database.BeginTransaction();
        }

        public void Dispose()
        {
            _factory?.Dispose();
            Client?.Dispose();

            //if (_transaction == null) return;

            //_transaction.Rollback();
            //_transaction.Dispose();
        }
    }
}
