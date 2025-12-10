# Task 001: Backend Scaffolding - Implementation Plan

## Overview
Create foundational .NET Aspire monorepo infrastructure with AppHost orchestration, REST API, ServiceDefaults, AgentHost, and Shared libraries.

## Projects to Create

### 1. PLS.AppHost (Orchestration)
- **Type**: .NET Aspire AppHost project
- **Purpose**: Orchestrate all services and resources for local development and deployment
- **Key Responsibilities**:
  - Define service composition with `AddProject` API
  - Configure service discovery and references
  - Manage dependencies with `WaitFor` API
  - Configure Redis, databases, and external services
- **Target Framework**: net9.0
- **Key NuGet Packages**:
  - Aspire.Hosting (latest)
  - Aspire.Hosting.Redis (latest)
  - Aspire.Hosting.Azure.Storage (latest)

### 2. PLS.ServiceDefaults (Shared Configuration)
- **Type**: .NET Aspire ServiceDefaults project
- **Purpose**: Centralize telemetry, resilience, service discovery, and health check configurations
- **Key Responsibilities**:
  - Configure OpenTelemetry (logs, traces, metrics)
  - Setup health check endpoints (`/health`, `/alive`)
  - Configure HTTP resilience (retry, circuit breaker)
  - Enable service discovery
- **Target Framework**: net9.0
- **Key NuGet Packages**:
  - Aspire.Microsoft.Azure.Storage (latest)
  - Microsoft.Extensions.ServiceDiscovery (latest)
  - Microsoft.Extensions.Http.Resilience (latest)
  - OpenTelemetry.Exporter.OpenTelemetryProtocol (latest)

### 3. PLS.Api (REST API)
- **Type**: ASP.NET Core Web API project
- **Purpose**: Backend REST API for PLS system
- **Key Responsibilities**:
  - Expose RESTful endpoints for leads, cases, clients, documents, agents
  - Handle HTTP requests with controllers
  - OpenAPI/Swagger documentation
  - Authentication and authorization
  - Call ServiceDefaults for telemetry, health checks, resilience
- **Target Framework**: net9.0
- **Key NuGet Packages**:
  - Microsoft.AspNetCore.OpenApi (latest)
  - Swashbuckle.AspNetCore (latest)
  - Microsoft.EntityFrameworkCore.SqlServer (latest)
  - Microsoft.EntityFrameworkCore.Tools (latest)
  - Serilog.AspNetCore (latest)
  - Microsoft.ApplicationInsights.AspNetCore (latest)
  - Microsoft.Identity.Web (latest) - for authentication

### 4. PLS.AgentHost (Agent Execution)
- **Type**: .NET Worker Service project
- **Purpose**: Host agents for distributed execution
- **Key Responsibilities**:
  - Host agent providers (CopywritingAgentProvider, AuditAgentProvider, etc.)
  - Execute background agent workflows
  - Orchestrate multi-agent coordination
  - Register agent tools and skills
  - Track agent telemetry and conversations
- **Target Framework**: net9.0
- **Key NuGet Packages**:
  - Microsoft.Extensions.Hosting (latest)
  - Azure.AI.Projects (latest) - for Azure AI Foundry agents
  - Azure.Identity (latest)
  - Microsoft.Extensions.Configuration.AzureKeyVault (latest)

### 5. PLS.Shared (Domain Models)
- **Type**: .NET Class Library project
- **Purpose**: Shared domain models, DTOs, enums, constants
- **Key Responsibilities**:
  - Define entity models (Lead, Case, Client, Document, Agent, Workflow, etc.)
  - Define DTOs for API contracts
  - Define enums (LeadStatus, CaseStage, DocumentType, AgentRole, etc.)
  - Define constants and configuration classes
- **Target Framework**: net9.0
- **Key NuGet Packages**:
  - System.ComponentModel.Annotations (latest) - for validation attributes

### 6. Test Projects
- **PLS.Api.UnitTests**: xUnit test project for Api unit tests (≥85% coverage)
- **PLS.Api.IntegrationTests**: xUnit test project for Api integration tests
- **PLS.AgentHost.UnitTests**: xUnit test project for AgentHost unit tests (≥85% coverage)

## Folder Structure

```
/workspaces/spec2cloud-marketing-agent/
├── src/
│   ├── PLS.AppHost/
│   │   ├── Program.cs
│   │   ├── appsettings.json
│   │   ├── appsettings.Development.json
│   │   └── PLS.AppHost.csproj
│   ├── PLS.ServiceDefaults/
│   │   ├── Extensions.cs
│   │   └── PLS.ServiceDefaults.csproj
│   ├── PLS.Api/
│   │   ├── Controllers/
│   │   │   ├── LeadsController.cs
│   │   │   ├── CasesController.cs
│   │   │   ├── ClientsController.cs
│   │   │   ├── DocumentsController.cs
│   │   │   └── AgentsController.cs
│   │   ├── Data/
│   │   │   ├── ApplicationDbContext.cs
│   │   │   └── Migrations/
│   │   ├── Program.cs
│   │   ├── appsettings.json
│   │   ├── appsettings.Development.json
│   │   └── PLS.Api.csproj
│   ├── PLS.AgentHost/
│   │   ├── Providers/
│   │   │   ├── CopywritingAgentProvider.cs
│   │   │   └── AuditAgentProvider.cs
│   │   ├── Orchestrators/
│   │   │   └── WorkflowOrchestrator.cs
│   │   ├── Program.cs
│   │   ├── appsettings.json
│   │   └── PLS.AgentHost.csproj
│   └── PLS.Shared/
│       ├── Models/
│       │   ├── Lead.cs
│       │   ├── Case.cs
│       │   ├── Client.cs
│       │   ├── Document.cs
│       │   ├── Agent.cs
│       │   ├── Workflow.cs
│       │   └── AuditLog.cs
│       ├── DTOs/
│       │   ├── LeadDto.cs
│       │   ├── CaseDto.cs
│       │   └── ClientDto.cs
│       ├── Enums/
│       │   ├── LeadStatus.cs
│       │   ├── CaseStage.cs
│       │   ├── DocumentType.cs
│       │   └── AgentRole.cs
│       └── PLS.Shared.csproj
├── tests/
│   ├── PLS.Api.UnitTests/
│   │   ├── Controllers/
│   │   └── PLS.Api.UnitTests.csproj
│   ├── PLS.Api.IntegrationTests/
│   │   ├── ApiTests/
│   │   └── PLS.Api.IntegrationTests.csproj
│   └── PLS.AgentHost.UnitTests/
│       ├── Providers/
│       └── PLS.AgentHost.UnitTests.csproj
└── PLS.sln
```

## Data Models (PLS.Shared/Models)

### Lead.cs
```csharp
public class Lead
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public LeadStatus Status { get; set; } // Enum: New, Qualified, Contacted, Converted
    public string Source { get; set; } // e.g., Website, Referral
    public DateTime CreatedAt { get; set; }
    public DateTime? QualifiedAt { get; set; }
    public Guid? AssignedAgentId { get; set; }
    
    // Navigation properties
    public Case? ConvertedCase { get; set; }
    public ICollection<AuditLog> AuditLogs { get; set; }
}
```

### Case.cs
```csharp
public class Case
{
    public Guid Id { get; set; }
    public string ReferenceNumber { get; set; } // Unique case identifier
    public CaseStage Stage { get; set; } // Enum: LeadGeneration, LegalPrep, Exchange, Completion, PostCompletion
    public TransactionType Type { get; set; } // Enum: Purchase, Sale, PurchaseAndSale
    public decimal EstimatedValue { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? CompletionDate { get; set; }
    
    // Foreign keys
    public Guid ClientId { get; set; }
    public Guid? LeadId { get; set; } // Optional - case may not originate from lead
    
    // Navigation properties
    public Client Client { get; set; }
    public Lead? Lead { get; set; }
    public ICollection<Document> Documents { get; set; }
    public ICollection<Workflow> Workflows { get; set; }
    public ICollection<AuditLog> AuditLogs { get; set; }
}
```

### Client.cs
```csharp
public class Client
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    public DateTime CreatedAt { get; set; }
    
    // Navigation properties
    public ICollection<Case> Cases { get; set; }
}
```

### Document.cs
```csharp
public class Document
{
    public Guid Id { get; set; }
    public string FileName { get; set; }
    public DocumentType Type { get; set; } // Enum: Contract, TitleDeed, Survey, SearchReport, etc.
    public string StorageUrl { get; set; } // Azure Blob Storage URL
    public long FileSizeBytes { get; set; }
    public DateTime UploadedAt { get; set; }
    public Guid CaseId { get; set; }
    
    // Navigation properties
    public Case Case { get; set; }
    public ICollection<AuditLog> AuditLogs { get; set; }
}
```

### Agent.cs (Agent Execution Metadata)
```csharp
public class Agent
{
    public Guid Id { get; set; }
    public string Name { get; set; } // e.g., "Copywriting Agent", "Audit Agent"
    public AgentRole Role { get; set; } // Enum: Copywriting, Audit, DocReview, etc.
    public string AzureAIAgentId { get; set; } // Azure AI Foundry agent ID
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
}
```

### Workflow.cs
```csharp
public class Workflow
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public WorkflowStatus Status { get; set; } // Enum: Pending, Running, Completed, Failed
    public DateTime StartedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
    public Guid CaseId { get; set; }
    
    // Navigation properties
    public Case Case { get; set; }
    public ICollection<AuditLog> AuditLogs { get; set; }
}
```

### AuditLog.cs
```csharp
public class AuditLog
{
    public Guid Id { get; set; }
    public string EntityType { get; set; } // e.g., "Lead", "Case", "Document"
    public Guid EntityId { get; set; }
    public string Action { get; set; } // e.g., "Created", "Updated", "Deleted", "AgentProcessed"
    public string PerformedBy { get; set; } // User or Agent name
    public string Details { get; set; } // JSON serialized details
    public DateTime Timestamp { get; set; }
}
```

## Enums (PLS.Shared/Enums)

```csharp
public enum LeadStatus { New, Qualified, Contacted, Converted, Rejected }
public enum CaseStage { LeadGeneration, LegalPrep, Exchange, Completion, PostCompletion, Archived }
public enum TransactionType { Purchase, Sale, PurchaseAndSale }
public enum DocumentType { Contract, TitleDeed, Survey, SearchReport, MortgageOffer, IDProof, Other }
public enum AgentRole { Copywriting, Audit, DocReview, Exchange, CompletionFunds, LandRegistry, Accounting }
public enum WorkflowStatus { Pending, Running, Completed, Failed, Cancelled }
```

## API Endpoints (PLS.Api/Controllers)

### LeadsController
- `GET /api/leads` - List all leads
- `GET /api/leads/{id}` - Get lead by ID
- `POST /api/leads` - Create new lead
- `PUT /api/leads/{id}` - Update lead
- `DELETE /api/leads/{id}` - Delete lead
- `POST /api/leads/{id}/qualify` - Qualify lead (triggers agent workflow)

### CasesController
- `GET /api/cases` - List all cases
- `GET /api/cases/{id}` - Get case by ID
- `POST /api/cases` - Create new case
- `PUT /api/cases/{id}` - Update case
- `DELETE /api/cases/{id}` - Delete case
- `GET /api/cases/{id}/documents` - Get case documents
- `GET /api/cases/{id}/workflows` - Get case workflows

### ClientsController
- `GET /api/clients` - List all clients
- `GET /api/clients/{id}` - Get client by ID
- `POST /api/clients` - Create new client
- `PUT /api/clients/{id}` - Update client
- `DELETE /api/clients/{id}` - Delete client

### DocumentsController
- `GET /api/documents/{id}` - Get document metadata
- `POST /api/documents` - Upload document
- `DELETE /api/documents/{id}` - Delete document
- `GET /api/documents/{id}/download` - Download document

### AgentsController
- `GET /api/agents` - List all agents
- `GET /api/agents/{id}` - Get agent by ID
- `POST /api/agents/{id}/execute` - Execute agent workflow

## Database Configuration

### ApplicationDbContext (PLS.Api/Data)
```csharp
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    
    public DbSet<Lead> Leads { get; set; }
    public DbSet<Case> Cases { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<Document> Documents { get; set; }
    public DbSet<Agent> Agents { get; set; }
    public DbSet<Workflow> Workflows { get; set; }
    public DbSet<AuditLog> AuditLogs { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure entity relationships, indexes, constraints
        modelBuilder.Entity<Lead>()
            .HasIndex(l => l.Email)
            .IsUnique();
        
        modelBuilder.Entity<Case>()
            .HasIndex(c => c.ReferenceNumber)
            .IsUnique();
        
        // Configure relationships
        modelBuilder.Entity<Case>()
            .HasOne(c => c.Client)
            .WithMany(cl => cl.Cases)
            .HasForeignKey(c => c.ClientId)
            .OnDelete(DeleteBehavior.Restrict);
        
        // ... additional configurations
    }
}
```

### Connection String (appsettings.json)
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=PLSDb;Trusted_Connection=True;MultipleActiveResultSets=true"
  }
}
```

## Testing Strategy

### Unit Tests (≥85% Coverage)
- **PLS.Api.UnitTests**:
  - Controllers: Test all controller actions with mocked DbContext
  - Validation: Test model validation attributes
  - Business logic: Test any service classes
  
- **PLS.AgentHost.UnitTests**:
  - Agent providers: Test agent creation, tool registration
  - Orchestrators: Test workflow coordination logic

### Integration Tests
- **PLS.Api.IntegrationTests**:
  - Test actual HTTP requests to API endpoints
  - Use WebApplicationFactory for in-memory server
  - Use testcontainers for SQL Server database
  - Test database operations end-to-end
  - Test health check endpoints

### Performance Tests
- Load testing for API endpoints
- Concurrency testing for multi-agent workflows
- Database query performance testing

## AppHost Configuration (PLS.AppHost/Program.cs)

```csharp
var builder = DistributedApplication.CreateBuilder(args);

// Add Redis for caching and agent coordination
var redis = builder.AddRedis("redis");

// Add SQL Server database
var sqlServer = builder.AddSqlServer("sql")
    .AddDatabase("plsdb");

// Add API service
var api = builder.AddProject<Projects.PLS_Api>("api")
    .WithReference(sqlServer)
    .WithReference(redis);

// Add AgentHost service
var agentHost = builder.AddProject<Projects.PLS_AgentHost>("agenthost")
    .WithReference(sqlServer)
    .WithReference(redis)
    .WaitFor(api);

builder.Build().Run();
```

## ServiceDefaults Configuration (PLS.ServiceDefaults/Extensions.cs)

```csharp
public static class Extensions
{
    public static IHostApplicationBuilder AddServiceDefaults(this IHostApplicationBuilder builder)
    {
        // Configure OpenTelemetry
        builder.ConfigureOpenTelemetry();
        
        // Add service discovery
        builder.Services.AddServiceDiscovery();
        
        // Configure HTTP resilience
        builder.Services.ConfigureHttpClientDefaults(http =>
        {
            http.AddStandardResilienceHandler();
            http.AddServiceDiscovery();
        });
        
        // Add health checks
        builder.Services.AddHealthChecks()
            .AddCheck("self", () => HealthCheckResult.Healthy());
        
        return builder;
    }
    
    public static WebApplication MapDefaultEndpoints(this WebApplication app)
    {
        app.MapHealthChecks("/health");
        app.MapHealthChecks("/alive", new HealthCheckOptions
        {
            Predicate = _ => false
        });
        
        return app;
    }
}
```

## Configuration Management

### Environment Variables
- `ASPNETCORE_ENVIRONMENT` - Development/Staging/Production
- `AZURE_KEY_VAULT_URL` - Azure Key Vault URL for secrets
- `APPLICATIONINSIGHTS_CONNECTION_STRING` - Application Insights telemetry

### Azure Key Vault Integration
- Store connection strings in Key Vault
- Store API keys and secrets
- Use Azure.Identity for authentication

## Observability

### Logging
- Structured logging with Serilog
- Log to Application Insights
- Log levels: Information, Warning, Error

### Tracing
- OpenTelemetry distributed tracing
- Trace API requests end-to-end
- Trace agent workflow execution

### Metrics
- ASP.NET Core runtime metrics
- Custom business metrics (leads created, cases processed, etc.)
- Agent execution metrics (duration, success rate)

## Security

### Authentication
- Use Microsoft.Identity.Web for Azure AD authentication
- JWT bearer tokens for API access
- Service-to-service authentication with managed identities

### Authorization
- Role-based access control (RBAC)
- Policy-based authorization for sensitive operations

### Data Protection
- Encrypt sensitive data at rest
- Use HTTPS for all API communication
- Validate and sanitize all inputs

## Next Steps

1. **Create solution and projects** using dotnet CLI
2. **Add NuGet packages** to all projects
3. **Implement data models** in PLS.Shared
4. **Implement ApplicationDbContext** with EF Core
5. **Generate initial migration** with `dotnet ef migrations add InitialCreate`
6. **Implement API controllers** with CRUD operations
7. **Configure AppHost** with service orchestration
8. **Implement ServiceDefaults** with telemetry and health checks
9. **Create unit tests** for controllers and services
10. **Create integration tests** with testcontainers
11. **Run all tests** and ensure ≥85% coverage
12. **Create MADR** documenting architectural decisions
13. **Update documentation** in /docs

## Acceptance Criteria Mapping

This implementation plan addresses all 14 acceptance criteria from the task specification:

✅ **AC001**: Monorepo folder structure (src/, tests/, infra/) - Covered in Folder Structure section
✅ **AC002**: .NET Aspire AppHost project - Covered in Projects section + AppHost Configuration
✅ **AC003**: ASP.NET Core Web API project - Covered in PLS.Api section
✅ **AC004**: ServiceDefaults project - Covered in PLS.ServiceDefaults section
✅ **AC005**: AgentHost project - Covered in PLS.AgentHost section
✅ **AC006**: Shared project - Covered in PLS.Shared section
✅ **AC007**: Solution file - Covered in implementation steps
✅ **AC008**: OpenAPI endpoints - Covered in API Endpoints section
✅ **AC009**: Health check endpoints - Covered in ServiceDefaults Configuration
✅ **AC010**: Database context with EF Core - Covered in Database Configuration section
✅ **AC011**: Configuration management - Covered in Configuration Management section
✅ **AC012**: Structured logging - Covered in Observability section
✅ **AC013**: Unit tests ≥85% coverage - Covered in Testing Strategy section
✅ **AC014**: Integration tests - Covered in Testing Strategy section

## Technology Versions

All packages will use **latest stable versions** at implementation time:
- **.NET**: 9.0 (latest LTS)
- **Aspire.Hosting**: Latest stable from NuGet
- **Entity Framework Core**: Latest stable (9.x)
- **xUnit**: Latest stable (2.x)
- **Serilog**: Latest stable
- **Application Insights**: Latest stable
