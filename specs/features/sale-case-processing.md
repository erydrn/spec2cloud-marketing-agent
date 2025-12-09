# Feature Requirements Document (FRD)

## Feature: Sale Case Processing

### 1. Overview

**Feature ID:** F006  
**Feature Name:** Sale Case Processing  
**PRD Reference:** REQ-7, REQ-10, REQ-14  
**Phase:** Pre-Completion Legal Work  
**Priority:** Critical  
**Agent Owner:** Conveyancing Agent (Sale)

### 2. Purpose

Automate the conveyancing workflow for property sale transactions, including title verification, contract pack preparation, coordination with buyer's solicitor, enquiry handling, completion statements, and contract exchange preparation.

### 3. Scope

**In Scope:**
- Land Registry office copies retrieval
- Contract and title review/verification
- Draft contract pack preparation
- Contract pack distribution to buyer's solicitor
- Enquiry receipt and response management
- Contract signing coordination with client
- Completion statement generation
- Contract exchange preparation
- Deposit request and tracking

**Out of Scope:**
- Client onboarding (handled by F004)
- Contract exchange execution (handled by F008)
- Completion and fund distribution (handled by F009)
- Post-completion matters (handled by F011, F012)

### 4. User Stories

```gherkin
As a Conveyancing Agent (Sale), I want the system to automatically generate draft contract packs, so that I can send them to buyer's solicitors within 48 hours of instruction.
```

```gherkin
As a Conveyancing Solicitor, I want automated responses to standard enquiries, so that I can focus on complex non-standard questions.
```

```gherkin
As a Seller, I want to receive a clear completion statement showing what I'll receive after deducting all costs, so that I can plan my onward transaction.
```

### 5. Functional Requirements

#### 5.1 Land Registry Office Copies
- **FR-6.1:** System must automatically order official copies from Land Registry:
  - Title register (OC1)
  - Title plan (OC2)
  - Use API integration where available
  - Support manual upload if API unavailable

- **FR-6.2:** Upon receipt, system must:
  - Extract title number and property description
  - Identify current registered proprietors
  - List all charges and restrictions
  - Flag any adverse entries or cautions
  - Validate seller ownership matches instruction

#### 5.2 Contract & Title Review
- **FR-6.3:** System must review seller's title:
  - Confirm good marketable title
  - Identify any defects or issues
  - Check for restrictive covenants affecting sale
  - Verify all mortgages are identified for redemption
  - Highlight any conditions requiring disclosure

- **FR-6.4:** Risk assessment must flag:
  - **Critical:** Missing deeds, unregistered land, adverse possession
  - **High:** Material restrictions, unresolved disputes
  - **Medium:** Standard covenants, minor title issues
  - **Low:** Discharged mortgages, standard easements

#### 5.3 Draft Contract Pack Preparation
- **FR-6.5:** System must compile contract pack including:
  - Draft contract incorporating standard conditions
  - Official copy title register and plan
  - Property Information Form (TA6) - pre-populated
  - Fittings and Contents Form (TA10) - template
  - Leasehold information pack (if applicable)
  - Planning documents, building regulations certificates
  - Warranties and guarantees
  - Management pack (for leasehold/freehold estates)

- **FR-6.6:** Contract drafting must:
  - Auto-populate property details, price, parties
  - Include special conditions based on title review
  - Add standard sale conditions
  - Incorporate any agreed terms from estate agent details
  - Generate deposit calculation (typically 10%)
  - Set proposed completion date

- **FR-6.7:** TA6 Form auto-population:
  - Property type, age, construction
  - Utilities (electric, gas, water suppliers)
  - Services (broadband, phone lines)
  - Council tax band
  - Known alterations or planning permissions
  - Disputes with neighbors
  - Flag questions requiring client input

#### 5.4 Contract Pack Distribution
- **FR-6.8:** System must send contract pack to buyer's solicitor via:
  - Secure email with password-protected attachments
  - Property exchange network (if integrated)
  - Secure file transfer portal

- **FR-6.9:** Distribution tracking must:
  - Confirm receipt by buyer's solicitor
  - Set reminder for expected enquiry receipt (10 days)
  - Track pack version if amendments made
  - Log all communications

#### 5.5 Enquiry Management
- **FR-6.10:** System must receive enquiries from buyer's solicitor:
  - Parse enquiry list (standard formats)
  - Categorize enquiries by topic
  - Flag urgent or unusual enquiries
  - Identify standard vs. non-standard questions

- **FR-6.11:** Automated enquiry responses:
  - Generate responses for standard enquiries (>100 common questions)
  - Extract answers from previously completed forms
  - Reference supporting documents
  - Flag enquiries requiring client input
  - Flag enquiries requiring solicitor judgment

- **FR-6.12:** Response workflow:
  - Auto-respond to standard enquiries
  - Create task for client to answer specific questions
  - Queue non-standard enquiries for solicitor review
  - Compile complete response pack
  - Send to buyer's solicitor with supporting documents
  - Track further enquiries or clarifications

#### 5.6 Contract Signing
- **FR-6.13:** When contract approved by buyer, system must:
  - Prepare contract for seller signature
  - Send contract to seller with signing instructions
  - Support e-signature via client portal
  - Accept wet signature via post/courier
  - Validate signature and date
  - Store signed contract securely

- **FR-6.14:** Pre-exchange checklist:
  - [ ] Contract pack sent to buyer
  - [ ] Enquiries satisfactorily answered
  - [ ] Contract approved by buyer's solicitor
  - [ ] Seller signed contract received
  - [ ] Deposit arrangements confirmed
  - [ ] Completion date agreed
  - [ ] Redemption statement obtained (if mortgage)

#### 5.7 Completion Statement
- **FR-6.15:** System must generate completion statement:
  - Purchase price (credit to seller)
  - Less: mortgage redemption amount
  - Less: estate agent commission
  - Less: solicitor fees and disbursements
  - Less: any other deductions (service charge arrears, etc.)
  - **Net proceeds to seller**

- **FR-6.16:** Statement must:
  - Show all calculations clearly
  - Itemize each fee and disbursement
  - Calculate daily apportionments (if applicable)
  - Present in client-friendly format
  - Require client approval before exchange

#### 5.8 Deposit Request & Tracking
- **FR-6.17:** System must coordinate deposit:
  - Calculate deposit amount (typically 10%)
  - Request deposit from buyer's solicitor
  - Confirm receipt in client account
  - Hold deposit as stakeholder
  - Track deposit status (pending, received, held)

- **FR-6.18:** For exchange preparation:
  - Confirm deposit funds cleared
  - Prepare deposit confirmation letter
  - Coordinate exchange timing with buyer's solicitor

### 6. Non-Functional Requirements

#### 6.1 Performance
- **NFR-6.1:** Contract pack preparation must complete within 2 hours
- **NFR-6.2:** Standard enquiry responses must generate within 5 minutes
- **NFR-6.3:** Completion statement must calculate within 30 seconds

#### 6.2 Accuracy
- **NFR-6.4:** Contract data accuracy must be ≥ 99%
- **NFR-6.5:** Completion statement calculations must be 100% accurate
- **NFR-6.6:** Standard enquiry response accuracy must be ≥ 95%

#### 6.3 Integration
- **NFR-6.7:** Land Registry API integration for official copies
- **NFR-6.8:** Support major property exchange networks

### 7. Dependencies

**Upstream Dependencies:**
- Client Onboarding feature (F004)
- Land Registry API access
- Document templates and standard forms

**Downstream Dependencies:**
- Contract Exchange feature (F008)
- Buyer's solicitor coordination
- Supplier Agent for external party coordination

**External Services:**
- Land Registry portal/API
- Mortgage lender redemption departments
- Estate agent commission statements

### 8. Acceptance Criteria

```gherkin
Given a sale case is instructed
When the Conveyancing Agent processes the case
Then official copies are ordered from Land Registry
And the title is reviewed for defects
And a draft contract pack is prepared within 2 hours
And the pack is sent to buyer's solicitor
And the case status updates to "Awaiting Enquiries"
```

```gherkin
Given enquiries are received from buyer's solicitor
When the system processes the enquiries
Then at least 80% of standard enquiries are auto-answered
And non-standard enquiries are flagged for solicitor
And responses are compiled and sent within 5 business days
```

```gherkin
Given all pre-exchange requirements are met
When the completion statement is generated
Then all calculations are accurate
And the seller approves the net proceeds
And the case is marked "Ready to Exchange"
```

### 9. Success Metrics

- **Contract Pack Delivery:** < 48 hours from instruction
- **Enquiry Response Time:** < 5 business days
- **Auto-Response Rate:** ≥ 80% of enquiries
- **Calculation Accuracy:** 100% (zero errors in financial calculations)
- **Exchange Readiness:** < 4 weeks from instruction to ready

### 10. Open Questions

1. What format should enquiry responses follow (firm standard)?
2. Should the system auto-chase buyer's solicitor if enquiries overdue?
3. What approval is required for non-standard contract special conditions?
4. Should completion statements include tax calculations (CGT)?
5. How should chain transactions be coordinated?

### 11. Future Enhancements

- AI-powered enquiry generation prediction
- Automated redemption statement requests to lenders
- Integration with estate agent portals for commission details
- Blockchain-based contract exchange
- Real-time chain coordination dashboard

---

**Document Version:** 1.0  
**Last Updated:** December 9, 2025  
**Author:** Product Manager  
**Status:** Draft
