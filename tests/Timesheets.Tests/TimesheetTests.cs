using System;
using Timesheets.Tests.Setup;
using Xunit;

namespace Timesheets.Tests
{
    [Collection(nameof(IntegrationTestsFixtureCollection))]
    public class TimesheetTests
    {
        private readonly IntegrationTestsFixture<StartupTests> _fixture;

        public TimesheetTests(IntegrationTestsFixture<StartupTests> fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void Test1()
        {
        }
    }
}
