# Expense Tracker API

A simple ASP.NET Core Web API for tracking expenses with user authentication.

## 🧱 Project Structure

The solution is divided into multiple projects to separate concerns:

- **Contracts** – interface definitions for repositories.
- **Entities** – domain models and EF Core configurations.
- **Repository** – data access layer using Entity Framework Core.
- **Service.Contracts** – service interfaces.
- **Service** – business logic implementations.
- **ExpenseTrackerApi** – the main API project hosting controllers and app configuration.
- **ExpenseTrackerApi.Presentation** – additional presentation artifacts having the controllers (e.g. Swagger XML).
- **Shared** – common DTOs.

Each project targets .NET 10 and uses a shared `ExpenseTrackerApi.slnx` solution file.

## 🚀 Getting Started

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/en-us/download)
- A supported database (SQLite/SQL Server/PostgreSQL) – configured via `appsettings.json`.

### Setup

1. **Clone the repository**
   ```bash
   git clone <repo-url>
   cd ExpenseTrackerAPI
   ```

2. **Restore packages**
   ```bash
   dotnet restore
   ```

3. **Configure database**

   Update the connection string in `ExpenseTrackerApi/appsettings.json` or use environment
   variables. The default uses LocalDB/SQLite depending on provider configuration in
   `Program.cs`.

4. **Apply migrations and seed data**
   ```bash
   cd ExpenseTrackerApi
   dotnet ef database update
   ```

5. **Run the application**
   ```bash
   dotnet watch run --project ExpenseTrackerApi
   ```

   The API will be available at `https://localhost:5001` (or the port shown in output).

## 📦 Features

- JWT-based authentication & authorization.
- CRUD operations for expenses linked to users.
- Expense categories with enum mapping.
- Clean architecture with layered projects.

## 🛠️ Development Notes

- Controllers are located in `ExpenseTrackerApi.Presentation/Controllers`.
- Services are injected via `ServiceExtensions` in `ExpenseTrackerApi/Extensions`.
- Entity configurations and migrations are in `ExpenseTrackerApi/Migrations`.
- Add new services or repositories by extending the interface contracts and registering
  them in `ServiceExtensions`.

## Inspiration

This project is based on the [Expense Tracker](https://roadmap.sh/projects/expense-tracker-api) project idea from [roadmap.sh](https://roadmap.sh).


---

Feel free to explore and extend this API for your own expense tracking needs!
