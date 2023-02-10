# BillShare

Project to share bill with your friends and manage your expenses

## Pre-requirements
1. [.NET 7.0](https://dotnet.microsoft.com/en-us/download/dotnet/7.0)
2. [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
3. [EF Core](https://learn.microsoft.com/en-us/ef/core/cli/dotnet), install with command `dotnet tool install --global dotnet-ef`

## Setup
1. Open project folder
2. Change connection string in appsettings.json or appsettings.Development.json, connection string template: `Server=<server name>;Database=BillShareTest;Trusted_Connection=True;TrustServerCertificate=True;`
3. Apply migrations with command `dotnet ef database update -p Infrastructure -s BillShare`
4. Run application with command `dotnet run`
