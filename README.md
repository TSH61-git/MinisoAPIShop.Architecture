# WebAPIShop

A robust and scalable RESTful Web API for an e-commerce platform built with .NET 9, designed to manage users, products, orders, categories, branches, and ratings. This API follows modern software engineering practices, including asynchronous programming, layered architecture, and comprehensive testing.

## Architecture Overview

The project implements a clean 3-layer architecture pattern to ensure separation of concerns, maintainability, and testability:

### Layers
- **Presentation Layer (WebApiShop)**: Contains controllers, middleware, and API endpoints. Handles HTTP requests and responses.
- **Service Layer**: Business logic layer containing services that orchestrate operations and apply business rules.
- **Repository Layer**: Data access layer using Entity Framework Core for database operations.

### Key Architectural Components
- **Dependency Injection**: All services and repositories are registered with the DI container for loose coupling.
- **Database-First Approach**: Entity Framework Core models are generated from an existing SQL Server database.
- **DTO Pattern**: Data Transfer Objects implemented as C# records for immutability and secure data transfer.
- **AutoMapper**: Automatic mapping between entities and DTOs to maintain separation between data models.

### Architecture Flow
```
HTTP Request → Controller → Service → Repository → Database
                      ↓
                AutoMapper
                      ↓
            DTO Response
```

## Key Features

- **Asynchronous Operations**: All database and I/O operations are fully asynchronous for optimal performance and scalability.
- **Global Exception Handling**: Custom middleware catches and logs unhandled exceptions, ensuring graceful error responses.
- **Traffic Monitoring**: Built-in rating middleware logs all API requests to a dedicated Rating table for auditing and analytics.
- **Comprehensive Logging**: NLog integration for structured logging across all layers.
- **CORS Support**: Configured to allow cross-origin requests from the frontend application (localhost:4200).
- **Security**: DTOs prevent over-posting and ensure only necessary data is transferred.
- **OpenAPI/Swagger**: Automatic API documentation generation for development and testing.

## Tech Stack

- **Framework**: .NET 9.0 Web API
- **Language**: C# with asynchronous programming
- **Database**: SQL Server (Express/LocalDB)
- **ORM**: Entity Framework Core (Database-First)
- **Mapping**: AutoMapper
- **Logging**: NLog
- **Testing**: xUnit with Moq and Entity Framework In-Memory
- **Documentation**: OpenAPI/Swagger
- **Password Security**: zxcvbn-core for password strength validation

## Getting Started

### Prerequisites
- .NET 9.0 SDK
- SQL Server (Express or LocalDB)
- Visual Studio 2022 or VS Code with C# extensions

### Installation

1. **Clone the repository**
   ```bash
   git clone <repository-url>
   cd WebAPIShop
   ```

2. **Configure Database Connection**
   - Open `WebApiShop/appsettings.Development.json`
   - Update the `DefaultConnection` string to point to your SQL Server instance:
     ```json
     {
       "ConnectionStrings": {
         "DefaultConnection": "Data Source=your-server;Initial Catalog=MyWebApiShop;Integrated Security=True;TrustServerCertificate=True"
       }
     }
     ```
   - Ensure the database `MyWebApiShop` exists and matches the schema defined in the Entity Framework context.

3. **Restore Dependencies**
   ```bash
   dotnet restore
   ```

4. **Run the Application**
   ```bash
   cd WebApiShop
   dotnet run
   ```

The API will be available at `https://localhost:5001` (or `http://localhost:5000` for HTTP).

### API Documentation
When running in development mode, Swagger UI is available at:
- `https://localhost:5001/swagger` (or your configured port)

## Testing

The project includes comprehensive unit and integration tests using xUnit.

### Running Tests
```bash
cd TestProject
dotnet test
```

### Test Coverage
- **Unit Tests**: Test individual components in isolation using Moq for mocking dependencies.
- **Integration Tests**: Test repository classes against an in-memory database using Entity Framework Core In-Memory provider.
- **Test Fixtures**: Shared database context setup for consistent test data.

### Test Structure
- Repository layer tests verify data access operations
- Service layer tests validate business logic
- Integration tests ensure end-to-end functionality

## Project Structure

```
WebAPIShop/
├── WebApiShop/          # Presentation layer
│   ├── Controllers/     # API endpoints
│   ├── MiddleWare/      # Custom middleware
│   └── Program.cs       # Application startup
├── Service/             # Business logic layer
├── Repository/          # Data access layer
├── Entity/              # Domain entities
├── DTOs/                # Data transfer objects
└── TestProject/         # Test suite
```

## Contributing

1. Follow the established architecture patterns
2. Write tests for new features
3. Use asynchronous programming throughout
4. Ensure proper logging and error handling
5. Update API documentation as needed
