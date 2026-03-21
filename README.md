# Expense Tracker API

[![.NET](https://img.shields.io/badge/.NET-10-orange)](https://dotnet.microsoft.com/) [![ASP.NET](https://img.shields.io/badge/ASP.NET-blueviolet)](https://dotnet.microsoft.com/) [![Docker](https://img.shields.io/badge/Docker-blue)](https://docker.com/) [![PostgreSQL](https://img.shields.io/badge/PostgreSQL-green)](https://postgresql.org/)

**Clean Architecture ASP.NET Core 10 Web API** for personal expense tracking. Features JWT auth, EF Core PostgreSQL, Swagger docs, layered design (Repository/Service patterns).

## 🛠️ Tech Stack

| Category  | Tech                         |
| --------- | ---------------------------- |
| Framework | ASP.NET Core 10              |
| ORM       | EF Core + Npgsql             |
| DB        | PostgreSQL/SQLite/SQL Server |
| Auth      | JWT + ASP.NET Identity       |
| Docs      | Swagger/OpenAPI              |
| Logging   | Serilog                      |
| Mapping   | AutoMapper                   |

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

### 🚀 Quick Start

**Prerequisites**:

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- PostgreSQL (or SQLite/SQL Server)
- EF Tools: `dotnet tool install --global dotnet-ef`

**1. Setup**

```bash
git clone <repo> && cd ExpenseTrackerAPI
dotnet restore
```

**2. Config** (ExpenseTrackerApi/appsettings.json or env):

```
ConnectionStrings:sqlConnection = "Host=localhost;Database=ExpenseTracker;Username=postgres;Password=pass"
JwtSettings:SecretKey = "your-32+char-secret" (or SECRETExpense env)
```

**3. DB & Run**

```bash
cd ExpenseTrackerApi
dotnet ef database update
dotnet run
```

Ports: **HTTPS 5001**, HTTP 5002. Swagger: **/swagger**

**Test Admin**: Register/login via /api/authentication, use Bearer token.

## 🔌 API Endpoints (Bearer Auth for /api/expenses)

| Method | Endpoint              | Desc           |
| ------ | --------------------- | -------------- |
| GET    | `/api/expenses`       | List expenses  |
| GET    | `/api/expenses/{id}`  | Get expense    |
| POST   | `/api/expenses`       | Create expense |
| PUT    | `/api/expenses/{id}`  | Update expense |
| DELETE | `/api/expenses/{id}`  | Delete expense |
| POST   | `/api/authentication` | Register/login |

**Quick Test**:

```bash
# Login (get token)
TOKEN=$(curl -s -X POST http://localhost:5002/api/authentication/login \\
  -H "Content-Type: application/json" -d '{"email":"test@test.com","password":"Test123456"}' | jq -r .token)

# Get expenses
curl http://localhost:5002/api/expenses -H "Authorization: Bearer $TOKEN"
```

## 📦 Features

- JWT auth (SECRETExpense env)
- User-scoped expense CRUD + categories
- EF migrations (Npgsql/PostgreSQL)
- Swagger docs + XML comments
- Serilog logging

## 🐳 Docker

```bash
docker build -t expense-tracker .
docker run -p 5001:5001 -e ConnectionStrings__sqlConnection="your-pg-conn" -e SECRETExpense="your-secret" expense-tracker
```

## 🧪 Testing

Add xUnit:

```bash
dotnet new xunit -n ExpenseTracker.Tests
dotnet add ExpenseTracker.Tests reference Service Service.Contracts Entities
dotnet test
```

## 🚀 Deployment

- **Azure**: `dotnet publish -c Release`
- Env: DB conn, SECRETExpense, ASPNETCORE_ENVIRONMENT=Production
- Disable dev CORS/Swagger if needed

## 🤝 Contributing

1. Fork/branch (feat/...)
2. `dotnet ef migrations add Name`
3. Test/PR

MIT License.

**Inspired by**: [roadmap.sh Expense Tracker](https://roadmap.sh/expense-tracker-api)
