using System;
using System.Collections.Generic;

namespace Timesheets.Models
{
    public class Entity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
    }

    public class Employee : Entity
    {
        public string Name { get; init; }
        public string Registration { get; init; }
        public virtual ICollection<Project> Projects { get; init; }
        public virtual ICollection<TimeEntry> TimeEntries { get; init; }
    }

    public class Project : Entity
    {
        public string Name { get; init; }
        public virtual ICollection<Employee> Employees { get; init; }
        public virtual ICollection<TimeEntry> TimeEntries { get; init; }
    }

    public class TimeEntry : Entity
    {
        public DateTime Date { get; init; }
        public TimeSpan Start { get; init; }
        public TimeSpan End { get; init; }
        public Guid ProjectId { get; init; }
        public Guid EmployeeId { get; init; }
        public virtual Employee Employee { get; init; }
        public virtual Project Project { get; init; }
    }
}