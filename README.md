# Backend API - Clean Architecture with C# .NET

A robust backend API built using C# .NET following Clean Architecture principles, designed for scalability, maintainability, and testability.

## Architecture Overview

This project implements **Clean Architecture** concepts to ensure separation of concerns and maintainability. While currently organized within a single project for simplicity, the architecture follows established patterns that can easily be scaled into separate projects for larger applications.

### Clean Architecture Benefits
- **Separation of Concerns**: Each layer has distinct responsibilities
- **Dependency Inversion**: Higher-level modules don't depend on lower-level modules
- **Testability**: Easy to unit test individual components
- **Flexibility**: Easy to swap implementations without affecting other layers
- **Maintainability**: Changes in one layer don't cascade to others

### Current Structure vs. Larger Projects
**Current Implementation**: Single project with logical separation
```
Controllers → Services → Repositories
```

**Future Scaling**: For larger projects, this would typically be separated into:
- **Presentation Layer** (API Controllers)
- **Application Layer** (Services, DTOs, Validation)
- **Domain Layer** (Business Logic, Entities)
- **Infrastructure Layer** (Data Access, External Services)

## Technology Stack

- **Framework**: ASP.NET Core
- **Language**: C#
- **Architecture**: Clean Architecture Pattern
- **ORM**: Entity Framework Core (ready for migration)
- **Mapping**: Riok AutoMapper
- **Validation**: FluentValidation
- **Authentication**: ASP.NET Identity with JWT Bearer Tokens
- **Database**: In-Memory (easily switchable to SQL Server or other databases)

## Application Flow

```
HTTP Request → Controllers → Services → Repositories → Database
                    ↓
HTTP Response ← DTOs ← Domain Models ← Data Access
```

1. **Controllers** receive HTTP requests and delegate to services
2. **Services** contain business logic and orchestrate operations
3. **Repositories** handle data access and persistence
4. **Mappers** transform between domain models and DTOs

## Key Features & Design Patterns

### Repository Pattern
- **Purpose**: Abstracts data access layer
- **Benefit**: Easy to switch between different data sources (In-Memory → SQL Server → NoSQL)
- **Implementation**: Generic repository pattern for common CRUD operations

### DTO Mapping with Riok AutoMapper
- **Purpose**: Separates internal domain models from API contracts
- **Benefit**: 
  - Prevents over-posting attacks
  - Allows internal model changes without breaking API
  - Clean separation between layers

### FluentValidation
- **Implementation**: Student registration and login validation
- **Benefits**:
  - Readable and maintainable validation rules
  - Separates validation logic from models
  - Easy to unit test validation scenarios
  - Supports complex validation scenarios

### ASP.NET Identity with JWT Bearer Tokens
- **Authentication**: ASP.NET Identity for user management
- **Authorization**: JWT Bearer tokens instead of traditional cookies
- **Security Benefits**:
  - Stateless authentication
  - Better for mobile and SPA applications
  - Supports cross-domain authentication
  - More secure for API-based architectures
  - Scalable across multiple servers

### Global Exception Handling Middleware
- **Purpose**: Centralized error handling across the application
- **Benefits**:
  - Consistent error responses
  - Prevents sensitive information leakage
  - Easier debugging and logging
  - Better user experience with structured error messages
  - Separation of error handling from business logic

### CORS Policy
- **Configuration**: Allows frontend-to-backend communication
- **Security**: Controlled cross-origin resource sharing
- **Flexibility**: Configurable for different environments (dev, staging, prod)

## Frontend Integration

### Radzen Framework
- **UI/UX**: Professional and responsive user interface
- **Components**: Rich set of pre-built components
- **Integration**: Seamless integration with backend APIs

### Frontend Architecture
- **Layered Approach**: Separation of concerns in frontend
- **Razor Pages**: Server-side rendering for optimal performance
- **Service Layer**: Abstracted API communication

## Future Enhancements

For larger projects, consider:
- **Separate Projects**: Split into distinct assemblies per layer
- **CQRS Pattern**: Command Query Responsibility Segregation
- **Domain Events**: For complex business logic
- **API Versioning**: Support multiple API versions
- **Caching**: Redis or in-memory caching
- **Logging**: Structured logging with Serilog
- **Health Checks**: Monitoring endpoint health
- **Testing** : 
  - **Unit Tests**: Test services and repositories independently
  - **Integration Tests**: Test complete request/response cycles
