# MVCProject.ITI (SmartTrip)

ASP.NET Core MVC web application for planning trips, estimating trip cost, and tracking driving activity.

Smart Trip Cost Analyzer helps drivers understand the real cost of every journey before they start driving. It combines route distance and duration, vehicle fuel/energy consumption, AC usage, and current fuel assumptions to estimate trip expenses and expected travel effort. The platform is designed for everyday users who want to plan smarter trips and for admins who need visibility into usage trends and cost-related data.

At a glance, the system supports:
- Trip planning with cost-aware route insights
- Personal vehicle profiles to improve estimate accuracy
- History and analytics to compare driving behavior over time
- Admin monitoring tools for fuel data, users, and trip activity

## Tech Stack
- .NET 9 (ASP.NET Core MVC + Razor Pages)
- Entity Framework Core (SQL Server)
- ASP.NET Core Identity (authentication/authorization)
- AutoMapper
- MailKit/MimeKit (email sender)

## Main Features
- User authentication and account management with Identity
- Vehicle garage management (add, edit, delete, set default vehicle)
- Start trips with route and trip-cost calculation
- Trip history and completion summary
- Analytics dashboard for user trip statistics
- Admin area for managing users, vehicles, trips, fuel prices, and fuel efficiency

## Project Structure
- `MVCProject/MVCProject.sln` — solution file
- `MVCProject/MVCProject.ITI` — main web app
  - `Controllers` — user-facing MVC controllers
  - `Areas/Admin` — admin dashboards and management pages
  - `DataAccessLayer` — EF Core context, entities, repositories, migrations
  - `Serviceslayer` — application/business services
  - `ViewModels` — view model classes
  - `Views` — Razor views

## Prerequisites
- [.NET SDK 9.0](https://dotnet.microsoft.com/download)
- SQL Server (local or remote)

## Configuration
1. Open:
   - `MVCProject/MVCProject.ITI/appsettings.json`
2. Set the required values:
   - `ConnectionStrings:DefaultConnection`
   - `Admin:SeedEmail`, `Admin:SeedPassword`, `Admin:SeedFullName`
   - `Email:*` (SMTP settings)
   - `ExternalApis:*` (weather and route service keys)

> Security note: do not commit real secrets. Use environment variables or .NET user secrets for sensitive values.

## Database Setup
From repository root:

```bash
cd MVCProject/MVCProject.ITI
dotnet ef database update
```

## Run the Application
From repository root:

```bash
dotnet run --project MVCProject/MVCProject.ITI/MVCProject.ITI.csproj
```

Then open the local URL printed in the terminal.

## Build and Test
From repository root:

```bash
dotnet build MVCProject/MVCProject.sln
dotnet test MVCProject/MVCProject.sln
```

## Notes
- Admin role seeding runs automatically on application startup.
- If email confirmation is enabled, make sure SMTP configuration is valid.
