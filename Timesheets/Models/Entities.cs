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
        public string Name { get; set; }
        public string Registration { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
        public virtual ICollection<TimeEntry> TimeEntries { get; set; }
    }

    public class Project : Entity
    {
        public string Name { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<TimeEntry> TimeEntries { get; set; }
    }

    public class TimeEntry : Entity
    {
        public DateTime Date { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
        public Guid ProjectId { get; set; }
        public Guid EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual Project Project { get; set; }
    }
}