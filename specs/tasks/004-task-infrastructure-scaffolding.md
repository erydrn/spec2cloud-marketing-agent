# Task: Infrastructure Scaffolding

**GitHub Issue:** [#4](https://github.com/erydrn/spec2cloud-marketing-agent/issues/4)

## Task Information

**Task ID:** 004  
**Task Name:** Infrastructure Scaffolding  
**Feature:** Infrastructure (Scaffolding)  
**Priority:** Critical  
**Estimated Complexity:** High  
**Dependencies:** 001-task-backend-scaffolding.md (requires project structure)

## Description

Set up Infrastructure as Code (IaC) using Bicep and Azure Developer CLI (azd) for the Property Legal Services (PLS) AI Agent System. Create deployment manifests, CI/CD pipelines, and environment configurations for Azure deployment.

## Technical Requirements

### 1. Azure Developer CLI Setup
- Initialize azd project with `azd init`
- Create `azure.yaml` manifest at repository root:
  - Define services (api, web, agenthouse)
  - Configure service types (code, container, static)
  - Set up service dependencies
  - Define hooks for pre/post deployment
- Configure environments:
  - Development (dev)
  - Staging (staging)
  - Production (prod)
- Set up environment-specific parameter files

### 2. Bicep Infrastructure Templates
Create `/infra` folder structure:
```
infra/
├── main.bicep                          # Main orchestrator
├── main.parameters.json                # Parameter file
├── abbreviations.json                  # Resource abbreviations
├── modules/
│   ├── app/
│   │   ├── api.bicep                   # API App Service/Container App
│   │   ├── web.bicep                   # Frontend Static Web App
│   │   └── agenthouse.bicep            # Agent Host Container App
│   ├── data/
│   │   ├── sql-server.bicep            # Azure SQL Server
│   │   ├── sql-database.bicep          # SQL Database
│   │   └── storage.bicep               # Blob Storage
│   ├── ai/
│   │   ├── ai-foundry.bicep            # Azure AI Foundry workspace
│   │   ├── openai.bicep                # Azure OpenAI service
│   │   └── document-intelligence.bicep # Azure AI Document Intelligence
│   ├── integration/
│   │   ├── service-bus.bicep           # Service Bus for messaging
│   │   ├── event-grid.bicep            # Event Grid for events
│   │   └── api-management.bicep        # API Management (optional)
│   ├── monitoring/
│   │   ├── log-analytics.bicep         # Log Analytics Workspace
│   │   ├── app-insights.bicep          # Application Insights
│   │   └── alerts.bicep                # Monitoring alerts
│   ├── security/
│   │   ├── key-vault.bicep             # Azure Key Vault
│   │   ├── managed-identity.bicep      # Managed Identities
│   │   └── networking.bicep            # VNet, NSGs (optional)
│   └── shared/
│       ├── resource-group.bicep        # Resource Group
│       └── tags.bicep                  # Standard tags module
└── hooks/
    ├── preprovision.sh                 # Pre-provision scripts
    ├── postprovision.sh                # Post-provision scripts
    ├── predeploy.sh                    # Pre-deployment scripts
    └── postdeploy.sh                   # Post-deployment scripts
```

### 3. Main Bicep Orchestration (main.bicep)
- Define parameters:
  - Environment name (dev, staging, prod)
  - Location (Azure region)
  - Resource name prefix
  - SKU selections for services
  - Feature flags
- Create resource group module
- Deploy shared resources:
  - Managed identities for services
  - Key Vault for secrets
  - Log Analytics and Application Insights
- Deploy data layer:
  - Azure SQL Server with Azure AD admin
  - SQL Database with appropriate tier
  - Blob Storage for documents
- Deploy AI services:
  - Azure AI Foundry workspace
  - Azure OpenAI with model deployments (GPT-4, embeddings)
  - Azure AI Document Intelligence
- Deploy application layer:
  - API service (Container Apps or App Service)
  - Frontend (Static Web Apps)
  - AgentHost (Container Apps)
- Deploy integration layer:
  - Service Bus namespace and queues
  - Event Grid topics
- Configure networking (if required):
  - Virtual Network
  - Subnets
  - Network Security Groups
  - Private Endpoints
- Set up monitoring:
  - Diagnostic settings for all resources
  - Action groups for alerts
  - Alert rules for key metrics
- Output connection strings and endpoints

### 4. API Service Infrastructure (modules/app/api.bicep)
- Choose deployment target:
  - **Option A:** Azure Container Apps (recommended for microservices)
  - **Option B:** Azure App Service (simpler, PaaS)
- Configure Container Apps (if chosen):
  - Container Apps Environment
  - Ingress configuration (HTTPS, external)
  - Scaling rules (CPU, memory, HTTP requests)
  - Container registry integration (Azure Container Registry)
  - Environment variables from Key Vault
  - Managed identity for Azure SDK
  - Dapr integration (optional for service mesh)
- Configure App Service (if chosen):
  - App Service Plan (Linux, appropriate tier)
  - Always On enabled
  - HTTPS only
  - Managed identity
  - App settings from Key Vault references
  - Deployment slots (staging, production)
- Configure common settings:
  - Health check endpoints
  - Custom domains and SSL certificates
  - CORS configuration
  - Logging to Application Insights
  - Autoscaling rules

### 5. Frontend Infrastructure (modules/app/web.bicep)
- Deploy Azure Static Web Apps:
  - Free or Standard tier
  - GitHub integration (repository and branch)
  - Build configuration (Next.js preset)
  - API backend routing (to API service)
  - Custom domains
  - SSL certificates (auto-managed)
  - Authentication providers (Azure AD B2C)
  - Environment variables
  - Staging environments (branches)
- Configure CDN (if needed):
  - Azure Front Door or Azure CDN
  - Caching rules
  - WAF policies

### 6. AgentHost Infrastructure (modules/app/agenthouse.bicep)
- Deploy Azure Container Apps:
  - Container Apps Environment (shared with API if possible)
  - Background processing configuration
  - Scale to zero when idle
  - Minimum/maximum replicas
  - Queue-based scaling (Service Bus)
  - Long-running job support
  - Environment variables from Key Vault
  - Managed identity for Azure AI Foundry
  - Persistent storage (if needed)
- Configure job processing:
  - Service Bus queue bindings
  - Dapr bindings (optional)
  - Scheduled jobs (cron)

### 7. Database Infrastructure (modules/data/sql-database.bicep)
- Create Azure SQL Server:
  - Azure AD admin configuration
  - Firewall rules (Azure services, development IPs)
  - Transparent Data Encryption enabled
  - Advanced Threat Protection enabled
  - Audit logging to Log Analytics
- Create SQL Database:
  - Appropriate tier (Basic for dev, Standard/Premium for prod)
  - Zone redundancy (production only)
  - Backup retention policy (7-35 days)
  - Point-in-time restore enabled
  - Long-term retention (optional)
- Configure connection strings in Key Vault
- Set up managed identity access

### 8. Azure AI Services Infrastructure
**AI Foundry Workspace (modules/ai/ai-foundry.bicep):**
- Create AI Foundry workspace
- Configure model deployments:
  - GPT-4 for agent reasoning
  - GPT-4o for document processing
  - Text-embedding-ada-002 for semantic search
- Set up prompt management
- Configure evaluation endpoints
- Enable safety filters and content moderation

**Document Intelligence (modules/ai/document-intelligence.bicep):**
- Create Azure AI Document Intelligence service
- Standard tier (supports custom models)
- Configure for:
  - Instruction form processing
  - Contract analysis
  - ID document verification

**Azure OpenAI (modules/ai/openai.bicep):**
- Create Azure OpenAI service
- Deploy required models with capacity:
  - GPT-4-turbo (for complex reasoning)
  - GPT-4o (for document understanding)
  - Text-embedding-ada-002 (for embeddings)
- Configure rate limits
- Set up managed identity access

### 9. Security Infrastructure (modules/security/key-vault.bicep)
- Create Azure Key Vault:
  - Soft delete enabled
  - Purge protection enabled (production)
  - RBAC authorization model
  - Private endpoint (production)
  - Network ACLs
- Configure access policies:
  - Managed identities for API, Web, AgentHost
  - Developer access (development only)
  - Azure DevOps service principal (CI/CD)
- Store secrets:
  - SQL connection strings
  - Azure AI Foundry API keys
  - External API keys (CRM, CMS, etc.)
  - Authentication secrets
  - Encryption keys
- Configure secret rotation policies

### 10. Monitoring Infrastructure (modules/monitoring/app-insights.bicep)
- Create Log Analytics Workspace:
  - Retention period (30-90 days)
  - Daily cap (if cost-sensitive)
- Create Application Insights:
  - Linked to Log Analytics
  - Sampling configuration
  - Availability tests
  - Smart detection enabled
- Configure diagnostic settings for all resources:
  - Audit logs
  - Metric logs
  - Performance counters
- Create alert rules:
  - API response time > 5 seconds
  - Error rate > 5%
  - Database DTU > 80%
  - Agent execution failures
  - Queue message age > 10 minutes
  - Storage account capacity > 80%
- Create action groups:
  - Email notifications to dev team
  - SMS for critical alerts
  - Webhook to incident management system

### 11. CI/CD Pipeline Setup
**GitHub Actions Workflow (`.github/workflows/azure-deploy.yml`):**
- Trigger on push to main branch and manual dispatch
- Jobs:
  - **Build Backend:**
    - Checkout code
    - Setup .NET SDK
    - Restore dependencies
    - Build projects
    - Run unit tests
    - Publish artifacts
    - Build Docker images
    - Push to Azure Container Registry
  - **Build Frontend:**
    - Checkout code
    - Setup Node.js
    - Install dependencies
    - Run lint and type check
    - Build Next.js application
    - Run unit tests
    - Upload build artifacts
  - **Provision Infrastructure:**
    - Azure CLI login with service principal
    - Run `azd provision` with environment parameters
    - Output infrastructure endpoints
  - **Deploy Backend:**
    - Deploy API to Container Apps/App Service
    - Deploy AgentHost to Container Apps
    - Run database migrations
    - Smoke test health endpoints
  - **Deploy Frontend:**
    - Deploy to Static Web Apps via GitHub Action
    - Validate deployment
    - Smoke test frontend
- Environment protection rules:
  - Development: Auto-deploy
  - Staging: Auto-deploy with approval
  - Production: Manual approval required
- Secret management:
  - Azure credentials in GitHub secrets
  - Environment-specific variables
  - Key Vault references

### 12. Environment Configuration
**Development Environment:**
- Single resource group
- Lower SKUs (Basic/Standard tier)
- Relaxed network rules
- Verbose logging
- No backup requirements
- Scale to zero when idle

**Staging Environment:**
- Separate resource group
- Mid-tier SKUs (Standard)
- Restricted network access
- Standard logging
- Daily backups
- Minimal redundancy

**Production Environment:**
- Separate resource group
- High-tier SKUs (Premium/Provisioned)
- Strict network isolation (VNet, Private Endpoints)
- Full audit logging
- Geo-redundant backups
- Zone redundancy
- High availability
- Disaster recovery setup
- Advanced threat protection

### 13. Resource Naming Convention
Follow Azure naming conventions:
- Resource groups: `rg-{appName}-{environment}-{region}`
- App Services: `app-{appName}-{service}-{environment}`
- Storage accounts: `st{appName}{service}{environment}` (lowercase, no hyphens)
- Key Vaults: `kv-{appName}-{environment}`
- SQL Servers: `sql-{appName}-{environment}`
- Implement via `abbreviations.json` file

### 14. Cost Management
- Implement Azure budgets and cost alerts
- Configure autoscaling for cost optimization:
  - Scale down during off-hours
  - Scale to zero for non-production
- Use appropriate tiers for each environment
- Enable cost analysis tags:
  - Environment (dev/staging/prod)
  - Service (api/web/agenthouse)
  - Cost center
  - Owner

## Acceptance Criteria

1. ✅ `azd init` successfully creates azure.yaml manifest
2. ✅ All Bicep modules compile without errors
3. ✅ `azd provision` successfully creates all Azure resources (dev environment)
4. ✅ Resource naming follows Azure conventions
5. ✅ Managed identities configured for all services
6. ✅ Key Vault stores all secrets and connection strings
7. ✅ Application Insights collects telemetry from all services
8. ✅ SQL Database accessible from API service via managed identity
9. ✅ Azure AI Foundry workspace accessible from AgentHost
10. ✅ GitHub Actions workflow deploys successfully to dev environment
11. ✅ Health endpoints accessible and returning 200 OK
12. ✅ Monitoring dashboards created in Azure Portal
13. ✅ Alert rules trigger test notifications
14. ✅ All resources tagged appropriately
15. ✅ Infrastructure documented in docs/guides/deployment.md

## Testing Requirements

### Infrastructure Tests
- ✅ Bicep linting passes (`az bicep lint`)
- ✅ Bicep what-if analysis runs without errors
- ✅ Provisioning completes in <15 minutes
- ✅ All resources created in correct resource group
- ✅ Networking configuration allows required traffic

### Deployment Tests
- ✅ API deploys and health check succeeds
- ✅ Frontend deploys and loads successfully
- ✅ AgentHost starts and connects to Azure AI Foundry
- ✅ Database migrations apply successfully
- ✅ Application Insights receives telemetry

### Security Tests
- ✅ Managed identities can access Key Vault secrets
- ✅ SQL Server only accepts Azure AD authentication
- ✅ Storage account requires HTTPS only
- ✅ Key Vault soft delete enabled
- ✅ All services use TLS 1.2+

### Cost Tests
- ✅ Development environment costs <$50/day
- ✅ Autoscaling reduces costs during off-hours
- ✅ Budget alerts configured

## Dependencies

**Upstream:** 
- 001-task-backend-scaffolding.md (requires projects to deploy)

**Downstream:** 
- All feature deployments use this infrastructure

**External Services:**
- Azure subscription with appropriate permissions
- GitHub repository with Actions enabled
- Azure Container Registry (created by azd)

## Technical Decisions Required

1. **Compute Platform:** Container Apps vs. App Service (recommend Container Apps for flexibility)
2. **Database:** Azure SQL vs. Cosmos DB (recommend Azure SQL for ACID transactions)
3. **Networking:** Public endpoints vs. Private endpoints (recommend public for dev, private for prod)
4. **Agent Hosting:** Container Apps vs. Azure Functions (recommend Container Apps for Agent Framework)
5. **Deployment Tool:** azd vs. Terraform vs. Pulumi (recommend azd for Azure-native)

## Notes

- Follow Azure Well-Architected Framework principles
- Use Azure Verified Modules where available
- Implement least-privilege access with managed identities
- Enable diagnostic logging for all resources
- Document all infrastructure decisions in ADRs
- Test provisioning in clean subscription to verify reproducibility
- Create runbooks for common operational tasks
- Implement blue-green deployment for zero-downtime updates
- Use Azure Policies for governance (optional)

## Definition of Done

- [ ] All code merged to main branch
- [ ] Infrastructure provisions successfully in clean environment
- [ ] All unit tests for infrastructure modules passing (if applicable)
- [ ] GitHub Actions workflow deploys to dev environment
- [ ] Deployment guide documentation complete
- [ ] ADR created for compute and database choices
- [ ] Peer review completed with 2 approvals
- [ ] Infrastructure costs estimated and approved
- [ ] Monitoring and alerting verified
- [ ] Disaster recovery plan documented
