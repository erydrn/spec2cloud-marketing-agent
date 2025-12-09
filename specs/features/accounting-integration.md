# Feature Requirements Document (FRD)

## Feature: Accounting Integration

### 1. Overview

**Feature ID:** F010  
**Feature Name:** Accounting Integration  
**PRD Reference:** REQ-16, REQ-18, REQ-25  
**Phase:** Completion Work (Cross-Cutting)  
**Priority:** High  
**Agent Owner:** Complete Agent (with Tech Agent support)

### 2. Purpose

Provide seamless integration between the conveyancing workflow and the firm's accounting system to ensure accurate financial recording, ledger management, invoicing, reporting, and compliance with SRA Accounts Rules.

### 3. Scope

**In Scope:**
- Bi-directional sync with accounting software
- Client and office ledger management
- Automatic posting of receipts and payments
- Invoice generation and tracking
- Disbursement recording
- VAT calculations and reporting
- Financial reporting and analytics
- SRA Accounts Rules compliance
- Audit trail and reconciliation support

**Out of Scope:**
- General firm accounting (payroll, expenses, etc.)
- Tax filing and returns
- Financial strategy or advice
- Banking system integration (separate concern)

### 4. User Stories

```gherkin
As an Accounts team member, I want automatic posting of transactions to ledgers, so that I don't have to manually duplicate data entry.
```

```gherkin
As a Finance Manager, I want real-time visibility of client account balances, so that I can ensure compliance with SRA Accounts Rules.
```

```gherkin
As a Complete Agent, I want automated invoice generation when cases complete, so that billing is accurate and timely.
```

```gherkin
As a Compliance Officer, I want comprehensive financial reports, so that I can demonstrate proper client money handling to regulators.
```

### 5. Functional Requirements

#### 5.1 Accounting System Integration
- **FR-10.1:** System must integrate with major legal accounting platforms:
  - **Primary:** LEAP, Practice Evolve, Proclaim
  - **Secondary:** Xero, QuickBooks (with legal accounting module)
  - **API-based:** Real-time sync via REST/SOAP APIs
  - **File-based:** CSV/XML export/import as fallback

- **FR-10.2:** Integration must be bi-directional:
  - **To Accounting:** Transactions, invoices, client data
  - **From Accounting:** Balance confirmations, payment status, ledger entries

- **FR-10.3:** Sync frequency must support:
  - Real-time for critical transactions (< 30 seconds)
  - Near real-time for standard transactions (< 5 minutes)
  - Batch sync for bulk operations (end of day)
  - Manual sync on demand

#### 5.2 Client & Office Ledger Management
- **FR-10.4:** System must maintain separate ledgers:
  - **Client Account:** All client funds held
  - **Office Account:** Firm's fees and general funds
  - Strict separation per SRA Accounts Rules

- **FR-10.5:** Client ledger must record:
  - Receipts (deposit, funds from client, mortgage advance)
  - Payments (purchase price, disbursements on behalf)
  - Transfers (inter-client transfers if applicable)
  - Running balance per client/case

- **FR-10.6:** Office ledger must record:
  - Professional fees earned
  - Disbursements recharged to clients
  - Operating expenses
  - VAT collected and payable

#### 5.3 Transaction Posting
- **FR-10.7:** Automatic posting must occur for:
  - Client fund receipts
  - Purchase price payments
  - Mortgage advances received
  - Redemption payments
  - Estate agent commissions
  - Professional fees
  - Disbursements
  - Refunds and transfers

- **FR-10.8:** Each posting must include:
  - Transaction date and time
  - Amount and currency
  - Transaction type (receipt/payment/transfer)
  - Case reference
  - Client name
  - Description/narrative
  - Supporting document reference
  - Authorization details

- **FR-10.9:** Posting rules must ensure:
  - Client account postings only for client funds
  - Office account postings for fees and firm expenses
  - No commingling of client and office funds
  - Correct VAT treatment
  - Proper nominal codes/categories

#### 5.4 Invoice Generation
- **FR-10.10:** System must generate invoices automatically:
  - Upon case completion
  - Upon interim billing (if applicable)
  - On client request
  - Per firm's billing policy

- **FR-10.11:** Invoice must contain:
  - Unique invoice number
  - Invoice date
  - Client details
  - Case reference
  - Itemized fees (professional fees by category)
  - Itemized disbursements
  - VAT calculation and breakdown
  - Total amount due
  - Payment terms and instructions

- **FR-10.12:** Invoice workflow:
  - Auto-generate from case completion
  - Review and approve (if required)
  - Post to accounting system
  - Send to client via email/portal
  - Track payment status
  - Send reminders for overdue invoices

#### 5.5 Disbursement Recording
- **FR-10.13:** System must record all disbursements:
  - Search fees
  - Land Registry fees
  - SDLT (Stamp Duty)
  - Bank transfer fees
  - Lender fees
  - Other third-party costs

- **FR-10.14:** Disbursement tracking must:
  - Record cost at time incurred
  - Link to case and invoice
  - Support VAT where applicable
  - Mark as paid/unpaid
  - Reconcile against supplier invoices

#### 5.6 VAT Calculations
- **FR-10.15:** VAT treatment must correctly apply:
  - Standard rate (20%) on professional fees
  - Exempt on residential conveyancing (in some cases)
  - Outside scope for disbursements paid on behalf
  - Correct VAT on own disbursements
  - Proper VAT invoice format

- **FR-10.16:** VAT reporting must provide:
  - VAT collected summary
  - VAT paid summary
  - Net VAT position
  - VAT return preparation data
  - Quarterly/annual VAT reports

#### 5.7 Financial Reporting
- **FR-10.17:** System must provide reports:
  - Client account balance by client
  - Office account summary
  - Aged debtors (unpaid invoices)
  - Cash flow projections
  - Revenue by case type
  - Fee earner performance
  - Disbursement analysis
  - Profit & loss by practice area

- **FR-10.18:** Compliance reports must include:
  - SRA Accounts Rules compliance report
  - Client money reconciliation
  - Residual balances report
  - Inter-client transfers log
  - Suspicious activity report (AML)

- **FR-10.19:** Report delivery:
  - On-demand generation
  - Scheduled reports (daily, weekly, monthly)
  - Export to Excel, PDF, CSV
  - Dashboard visualization
  - Email distribution to stakeholders

#### 5.8 Reconciliation Support
- **FR-10.20:** System must support reconciliation:
  - Daily client account reconciliation
  - Match accounting system vs. bank statement
  - Identify unallocated funds
  - Flag discrepancies
  - Generate reconciliation report

- **FR-10.21:** Reconciliation workflow:
  - Import bank statement
  - Auto-match transactions
  - Manual match unidentified items
  - Adjust for timing differences
  - Confirm reconciled balance
  - Archive reconciliation record

#### 5.9 Audit Trail
- **FR-10.22:** System must maintain audit trail:
  - All transactions with timestamps
  - User who authorized each transaction
  - Changes to financial data
  - Invoice adjustments and credits
  - Failed transactions and reasons
  - System errors and resolutions

- **FR-10.23:** Audit log must be:
  - Immutable (no editing/deletion)
  - Comprehensive (all financial activity)
  - Exportable for regulator review
  - Searchable by case, client, date, amount
  - Retained per regulatory requirements (6+ years)

#### 5.10 Error Handling & Validation
- **FR-10.24:** System must validate:
  - Sufficient funds before posting payment
  - Correct account (client vs. office)
  - Valid case and client references
  - Reasonable amounts (flag outliers)
  - Proper authorization levels

- **FR-10.25:** Error handling must:
  - Retry failed sync automatically
  - Alert accounts team of persistent failures
  - Queue transactions for manual review
  - Prevent data loss
  - Log all errors for investigation

### 6. Non-Functional Requirements

#### 6.1 Accuracy
- **NFR-10.1:** Financial data sync must be 100% accurate
- **NFR-10.2:** Invoice calculations must be error-free
- **NFR-10.3:** Reconciliation must identify all discrepancies

#### 6.2 Performance
- **NFR-10.4:** Transaction posting must complete within 30 seconds
- **NFR-10.5:** Report generation must complete within 2 minutes
- **NFR-10.6:** Sync with accounting system must process 1000+ transactions/hour

#### 6.3 Reliability
- **NFR-10.7:** Integration uptime must be ≥ 99.5%
- **NFR-10.8:** Failed syncs must retry automatically
- **NFR-10.9:** No financial data may be lost during system issues

#### 6.4 Security
- **NFR-10.10:** All financial data encrypted at rest and in transit
- **NFR-10.11:** Access to financial functions restricted by role
- **NFR-10.12:** API keys and credentials securely stored

#### 6.5 Compliance
- **NFR-10.13:** Must comply with SRA Accounts Rules 2019
- **NFR-10.14:** Must support Making Tax Digital (MTD) requirements
- **NFR-10.15:** Audit trail must meet regulatory standards

### 7. Dependencies

**Upstream Dependencies:**
- Completion Fund Management feature (F009)
- Case Creation Automation feature (F003)
- Document Automation feature (F007)

**Downstream Dependencies:**
- Accounting software (LEAP, Practice Evolve, etc.)
- Banking system for statement imports
- Tax reporting systems

**External Services:**
- Accounting software APIs
- HMRC Making Tax Digital APIs (future)
- Bank statement feeds

### 8. Acceptance Criteria

```gherkin
Given a completion occurs with multiple fund movements
When transactions are posted to accounting system
Then all receipts and payments are accurately recorded
And client account is debited/credited correctly
And office account receives fees appropriately
And sync completes within 30 seconds
And accounting ledgers match case records 100%
```

```gherkin
Given a case completes
When invoice is generated
Then all fees and disbursements are itemized
And VAT is calculated correctly
And invoice is posted to accounting system
And client receives invoice via email
And payment tracking begins
```

```gherkin
Given daily reconciliation is performed
When comparing accounting system to bank statement
Then all transactions are matched
And discrepancies are identified and flagged
And reconciliation report is generated
And accounts team can review and resolve issues
```

### 9. Success Metrics

- **Sync Accuracy:** 100% of transactions sync correctly
- **Sync Timeliness:** ≥ 99% sync within 30 seconds
- **Invoice Accuracy:** 100% accurate calculations
- **Reconciliation:** 100% daily reconciliations complete
- **Audit Compliance:** Zero SRA compliance issues
- **User Efficiency:** 80% reduction in manual accounting data entry

### 10. Open Questions

1. Which accounting platform(s) should be prioritized for Phase 1?
2. What is the process for handling accounting system downtime?
3. Should the system support multiple currencies?
4. What approval workflows are required for invoice adjustments?
5. How should historical financial data migration be handled?

### 11. Future Enhancements

- AI-powered anomaly detection in financial data
- Predictive cash flow forecasting
- Automated VAT return filing (MTD integration)
- Blockchain-based audit trail
- Real-time financial dashboards with drill-down
- Integration with tax advisory platforms
- Automated fee earner profitability analysis
- Client lifetime value calculation

---

**Document Version:** 1.0  
**Last Updated:** December 9, 2025  
**Author:** Product Manager  
**Status:** Draft
