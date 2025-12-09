# Feature Requirements Document (FRD)

## Feature: Land Registry Registration

### 1. Overview

**Feature ID:** F011  
**Feature Name:** Land Registry Registration  
**PRD Reference:** REQ-19, REQ-21  
**Phase:** Post-Completion Work  
**Priority:** Critical  
**Agent Owner:** Post Agent

### 2. Purpose

Automate the post-completion registration process with HM Land Registry, including SDLT (Stamp Duty Land Tax) submission, document preparation, electronic submission via e-DRS, and tracking through to registration completion.

### 3. Scope

**In Scope:**
- SDLT form completion and submission to HMRC
- Registration document preparation (AP1, TR1, DS1, etc.)
- Electronic Document Registration Service (e-DRS) submission
- Application tracking and status monitoring
- Requisition handling and responses
- Registration completion confirmation
- Title document retrieval
- Cancellation and amendment handling

**Out of Scope:**
- SDLT tax advice or optimization
- Complex land registration cases requiring manual handling
- First registration of unregistered land (Phase 2)
- Leasehold registration (Phase 2)

### 4. User Stories

```gherkin
As a Post Agent, I want automated SDLT form completion, so that I can submit returns to HMRC within the 14-day deadline without manual data entry.
```

```gherkin
As a Conveyancing Solicitor, I want electronic submission to Land Registry, so that registration completes faster and without paper handling.
```

```gherkin
As a Post Agent, I want automatic tracking of registration applications, so that I know the status without manually checking the portal.
```

```gherkin
As a Client, I want confirmation when my property is registered, so that I know my ownership is officially recorded.
```

### 5. Functional Requirements

#### 5.1 SDLT Processing
- **FR-11.1:** System must determine SDLT applicability:
  - Purchase price threshold (currently £250,000 for residential)
  - First-time buyer relief
  - Additional property surcharge (3%)
  - Non-residential vs residential rates
  - Exemptions and reliefs

- **FR-11.2:** SDLT form completion must:
  - Auto-populate SDLT1 or SDLT3 form
  - Calculate tax due based on current rates
  - Apply relevant reliefs automatically
  - Validate all required fields
  - Generate form in HMRC format

- **FR-11.3:** SDLT form data must include:
  - Property address and description
  - Purchase price and consideration
  - Buyer details (name, address, NINO)
  - Effective date of transaction
  - Property type (freehold/leasehold)
  - Additional property indicator
  - First-time buyer indicator
  - Tax calculation breakdown

- **FR-11.4:** SDLT submission must:
  - Submit electronically to HMRC portal
  - Receive SDLT reference number
  - Download SDLT5 certificate
  - Store certificate in case file
  - Track submission status

- **FR-11.5:** SDLT payment coordination:
  - Generate payment reference
  - Provide payment instructions to client
  - Confirm payment made
  - Obtain receipt from HMRC
  - Link payment to registration application

#### 5.2 Registration Document Preparation
- **FR-11.6:** System must prepare registration documents:
  - **AP1:** Application to register
  - **TR1:** Transfer of whole registered title
  - **DS1:** Discharge of mortgage (if seller had mortgage)
  - **OS1:** Official search result
  - **DI:** Disclosable interests (if applicable)

- **FR-11.7:** AP1 form must include:
  - Title number
  - Property address
  - Nature of application (transfer, discharge)
  - Applicant details
  - Fee calculation
  - Priority protection reference (from OS1)
  - Supporting documents list

- **FR-11.8:** TR1 transfer deed must:
  - Identify property by title number
  - State transferor (seller) and transferee (buyer)
  - Declare consideration (price paid)
  - Include covenants and easements
  - Be properly executed (signed)
  - Include witness details

- **FR-11.9:** Document validation must check:
  - All mandatory fields completed
  - Signatures present and valid
  - Dates correct and consistent
  - Consideration matches completion statement
  - No errors or omissions

#### 5.3 e-DRS Submission
- **FR-11.10:** Electronic submission must:
  - Connect to Land Registry portal
  - Upload all registration documents
  - Include SDLT5 certificate
  - Pay registration fee electronically
  - Receive acknowledgment and reference number

- **FR-11.11:** Submission preparation:
  - Scan signed documents (if not already electronic)
  - Convert to approved formats (PDF/TIF)
  - Validate file sizes and quality
  - Compile complete application bundle
  - Perform pre-submission checks

- **FR-11.12:** Pre-submission validation:
  - All required documents present
  - SDLT certificate obtained
  - Priority period still valid (OS1 < 30 business days)
  - Fee calculation correct
  - Applicant authority confirmed

- **FR-11.13:** Submission tracking must:
  - Store application reference number
  - Track submission date and time
  - Monitor processing status
  - Alert on status changes
  - Calculate expected completion date

#### 5.4 Application Tracking & Status Monitoring
- **FR-11.14:** System must poll Land Registry for status:
  - Frequency: Daily for pending applications
  - Status values: Pending, In Progress, Requisition Issued, Completed, Cancelled
  - Automated status updates to case record
  - Notifications on status changes

- **FR-11.15:** Dashboard must display:
  - All pending applications
  - Time since submission
  - Current status
  - Expected completion date
  - Overdue applications (>20 business days)
  - Requisitions requiring action

#### 5.5 Requisition Handling
- **FR-11.16:** When requisition issued by Land Registry:
  - System must receive requisition notice
  - Parse requisition details
  - Categorize by type (document, clarification, payment)
  - Assign to appropriate team member
  - Set response deadline (typically 15 business days)

- **FR-11.17:** Common requisitions to handle:
  - Missing document or signature
  - Unclear execution
  - Fee shortfall
  - Title discrepancy
  - Identity verification
  - SDLT issue

- **FR-11.18:** Requisition response workflow:
  - Review requisition requirements
  - Gather required documents/information
  - Prepare response
  - Submit via e-DRS or portal
  - Track response submission
  - Monitor for further requisitions

#### 5.6 Registration Completion
- **FR-11.19:** Upon registration completion:
  - Receive completion notification from Land Registry
  - Download new title register (official copy)
  - Download updated title plan
  - Store in case file and document management
  - Update case status to "Registered"

- **FR-11.20:** Post-registration actions:
  - Send completion confirmation to client
  - Send title documents to client (via portal or post)
  - Send copy to lender (if mortgage)
  - Notify accounts team (for file closure)
  - Create archive task

- **FR-11.21:** Client notification must include:
  - Confirmation of registration
  - New title number (if changed)
  - Title documents (register and plan)
  - Explanation of what has been registered
  - Next steps (if any)

#### 5.7 SDLT12 Renewals
- **FR-11.22:** For SDLT12 certificates expiring:
  - Identify applications with expiring SDLT12 (>12 months old)
  - Request renewal from HMRC
  - Submit renewed certificate to Land Registry
  - Track renewal status

#### 5.8 Cancellation & Amendment Handling
- **FR-11.23:** If application must be cancelled:
  - Submit cancellation request to Land Registry
  - Provide reason for cancellation
  - Confirm cancellation receipt
  - Update case status
  - Notify stakeholders

- **FR-11.24:** If amendment required:
  - Identify error or omission
  - Prepare amendment application
  - Submit corrected documents
  - Track amendment processing
  - Confirm amendment completion

### 6. Non-Functional Requirements

#### 6.1 Performance
- **NFR-11.1:** SDLT form completion must complete within 2 minutes
- **NFR-11.2:** Registration document generation must complete within 5 minutes
- **NFR-11.3:** e-DRS submission must complete within 10 minutes

#### 6.2 Accuracy
- **NFR-11.4:** SDLT calculations must be 100% accurate
- **NFR-11.5:** Document data population must be error-free
- **NFR-11.6:** Form validation must catch 100% of errors before submission

#### 6.3 Timeliness
- **NFR-11.7:** SDLT submission must occur within 14 days of completion (legal requirement)
- **NFR-11.8:** Registration application must submit within 30 days of completion (priority period)
- **NFR-11.9:** Requisitions must be responded to within 15 days

#### 6.4 Integration
- **NFR-11.10:** Must integrate with Land Registry portal/API
- **NFR-11.11:** Must integrate with HMRC SDLT gateway
- **NFR-11.12:** Must handle Land Registry downtime gracefully

#### 6.5 Compliance
- **NFR-11.13:** Must comply with SDLT filing requirements
- **NFR-11.14:** Must meet Land Registry practice guides
- **NFR-11.15:** Audit trail required for all submissions

### 7. Dependencies

**Upstream Dependencies:**
- Completion Fund Management (F009)
- Document Automation (F007)
- Completion confirmation and documents

**Downstream Dependencies:**
- Land Registry portal/API access
- HMRC SDLT gateway access
- Case Archive & Closure (F012)

**External Services:**
- HM Land Registry e-DRS system
- HMRC SDLT online service
- Document scanning/conversion services

### 8. Acceptance Criteria

```gherkin
Given a purchase completion has occurred
When the Post Agent initiates registration
Then SDLT form is auto-populated with case data
And SDLT tax is calculated correctly
And form is submitted to HMRC within 14 days
And SDLT5 certificate is received and stored
```

```gherkin
Given SDLT certificate is obtained
When registration documents are prepared
Then AP1, TR1, and supporting docs are generated
And all forms are validated for completeness
And application is submitted via e-DRS
And submission reference is received
And tracking begins automatically
```

```gherkin
Given an application is submitted
When Land Registry issues requisition
Then the system receives and parses the requisition
And the team is notified
And response is prepared and submitted within 15 days
And application progresses to completion
And client receives title documents within 5 business days
```

### 9. Success Metrics

- **Registration Time:** < 5 business days from submission to completion (PRD target)
- **SDLT Compliance:** 100% submissions within 14-day deadline
- **First-Time Success:** ≥ 90% applications complete without requisitions
- **Requisition Response:** 100% responses within 15-day deadline
- **Automation Rate:** ≥ 85% of standard registrations fully automated
- **Client Notification:** 100% receive title documents within 5 days of completion

### 10. Open Questions

1. Should system support Business Gateway (portal) and/or e-DRS API?
2. What backup process if Land Registry systems unavailable?
3. Should system support historical SDLT reclaims or refunds?
4. How should complex title issues (restrictive covenants) be handled?
5. Should clients receive physical or only digital title documents?

### 11. Future Enhancements

- AI-powered requisition prediction and prevention
- Automated response to common requisitions
- Integration with digital identity for instant verification
- Blockchain-based land registry (future-proofing)
- Predictive analytics for registration timeline
- Automated SDLT optimization suggestions
- Real-time collaboration with Land Registry case workers
- Support for complex registrations (first registration, leasehold)

---

**Document Version:** 1.0  
**Last Updated:** December 9, 2025  
**Author:** Product Manager  
**Status:** Draft
