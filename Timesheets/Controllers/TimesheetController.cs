using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
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
            var timesheets = await _dbContext.TimeEntries.Where(x => x.Employee.Id == employeeId).ToListAsync();

            if (timesheets is null || timesheets.Count == 0)
                return NoContent();

            return Ok(timesheets);
        }

        [HttpPost]
        public async Task<IActionResult> InsertTimeSheetAsync(TimeEntry timeEntry)
        {
            await _dbContext.AddAsync(timeEntry);
            await _dbContext.SaveChangesAsync();

            return Created("time entry", timeEntry);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTimeEntryAsync(TimeEntry timeEntry)
        {
            var updated = _dbContext.Update(timeEntry);
            updated.State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();

            return Ok(updated.Entity);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteTimeEntryAsync(Guid timeEntryId)
        {
            var timeEntry = _dbContext.TimeEntries.FirstOrDefault(x => x.Id == timeEntryId);
            _dbContext.TimeEntries.Remove(timeEntry);
            await _dbContext.SaveChangesAsync();

            return Ok();
        }
    }

}