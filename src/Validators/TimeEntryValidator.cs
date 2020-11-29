using FluentValidation;
using Timesheets.Models;

namespace Timesheets.Validators
{
    public class TimeEntryValidator : AbstractValidator<TimeEntry>
    {
        public TimeEntryValidator()
        {
            RuleFor(x => x.Date).NotNull().NotEmpty();
            RuleFor(x => x.EmployeeId).NotNull().NotEmpty();
            RuleFor(x => x.Start).NotNull().NotEmpty().LessThan(x => x.End);
            RuleFor(x => x.End).NotNull().NotEmpty().GreaterThan(x => x.Start);
            RuleFor(x => x.ProjectId).NotNull().NotEmpty();
        }
    }
}
