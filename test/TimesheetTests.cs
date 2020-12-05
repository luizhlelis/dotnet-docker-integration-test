using FluentAssertions;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Timesheets.Models;
using Timesheets.Tests.Setup;
using Xunit;

namespace Timesheets.Tests
{
    public class TimesheetTests : TestingCaseFixture<TestingStartup>
    {
        [Fact]
        public void ReturnsEmptyTimesheetsList()
        {
            var employeeId = "e978f409-9955-497d-8f97-917dfc054b80";
            var response = Client.GetAsync($"/api/Timesheet?employeeId={employeeId}");

            response.Result.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public void ReturnsTimesheetsList()
        {
            var employeeId = "05a41567-b511-441b-b6aa-b74f41fb7a09";
            var response = Client.GetAsync($"/api/Timesheet?employeeId={employeeId}");

            response.Result.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task ReturnsBadRequestOnInsertValidationFail()
        {
            var timeEntry = new TimeEntry()
            {
                Date = DateTime.Now,
                Start = TimeSpan.FromHours(18),
                End = TimeSpan.FromHours(10),
                EmployeeId = Guid.Empty,
                ProjectId = Guid.Empty,
            };

            HttpRequestMessage request = new HttpRequestMessage(new HttpMethod("post"), "/api/Timesheet");
            request.Content = new StringContent(JsonConvert.SerializeObject(timeEntry), Encoding.UTF8, "application/json");
            var response = await Client.SendAsync(request);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task ReturnsOKOnInsert()
        {
            var timeEntry = new TimeEntry()
            {
                Date = DateTime.Now,
                Start = TimeSpan.FromHours(09),
                End = TimeSpan.FromHours(12),
                EmployeeId = Guid.Parse("996b58c5-b711-42e5-b422-cbfe8ee2af43"),
                ProjectId = Guid.Parse("d567a6ce-aff3-481b-a7d0-6e63bfc31eee"),
            };

            HttpRequestMessage request = new HttpRequestMessage(new HttpMethod("post"), "/api/Timesheet");
            request.Content = new StringContent(JsonConvert.SerializeObject(timeEntry), Encoding.UTF8, "application/json");
            var response = await Client.SendAsync(request);

            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Fact]
        public void ReturnsNotFoundOnDelete()
        {
            var response = Client.DeleteAsync($"/api/Timesheet?timeEntryId={Guid.NewGuid().ToString()}");

            response.Result.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
