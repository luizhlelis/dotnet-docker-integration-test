using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Timesheets.Models;

namespace Timesheets
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Timesheets", Version = "v1" });
            });
            services.AddDbContext<TimeSheetContext>(options =>
                options
                    .UseSqlServer(Configuration.GetConnectionString("TimeSheetContext"))
                    .LogTo(Console.WriteLine));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Timesheets v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            Seed(app);
        }

        private static void Seed(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            using var context = serviceScope.ServiceProvider.GetService<TimeSheetContext>();

            context.Database.EnsureCreated();

            if (context.Employees.Any()) return;

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
                Registration = "000002",
                Projects = new List<Project>
                {
                    project1,
                    project2
                },
                TimeEntries = new List<TimeEntry>()
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
            };
            var employee2 = new Employee
            {
                Id = Guid.Parse("996b58c5-b711-42e5-b422-cbfe8ee2af43"),
                Name = "Maria",
                Registration = "000001",
                Projects = new List<Project>
                {
                    project1,
                    project3
                }
            };
            context.AddRange(employee1, employee2);
            context.SaveChanges();
        }
    }
}
