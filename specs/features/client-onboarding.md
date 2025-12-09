# Feature Requirements Document (FRD)

## Feature: Client Onboarding

### 1. Overview

**Feature ID:** F004  
**Feature Name:** Client Onboarding  
**PRD Reference:** REQ-8  
**Phase:** Pre-Completion Legal Work  
**Priority:** Critical  
**Agent Owner:** Conveyancing Agent

### 2. Purpose

Automate the client onboarding process including Client Care Letter (CCL) management, client questionnaire completion, identity verification, Anti-Money Laundering (AML) checks, and bank Terms of Business (TOB) review to ensure compliance and readiness for conveyancing work.

### 3. Scope

**In Scope:**
- Signed CCL receipt and review
- Client questionnaire delivery and completion tracking
- Identity document collection and verification
- AML checks and risk assessment
- Bank Terms of Business review
- Source of funds verification
- Client portal access provisioning
- Compliance documentation and audit trail

**Out of Scope:**
- Initial case creation (handled by New Business Agent)
- Property-specific legal work (searches, contracts)
- Financial advice or mortgage brokering
- Legal advice on tax or financial planning

### 4. User Stories

```gherkin
As a Conveyancing Agent, I want to automatically track receipt of signed CCL, so that I know the client has formally engaged our services.
```

```gherkin
As a Client, I want to complete my questionnaire and upload ID documents via a secure portal, so that I don't have to visit the office or mail documents.
```

```gherkin
As a Compliance Officer, I want automated AML checks completed for every client, so that we meet regulatory obligations without manual processing delays.
```

```gherkin
As a Conveyancing Solicitor, I want all onboarding requirements tracked in one place, so that I know when a case is ready to progress to contract review.
```

### 5. Functional Requirements

#### 5.1 Client Care Letter (CCL) Management
- **FR-4.1:** System must track CCL delivery and acknowledgment:
  - Record date/time CCL sent to client
  - Monitor client portal access (CCL viewed)
  - Track electronic signature or email confirmation
  - Flag CCL as "pending" if not signed within 5 business days

- **FR-4.2:** Upon CCL receipt, system must:
  - Validate signature and date
  - Update case status to "CCL Signed"
  - Store signed copy in document management system
  - Notify assigned solicitor

#### 5.2 Client Questionnaire
- **FR-4.3:** System must deliver client questionnaire based on case type:
  - **Purchase:** Buyer questionnaire (financing, chain status, special requirements)
  - **Sale:** Seller questionnaire (property details, fixtures, utilities, occupancy)
  - **Remortgage:** Borrower questionnaire (current mortgage, reason for remortgage)

- **FR-4.4:** Questionnaire must capture:
  - Personal circumstances (employment, residency status)
  - Property-specific information
  - Financial source confirmation
  - Special requests or concerns
  - Preferred communication method

- **FR-4.5:** System must support questionnaire completion:
  - Online form with save/resume capability
  - Mobile-responsive interface
  - Conditional questions based on previous answers
  - File upload for supporting documents
  - Progress indicator

- **FR-4.6:** Upon questionnaire completion, system must:
  - Validate all required fields are answered
  - Extract key information to case record
  - Flag any concerns for solicitor review
  - Send completion confirmation to client
  - Notify assigned solicitor

#### 5.3 Identity Verification
- **FR-4.7:** System must collect identity documents:
  - Passport or driver's license (photo page)
  - Proof of address (utility bill, bank statement < 3 months old)
  - National Insurance number
  - Additional ID for joint buyers/sellers

- **FR-4.8:** System must perform automated ID verification:
  - OCR extraction of ID data (name, DOB, document number)
  - Face matching between photo ID and selfie (optional)
  - Document authenticity checks (security features, expiry)
  - Data consistency validation (ID matches client record)
  - Generate verification confidence score

- **FR-4.9:** For low-confidence verifications (<85%), system must:
  - Flag for manual review by compliance team
  - Highlight specific issues (blurry image, data mismatch, expired document)
  - Allow upload of alternative ID documents
  - Support video verification as backup method

#### 5.4 Anti-Money Laundering (AML) Checks
- **FR-4.10:** System must perform automated AML checks:
  - Search against sanctions lists (OFAC, UN, EU, UK)
  - Politically Exposed Persons (PEP) screening
  - Adverse media search
  - Credit reference check (optional)
  - Company registry search for corporate clients

- **FR-4.11:** System must calculate AML risk score based on:
  - Client profile (age, occupation, location)
  - Transaction value and property type
  - Source of funds characteristics
  - Sanctions/PEP/adverse media hits
  - Behavioral red flags (urgency, cash payment, complex structure)

- **FR-4.12:** Risk categorization must route cases:
  - **Low Risk (<30):** Auto-approve, standard monitoring
  - **Medium Risk (30-70):** Flag for solicitor review, enhanced monitoring
  - **High Risk (>70):** Escalate to MLRO, senior approval required, refuse if unmitigable

- **FR-4.13:** System must generate AML compliance report:
  - Summary of checks performed
  - Risk score and categorization
  - Hits and false positives reviewed
  - Approval decision and approver name
  - Date of compliance

#### 5.5 Source of Funds Verification
- **FR-4.14:** System must collect source of funds declaration:
  - Savings (bank statements)
  - Sale of property (completion statement)
  - Mortgage/loan (offer letter)
  - Gift from family (gift letter + donor ID)
  - Inheritance (probate documents)
  - Business proceeds (accounts, sale agreement)
  - Other (supporting evidence required)

- **FR-4.15:** System must validate source of funds:
  - Documents match declared amount
  - Documents are dated appropriately (< 6 months)
  - Donor identity verified for gifts
  - Funds are sufficient for transaction (deposit + fees)
  - No suspicious patterns or red flags

- **FR-4.16:** For gifts, system must additionally verify:
  - Donor relationship to client
  - Donor ID and address
  - Gift letter signed and dated
  - Evidence of funds in donor's account
  - Confirmation gift is not a loan

#### 5.6 Bank Terms of Business (TOB) Review
- **FR-4.17:** If client has mortgage, system must:
  - Identify lender from mortgage offer
  - Retrieve lender's Terms of Business (TOB) from library
  - Auto-accept standard TOB if on approved list
  - Flag non-standard TOB for solicitor review

- **FR-4.18:** For non-standard lenders, system must:
  - Extract key terms from TOB document
  - Highlight unusual clauses (retention, clawback, additional fees)
  - Require solicitor approval before proceeding
  - Record TOB acceptance in case file

#### 5.7 Onboarding Completion & Case Readiness
- **FR-4.19:** System must track onboarding checklist:
  - [ ] Signed CCL received
  - [ ] Client questionnaire completed
  - [ ] ID documents verified
  - [ ] AML checks passed
  - [ ] Source of funds verified
  - [ ] Bank TOB reviewed (if applicable)

- **FR-4.20:** When all items completed, system must:
  - Update case status to "Onboarding Complete - Ready for Legal Work"
  - Notify assigned solicitor that case can proceed
  - Create initial task list for conveyancing work
  - Set expected milestone dates

- **FR-4.21:** Dashboard must show:
  - Cases pending onboarding by stage
  - Average onboarding time
  - Compliance pass/fail rates
  - Blockers requiring manual intervention

### 6. Non-Functional Requirements

#### 6.1 Performance
- **NFR-4.1:** ID verification must complete within 60 seconds
- **NFR-4.2:** AML checks must complete within 2 minutes
- **NFR-4.3:** Client portal must load within 3 seconds

#### 6.2 Security
- **NFR-4.4:** All ID documents must be encrypted at rest and in transit
- **NFR-4.5:** Client portal must use MFA (multi-factor authentication)
- **NFR-4.6:** AML data must be access-controlled to compliance team only

#### 6.3 Compliance
- **NFR-4.7:** System must comply with SRA Code of Conduct
- **NFR-4.8:** AML checks must meet Money Laundering Regulations 2017
- **NFR-4.9:** Data retention must follow GDPR and legal retention policies
- **NFR-4.10:** Audit trail must capture all compliance decisions

#### 6.4 Usability
- **NFR-4.11:** Client questionnaire must be completable in < 15 minutes
- **NFR-4.12:** ID upload must support camera capture from mobile devices
- **NFR-4.13:** System must provide progress indicators and clear instructions

### 7. Dependencies

**Upstream Dependencies:**
- Case Creation Automation feature (F003)
- Client portal infrastructure
- Document management system

**Downstream Dependencies:**
- Purchase/Sale case processing features (F005, F006)
- Email notification service

**External Services:**
- ID verification service (e.g., Onfido, Yoti)
- AML screening service (e.g., Dow Jones, Refinitiv)
- OCR service for document extraction

### 8. Acceptance Criteria

```gherkin
Given a new case is created
When the Conveyancing Agent initiates onboarding
Then the client receives CCL and questionnaire via email
And the client can access secure portal to complete onboarding
And all documents are uploaded and verified
And AML checks are completed automatically
And the case status updates to "Onboarding Complete"
And the entire process completes within 3 business days
```

```gherkin
Given a client uploads passport and proof of address
When the ID verification runs
Then the documents are validated for authenticity
And the client data is extracted and matched to case record
And if verification confidence is >85%, ID is auto-approved
And the compliance team is notified of the result
```

```gherkin
Given an AML check returns a high-risk score
When the system processes the result
Then the case is flagged for MLRO review
And the assigned solicitor is notified
And the case cannot proceed until approval
And all decisions are logged in the audit trail
```

### 9. Success Metrics

- **Onboarding Completion Time:** < 5 business days (target: 3 days)
- **ID Verification Automation:** ≥ 90% auto-approved without manual review
- **AML Pass Rate:** ≥ 98% of standard residential cases pass with low/medium risk
- **Questionnaire Completion Rate:** ≥ 95% of clients complete within 7 days
- **Compliance Accuracy:** 100% of cases have complete onboarding records
- **Client Satisfaction:** ≥ 4.5/5 rating for onboarding process

### 10. Open Questions

1. Should video verification be mandatory or optional for ID checks?
2. What is the threshold for requiring enhanced due diligence?
3. Are there specific lenders with pre-approved TOB agreements?
4. Should the system support non-UK clients with international ID?
5. What is the process for updating client information post-onboarding?
6. Are there specific red flags that should trigger immediate case review?

### 11. Future Enhancements

- Biometric verification (fingerprint, facial recognition)
- Blockchain-based identity credentials
- Continuous monitoring for PEP status changes
- Integration with Open Banking for source of funds verification
- AI-powered document fraud detection
- Automated risk scoring using machine learning
- Real-time AML screening during transaction
- Digital identity wallet integration (e.g., Gov.uk Verify)

---

**Document Version:** 1.0  
**Last Updated:** December 9, 2025  
**Author:** Product Manager  
**Status:** Draft
