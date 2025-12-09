# Feature Requirements Document (FRD)

## Feature: Contract Exchange

### 1. Overview

**Feature ID:** F008  
**Feature Name:** Contract Exchange  
**PRD Reference:** REQ-14  
**Phase:** Pre-Completion Legal Work  
**Priority:** Critical  
**Agent Owner:** Conveyancing Agent (with Manager Agent oversight)

### 2. Purpose

Facilitate the formal exchange of contracts between buyer and seller solicitors, marking the point at which the transaction becomes legally binding, with proper coordination, validation, and documentation of this critical milestone.

### 3. Scope

**In Scope:**
- Exchange readiness validation
- Formula A, B, C exchange methods
- Deposit confirmation and handling
- Exchange coordination between parties
- Exchange documentation and confirmation
- Post-exchange notifications and tasks
- Insurance requirement reminders
- Completion date confirmation

**Out of Scope:**
- Pre-exchange legal work (handled by F005, F006)
- Completion fund transfer (handled by F009)
- Contract drafting (handled by F007)
- Client onboarding (handled by F004)

### 4. User Stories

```gherkin
As a Conveyancing Solicitor, I want automated exchange readiness validation, so that I don't exchange contracts with outstanding issues.
```

```gherkin
As a Conveyancing Agent, I want coordinated exchange with counterparty, so that both parties exchange simultaneously without risk of one party withdrawing.
```

```gherkin
As a Manager Agent, I want audit trail of all exchange communications, so that we can prove exchange occurred and when.
```

```gherkin
As a Client, I want immediate confirmation when contracts are exchanged, so that I know the transaction is legally committed.
```

### 5. Functional Requirements

#### 5.1 Exchange Readiness Validation
- **FR-8.1:** System must validate pre-exchange checklist for Purchase:
  - [ ] Contract pack received and reviewed
  - [ ] All searches completed and acceptable
  - [ ] Mortgage offer received and conditions satisfied
  - [ ] Enquiries answered satisfactorily
  - [ ] Title report issued to client
  - [ ] Certificate of Title issued to lender (if applicable)
  - [ ] Source of funds verified
  - [ ] Deposit funds received in client account
  - [ ] Buildings insurance arranged from exchange date
  - [ ] Client authority to exchange obtained
  - [ ] Signed contract received
  - [ ] Completion date agreed with seller

- **FR-8.2:** System must validate pre-exchange checklist for Sale:
  - [ ] Contract pack sent to buyer
  - [ ] Enquiries answered satisfactorily
  - [ ] Redemption statement obtained (if mortgage)
  - [ ] Contract approved by buyer
  - [ ] Seller signed contract received
  - [ ] Deposit arrangements confirmed
  - [ ] Completion date agreed with buyer
  - [ ] Completion statement approved by client
  - [ ] Client authority to exchange obtained
  - [ ] Onward purchase ready to exchange (if in chain)

- **FR-8.3:** Readiness dashboard must display:
  - Checklist completion status
  - Outstanding items with responsible party
  - Blockers preventing exchange
  - Chain status (if applicable)
  - Recommended actions

#### 5.2 Exchange Methods
- **FR-8.4:** System must support standard exchange formulas:
  - **Formula A:** Simple exchange (no chain)
  - **Formula B:** Synchronous exchange in chain
  - **Formula C:** Exchange with holdover of part of deposit

- **FR-8.5:** Formula A (Simple Exchange):
  - Confirm both parties ready
  - Agree exchange time
  - Execute telephone exchange
  - Confirm and document

- **FR-8.6:** Formula B (Chain Exchange):
  - Identify all parties in chain
  - Coordinate readiness across chain
  - Execute synchronous exchange
  - Confirm all links exchanged
  - Document chain sequence

- **FR-8.7:** Formula C (Deposit Holdover):
  - Calculate deposit split
  - Document deposit holding arrangements
  - Execute exchange with holdover agreement
  - Confirm deposit distribution

#### 5.3 Deposit Handling
- **FR-8.8:** System must validate deposit:
  - Confirm deposit received in client account
  - Verify amount (typically 10% of purchase price)
  - Confirm funds cleared (not pending)
  - Document deposit source
  - Prepare deposit confirmation

- **FR-8.9:** For sales, deposit receipt must:
  - Confirm deposit received from buyer
  - Hold as stakeholder or agent
  - Document holding terms
  - Credit to client account
  - Generate receipt confirmation

#### 5.4 Exchange Coordination
- **FR-8.10:** System must facilitate exchange communication:
  - Telephone exchange dialogue script
  - Real-time status updates
  - Counterparty confirmation tracking
  - Time stamping of all communications
  - Recording of exchange conversation (optional)

- **FR-8.11:** Exchange dialogue must include:
  - "I hold a contract signed by my client"
  - Confirmation of contract terms (property, price, completion date)
  - Confirmation of deposit arrangements
  - Agreement to exchange
  - Time and date of exchange noted
  - Formula used (A/B/C) confirmed

- **FR-8.12:** Chain coordination must:
  - Show real-time status of all chain links
  - Prevent exchange until all links ready
  - Execute all exchanges within defined window
  - Confirm completion down chain
  - Alert if any link fails

#### 5.5 Exchange Documentation
- **FR-8.13:** Upon exchange, system must generate:
  - Exchange memorandum
  - Confirmation letter to client
  - Confirmation to counterparty
  - Notification to lender (purchase)
  - Notification to estate agent
  - Internal exchange record for file

- **FR-8.14:** Exchange memorandum must record:
  - Date and time of exchange
  - Method (telephone, in person, electronic)
  - Formula used (A/B/C)
  - Completion date confirmed
  - Deposit arrangements
  - Names of solicitors exchanging
  - Contract version exchanged

- **FR-8.15:** Audit trail must capture:
  - All communications leading to exchange
  - Exact time exchange occurred
  - Who authorized exchange
  - Readiness validation results
  - Any conditions or special arrangements

#### 5.6 Post-Exchange Actions
- **FR-8.16:** Immediately after exchange, system must:
  - Update case status to "Exchanged - Awaiting Completion"
  - Lock contract terms (prevent further amendments)
  - Set completion date countdown
  - Generate post-exchange task list
  - Send notifications to all parties

- **FR-8.17:** For purchase, post-exchange tasks:
  - Confirm buildings insurance in place
  - Request mortgage advance from lender
  - Calculate exact completion funds required
  - Prepare completion statement
  - Order pre-completion searches (if applicable)
  - Prepare completion documents

- **FR-8.18:** For sale, post-exchange tasks:
  - Request final redemption statement
  - Prepare transfer deed
  - Arrange key handover
  - Prepare completion statement (final)
  - Coordinate access for completion inspection

#### 5.7 Insurance & Risk Management
- **FR-8.19:** For purchase, system must:
  - Verify buildings insurance effective from exchange
  - Send insurance reminder 24 hours before exchange
  - Block exchange if insurance not confirmed
  - Store insurance certificate in case file

- **FR-8.20:** Risk warnings must issue:
  - Exchange is legally binding (no withdrawal without penalty)
  - Deposit at risk if buyer fails to complete
  - Seller can retain deposit and resell if buyer defaults
  - Client confirmation required before exchange

#### 5.8 Completion Date Management
- **FR-8.21:** System must manage completion date:
  - Agreed date at exchange (typically 10-28 days hence)
  - Countdown timer to completion
  - Reminders at key milestones (7 days, 3 days, 1 day before)
  - Flag if completion date at risk
  - Support completion date amendments (if both parties agree)

### 6. Non-Functional Requirements

#### 6.1 Reliability
- **NFR-8.1:** Exchange recording must be 100% reliable (no data loss)
- **NFR-8.2:** System must handle network issues gracefully
- **NFR-8.3:** Audit trail must be immutable and tamper-proof

#### 6.2 Security
- **NFR-8.4:** Exchange authorization must require MFA
- **NFR-8.5:** Only authorized solicitors can execute exchange
- **NFR-8.6:** All communications encrypted end-to-end

#### 6.3 Compliance
- **NFR-8.7:** Exchange process must comply with Law Society protocols
- **NFR-8.8:** All exchange documentation must be compliant with SRA requirements
- **NFR-8.9:** Audit trail must meet regulatory standards

#### 6.4 Usability
- **NFR-8.10:** Exchange interface must be intuitive for high-stress situations
- **NFR-8.11:** Checklist validation must complete in < 10 seconds
- **NFR-8.12:** Exchange confirmation must be immediate

### 7. Dependencies

**Upstream Dependencies:**
- Purchase Case Processing (F005)
- Sale Case Processing (F006)
- Document Automation (F007)
- Client approval and signed contracts

**Downstream Dependencies:**
- Completion Fund Management (F009)
- Manager Agent oversight
- Counterparty solicitor coordination

**External Services:**
- Telephone system (for exchange calls)
- Email notification service
- Insurance verification services

### 8. Acceptance Criteria

```gherkin
Given a purchase case is ready to exchange
When the solicitor validates readiness
Then all checklist items are confirmed complete
And the client has authorized exchange
And deposit funds are confirmed in account
And buildings insurance is arranged
And the system shows "Ready to Exchange"
```

```gherkin
Given both parties are ready for Formula A exchange
When the solicitors conduct telephone exchange
Then the exchange is documented with timestamp
And memorandums are generated
And clients are notified immediately
And case status updates to "Exchanged"
And post-exchange tasks are created
And the completion countdown begins
```

```gherkin
Given a 4-party chain using Formula B
When all parties confirm readiness
Then exchanges execute synchronously
And all chain links are confirmed
And if any link fails, all exchanges are aborted
And all parties receive status updates
```

### 9. Success Metrics

- **Exchange Success Rate:** 100% (no failed exchanges due to system issues)
- **Readiness Validation Time:** < 10 seconds
- **Exchange Documentation:** 100% complete within 1 hour
- **Client Notification:** 100% within 5 minutes of exchange
- **Audit Trail Completeness:** 100% of exchanges fully documented
- **Chain Coordination:** 95%+ successful chain exchanges

### 10. Open Questions

1. Should the system support electronic exchange (beyond telephone)?
2. What is backup process if system unavailable during exchange?
3. Should there be supervisor approval for high-value exchanges?
4. How should aborted exchanges be handled and documented?
5. What integrations exist with property chain coordination platforms?

### 11. Future Enhancements

- Blockchain-based instant exchange
- AI-powered chain coordination optimization
- Predictive analytics for exchange readiness
- Integration with national property exchange network
- Automated insurance verification via APIs
- Real-time chain visualization dashboard
- Smart contract implementation for automated exchange

---

**Document Version:** 1.0  
**Last Updated:** December 9, 2025  
**Author:** Product Manager  
**Status:** Draft
