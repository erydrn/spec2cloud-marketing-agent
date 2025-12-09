# Feature Requirements Document (FRD)

## Feature: Sales Lead Management

### 1. Overview

**Feature ID:** F002  
**Feature Name:** Sales Lead Management  
**PRD Reference:** REQ-3, REQ-5  
**Phase:** Pre-Case Creation  
**Priority:** High  
**Agent Owner:** Sales Agent

### 2. Purpose

Enable Sales Team to efficiently process, qualify, and convert leads into cases through automated data capture, quote management, and client communication workflows.

### 3. Scope

**In Scope:**
- Detailed lead capture and data enrichment
- Lead qualification workflows
- Quote chase and follow-up automation
- Call logging and activity tracking
- Quote agreement and approval
- Client consent to case management
- Lead closing (won/lost tracking)
- Tick-match request processing
- Integration with Digital Quoting and CRM systems

**Out of Scope:**
- Initial lead capture from marketing channels (handled by Digital Marketing Agent)
- Case creation in CMS (handled by New Business Agent)
- Legal advice or property valuation
- Payment processing
- Contract negotiation

### 4. User Stories

```gherkin
As a Sales Agent, I want to receive pre-qualified leads with complete information, so that I can quickly assess and convert them into cases.
```

```gherkin
As a Sales Agent, I want automated quote generation based on lead details, so that I can respond to prospects within hours, not days.
```

```gherkin
As a Sales Team Lead, I want visibility into pipeline status and conversion rates, so that I can coach my team and forecast revenue.
```

```gherkin
As a Client, I want to receive a quote quickly and track my application status online, so that I can make informed decisions.
```

### 5. Functional Requirements

#### 5.1 Lead Processing & Data Capture
- **FR-2.1:** System must present assigned leads to Sales Agent with:
  - Lead quality score and source
  - All captured contact and property information
  - Lead history and previous interactions
  - Recommended next action based on lead grade

- **FR-2.2:** Sales Agent must be able to enrich lead data by capturing:
  - Additional property details (tenure, bedrooms, property type)
  - Client circumstances (first-time buyer, chain status, financing)
  - Specific requirements (timeline, concerns, preferences)
  - Competing quotes or market research

#### 5.2 Lead Qualification
- **FR-2.3:** System must guide Sales Agent through qualification checklist:
  - Verify contact information
  - Confirm transaction type and property details
  - Assess client readiness (financing approved, property identified)
  - Identify special circumstances or risks
  - Determine service level required

- **FR-2.4:** System must calculate and display:
  - Estimated transaction value
  - Recommended service package
  - Win probability score
  - Expected close timeline

#### 5.3 Quote Management
- **FR-2.5:** System must support quote generation:
  - Auto-generate quote from Digital Quoting integration
  - Manual quote adjustment with approval workflow
  - Multi-service bundling (conveyancing + searches + insurance)
  - Tiered pricing based on transaction value and complexity
  - Promotional codes and discounts

- **FR-2.6:** Quote delivery must include:
  - Email quote with PDF attachment
  - Client portal link for online acceptance
  - Itemized cost breakdown
  - Terms and conditions
  - Validity period (auto-expire after X days)

#### 5.4 Quote Chase & Follow-Up
- **FR-2.7:** System must automate quote follow-up:
  - Send reminder emails at day 2, 5, and 7 after quote delivery
  - Flag quotes nearing expiration for Sales Agent action
  - Track quote opens and client portal visits
  - Suggest phone call after 3 days of no response

- **FR-2.8:** Sales Agent must be able to:
  - Log call outcomes and notes
  - Schedule follow-up tasks and reminders
  - Update quote or provide revised pricing
  - Mark lead as lost with reason code

#### 5.5 Client Consent & Case Agreement
- **FR-2.9:** When client accepts quote, system must:
  - Capture formal consent to proceed
  - Collect initial case information (property address, parties, timeline)
  - Request required documentation (ID, proof of funds)
  - Generate instruction form or confirmation letter
  - Trigger case creation workflow (handoff to New Business Agent)

- **FR-2.10:** System must support consent methods:
  - Online portal acceptance (e-signature)
  - Email confirmation with reply tracking
  - Phone verbal consent with recording reference
  - In-person signed agreement upload

#### 5.6 Lead Closing
- **FR-2.11:** Sales Agent must close lead as:
  - **Won:** Lead converted to case (automatically upon consent)
  - **Lost:** Lead declined with reason (price, timeline, service, competitor, other)
  - **On Hold:** Lead postponed with follow-up date
  - **Invalid:** Lead does not meet service criteria

- **FR-2.12:** System must track lost reasons for analysis:
  - Price too high
  - Timeline not aligned
  - Service not suitable
  - Chose competitor
  - Client unresponsive
  - Transaction cancelled

#### 5.7 Tick-Match Request Processing
- **FR-2.13:** System must handle tick-match requests (client referrals from estate agents/brokers):
  - Validate tick-match code or referral reference
  - Link lead to referral partner account
  - Apply partner-specific pricing or commission structure
  - Notify partner of lead status updates
  - Track referral fees for accounting

#### 5.8 Activity Tracking & Reporting
- **FR-2.14:** System must log all sales activities:
  - Calls (date, time, duration, outcome)
  - Emails sent and received
  - Quote versions and revisions
  - Document uploads and reviews
  - Status changes and pipeline movement

- **FR-2.15:** Sales Agent dashboard must display:
  - Active leads by stage (new, contacted, quoted, negotiating, won, lost)
  - Overdue tasks and follow-ups
  - Conversion rate and pipeline value
  - Average days to close
  - Personal performance vs. team average

### 6. Non-Functional Requirements

#### 6.1 Performance
- **NFR-2.1:** Quote generation must complete within 10 seconds
- **NFR-2.2:** Dashboard must load within 2 seconds
- **NFR-2.3:** System must support 50+ concurrent Sales Agents

#### 6.2 Usability
- **NFR-2.4:** Sales Agent interface must be mobile-responsive
- **NFR-2.5:** Lead processing workflow must require ≤ 5 clicks to complete standard actions
- **NFR-2.6:** System must provide contextual help and tooltips

#### 6.3 Reliability
- **NFR-2.7:** Quote delivery must have 99% success rate
- **NFR-2.8:** Failed email deliveries must retry and alert agent

#### 6.4 Integration
- **NFR-2.9:** CRM sync must occur in real-time (< 30 seconds latency)
- **NFR-2.10:** Digital Quoting integration must have fallback to manual quote entry

### 7. Dependencies

**Upstream Dependencies:**
- Digital Marketing Lead Capture feature (F001)
- CRM system with lead/opportunity management
- Digital Quoting system API

**Downstream Dependencies:**
- Case Creation Automation feature (F003)
- Email notification service
- Client portal infrastructure

**External Services:**
- E-signature platform (for online consent)
- Document storage system
- Call recording system (optional)

### 8. Acceptance Criteria

```gherkin
Given a Sales Agent receives a new qualified lead
When they access the lead in the system
Then all lead data is displayed with qualification checklist
And a quote can be generated in less than 10 seconds
And the quote is automatically emailed to the client
And the lead status is updated to "Quoted"
```

```gherkin
Given a client accepts a quote via the portal
When the consent is captured
Then the Sales Agent is notified immediately
And the case creation workflow is triggered
And the lead status is changed to "Won"
And the CRM opportunity is marked as closed-won
```

```gherkin
Given a Sales Agent has 10 open leads
When 3 quotes are overdue for follow-up
Then the dashboard highlights the overdue items
And automated reminder emails are sent to clients
And the agent sees recommended next actions
```

### 9. Success Metrics

- **Lead-to-Quote Time:** < 4 hours for qualified leads
- **Quote-to-Conversion Rate:** ≥ 40% (PRD target)
- **Response Time:** < 2 hours for client inquiries during business hours
- **Quote Accuracy:** ≥ 98% (no unplanned pricing adjustments)
- **Sales Agent Productivity:** 3x more leads handled per agent vs. manual process
- **Client Satisfaction:** ≥ 4.5/5 rating for quote process

### 10. Open Questions

1. What is the typical quote validity period (7, 14, 30 days)?
2. Should quotes be automatically revised based on market changes?
3. What approval levels are required for discounted quotes?
4. Should the system integrate with specific estate agent portals?
5. Are there requirements for multi-party transactions (joint buyers)?
6. What is the process for handling commercial vs. residential pricing?

### 11. Future Enhancements

- AI-powered quote optimization based on win/loss analysis
- Automated competitive intelligence (tracking competitor pricing)
- Video call integration for virtual consultations
- Multi-language support for international clients
- Predictive lead scoring using machine learning
- Automated proposal generation with personalized content
- Integration with property valuation services

---

**Document Version:** 1.0  
**Last Updated:** December 9, 2025  
**Author:** Product Manager  
**Status:** Draft
