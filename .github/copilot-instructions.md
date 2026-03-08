# Copilot Instructions for WebAPIShop

## Project Overview
WebAPIShop is a robust RESTful Web API for an e-commerce platform built with .NET 9. It manages users, products, orders, categories, branches, and ratings using a clean 3-layer architecture (Presentation, Service, Repository) with asynchronous operations, Entity Framework Core (database-first), and comprehensive testing.

## Tech Stack
- **Framework**: .NET 9.0 Web API
- **Language**: C# (async/await patterns)
- **Database**: SQL Server (Express/LocalDB)
- **ORM**: Entity Framework Core (database-first approach)
- **Mapping**: AutoMapper for entity-DTO conversions
- **Logging**: NLog (file and email logging)
- **Testing**: xUnit with Moq and EF In-Memory
- **Documentation**: OpenAPI/Swagger
- **Security**: zxcvbn-core for password validation

## Project Structure
- **WebApiShop/**: Presentation layer (controllers, middleware, Program.cs)
- **Service/**: Business logic layer (services, AutoMapper profiles)
- **Repository/**: Data access layer (EF Core context, repositories) - see `repository-layer-instructions.md` for detailed guidelines
- **Entity/**: Domain entities (EF models)
- **DTOs/**: Data Transfer Objects (immutable records)
- **TestProject/**: Unit and integration tests

## Architecture Patterns
- **Dependency Injection**: All services/repositories registered in Program.cs
- **Layered Architecture**: Controllers → Services → Repositories → Database
- **DTO Pattern**: Use records for DTOs, AutoMapper for conversions
- **Async Operations**: All I/O operations are asynchronous
- **Global Exception Handling**: Custom ErrorHandlingMiddleware
- **Request Monitoring**: RatingMiddleware logs all API requests

## Coding Guidelines
- Use async/await for all database and I/O operations
- Implement interfaces for services and repositories (e.g., IUserService, IUserRepository)
- Use DTOs for all API inputs/outputs to prevent over-posting
- Follow naming conventions: PascalCase for classes/methods, camelCase for parameters
- Use meaningful variable names and add comments for complex logic
- Handle exceptions gracefully and log errors
- Write unit tests for all new functionality

## Build and Run
- **Build**: `dotnet build WebAPIShop.sln`
- **Run API**: `cd WebApiShop && dotnet run`
- **Run Tests**: `dotnet test`
- **Database**: Ensure SQL Server Express is running; connection string in `appsettings.Development.json`
- **Swagger**: Available at `/swagger` in development mode

## Development Workflow
1. Create/modify entities in Entity/ (if database schema changes)
2. Update DTOs in DTOs/ for new API contracts
3. Add AutoMapper profiles in Service/AutoMapping.cs
4. Implement repository methods in Repository/
5. Add business logic in Service/
6. Create controllers in WebApiShop/Controllers/
7. Register new services/repositories in Program.cs
8. Write tests in TestProject/
9. Update README.md if needed

## Common Tasks
- **Add new entity**: Create class in Entity/, add DbSet in context, create repository/service/DTOs
- **Add API endpoint**: Create controller method, ensure proper routing and HTTP verbs
- **Database changes**: Use EF migrations or update model from database
- **Testing**: Use Moq for mocking, EF In-Memory for integration tests
- **Logging**: Use ILogger injected into services/controllers

## Configuration
- **Connection Strings**: In `appsettings.Development.json`
- **CORS**: Configured for localhost:4200 (frontend)
- **NLog**: Configured in `nlog.config` (logs to file and emails errors)
- **OpenAPI**: Enabled in development with Swagger UI

## Best Practices
- Always use DI; avoid static classes/methods
- Validate inputs using model validation attributes
- Use pagination for list endpoints (PageResponseDTO)
- Implement proper error handling and status codes
- Keep controllers thin; move logic to services
- Use transactions for multi-table operations
- Follow RESTful conventions for endpoints</content>

## Agent Behavior
- **Database-First Safety**: Never suggest manual changes to files in the `Entity/` folder. Remind the user to update the DB and re-run scaffolding if a schema change is needed.
- **Communication**: Explain the reasoning behind a solution before providing the code.
- **Consistency**: Ensure all new services and repositories are automatically suggested for registration in `Program.cs`.