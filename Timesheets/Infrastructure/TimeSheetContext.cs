using Timesheets.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Timesheets
{
    public class TimeSheetContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<TimeEntry> TimeEntries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var project1 = new Project
            {
                Id = Guid.Parse("40979172-beb3-4111-b429-d1affcc4826e"),
                Name = "Internal Activities"
            };
            var project2 = new Project
            {
                Id = Guid.Parse("d567a6ce-aff3-481b-a7d0-6e63bfc31eee"),
                Name = "Ecommerce"
            };
            var project3 = new Project
            {
                Id = Guid.Parse("d1e31c6a-57e3-4d15-85bf-b0b4d8f01d2c"),
                Name = "Mobile App"
            };

            modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    Id = Guid.Parse("05a41567-b511-441b-b6aa-b74f41fb7a09"),
                    Name = "João",
                    Registration = "000002",
                    Projects = new List<Project>
                    {
                        project1,
                        project2
                    },
                    TimeEntries = new List<TimeEntry>
                    {
                        new TimeEntry
                        {
                            Date = new DateTime(2020, 10, 1),
                            Project = project1,
                            Start = "09:00",
                            End = "11:30"
                        },
                        new TimeEntry
                        {
                            Date = new DateTime(2020, 10, 1),
                            Project = project1,
                            Start = "12:30",
                            End = "16:00"
                        }
                    }
                },
                new Employee
                {
                    Id = Guid.Parse("996b58c5-b711-42e5-b422-cbfe8ee2af43"),
                    Name = "Maria",
                    Registration = "000001",
                    Projects = new List<Project>
                    {
                        project1,
                        project3
                    }
                });
        }
    }
}