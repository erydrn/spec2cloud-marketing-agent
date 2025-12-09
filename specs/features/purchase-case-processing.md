# Feature Requirements Document (FRD)

## Feature: Purchase Case Processing

### 1. Overview

**Feature ID:** F005  
**Feature Name:** Purchase Case Processing  
**PRD Reference:** REQ-7, REQ-9, REQ-14  
**Phase:** Pre-Completion Legal Work  
**Priority:** Critical  
**Agent Owner:** Conveyancing Agent (Purchase)

### 2. Purpose

Automate the conveyancing workflow for property purchase transactions, including contract pack review, property searches, mortgage offer handling, enquiries, title reporting, and source of funds checks to accelerate the path to exchange and completion.

### 3. Scope

**In Scope:**
- Contract pack receipt and automated review
- Title document analysis and risk identification
- Property search ordering, tracking, and review
- Mortgage offer receipt and validation
- Enquiries (questions to seller's solicitor) generation and tracking
- Title report generation for client and lender
- Contract replies review and signing coordination
- Source of funds final verification
- Pre-exchange readiness assessment

**Out of Scope:**
- Client onboarding (handled by F004)
- Contract exchange execution (handled by F008)
- Completion and fund transfer (handled by F009)
- Post-completion registration (handled by F011)

### 4. User Stories

```gherkin
As a Conveyancing Agent (Purchase), I want automated contract pack review that highlights key issues, so that I can focus on complex legal analysis rather than data extraction.
```

```gherkin
As a Conveyancing Solicitor, I want the system to automatically order property searches from suppliers, so that I don't have to manually coordinate with multiple providers.
```

```gherkin
As a Conveyancing Agent (Purchase), I want automated tracking of all required documents (ID, AML, mortgage offers, searches), so that I know exactly what's missing before completion.
```

```gherkin
As a Client, I want to understand the title risks and property issues in plain language, so that I can make informed decisions about proceeding with the purchase.
```

### 5. Functional Requirements

#### 5.1 Contract Pack Receipt & Review
- **FR-5.1:** System must receive contract pack from seller's solicitor via:
  - Email attachment
  - Secure file transfer portal
  - Integration with property exchange networks

- **FR-5.2:** Contract pack must include:
  - Draft contract
  - Title documents (official copies, title plan)
  - Seller's Property Information Form (TA6)
  - Seller's Fittings and Contents Form (TA10)
  - Lease documents (if leasehold)
  - Management pack (if leasehold)
  - Planning documents, warranties, certificates

- **FR-5.3:** System must perform automated contract pack review:
  - Extract key clauses and special conditions
  - Identify property boundaries from title plan
  - Check title restrictions and covenants
  - Validate seller's legal ownership
  - Highlight leasehold terms (ground rent, service charge, lease length)
  - Flag unusual or problematic clauses
  - Generate risk summary with RAG (Red/Amber/Green) rating

- **FR-5.4:** System must validate:
  - Property address matches instruction
  - Purchase price matches agreed amount
  - Completion date is feasible
  - Deposit amount is correct
  - All required documents are present

#### 5.2 Title Document Analysis
- **FR-5.5:** System must analyze title documents:
  - Extract title number and property description
  - Identify registered proprietors
  - List all charges (mortgages, restrictions)
  - Extract restrictive covenants and easements
  - Identify title defects or cautions
  - Check for adverse entries

- **FR-5.6:** Risk identification must flag:
  - **High Risk:** Missing title, adverse possessors, unresolved restrictions
  - **Medium Risk:** Restrictive covenants affecting use, informal easements
  - **Low Risk:** Standard covenants, registered charges (to be discharged)

#### 5.3 Property Searches
- **FR-5.7:** System must automatically order searches:
  - Local Authority Search (LLC1, CON29)
  - Environmental Search (contamination, flood risk, radon)
  - Chancel Repair Liability Search
  - Water & Drainage Search
  - Additional searches based on property type/location

- **FR-5.8:** Search ordering must:
  - Select approved suppliers from firm's panel
  - Submit property details electronically
  - Track order status and expected return date
  - Send reminders if searches overdue
  - Receive search results automatically

- **FR-5.9:** System must review search results:
  - Extract key findings and risks
  - Highlight adverse entries (planning issues, contamination, flooding)
  - Generate plain-language summary for client
  - Flag issues requiring further investigation
  - Calculate insurance requirements if applicable

#### 5.4 Mortgage Offer Processing
- **FR-5.10:** System must receive and validate mortgage offer:
  - Extract lender, amount, interest rate, term
  - Verify loan amount + deposit = purchase price
  - Check special conditions and retention clauses
  - Validate property valuation matches purchase price
  - Identify any unusual requirements

- **FR-5.11:** System must generate mortgage offer report:
  - Summarize key terms for client
  - Highlight any concerns (retention, repair conditions)
  - Confirm Certificate of Title requirements
  - Create lender task list (specific requirements to fulfill)

#### 5.5 Enquiries (Questions to Seller)
- **FR-5.12:** System must generate enquiries automatically:
  - Standard enquiries based on property type
  - Specific enquiries from contract pack review
  - Specific enquiries from search results
  - Client-requested enquiries from questionnaire
  - Lender-required enquiries from mortgage offer

- **FR-5.13:** Enquiry management:
  - Send enquiries to seller's solicitor
  - Track response deadline (typically 5 business days)
  - Log replies as received
  - Flag unsatisfactory or incomplete replies
  - Generate follow-up enquiries if needed

- **FR-5.14:** Reply review must:
  - Extract key information from replies
  - Identify discrepancies with contract pack
  - Flag new risks or concerns
  - Update case risk assessment
  - Notify solicitor of material changes

#### 5.6 Title Report to Client & Lender
- **FR-5.15:** System must generate title report for client:
  - Plain-language summary of property and title
  - Key findings from searches and enquiries
  - Identified risks and recommended actions
  - Conditions that will affect property use
  - Recommendation to proceed or not

- **FR-5.16:** System must generate Certificate of Title for lender:
  - Confirm good and marketable title
  - Confirm property valuation is reasonable
  - Disclose any adverse findings
  - Confirm compliance with lender requirements
  - Sign-off by qualified solicitor (human approval)

#### 5.7 Source of Funds Final Verification
- **FR-5.17:** Before exchange, system must re-verify:
  - Deposit funds are available in client account
  - Mortgage funds are confirmed by lender
  - All source of funds documentation is current (<6 months)
  - No changes in client circumstances (employment, credit)
  - AML clearance is still valid

#### 5.8 Pre-Exchange Readiness
- **FR-5.18:** System must validate exchange readiness:
  - [ ] Contract pack received and reviewed
  - [ ] All searches completed and reviewed
  - [ ] Mortgage offer received and validated
  - [ ] Enquiries satisfactorily answered
  - [ ] Title report issued to client
  - [ ] Certificate of Title issued to lender
  - [ ] Source of funds verified
  - [ ] Deposit funds received in client account
  - [ ] Insurance arranged (buildings insurance from exchange)
  - [ ] Client approval to exchange obtained

- **FR-5.19:** When readiness achieved, system must:
  - Update case status to "Ready to Exchange"
  - Notify solicitor and client
  - Coordinate with seller's solicitor for exchange date
  - Prepare exchange documents

### 6. Non-Functional Requirements

#### 6.1 Performance
- **NFR-5.1:** Contract pack review must complete within 30 minutes
- **NFR-5.2:** Search ordering must complete within 5 minutes
- **NFR-5.3:** Title report generation must complete within 10 minutes

#### 6.2 Accuracy
- **NFR-5.4:** Contract data extraction accuracy must be ≥ 98%
- **NFR-5.5:** Risk identification recall must be ≥ 95% (detect 95% of risks)
- **NFR-5.6:** False positive rate for risk flagging must be < 10%

#### 6.3 Integration
- **NFR-5.7:** Search suppliers must have API integration or secure portal
- **NFR-5.8:** System must support major search providers (TM Group, Groundsure, Searchflow)

### 7. Dependencies

**Upstream Dependencies:**
- Client Onboarding feature (F004)
- Case Creation Automation feature (F003)

**Downstream Dependencies:**
- Contract Exchange feature (F008)
- Supplier Agent coordination
- Tech Agent for integrations

**External Services:**
- Property search providers
- Land Registry API
- Lender portals for mortgage offer receipt
- Property information databases

### 8. Acceptance Criteria

```gherkin
Given a contract pack is received from seller's solicitor
When the Conveyancing Agent processes the pack
Then all documents are extracted and classified
And key contract terms are identified
And title risks are flagged with severity ratings
And property searches are automatically ordered
And a summary report is generated
And the process completes within 30 minutes
```

```gherkin
Given all searches have been received
When the system reviews the search results
Then key findings are extracted
And risks are highlighted in plain language
And enquiries are automatically generated based on findings
And the client receives a summary report
```

```gherkin
Given all pre-exchange requirements are met
When the system validates readiness
Then the case status updates to "Ready to Exchange"
And the solicitor is notified
And a readiness checklist is displayed with all items completed
```

### 9. Success Metrics

- **Contract Review Time:** < 2 hours from receipt to report
- **Search Turnaround:** < 7 days from order to receipt
- **Enquiry Response Time:** < 5 days from sending to receipt
- **Document Processing Automation:** ≥ 90% of standard documents
- **Risk Identification Accuracy:** ≥ 95% of material risks flagged
- **Exchange Readiness Time:** < 4 weeks from case creation to ready

### 10. Open Questions

1. Which search providers should be integrated in Phase 1?
2. What is the threshold for "material risk" requiring solicitor review?
3. Should the system generate enquiries in specific firm format?
4. What level of human review is required for automated reports?
5. Should the system support indemnity insurance quotes for title defects?

### 11. Future Enhancements

- AI-powered contract negotiation suggestions
- Predictive timeline estimation based on historical data
- Automated indemnity insurance ordering
- Integration with property blockchain registries
- Real-time collaboration with seller's solicitor
- Automated risk mitigation recommendations

---

**Document Version:** 1.0  
**Last Updated:** December 9, 2025  
**Author:** Product Manager  
**Status:** Draft
