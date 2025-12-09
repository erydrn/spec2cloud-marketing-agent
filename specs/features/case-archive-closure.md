# Feature Requirements Document (FRD)

## Feature: Case Archive & Closure

### 1. Overview

**Feature ID:** F012  
**Feature Name:** Case Archive & Closure  
**PRD Reference:** REQ-20, REQ-22  
**Phase:** Post-Completion Work  
**Priority:** High  
**Agent Owner:** Complete Agent

### 2. Purpose

Automate the final case closure and archival process, ensuring all documents are properly stored, regulatory retention requirements are met, and case data is accessible for future reference while maintaining compliance and operational efficiency.

### 3. Scope

**In Scope:**
- Case closure checklist and validation
- Document collection and organization
- Archive package preparation
- Regulatory retention policy enforcement
- Secure long-term storage
- Archive retrieval and search
- Disposal/deletion after retention period
- Client file delivery (if requested)
- Closure reporting and analytics

**Out of Scope:**
- Active case management
- Ongoing client relationship management (CRM)
- Physical document storage management
- Backup and disaster recovery (infrastructure concern)

### 4. User Stories

```gherkin
As a Complete Agent, I want automated case closure checklist, so that I ensure all post-completion tasks are finished before archiving.
```

```gherkin
As a Compliance Officer, I want proper document retention, so that we meet regulatory requirements and can retrieve files if needed.
```

```gherkin
As an Operations Manager, I want automated archival, so that we reduce storage costs and maintain organized records.
```

```gherkin
As a Client, I want to access my archived documents years later, so that I can reference them for future transactions or disputes.
```

### 5. Functional Requirements

#### 5.1 Case Closure Checklist
- **FR-12.1:** System must validate closure readiness:
  - [ ] Registration completed with Land Registry
  - [ ] Title documents received and sent to client
  - [ ] All invoices raised and paid
  - [ ] No outstanding disbursements
  - [ ] No client funds held (all refunded or transferred)
  - [ ] File review completed by supervising solicitor
  - [ ] Client satisfaction survey sent
  - [ ] Complaints or issues resolved
  - [ ] All documents filed in case folder

- **FR-12.2:** Closure validation must:
  - Check each requirement automatically where possible
  - Flag outstanding items for manual resolution
  - Require supervisor approval to close
  - Prevent closure if critical items incomplete
  - Log closure authorization

- **FR-12.3:** Financial closure validation:
  - Client account balance = £0.00
  - Office account settled
  - No pending transactions
  - All bills sent and payment status recorded
  - Aged debt reviewed (if unpaid invoices)

#### 5.2 Document Collection & Organization
- **FR-12.4:** System must collect all case documents:
  - Instruction form and CCL
  - ID and AML documents
  - Contract pack (sent and received)
  - Searches and reports
  - Mortgage documents
  - Correspondence (emails, letters)
  - Completion documents
  - SDLT certificate
  - Registration documents
  - Title register and plan
  - Financial records (statements, invoices)

- **FR-12.5:** Document organization must:
  - Create standardized folder structure
  - Categorize documents by type
  - Remove duplicate copies
  - Maintain chronological order
  - Index all documents for search
  - Generate document inventory list

- **FR-12.6:** Document quality checks:
  - All documents readable (not corrupt)
  - Proper file formats (PDF preferred)
  - Appropriate resolution for scanned documents
  - Sensitive data redacted if required
  - Original signatures preserved

#### 5.3 Archive Package Preparation
- **FR-12.7:** Archive package must include:
  - Complete document set
  - Case metadata (client, property, dates, amounts)
  - Document index/catalog
  - Key milestones and timeline
  - Financial summary
  - Closure report

- **FR-12.8:** Metadata must capture:
  - Client name(s)
  - Property address
  - Case reference number
  - Transaction type (purchase/sale)
  - Completion date
  - Registration date
  - Key parties (estate agent, lender, other solicitor)
  - File handler/supervising solicitor
  - Storage location and identifier

- **FR-12.9:** Archive format must:
  - Support long-term preservation (PDF/A standard)
  - Be encrypted for security
  - Include integrity checksums
  - Be compressed to optimize storage
  - Be platform-independent

#### 5.4 Retention Policy Enforcement
- **FR-12.10:** Retention periods must be configured:
  - **Conveyancing files:** Minimum 6 years (SRA requirement)
  - **Extended for specific cases:** Leasehold (15+ years), disputes (until resolved + 6 years)
  - **Financial records:** 6 years (tax requirement)
  - **Identity documents:** Per AML retention requirements

- **FR-12.11:** Retention tracking must:
  - Calculate retention end date upon archival
  - Track remaining retention time
  - Alert before disposal date
  - Support retention extension (if case reopened or dispute)
  - Log all retention decisions

- **FR-12.12:** Disposal process must:
  - Identify cases eligible for disposal
  - Require approval before deletion
  - Securely delete data (unrecoverable)
  - Generate disposal certificate
  - Maintain disposal audit log

#### 5.5 Secure Storage
- **FR-12.13:** Storage must provide:
  - Cloud-based storage (Azure Blob Storage or equivalent)
  - Encryption at rest (AES-256)
  - Geo-redundant backup
  - Access logging and monitoring
  - Compliance with data protection regulations

- **FR-12.14:** Storage organization:
  - Hierarchical structure (year > month > case)
  - Efficient retrieval mechanisms
  - Storage optimization (compression, deduplication)
  - Scalability for growing archive

- **FR-12.15:** Access control must enforce:
  - Role-based permissions
  - Need-to-know principle
  - Multi-factor authentication for sensitive cases
  - Audit trail of all access
  - Time-limited access for external parties

#### 5.6 Archive Retrieval & Search
- **FR-12.16:** Search must support queries by:
  - Client name
  - Property address
  - Case reference number
  - Date range (instruction, completion, registration)
  - File handler
  - Transaction type
  - Full-text search within documents

- **FR-12.17:** Retrieval must provide:
  - Quick access to archive package
  - Individual document download
  - Bulk export option
  - Preview without full download
  - Download history tracking

- **FR-12.18:** Performance requirements:
  - Search results within 5 seconds
  - Document retrieval within 30 seconds
  - Support for concurrent retrievals

#### 5.7 Client File Delivery
- **FR-12.19:** If client requests file copy:
  - Generate client-safe version (remove internal notes, redact as needed)
  - Provide via secure download link
  - Support physical copy request (CD/USB)
  - Charge fee if applicable
  - Track delivery and receipt

- **FR-12.20:** Client portal access:
  - Allow clients to access their archived files
  - Provide limited time access (e.g., 7 days)
  - Restrict to client's own cases
  - Log all client access

#### 5.8 Closure Reporting & Analytics
- **FR-12.21:** Closure reports must show:
  - Cases closed per period
  - Average time from completion to closure
  - Outstanding closure items
  - Aged unclosed files
  - Archive storage metrics

- **FR-12.22:** Analytics should provide:
  - Closure bottlenecks
  - Document completeness trends
  - Retention cost projections
  - Compliance performance

### 6. Non-Functional Requirements

#### 6.1 Storage Efficiency
- **NFR-12.1:** Archive compression must achieve ≥ 60% size reduction
- **NFR-12.2:** Deduplication must eliminate redundant copies
- **NFR-12.3:** Storage cost per case must be < £5/year

#### 6.2 Retrieval Performance
- **NFR-12.4:** Search must return results within 5 seconds
- **NFR-12.5:** Document download must begin within 30 seconds
- **NFR-12.6:** System must support 50+ concurrent retrievals

#### 6.3 Data Integrity
- **NFR-12.7:** Archive integrity checks must run monthly
- **NFR-12.8:** Corrupted archives must alert immediately
- **NFR-12.9:** Backup must support point-in-time recovery

#### 6.4 Security
- **NFR-12.10:** All archives encrypted at rest
- **NFR-12.11:** Secure deletion must meet DOD 5220.22-M standard
- **NFR-12.12:** Access logs retained for 7 years

#### 6.5 Compliance
- **NFR-12.13:** Must meet SRA record-keeping requirements
- **NFR-12.14:** Must comply with GDPR data retention rules
- **NFR-12.15:** Audit trail required for all archive operations

### 7. Dependencies

**Upstream Dependencies:**
- Land Registry Registration (F011)
- Completion Fund Management (F009)
- Document Automation (F007)

**Downstream Dependencies:**
- Cloud storage service (Azure Blob Storage)
- Document management system
- CRM for client relationship continuation

**External Services:**
- Azure Blob Storage or AWS S3
- Encryption services
- Search indexing service (Azure Cognitive Search)

### 8. Acceptance Criteria

```gherkin
Given a case has completed and registered
When the Complete Agent initiates closure
Then closure checklist is validated
And all documents are collected and organized
And archive package is prepared with metadata
And package is stored securely in cloud
And retention period is calculated and tracked
And case status updates to "Archived"
```

```gherkin
Given 100 cases are archived
When searching for a specific case by address
Then search returns results within 5 seconds
And user can download the archive package
And all documents are intact and readable
And access is logged in audit trail
```

```gherkin
Given a case retention period has expired
When disposal process runs
Then case is identified for disposal
And supervisor approves disposal
And data is securely deleted
And disposal certificate is generated
And audit log records the disposal
```

### 9. Success Metrics

- **Closure Timeliness:** ≥ 95% of cases closed within 30 days of registration
- **Document Completeness:** ≥ 99% of archived cases have all required documents
- **Retrieval Success:** 100% of retrievals successful within 30 seconds
- **Storage Cost:** < £5 per case per year
- **Compliance:** Zero retention policy violations
- **Search Performance:** 100% of searches complete within 5 seconds

### 10. Open Questions

1. Should physical document storage be supported alongside digital?
2. What is the process for reopening archived cases?
3. Should clients have ongoing portal access to archived files?
4. What approval is required for early disposal (before retention ends)?
5. Should there be different retention periods for different case types?

### 11. Future Enhancements

- AI-powered document categorization
- Automated file review and quality scoring
- Predictive retention period adjustment based on case characteristics
- Blockchain-based immutable archive records
- Advanced analytics on closed case patterns
- Integration with client communication for archive access offers
- Automated disposal with AI risk assessment
- Perpetual archive option for premium service

---

**Document Version:** 1.0  
**Last Updated:** December 9, 2025  
**Author:** Product Manager  
**Status:** Draft
