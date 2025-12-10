# Task: Backend Scaffolding

**GitHub Issue:** [#1](https://github.com/erydrn/spec2cloud-marketing-agent/issues/1)

## Task Information

**Task ID:** 001  
**Task Name:** Backend Scaffolding  
**Feature:** Infrastructure (Scaffolding)  
**Priority:** Critical  
**Estimated Complexity:** High  
**Dependencies:** None (must be completed first)

## Description

Create the foundational backend infrastructure for the Property Legal Services (PLS) AI Agent System following .NET Aspire architecture patterns. This includes setting up the monorepo structure with AppHost orchestration, API service, ServiceDefaults, and AgentHost projects.

## Technical Requirements

### 1. Monorepo Structure
- Create solution file `PLS.sln` at repository root
- Establish project hierarchy following Aspire conventions:
  - `src/PLS.AppHost/` - Orchestration and service discovery
  - `src/PLS.Api/` - REST API and WebSocket endpoints
  - `src/PLS.ServiceDefaults/` - Shared telemetry, resilience, service discovery
  - `src/PLS.AgentHost/` - Agent framework execution environment
  - `src/PLS.Shared/` - Shared models, DTOs, contracts
  - `tests/PLS.Api.Tests/` - API unit and integration tests
  - `tests/PLS.AgentHost.Tests/` - Agent framework tests

### 2. PLS.AppHost Project
- Initialize .NET Aspire AppHost project (latest stable version)
- Configure service orchestration for:
  - API service with health checks
  - AgentHost service with background processing
  - Azure AI Foundry connection strings
  - Database connections (Azure SQL or Cosmos DB)
  - Redis cache connection
  - Application Insights telemetry
- Set up local development environment configuration
- Configure HTTPS endpoints and certificates
- Implement environment-based configuration (Development, Staging, Production)

### 3. PLS.Api Project
- Create ASP.NET Core Web API project (latest stable version)
- Configure middleware pipeline:
  - Exception handling middleware
  - CORS policy for frontend origin
  - Authentication/Authorization (Azure AD B2C placeholder)
  - Request logging and correlation IDs
  - Rate limiting and throttling
  - Health checks (`/health`, `/health/ready`, `/health/live`)
- Set up OpenAPI/Swagger specification:
  - Versioned API endpoints (v1)
  - Comprehensive documentation
  - Example requests/responses
  - Security definitions
- Implement controllers structure:
  - `LeadsController` (F001, F002)
  - `CasesController` (F003)
  - `DocumentsController` (F007)
  - `CompletionController` (F009)
  - `AgentsController` (agent status and health)
- Configure dependency injection container
- Set up Entity Framework Core (Code First approach)
  - `ApplicationDbContext` with entity configurations
  - Database connection string management
  - Migration strategy

### 4. PLS.ServiceDefaults Project
- Create shared service defaults library
- Implement telemetry configuration:
  - Application Insights integration
  - Structured logging with Serilog
  - Distributed tracing with OpenTelemetry
  - Metrics collection (custom and system)
- Configure resilience patterns:
  - Retry policies with exponential backoff
  - Circuit breaker patterns
  - Timeout policies
  - Bulkhead isolation
- Implement service discovery helpers
- Create health check extensions

### 5. PLS.AgentHost Project
- Create dedicated agent execution host (Console Application or Worker Service)
- Integrate Microsoft Agent Framework:
  - Agent provider pattern implementation
  - Agent lifecycle management
  - Tool/skill registration infrastructure
  - Memory and conversation state management
  - Telemetry middleware for agent actions
- Configure background job processing:
  - Queue-based task processing
  - Scheduled agent workflows
  - Long-running agent operations
- Implement agent orchestration services:
  - Sequential workflow coordinator
  - Concurrent workflow coordinator
  - Agent-to-agent communication protocol
- Set up Azure AI Foundry SDK:
  - Model client configuration
  - Prompt asset management
  - Evaluation harness integration
  - Safety guardrails enforcement

### 6. PLS.Shared Project
- Define shared data models and DTOs:
  - Lead entities (Lead, LeadSource, LeadQualification)
  - Case entities (Case, CaseType, CaseStatus)
  - Client entities (Client, Company)
  - Document entities (Document, DocumentType, DocumentVersion)
  - Agent entities (AgentTask, AgentStatus, AgentResponse)
- Create contract interfaces for services
- Implement validation attributes and fluent validators
- Define enumerations and constants
- Create API response wrappers (ApiResponse<T>, ErrorResponse)

### 7. Infrastructure Setup
- Configure `.gitignore` for .NET projects (bin, obj, user secrets, etc.)
- Set up `Directory.Build.props` for shared MSBuild properties
- Configure `Directory.Packages.props` for centralized package management
- Create `global.json` to lock .NET SDK version
- Implement code quality tools:
  - `.editorconfig` with C# coding standards
  - StyleCop.Analyzers configuration
  - SonarAnalyzer.CSharp
- Set up pre-commit hooks (Husky.NET):
  - Format check (dotnet format)
  - Code analysis
  - Commit message linting (Conventional Commits)

### 8. Database Initialization
- Create initial EF Core migration (`InitialCreate`)
- Define core database schema:
  - Leads table with indexes
  - Cases table with foreign keys
  - Clients and Companies tables
  - Documents table with blob storage references
  - AgentTasks table for workflow tracking
  - AuditLog table for compliance
- Implement seed data for development:
  - Sample lead sources
  - Case types and statuses
  - Document types
  - Test agents

### 9. Configuration Management
- Implement strongly-typed configuration classes:
  - `AzureAIConfiguration` (Azure AI Foundry settings)
  - `DatabaseConfiguration` (connection strings)
  - `IntegrationConfiguration` (CRM, CMS endpoints)
  - `SecurityConfiguration` (auth, encryption keys)
- Support multiple configuration sources:
  - appsettings.json (default settings)
  - appsettings.{Environment}.json (environment-specific)
  - User secrets (local development)
  - Azure Key Vault (production secrets)
  - Environment variables (Azure deployment)
- Validate configuration on startup

### 10. Logging and Observability
- Configure structured logging with Serilog:
  - Console sink (development)
  - Application Insights sink (production)
  - File sink (fallback)
  - Log level per namespace
- Implement correlation ID propagation
- Set up custom metrics:
  - Request duration
  - Agent execution time
  - Document processing time
  - Integration API latency
- Create Application Insights query templates

## Acceptance Criteria

1. ✅ Solution builds successfully with `dotnet build` without errors
2. ✅ All projects follow .NET Aspire architecture patterns
3. ✅ AppHost successfully orchestrates all services locally
4. ✅ API project serves health endpoints (`/health` returns 200 OK)
5. ✅ OpenAPI specification accessible at `/swagger` endpoint
6. ✅ ServiceDefaults properly integrated with telemetry in all projects
7. ✅ AgentHost starts and can connect to Azure AI Foundry (connection check)
8. ✅ Database migrations apply successfully with `dotnet ef database update`
9. ✅ All projects have corresponding unit test projects with ≥1 passing test
10. ✅ Code quality checks pass (format, analyzers, no warnings)
11. ✅ Pre-commit hooks execute successfully
12. ✅ Configuration loads correctly from appsettings and user secrets
13. ✅ Structured logging outputs to console and Application Insights
14. ✅ CORS policy allows frontend origin requests

## Testing Requirements

### Unit Tests
- ✅ ServiceDefaults health check extensions
- ✅ Configuration validation logic
- ✅ Shared model validation attributes
- ✅ API response wrapper serialization

**Minimum Coverage:** ≥85% for shared libraries

### Integration Tests
- ✅ AppHost service orchestration (services start and register)
- ✅ API health endpoints return correct status
- ✅ Database connection and migration application
- ✅ Application Insights telemetry collection
- ✅ Azure AI Foundry connection (with test endpoint)

**Test Environment:** Local development with testcontainers for database

### Performance Tests
- ✅ API baseline latency (<100ms for health checks)
- ✅ Database connection pool performance
- ✅ Memory footprint (<500MB idle for all services)

## Dependencies

**Upstream:** None (foundational task)

**Downstream:** All feature implementation tasks depend on this scaffolding

**External Services:**
- Azure AI Foundry (for agent framework)
- Azure SQL Database or Cosmos DB
- Azure Application Insights
- Azure Key Vault (optional for production)

## Technical Decisions Required

1. **Database Choice:** Azure SQL Database vs. Cosmos DB (recommend SQL for transactional consistency)
2. **Agent Framework Version:** Specify Microsoft Agent Framework version
3. **Authentication Provider:** Azure AD B2C vs. Azure AD (recommend B2C for external clients)
4. **Cache Strategy:** Redis vs. Azure Cache for Redis (recommend managed service)
5. **Secret Management:** Azure Key Vault integration timing (recommend from start)

## Notes

- Follow AGENTS.md guidelines for architecture patterns
- Use latest stable versions of all packages (check NuGet)
- Ensure all async operations use proper cancellation token support
- Implement graceful shutdown for long-running agent operations
- Document all configuration settings in README.md
- Create ADR (Architecture Decision Record) for major technology choices

## Definition of Done

- [ ] All code merged to main branch
- [ ] All unit tests passing with ≥85% coverage
- [ ] Integration tests passing in CI pipeline
- [ ] OpenAPI specification generated and documented
- [ ] README.md updated with setup instructions
- [ ] ADR created for database and agent framework choices
- [ ] Peer review completed with 2 approvals
- [ ] No code quality warnings or errors
- [ ] Health checks verified in deployed development environment
