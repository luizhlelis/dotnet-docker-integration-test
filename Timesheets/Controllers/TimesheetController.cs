using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Threading.Tasks;
using Timesheets;
using Timesheets.Models;

namespace DotnetDockerIntegrationTests.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimesheetController : ControllerBase
    {
        private readonly TimeSheetContext _dbContext;

        public TimesheetController(TimeSheetContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetTimesheetsByEmployeeAsync(Guid employeeId)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public async Task<IActionResult> InsertTimeSheetAsync(TimeEntry timeEntry)
        {
            throw new NotImplementedException();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTimeEntryAsync(Guid timeEntryId, TimeEntry timeEntry)
        {
            throw new NotImplementedException();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteTimeEntryAsync(Guid timeEntryId)
        {
            throw new NotImplementedException();
        }
    }

}