# Feature Requirements Document (FRD)

## Feature: Completion Fund Management

### 1. Overview

**Feature ID:** F009  
**Feature Name:** Completion Fund Management  
**PRD Reference:** REQ-15, REQ-16, REQ-17, REQ-18  
**Phase:** Completion Work  
**Priority:** Critical  
**Agent Owner:** Complete Agent

### 2. Purpose

Automate the complex financial processes required for property completion, including fund requisition, receipt, allocation, transfer, reconciliation, and surplus handling, ensuring accurate and timely fund movements between all parties.

### 3. Scope

**In Scope:**
- Completion pack preparation and delivery to accounts
- Fund requisition from clients and lenders
- Receipt and allocation of funds to client/office accounts
- Completion statement generation and approval
- Fund transfers to counterparty solicitors
- Mortgage redemption payments
- Bill generation and invoicing
- Multi-party fund coordination
- Surplus balance handling
- Financial reconciliation
- Audit trail and compliance records

**Out of Scope:**
- Actual bank transfers (system generates instructions, bank executes)
- Investment advice or fund management
- Tax calculations or advice
- Accounting system general ledger (integration only)

### 4. User Stories

```gherkin
As a Complete Agent, I want automated fund calculations and reconciliation, so that completion proceeds without financial errors or delays.
```

```gherkin
As an Accounts team member, I want the system to automatically track fund movements and generate invoices, so that our ledgers are always accurate and up-to-date.
```

```gherkin
As a Client, I want clear instructions on exactly how much I need to transfer and when, so that I can ensure funds arrive on time for completion.
```

```gherkin
As a Compliance Officer, I want complete audit trails for all fund movements, so that we can demonstrate proper client money handling.
```

### 5. Functional Requirements

#### 5.1 Completion Pack Preparation
- **FR-9.1:** System must generate completion pack containing:
  - Completion statement (itemized funds required/payable)
  - Payment instructions (bank details, reference, amount)
  - Timing requirements (funds needed by date/time)
  - Supporting documents (mortgage advance request, redemption statement)
  - Contact information for queries

- **FR-9.2:** Completion pack must be delivered:
  - To accounts team electronically
  - To client via portal/email
  - To lender (mortgage advance request)
  - With read receipts and tracking

#### 5.2 Fund Requisition & Receipt
- **FR-9.3:** For purchase completion, system must calculate funds required:
  - Purchase price
  - Less: mortgage advance
  - Plus: solicitor fees and disbursements
  - Plus: stamp duty (SDLT)
  - Plus: search fees and other costs
  - Plus: lender fees
  - **= Total funds required from client**

- **FR-9.4:** System must request funds from:
  - **Client:** Deposit balance + additional funds needed
  - **Lender:** Mortgage advance (submit mortgage advance request)
  - **Third parties:** Any other funds (e.g., gift funds)

- **FR-9.5:** Fund receipt tracking must:
  - Monitor client account for incoming transfers
  - Match received funds to case via payment reference
  - Confirm funds cleared (not pending)
  - Alert if funds not received by deadline
  - Escalate if completion at risk

- **FR-9.6:** For sale completion, system must track:
  - Purchase funds received from buyer's solicitor
  - Confirm amount matches agreed price
  - Allocate to client account
  - Flag discrepancies immediately

#### 5.3 Account Allocation
- **FR-9.7:** System must allocate funds to correct accounts:
  - **Client Account:** Client funds, deposit, purchase price received
  - **Office Account:** Firm's fees and disbursements
  - Maintain strict separation per SRA Accounts Rules

- **FR-9.8:** Ledger entries must record:
  - Date and time of transaction
  - Amount and currency
  - Source and destination
  - Case reference
  - Transaction type (receipt, payment, transfer)
  - Authorization details

#### 5.4 Completion Statement
- **FR-9.9:** For purchase, completion statement must show:
  - Purchase price payable to seller
  - Solicitor fees and disbursements
  - Mortgage advance received
  - Deposit paid at exchange
  - Additional costs (searches, SDLT, etc.)
  - **Balance required from client**

- **FR-9.10:** For sale, completion statement must show:
  - Purchase price received from buyer
  - Less: mortgage redemption amount
  - Less: estate agent commission
  - Less: solicitor fees and disbursements
  - Less: other deductions
  - **Net proceeds payable to client**

- **FR-9.11:** Statement approval workflow:
  - Generate statement 7 days before completion
  - Send to client for review and approval
  - Flag if client queries or disputes
  - Require formal approval before proceeding
  - Lock statement post-approval (changes require new approval)

#### 5.5 Fund Transfers
- **FR-9.12:** For purchase completion, system must generate payment instructions:
  - Transfer purchase price to seller's solicitor
  - Include correct reference (seller name, case ref)
  - Use verified bank details (check against previous communications)
  - Schedule for completion date/time
  - Require dual authorization for high values

- **FR-9.13:** For sale completion, system must generate payment instructions:
  - Mortgage redemption to lender
  - Estate agent commission
  - Solicitor fees (office account transfer)
  - Net proceeds to client
  - All timed for post-completion execution

- **FR-9.14:** Payment verification must:
  - Confirm sufficient funds available
  - Validate bank details (fraud check)
  - Detect suspicious patterns
  - Require additional approval for unusual amounts
  - Log all verifications

#### 5.6 Mortgage Redemption
- **FR-9.15:** For sale cases with mortgage, system must:
  - Request redemption statement from lender (calculated to completion date)
  - Verify redemption amount
  - Prepare redemption payment
  - Send to lender on completion
  - Obtain redemption confirmation
  - Request DS1 (discharge of mortgage) form

#### 5.7 Bill Generation & Invoicing
- **FR-9.16:** System must generate bills for:
  - Professional fees (solicitor time)
  - Disbursements (searches, Land Registry fees, etc.)
  - VAT calculations
  - Total amount payable

- **FR-9.17:** Invoicing must:
  - Itemize all charges
  - Apply correct VAT treatment
  - Reference case and client
  - Generate invoice number
  - Send to client and accounts
  - Track payment status

- **FR-9.18:** Fee calculations must consider:
  - Case value (ad valorem fees)
  - Service level (standard/premium)
  - Complexity (leasehold, shared ownership)
  - Disbursements incurred
  - Any discounts or promotions applied

#### 5.8 Multi-Party Fund Coordination
- **FR-9.19:** For chain transactions, system must:
  - Coordinate timing of all fund transfers
  - Ensure seller receives funds before buyer releases
  - Track completion up/down the chain
  - Handle simultaneous completions
  - Alert if any link in chain fails

- **FR-9.20:** Coordination must include:
  - Real-time status updates
  - Communication with all solicitors in chain
  - Contingency planning for delays
  - Escalation procedures

#### 5.9 Surplus & Deficiency Handling
- **FR-9.21:** If surplus funds after completion:
  - Calculate exact surplus amount
  - Notify client
  - Obtain client instructions (return or hold for disbursements)
  - Execute refund promptly
  - Reconcile final balances

- **FR-9.22:** If deficiency identified:
  - Alert immediately
  - Calculate shortfall
  - Request additional funds
  - Hold completion if funds not received
  - Escalate to management

#### 5.10 Reconciliation & Audit
- **FR-9.23:** Daily reconciliation must:
  - Match all receipts and payments
  - Verify client account balances
  - Identify discrepancies
  - Generate reconciliation report
  - Flag unallocated funds

- **FR-9.24:** Audit trail must record:
  - All fund movements with timestamps
  - Authorization details
  - Bank transaction references
  - Supporting documentation
  - Changes and amendments
  - Compliance with SRA Accounts Rules

### 6. Non-Functional Requirements

#### 6.1 Accuracy
- **NFR-9.1:** Financial calculations must be 100% accurate (zero tolerance for errors)
- **NFR-9.2:** Fund allocation must be correct 100% of the time
- **NFR-9.3:** Reconciliation must identify 100% of discrepancies

#### 6.2 Security
- **NFR-9.4:** All financial data encrypted at rest and in transit
- **NFR-9.5:** Payment instructions require dual authorization >£50k
- **NFR-9.6:** Bank detail changes must be verified out-of-band
- **NFR-9.7:** Access to financial functions restricted to authorized users

#### 6.3 Compliance
- **NFR-9.8:** Must comply with SRA Accounts Rules 2019
- **NFR-9.9:** Must meet anti-money laundering requirements
- **NFR-9.10:** Audit trail must be immutable and complete
- **NFR-9.11:** Client money must be properly segregated

#### 6.4 Performance
- **NFR-9.12:** Fund calculations must complete within 5 seconds
- **NFR-9.13:** Payment instructions must generate within 10 seconds
- **NFR-9.14:** Reconciliation must complete within 1 minute

### 7. Dependencies

**Upstream Dependencies:**
- Contract Exchange feature (F008)
- Accounting Integration feature (F010)
- Client/lender funds receipt

**Downstream Dependencies:**
- Banking system for fund transfers
- CMS for case completion status
- Land Registry Registration feature (F011)

**External Services:**
- Banking APIs for balance checking
- Payment networks (CHAPS, Faster Payments)
- Lender portals for mortgage advance/redemption

### 8. Acceptance Criteria

```gherkin
Given a purchase case is ready for completion
When the Complete Agent prepares completion pack
Then all fund calculations are accurate
And client receives clear payment instructions
And lender receives mortgage advance request
And accounts team receives completion pack
And all deadlines and amounts are clearly specified
```

```gherkin
Given completion funds are received from all parties
When the system verifies and allocates funds
Then client account is credited correctly
And sufficient funds are confirmed for completion
And payment instructions are generated
And dual authorization is obtained for high values
And funds are transferred on completion date
```

```gherkin
Given a sale completion occurs
When funds are received from buyer
Then mortgage is redeemed
And estate agent is paid
And solicitor fees are transferred to office account
And net proceeds are paid to client
And all transactions are reconciled
And audit trail is complete
```

### 9. Success Metrics

- **Completion Accuracy:** 99%+ accuracy in fund calculations (PRD target)
- **Fund Transfer Success:** 100% of completions execute without fund errors
- **Reconciliation:** 100% daily reconciliations with zero discrepancies
- **Timely Completion:** 99%+ completions occur on scheduled date
- **Audit Compliance:** 100% of files have complete audit trail
- **Client Satisfaction:** ≥ 4.5/5 rating for financial transparency

### 10. Open Questions

1. What are payment authorization thresholds requiring dual sign-off?
2. Should the system integrate with specific banking platforms?
3. What is escalation process for failed completions due to funds?
4. How should foreign currency transactions be handled?
5. What is the process for handling post-completion adjustments?

### 11. Future Enhancements

- Real-time banking integration (Open Banking APIs)
- Cryptocurrency payment support
- AI-powered fraud detection
- Predictive fund arrival timing
- Automated reconciliation with machine learning
- Blockchain-based fund tracking
- Smart contracts for automated conditional payments
- Integration with instant payment networks

---

**Document Version:** 1.0  
**Last Updated:** December 9, 2025  
**Author:** Product Manager  
**Status:** Draft
