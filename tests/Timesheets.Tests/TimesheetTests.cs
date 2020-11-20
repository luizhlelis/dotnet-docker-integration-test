using FluentAssertions;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using Timesheets.Models;
using Timesheets.Tests.Setup;
using Xunit;

namespace Timesheets.Tests
{
    [Collection(nameof(IntegrationTestsFixtureCollection))]
    public class TimesheetTests
    {
        private readonly IntegrationTestsFixture<Startup> _fixture;

        public TimesheetTests(IntegrationTestsFixture<Startup> fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void ReturnsEmptyTimesheetsList()
        {
            var employeeId = "996b58c5-b711-42e5-b422-cbfe8ee2af43";
            var response = _fixture.Client.GetAsync($"/api/Timesheet?employeeId={employeeId}");

            response.Result.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public void ReturnsTimesheetsList()
        {
            var employeeId = "05a41567-b511-441b-b6aa-b74f41fb7a09";
            var response = _fixture.Client.GetAsync($"/api/Timesheet?employeeId={employeeId}");

            response.Result.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public void ReturnsBadRequestOnInsertValidationFail()
        {
            var timeEntry = new TimeEntry()
            {
                Date = DateTime.Now,
                Start = TimeSpan.FromHours(18),
                End = TimeSpan.FromHours(10),
                EmployeeId = Guid.Empty,
                ProjectId = Guid.Empty,
            };

            var response = _fixture.Client.PostAsync("/api/Timesheet", new StringContent(JsonConvert.SerializeObject(timeEntry)));

            response.Result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
