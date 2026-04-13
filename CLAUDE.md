# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Commands

```bash
# Build
dotnet build FiapGames.slnx

# Run API (Swagger UI available at http://localhost:5062 in Development)
dotnet run --project FiapCloudGames.API

# Run all tests
dotnet test FiapCloudGames.Tests/FiapCloudGames.Tests.csproj

# Run a specific test class
dotnet test --filter "FiapCloudGames.Tests.Domain.UserEntityTests"

# Run a specific test method
dotnet test --filter "FullyQualifiedName~MethodName"

# Apply EF Core migrations (run from repo root)
dotnet ef database update --project FiapCloudGames.Infrastructure --startup-project FiapCloudGames.API

# Add a new migration
dotnet ef migrations add MigrationName --project FiapCloudGames.Infrastructure --startup-project FiapCloudGames.API
```

## Architecture

This project follows Clean Architecture with four main layers (outer layers depend on inner ones):

**Domain** (`FiapCloudGames.Domain`) — innermost, no external dependencies
- Entities: `User`, `Game`
- `Email` value object with regex validation
- `UserRole` enum: `User`, `Admin`
- `DomainException` for business rule violations
- Repository interfaces: `IUserRepository`, `IGameRepository`

**Application** (`FiapCloudGames.Application`)
- Services: `UserService`, `GameService`, `AuthService`
- Service interfaces: `IUserService`, `IGameService`, `IAuthService`, `IJwtTokenService`
- DTOs for input/output (separate from domain entities)

**Infrastructure** (`FiapCloudGames.Infrastructure`)
- `AppDbContext` — EF Core with SQL Server; auto-seeds on startup (admin + 5 users + 20 games)
- Repositories implement domain interfaces
- `JwtTokenService` — JWT generation/validation
- `InfrastructureExtensions` — registers all infra DI

**API** (`FiapCloudGames.API`)
- Controllers: `AuthController`, `UserController`, `GameController`
- `ErrorHandlingMiddleware` — catches `DomainException` and maps to HTTP responses
- `Program.cs` — configures all services, JWT auth, Swagger

## Key Conventions

- **Business rules live in entities**, not services. Validation (password rules, email format, etc.) is enforced in domain entity constructors/methods and throws `DomainException` on failure.
- **Controllers are thin** — they delegate entirely to application services.
- **Authorization:** endpoints use `[Authorize]` for authenticated users and `[Authorize(Roles = "Admin")]` for admin-only operations. The `POST /users` (register) endpoint is public.
- **Password hashing:** BCrypt.Net-Next is used in `UserService`, not in the domain entity.
- **Seeded credentials (development):**
  - Admin: `admin@fcg.com` / `Admin@123`
  - Users: e.g. `yuri@fcg.com` / `Fiap@123`

## Database

- SQL Server LocalDB by default: `(localdb)\MSSQLLocalDB;Initial Catalog=FiapCloudGamesDB`
- Connection string is in `FiapCloudGames.API/appsettings.json`
- `AppDbContext.SeedAsync()` is called on startup and is idempotent

## Tests

Tests use **xUnit** + **Moq**. Tests are organized to mirror the source:
- `FiapCloudGames.Tests/Domain/` — entity-level tests (no mocks needed)
- `FiapCloudGames.Tests/Application/` — service tests with mocked repositories

Global usings for xUnit are in `GlobalUsings.cs`.
