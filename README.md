# dotnet-docker-integration-test

### Summary
Sample ASP NET Core 5 API using docker-compose to run integration tests. The API is a timesheet system sample that the employee of a company launch worked hours. Developed using [.NET 5](https://dotnet.microsoft.com/download/dotnet/5.0), SqlServer with [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/what-is-new/ef-core-5.0/whatsnew) and [Docker](https://hub.docker.com/_/microsoft-dotnet-core) (containerized SQLServer and .NET 5 SDK).

### Commands
To restore nuget packages (inside api folder):
- nuget restore Timesheets.sln

This command will generate the containerized Databases (mongo and SQLServer) and will up SqlServer Seed Container wich will update SqlSever Database with migrations. MongoDb will initialize with "init-mongo.js" file configuration:
- docker-compose up
- docker-compose up -d (if you want to run in background mode)

To run application (from Timesheets folder where the API is located):
- dotnet run
