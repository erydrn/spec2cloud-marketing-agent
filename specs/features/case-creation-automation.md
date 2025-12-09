# Feature Requirements Document (FRD)

## Feature: Case Creation Automation

### 1. Overview

**Feature ID:** F003  
**Feature Name:** Case Creation Automation  
**PRD Reference:** REQ-4, REQ-6  
**Phase:** Pre-Case Creation  
**Priority:** Critical  
**Agent Owner:** New Business Agent

### 2. Purpose

Automate the processing of instruction forms and case creation in the Case Management System (CMS), eliminating manual data entry and ensuring accurate, consistent case setup for legal team assignment.

### 3. Scope

**In Scope:**
- Manual instruction form processing (digital and scanned documents)
- Automated data extraction from instruction forms
- Client and company record creation in CMS
- Case creation and configuration in CMS
- Client Care Letter (CCL) generation
- Inbox management for instruction form submissions
- Legal team assignment based on case type and workload
- Data validation and error handling
- Integration with CMS, CRM, and Lead Systems

**Out of Scope:**
- Initial lead capture (handled by Digital Marketing Agent)
- Sales qualification (handled by Sales Agent)
- Legal document processing (handled by Conveyancing Agent)
- Client onboarding activities post-case creation

### 4. User Stories

```gherkin
As a New Business Support team member, I want the system to automatically extract data from instruction forms, so that I don't have to manually re-type information into the CMS.
```

```gherkin
As a Legal Team Lead, I want cases to be automatically assigned to the appropriate legal team based on case type and workload, so that work is distributed efficiently.
```

```gherkin
As a New Business Agent, I want to validate extracted data against CRM records, so that duplicate clients are not created.
```

```gherkin
As a Client, I want to receive my Client Care Letter immediately after case creation, so that I understand the next steps and timeline.
```

### 5. Functional Requirements

#### 5.1 Instruction Form Processing
- **FR-3.1:** System must monitor designated inboxes for:
  - Email submissions with instruction forms (PDF, Word, images)
  - Referrals from Introducer Portal
  - Uploads from Digital Quoting system
  - Web form submissions

- **FR-3.2:** System must support instruction form types:
  - Purchase transaction forms
  - Sale transaction forms
  - Remortgage/transfer of equity forms
  - Custom firm-specific forms

- **FR-3.3:** For each received form, system must:
  - Classify form type automatically
  - Extract all data fields using OCR/AI
  - Validate extracted data against business rules
  - Flag incomplete or ambiguous data for manual review
  - Attach original form to case record

#### 5.2 Data Extraction
- **FR-3.4:** System must extract the following data points:
  - **Client Information:** Name, DOB, email, phone, address
  - **Property Details:** Address, price, tenure, property type
  - **Transaction Type:** Purchase, sale, remortgage
  - **Parties:** Buyer, seller, estate agent, mortgage broker
  - **Timeline:** Target completion date, key milestones
  - **Referral Information:** Source, tick-match code, partner details
  - **Financial:** Deposit amount, mortgage details, funding source

- **FR-3.5:** System must normalize extracted data:
  - Format phone numbers (UK standard)
  - Standardize addresses using Royal Mail PAF
  - Parse dates into consistent format
  - Validate email addresses
  - Convert currency values to numeric format

#### 5.3 Client & Company Record Management
- **FR-3.6:** Before creating new records, system must:
  - Search CRM for existing client records (name + DOB or email)
  - Search CRM for existing company records (name + address)
  - Prompt user to merge with existing record if match found (>80% confidence)
  - Flag potential duplicates for manual review (50-80% confidence)

- **FR-3.7:** System must create/update CRM records:
  - Client contact record with all personal information
  - Company record for corporate clients
  - Related party records (estate agents, brokers, co-buyers)
  - Link all records with appropriate relationships

#### 5.4 Case Creation in CMS
- **FR-3.8:** System must create case in CMS with:
  - Unique case reference number (auto-generated)
  - Case type (purchase/sale/remortgage)
  - Property address and details
  - All party information
  - Financial summary
  - Timeline and key dates
  - Attached instruction form and supporting documents

- **FR-3.9:** Case configuration must include:
  - Matter type and sub-type classification
  - Fee structure and billing arrangements
  - Service level (standard, premium, express)
  - Special handling flags (leasehold, new build, auction, etc.)
  - Referral/introducer commission details

#### 5.5 Client Care Letter (CCL) Generation
- **FR-3.10:** System must automatically generate CCL:
  - Populate template with client and case details
  - Include cost breakdown and payment terms
  - Attach terms of business and privacy policy
  - Generate as PDF with firm branding
  - Send via email with tracked delivery

- **FR-3.11:** CCL must include:
  - Welcome message and case reference
  - Assigned solicitor/case handler details
  - Itemized fee breakdown
  - Expected timeline and milestones
  - Document checklist (what client needs to provide)
  - Contact information and support channels

#### 5.6 Legal Team Assignment
- **FR-3.12:** System must assign case to legal team based on:
  - Case type (purchase vs. sale vs. remortgage)
  - Transaction complexity (leasehold, new build, shared ownership)
  - Team capacity and current workload
  - Geographic specialization (if applicable)
  - Client service level (premium cases to senior teams)

- **FR-3.13:** Assignment algorithm must:
  - Balance workload across available team members
  - Consider team member holiday/absence schedules
  - Route complex cases to experienced handlers
  - Allow manual override with approval

- **FR-3.14:** Upon assignment, system must:
  - Notify assigned solicitor/case handler via email
  - Update CMS case status to "Assigned - Awaiting Documentation"
  - Create initial task list for case handler
  - Send welcome email to client from assigned handler

#### 5.7 Inbox Management
- **FR-3.15:** System must provide inbox for New Business team:
  - Display all received instruction forms with processing status
  - Filter by status (new, in progress, completed, error)
  - Allow manual processing for forms with extraction errors
  - Provide bulk actions (assign, close, delete)
  - Show processing metrics (SLA compliance, throughput)

- **FR-3.16:** Error handling:
  - Flag forms with low confidence extraction (<70%)
  - Highlight missing required fields
  - Provide side-by-side view (original form + extracted data)
  - Allow manual correction and resubmission

#### 5.8 Data Validation & Quality
- **FR-3.17:** System must validate:
  - All required fields are present
  - Email and phone formats are correct
  - Addresses are valid UK postcodes
  - Transaction value is within acceptable range
  - Timeline is realistic (completion date in future)
  - No conflicting data (e.g., sale price ≠ purchase price in chain)

- **FR-3.18:** System must enforce business rules:
  - Minimum transaction value (if applicable)
  - Service area coverage (postcode validation)
  - Compliance with firm's acceptance criteria
  - Conflict of interest checks (representing both parties)

### 6. Non-Functional Requirements

#### 6.1 Performance
- **NFR-3.1:** Instruction form processing must complete within 5 minutes
- **NFR-3.2:** Data extraction accuracy must be ≥ 95% for typed forms
- **NFR-3.3:** Data extraction accuracy must be ≥ 85% for handwritten forms
- **NFR-3.4:** Case creation in CMS must complete within 30 seconds

#### 6.2 Reliability
- **NFR-3.5:** System must handle 200+ instruction forms per day
- **NFR-3.6:** Failed extractions must not lose original form data
- **NFR-3.7:** CMS integration must retry failed operations automatically

#### 6.3 Usability
- **NFR-3.8:** Manual review interface must allow corrections in ≤ 2 minutes
- **NFR-3.9:** System must provide inline help for field definitions

#### 6.4 Integration
- **NFR-3.10:** CMS API must support real-time case creation
- **NFR-3.11:** CRM sync must maintain referential integrity
- **NFR-3.12:** System must handle CMS downtime gracefully with queuing

#### 6.5 Compliance
- **NFR-3.13:** All data extraction must be auditable with confidence scores
- **NFR-3.14:** System must log all automated decisions for compliance review

### 7. Dependencies

**Upstream Dependencies:**
- Sales Lead Management feature (F002) - provides instruction trigger
- Email server access for inbox monitoring
- OCR/AI service for data extraction

**Downstream Dependencies:**
- CMS with REST API or equivalent integration
- CRM system for client/company records
- Client Onboarding feature (F004)
- Email notification service for CCL delivery

**External Services:**
- Azure AI Document Intelligence or equivalent OCR service
- Royal Mail Postcode Address File (PAF) for address validation
- Email validation service

### 8. Acceptance Criteria

```gherkin
Given a new instruction form is received via email
When the New Business Agent processes the form
Then the data is extracted with >90% accuracy
And a client record is created in CRM
And a case is created in CMS with unique reference
And the Client Care Letter is generated and sent
And the case is assigned to the appropriate legal team
And the entire process completes within 24 hours
```

```gherkin
Given an instruction form has incomplete data
When the extraction completes
Then the form is flagged for manual review
And the New Business team member can correct the data
And the corrected data is used to create the case
And no data is lost in the process
```

```gherkin
Given 100 instruction forms are processed
When reviewing extraction accuracy
Then at least 95 have all required fields extracted correctly
And at least 90 result in successful case creation without manual intervention
And no duplicate client records are created
```

### 9. Success Metrics

- **Case Creation Time:** < 24 hours from instruction to case assignment (PRD target)
- **Automation Rate:** ≥ 80% of forms processed without manual intervention
- **Data Accuracy:** ≥ 95% extraction accuracy for required fields
- **Duplicate Prevention:** < 2% duplicate client/case records created
- **CCL Delivery:** 100% of cases receive CCL within 1 hour of creation
- **Team Assignment:** 100% of cases assigned within 4 hours of creation

### 10. Open Questions

1. What is the expected daily volume of instruction forms?
2. Are there legacy forms that need to be supported?
3. What is the process for handling joint buyers/sellers?
4. Should the system support multi-language instruction forms?
5. What is the escalation process for forms that fail automated processing?
6. Are there specific CMS platforms that must be supported?
7. What happens if CMS is unavailable during peak submission times?

### 11. Future Enhancements

- AI-powered handwriting recognition for complex forms
- Intelligent form classification using machine learning
- Automated quality scoring for case viability
- Pre-validation of instruction data before submission (web form)
- Integration with digital identity verification services
- Blockchain-based audit trail for case creation
- Predictive timeline estimation based on historical data
- Mobile app for instruction form submission with camera capture

---

**Document Version:** 1.0  
**Last Updated:** December 9, 2025  
**Author:** Product Manager  
**Status:** Draft
