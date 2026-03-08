# Repository Layer Instructions for WebAPIShop

## Overview
The Repository layer is the data access layer of the WebAPIShop application, built using Entity Framework Core with a database-first approach. It provides abstracted access to the SQL Server database, ensuring separation of concerns and testability.

## Key Components
- **MyWebApiShopContext**: The EF Core DbContext class, generated from the database schema.
- **Repository Classes**: Implement data access logic (e.g., `ProductRepository`, `UserRepository`).
- **Repository Interfaces**: Define contracts for repositories (e.g., `IProductRepository`).

## Guidelines
- **Database-First Approach**: Entities in the `Entity/` folder are auto-generated. Never manually edit these files. For schema changes, update the database and re-scaffold the context.
- **Async Operations**: All repository methods must use async/await patterns (e.g., `await _context.SaveChangesAsync();`).
- **Interface Segregation**: Each repository implements an interface for dependency injection and testing.
- **Context Injection**: Inject `MyWebApiShopContext` via constructor for DI.
- **Eager Loading**: Use `.Include()` to load related entities and prevent N+1 query issues (e.g., `.Include(p => p.Category)`).
- **Query Building**: Use `IQueryable` for composable queries. Apply filters, sorting, and pagination before materializing with `.ToListAsync()`, `.FirstOrDefaultAsync()`, etc.
- **Transactions**: For operations involving multiple tables, use explicit transactions:
  ```csharp
  await using var transaction = await _context.Database.BeginTransactionAsync();
  try {
      // operations
      await _context.SaveChangesAsync();
      await transaction.CommitAsync();
  } catch {
      await transaction.RollbackAsync();
      throw;
  }
  ```
- **Error Handling**: Do not catch exceptions in repositories; let them bubble up to the service layer for handling.
- **Naming Conventions**:
  - Repository classes: `EntityNameRepository` (e.g., `ProductRepository`).
  - Methods: `ActionEntityAsync` (e.g., `GetProductsAsync`, `AddProductAsync`, `UpdateProductAsync`, `DeleteProductAsync`).
- **CRUD Patterns**: Follow standard Create, Read, Update, Delete patterns.
- **Pagination**: For list methods, return tuples like `(List<Entity>, int totalCount)` for pagination support.
- **Testing**:
  - Unit tests: Mock the context using Moq.
  - Integration tests: Use EF In-Memory provider to test against an in-memory database.

## Adding a New Repository
1. Create the interface in `Repository/IEntityRepository.cs`.
2. Implement the repository in `Repository/EntityRepository.cs`.
3. Register in `Program.cs`: `builder.Services.AddScoped<IEntityRepository, EntityRepository>();`.
4. Ensure the entity has a `DbSet<Entity>` in `MyWebApiShopContext`.

## Common Patterns
- **Get by ID**: `await _context.Entities.FindAsync(id);`
- **Get with Includes**: `await _context.Entities.Include(e => e.Related).FirstOrDefaultAsync(e => e.Id == id);`
- **Add**: `_context.Entities.Add(entity); await _context.SaveChangesAsync();`
- **Update**: Modify entity properties; `await _context.SaveChangesAsync();`
- **Delete**: `_context.Entities.Remove(entity); await _context.SaveChangesAsync();`
- **Exists Check**: `await _context.Entities.AnyAsync(e => e.Condition);`

## Best Practices
- Avoid business logic in repositories; keep them focused on data access.
- Use LINQ for queries to leverage EF's capabilities.
- Profile queries for performance; use `.AsNoTracking()` for read-only operations.
- Handle concurrency with `ConcurrencyCheck` attributes if needed (though not currently used).
- Log database operations if required, but primarily rely on service layer logging.