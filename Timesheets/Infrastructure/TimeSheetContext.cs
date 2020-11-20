using Microsoft.EntityFrameworkCore;
using System;
using Timesheets.Models;

namespace Timesheets
{
    public class TimeSheetContext : DbContext
    {
        public TimeSheetContext(DbContextOptions<TimeSheetContext> options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<TimeEntry> TimeEntries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            Seed(modelBuilder);
        }

        private static void Seed(ModelBuilder modelBuilder)
        {
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
            var employee1 = new Employee
            {
                Id = Guid.Parse("05a41567-b511-441b-b6aa-b74f41fb7a09"),
                Name = "João",
                Registration = "000002"
            };
            var employee2 = new Employee
            {
                Id = Guid.Parse("996b58c5-b711-42e5-b422-cbfe8ee2af43"),
                Name = "Maria",
                Registration = "000001"
            };
            modelBuilder.Entity<Project>().HasData(
                project1,
                project2,
                project3);
            modelBuilder.Entity<Employee>().HasData(employee1, employee2);
            modelBuilder.Entity("EmployeeProject").HasData(
                new
                {
                    EmployeesId = employee1.Id,
                    ProjectsId = project1.Id
                },
                new
                {
                    EmployeesId = employee1.Id,
                    ProjectsId = project2.Id
                },
                new
                {
                    EmployeesId = employee2.Id,
                    ProjectsId = project1.Id
                },
                new
                {
                    EmployeesId = employee2.Id,
                    ProjectsId = project3.Id
                }
            );
            modelBuilder.Entity<TimeEntry>().HasData(
                new TimeEntry
                {
                    Date = new DateTime(2020, 10, 01),
                    EmployeeId = employee1.Id,
                    ProjectId = project1.Id,
                    Start = TimeSpan.FromHours(09),
                    End = TimeSpan.FromHours(12)
                },
                new TimeEntry
                {
                    Date = new DateTime(2020, 10, 01),
                    EmployeeId = employee1.Id,
                    ProjectId = project2.Id,
                    Start = TimeSpan.FromHours(13),
                    End = TimeSpan.FromHours(17)
                },
                new TimeEntry
                {
                    Date = new DateTime(2020, 10, 02),
                    EmployeeId = employee1.Id,
                    ProjectId = project2.Id,
                    Start = TimeSpan.FromHours(09),
                    End = TimeSpan.FromHours(12)
                });
        }
    }
}