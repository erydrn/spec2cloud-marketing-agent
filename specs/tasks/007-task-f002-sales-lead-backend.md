# Technical Task: Sales Lead Management - Backend

**GitHub Issue:** [#8](https://github.com/erydrn/spec2cloud-marketing-agent/issues/8)

## Task Information

**Task ID:** 007  
**Task Name:** Sales Lead Management - Backend API  
**Feature:** F002 - Sales Lead Management  
**Priority:** High  
**Complexity:** High  
**Estimated Effort:** 10-12 days  
**Dependencies:**
- Task 001: Backend Scaffolding
- Task 005: Digital Marketing Backend (leads flow from F001 to F002)
- CRM and Digital Quoting system API access

## Description

Build backend API services for Sales Team to process assigned leads through detailed qualification, quote generation and management, automated follow-up workflows, activity tracking, client consent capture, and lead closing (won/lost). This service handles the sales pipeline from initial lead assignment through case conversion or lead closure.

## Technical Requirements

### 1. API Endpoints (OpenAPI 3.0 Schema Required)

#### 1.1 Lead Processing Endpoints

**GET /api/v1/sales/leads/assigned**
- Retrieve leads assigned to authenticated sales agent
- Query parameters: `?status=NEW&sortBy=createdAt&order=desc&page=1&pageSize=50`
- Response: Array of SalesLead objects with pagination metadata

**GET /api/v1/sales/leads/{leadId}**
- Retrieve full lead details with sales-specific enrichment
- Response includes: lead data, qualification checklist, quote history, activity timeline, win probability score

**PATCH /api/v1/sales/leads/{leadId}**
- Enrich lead data (additional property details, client circumstances, requirements)
- Request body: Partial SalesLead object
- Response: Updated SalesLead

#### 1.2 Lead Qualification Endpoints

**POST /api/v1/sales/leads/{leadId}/qualify**
- Complete qualification checklist for lead
- Request body:
  ```yaml
  QualificationRequest:
    type: object
    required: [checklistItems]
    properties:
      checklistItems:
        type: object
        properties:
          contactVerified: boolean
          propertyDetailsConfirmed: boolean
          clientReadinessAssessed: boolean
          specialCircumstancesIdentified: boolean
          serviceLevelDetermined: string (STANDARD, PREMIUM, EXPRESS)
      estimatedValue: number
      recommendedServicePackage: string
      winProbability: number (0-100)
      expectedCloseDate: string (ISO date)
      qualificationNotes: string
  ```
- Response: Updated lead with qualification status and calculated metrics

#### 1.3 Quote Management Endpoints

**POST /api/v1/sales/leads/{leadId}/quotes**
- Generate new quote for lead
- Request body:
  ```yaml
  QuoteRequest:
    type: object
    required: [servicePackage, transactionType]
    properties:
      servicePackage: string (STANDARD, PREMIUM, EXPRESS)
      transactionType: string (PURCHASE, SALE, REMORTGAGE)
      transactionValue: number
      additionalServices:
        type: array
        items:
          type: string (SEARCHES, INSURANCE, INDEMNITY)
      promotionalCode: string
      customAdjustments:
        type: object
        properties:
          discountPercentage: number
          discountReason: string
          requiresApproval: boolean
  ```
- Response (201 Created): Quote object with quoteId, pricing breakdown, validity period

**GET /api/v1/sales/leads/{leadId}/quotes**
- Retrieve all quotes for lead (including revisions)

**GET /api/v1/sales/quotes/{quoteId}**
- Retrieve specific quote details

**PATCH /api/v1/sales/quotes/{quoteId}**
- Update quote (adjust pricing, extend validity, add services)

**POST /api/v1/sales/quotes/{quoteId}/send**
- Send quote to client via email with PDF attachment and portal link
- Request body: `{ "recipientEmail": "string", "ccEmails": ["string"], "message": "string" }`

**POST /api/v1/sales/quotes/{quoteId}/approve**
- Approve quote requiring authorization (discounted quotes)
- Request body: `{ "approverComments": "string" }`

#### 1.4 Quote Follow-Up Endpoints

**GET /api/v1/sales/quotes/pending-followup**
- Retrieve quotes requiring follow-up based on automated chase rules
- Filters: `?daysOpen=2,5,7&nearExpiry=true`

**POST /api/v1/sales/quotes/{quoteId}/schedule-followup**
- Schedule follow-up task for quote
- Request body: `{ "followUpDate": "ISO date", "followUpType": "EMAIL|CALL", "notes": "string" }`

#### 1.5 Activity Tracking Endpoints

**POST /api/v1/sales/leads/{leadId}/activities**
- Log activity (call, email, meeting, note)
- Request body:
  ```yaml
  ActivityRequest:
    type: object
    required: [activityType, activityDate]
    properties:
      activityType: string (CALL, EMAIL, MEETING, NOTE)
      activityDate: string (ISO datetime)
      duration: number (minutes, for CALL/MEETING)
      outcome: string (CONNECTED, VOICEMAIL, NO_ANSWER, POSITIVE, NEGATIVE)
      notes: string
      nextAction: string
      nextActionDate: string (ISO date)
  ```
- Response (201 Created): Activity object

**GET /api/v1/sales/leads/{leadId}/activities**
- Retrieve activity timeline for lead

#### 1.6 Client Consent & Case Agreement Endpoints

**POST /api/v1/sales/quotes/{quoteId}/accept**
- Client accepts quote (capture consent to proceed)
- Request body:
  ```yaml
  ConsentRequest:
    type: object
    required: [consentMethod, agreedTerms]
    properties:
      consentMethod: string (ONLINE_PORTAL, EMAIL_CONFIRMATION, PHONE_VERBAL, IN_PERSON)
      agreedTerms: boolean
      signatureData: string (base64 for e-signature)
      recordingReference: string (for phone verbal consent)
      consentDate: string (ISO datetime)
      initialCaseInfo:
        type: object
        properties:
          propertyAddress: string
          targetCompletionDate: string (ISO date)
          additionalParties: array
  ```
- Response: Triggers case creation workflow (handoff to F003), returns caseId

**POST /api/v1/sales/leads/{leadId}/request-documentation**
- Request required documentation from client (ID, proof of funds)
- Request body: `{ "documentTypes": ["ID", "PROOF_OF_FUNDS", "MORTGAGE_OFFER"], "message": "string" }`

#### 1.7 Lead Closing Endpoints

**POST /api/v1/sales/leads/{leadId}/close**
- Close lead with outcome
- Request body:
  ```yaml
  CloseRequest:
    type: object
    required: [outcome, reason]
    properties:
      outcome: string (WON, LOST, ON_HOLD, INVALID)
      reason: string (PRICE_TOO_HIGH, TIMELINE_MISALIGNED, SERVICE_NOT_SUITABLE, COMPETITOR, UNRESPONSIVE, TRANSACTION_CANCELLED, OTHER)
      competitorDetails: string (if reason=COMPETITOR)
      onHoldFollowUpDate: string (ISO date, if outcome=ON_HOLD)
      closingNotes: string
  ```
- Response: Updated lead with closed status, triggers CRM update

#### 1.8 Tick-Match Request Endpoints

**POST /api/v1/sales/leads/{leadId}/tick-match**
- Process tick-match request (client referral from estate agent/broker)
- Request body:
  ```yaml
  TickMatchRequest:
    type: object
    required: [tickMatchCode, partnerType]
    properties:
      tickMatchCode: string
      partnerType: string (ESTATE_AGENT, MORTGAGE_BROKER, IFA)
      partnerName: string
      partnerContactEmail: string
      specialPricingApplies: boolean
      commissionStructure: object
  ```
- Response: Linked referral partner, applied pricing adjustments

**GET /api/v1/sales/referral-partners**
- Retrieve list of registered referral partners with tick-match codes

#### 1.9 Sales Dashboard & Reporting Endpoints

**GET /api/v1/sales/dashboard/my-pipeline**
- Retrieve authenticated agent's pipeline summary
- Response:
  ```yaml
  PipelineSummary:
    type: object
    properties:
      leadsByStage:
        type: object (NEW, CONTACTED, QUOTED, NEGOTIATING, WON, LOST counts)
      overdueFollowUps: number
      conversionRate: number (percentage)
      pipelineValue: number (total estimated value of active leads)
      averageDaysToClose: number
      thisMonthStats:
        type: object (leads_won, leads_lost, quotes_sent, calls_made)
  ```

**GET /api/v1/sales/dashboard/team-performance**
- Retrieve team-wide performance metrics (for team leads)
- Filters: `?teamId=xxx&dateFrom=xxx&dateTo=xxx`

### 2. Data Processing Requirements

#### 2.1 Lead Enrichment
When sales agent enriches lead:
- Validate and structure additional property details (tenure, bedrooms, property type)
- Parse client circumstances (first-time buyer, chain status, financing)
- Extract specific requirements (timeline, concerns, preferences)
- Store competing quotes or market research data

#### 2.2 Qualification Checklist Processing
Implement qualification workflow:
- Guide agent through checklist items (contact verified, property confirmed, etc.)
- Calculate estimated transaction value based on property type, value, complexity
- Recommend service package (STANDARD, PREMIUM, EXPRESS) based on transaction value and timeline
- Calculate win probability score based on:
  - Lead quality score (from F001): 30%
  - Client readiness (financing approved, property identified): 40%
  - Timeline alignment (urgency vs. current capacity): 20%
  - Agent assessment (special circumstances, risks): 10%
- Estimate expected close timeline based on historical data and lead characteristics

#### 2.3 Quote Generation Logic
Integrate with Digital Quoting system or implement pricing engine:
- Retrieve pricing rules based on transaction type, value, service package
- Calculate base conveyancing fee (tiered by transaction value)
- Add additional service costs (searches, insurance, indemnity policies)
- Apply promotional codes (validate against active campaigns)
- Apply discounts (require approval if discount >10%)
- Calculate total cost with itemized breakdown
- Set quote validity period (default 30 days, configurable)

**Pricing Structure Example:**
- Purchase (£250k - £500k), Standard: £800 base + £300 searches + VAT
- Sale (£250k - £500k), Standard: £600 base + £50 redemption admin + VAT
- Remortgage: £350 base + VAT

#### 2.4 Quote Chase & Follow-Up Automation
Automated follow-up rules:
- Day 2 after quote sent: Send reminder email ("Have you had a chance to review?")
- Day 5: Flag for sales agent action (recommend phone call)
- Day 7: Send final reminder email ("Quote expires in X days")
- Day 3 before expiry: Flag as "Near Expiry" in dashboard
- Track quote opens (email tracking pixel) and portal visits

#### 2.5 Activity Tracking & Logging
Log all sales activities:
- Calls: Date, time, duration, outcome (connected, voicemail, no answer), notes, next action
- Emails: Date, subject, sent/received, template used
- Meetings: Date, time, duration, attendees, notes
- Notes: Free-form notes added by agent

Activity metrics:
- Total calls made per agent per day/week/month
- Average call duration
- Connection rate (connected calls / total calls)
- Email response rate
- Average response time to client inquiries

#### 2.6 Client Consent Processing
Capture client consent to proceed:
- **Online Portal:** E-signature captured, IP address logged, timestamp
- **Email Confirmation:** Reply-to-proceed email, track reply timestamp
- **Phone Verbal:** Recording reference stored, confirmation statement read, agent attests
- **In-Person:** Scanned signed agreement uploaded

Upon consent:
- Update lead status to WON
- Trigger case creation workflow (handoff to F003 New Business Agent)
- Send confirmation email to client with next steps
- Notify assigned solicitor/case handler
- Update CRM opportunity to "Closed-Won"

#### 2.7 Lead Closing Logic
When lead closed:
- Update lead status to CLOSED
- Set close date and outcome (WON, LOST, ON_HOLD, INVALID)
- Log closing reason and notes
- If WON: Ensure case creation triggered, link lead to case record
- If LOST: Update CRM opportunity to "Closed-Lost" with reason
- If ON_HOLD: Schedule follow-up task for specified date
- Calculate time-to-close metric (days from lead capture to close)

Lost reason tracking for analysis:
- Price too high: Track competitive pricing data
- Timeline not aligned: Track client timeline vs. firm capacity
- Service not suitable: Identify service gaps
- Chose competitor: Track competitor names for market intelligence
- Client unresponsive: Track outreach attempts before closing
- Transaction cancelled: External factors (buyer pulled out, sale fell through)

### 3. Data Storage (CosmosDB)

#### 3.1 Sales Leads Container (extends F001 Leads)
Additional fields for sales processing:
```json
{
  "id": "uuid",
  "partitionKey": "leadId",
  "leadId": "string (from F001)",
  "salesEnrichment": {
    "additionalPropertyDetails": {
      "tenure": "FREEHOLD | LEASEHOLD",
      "bedrooms": "number",
      "propertyTypeDetailed": "string"
    },
    "clientCircumstances": {
      "firstTimeBuyer": "boolean",
      "chainStatus": "NO_CHAIN | UPWARD | DOWNWARD | BOTH",
      "financingType": "CASH | MORTGAGE | MIXED",
      "mortgageApproved": "boolean"
    },
    "specificRequirements": {
      "targetCompletionDate": "ISO date",
      "concerns": ["string"],
      "preferences": ["string"]
    },
    "competingQuotes": ["object"]
  },
  "qualification": {
    "status": "NOT_STARTED | IN_PROGRESS | COMPLETED",
    "checklistItems": {
      "contactVerified": "boolean",
      "propertyDetailsConfirmed": "boolean",
      "clientReadinessAssessed": "boolean",
      "specialCircumstancesIdentified": "boolean",
      "serviceLevelDetermined": "string"
    },
    "estimatedValue": "number",
    "recommendedServicePackage": "string",
    "winProbability": "number",
    "expectedCloseDate": "ISO date",
    "qualificationNotes": "string",
    "completedAt": "datetime",
    "completedBy": "string (agent_id)"
  },
  "salesStatus": "NEW | CONTACTED | QUOTED | NEGOTIATING | WON | LOST | ON_HOLD | INVALID",
  "assignedAgent": "string (agent_id)",
  "closedAt": "datetime",
  "closedOutcome": "WON | LOST | ON_HOLD | INVALID",
  "closedReason": "string",
  "closingNotes": "string",
  "caseId": "string (if won, link to case created in F003)",
  "referralPartner": {
    "tickMatchCode": "string",
    "partnerType": "string",
    "partnerName": "string",
    "commissionRate": "number"
  }
}
```

#### 3.2 Quotes Container
Schema:
```json
{
  "id": "uuid",
  "partitionKey": "leadId",
  "quoteId": "uuid",
  "leadId": "string",
  "quoteVersion": "number (1, 2, 3 for revisions)",
  "status": "DRAFT | SENT | VIEWED | ACCEPTED | EXPIRED | SUPERSEDED",
  "servicePackage": "STANDARD | PREMIUM | EXPRESS",
  "transactionType": "PURCHASE | SALE | REMORTGAGE",
  "transactionValue": "number",
  "pricing": {
    "baseFee": "number",
    "additionalServices": [
      {
        "service": "SEARCHES | INSURANCE | INDEMNITY",
        "cost": "number"
      }
    ],
    "subtotal": "number",
    "discountPercentage": "number",
    "discountAmount": "number",
    "vat": "number",
    "totalCost": "number"
  },
  "promotionalCode": "string",
  "validUntil": "ISO date",
  "sentAt": "datetime",
  "sentTo": "string (email)",
  "viewedAt": "datetime",
  "acceptedAt": "datetime",
  "expiresAt": "datetime",
  "approvalRequired": "boolean",
  "approvedBy": "string (manager_id)",
  "approvedAt": "datetime",
  "createdAt": "datetime",
  "createdBy": "string (agent_id)"
}
```

#### 3.3 Activities Container
Schema:
```json
{
  "id": "uuid",
  "partitionKey": "leadId",
  "activityId": "uuid",
  "leadId": "string",
  "activityType": "CALL | EMAIL | MEETING | NOTE",
  "activityDate": "datetime",
  "duration": "number (minutes)",
  "outcome": "string",
  "notes": "string",
  "nextAction": "string",
  "nextActionDate": "ISO date",
  "performedBy": "string (agent_id)",
  "createdAt": "datetime"
}
```

### 4. Integration Requirements

#### 4.1 Digital Quoting System Integration
- API integration to retrieve pricing rules and generate quotes
- Support for custom pricing adjustments and approvals
- Fallback to manual quote entry if Digital Quoting unavailable

#### 4.2 CRM Integration
- Update lead/opportunity status in CRM (NEW → CONTACTED → QUOTED → WON/LOST)
- Sync activity logs (calls, emails, meetings) to CRM timeline
- Retrieve existing client history for returning clients
- Update opportunity close date and reason

#### 4.3 Email Notification Service
- Send quote emails with PDF attachments
- Send follow-up reminder emails (automated chase)
- Send consent confirmation emails
- Track email opens and link clicks (tracking pixel, UTM parameters)

#### 4.4 Document Management System
- Store quote PDFs
- Store signed consent forms
- Store client-uploaded documents (ID, proof of funds)

#### 4.5 Case Creation Integration (F003 Handoff)
- Trigger case creation workflow when quote accepted
- Pass lead data, quote details, and consent information to F003
- Link case record back to lead record

### 5. Business Logic & Validation

#### 5.1 Input Validation
- Quote pricing adjustments must not exceed configured max discount without approval
- Promotional codes must be valid and active
- Client consent must include all required fields (consent method, agreed terms)
- Activity logs must have valid activity type and date

#### 5.2 Authorization
- Sales agents can only access leads assigned to them
- Team leads can access all team leads
- Managers can access all leads across teams
- Quote approvals require manager role for discounts >10%

#### 5.3 Business Rules
- Quotes expire after configured validity period (default 30 days)
- Expired quotes cannot be accepted (must generate new quote)
- Lead can only be closed once (status change to CLOSED is final)
- WON leads must have accepted quote linked
- LOST leads must have closing reason specified

## Acceptance Criteria

1. **Lead Processing:**
   - [ ] Sales agents can retrieve assigned leads with filters and pagination
   - [ ] Lead enrichment updates additional property details and client circumstances
   - [ ] Qualification checklist can be completed with all items
   - [ ] Win probability score calculated based on qualification data

2. **Quote Management:**
   - [ ] Quotes generated with correct pricing based on transaction type and value
   - [ ] Additional services (searches, insurance) added to quote pricing
   - [ ] Promotional codes applied correctly with validation
   - [ ] Discounted quotes require manager approval (>10%)
   - [ ] Quotes sent via email with PDF attachment and portal link
   - [ ] Quote status tracked (SENT, VIEWED, ACCEPTED, EXPIRED)

3. **Quote Follow-Up:**
   - [ ] Automated follow-up reminders sent at day 2, 5, 7
   - [ ] Quotes near expiry flagged in dashboard
   - [ ] Manual follow-up tasks scheduled correctly
   - [ ] Quote opens and portal visits tracked

4. **Activity Tracking:**
   - [ ] All activity types logged (CALL, EMAIL, MEETING, NOTE)
   - [ ] Activity timeline retrieved for lead
   - [ ] Activity metrics calculated (calls made, connection rate, etc.)

5. **Client Consent:**
   - [ ] Consent captured for all consent methods (online, email, phone, in-person)
   - [ ] Accepted quote triggers case creation workflow (handoff to F003)
   - [ ] Consent confirmation email sent to client
   - [ ] CRM opportunity updated to "Closed-Won"

6. **Lead Closing:**
   - [ ] Leads closed with outcome (WON, LOST, ON_HOLD, INVALID)
   - [ ] Closing reason required and stored
   - [ ] Time-to-close metric calculated
   - [ ] CRM opportunity updated with close reason
   - [ ] ON_HOLD leads schedule follow-up task

7. **Tick-Match Processing:**
   - [ ] Tick-match codes validated against referral partner database
   - [ ] Referral partners linked to leads
   - [ ] Special pricing applied for partner referrals
   - [ ] Partner commission tracked

8. **Sales Dashboard:**
   - [ ] Agent pipeline summary displays leads by stage
   - [ ] Overdue follow-ups flagged
   - [ ] Conversion rate and pipeline value calculated
   - [ ] Team performance metrics available for team leads

9. **Integration:**
   - [ ] Digital Quoting integration generates quotes automatically
   - [ ] CRM integration syncs lead status and activities
   - [ ] Email service sends quotes and follow-ups
   - [ ] Case creation triggered on quote acceptance

10. **Performance & Quality:**
    - [ ] Quote generation completes within 10 seconds
    - [ ] Dashboard loads within 2 seconds
    - [ ] All API endpoints return within 500ms (p95)
    - [ ] Unit tests achieve ≥85% coverage
    - [ ] Integration tests pass for all critical flows

## Testing Requirements

### Unit Tests (≥85% Coverage)
- Qualification checklist processing and win probability calculation
- Quote pricing calculation with additional services and discounts
- Automated follow-up rule evaluation
- Activity logging and metrics calculation
- Client consent validation
- Lead closing logic and status updates

### Integration Tests
- End-to-end lead processing: Assign lead → Qualify → Generate quote → Send quote → Accept → Trigger case creation
- CRM sync: Update lead status → Verify CRM opportunity updated
- Digital Quoting integration: Request quote → Verify pricing returned
- Email service: Send quote → Verify email delivered with PDF attachment
- Tick-match processing: Submit tick-match code → Verify partner linked and pricing adjusted

### API Contract Tests
- OpenAPI schema validation for all endpoints
- Request/response validation
- Error response format validation

### Performance Tests
- Quote generation: Generate 100 quotes concurrently, verify <10 second completion
- Dashboard load: Retrieve pipeline for agent with 100 leads, verify <2 second response

### Test Scenarios
1. **Quote Generation:** Sales agent generates quote for £300k purchase → Verify pricing calculated, quote sent, email delivered
2. **Quote Acceptance:** Client accepts quote via portal → Verify consent captured, case creation triggered, CRM updated
3. **Automated Follow-Up:** Quote sent 2 days ago → Verify reminder email sent automatically
4. **Lead Closing (Lost):** Agent closes lead as LOST with reason "Competitor" → Verify status updated, CRM opportunity closed
5. **Tick-Match:** Agent processes tick-match referral → Verify partner linked, commission tracked

## Dependencies

### Upstream Dependencies
- **Task 001:** Backend scaffolding
- **Task 005:** Digital Marketing Backend (leads assigned from F001)
- **CRM System:** API access for opportunity management
- **Digital Quoting System:** API access for quote generation

### Downstream Dependencies
- **Task 003:** Case Creation Automation (F003) for case creation handoff
- **Email Service:** For quote delivery and follow-ups
- **Document Management System:** For quote PDF storage
- **Notification Service:** For agent notifications

### External Services
- **Digital Quoting Tool:** Pricing engine API
- **CRM:** Dynamics 365, Salesforce, or similar
- **Email Tracking Service:** For open/click tracking
- **E-Signature Service:** For online consent capture

## Technical Decisions Required

1. **Digital Quoting Integration:** Build vs. buy pricing engine? Which vendor?
2. **E-Signature Platform:** DocuSign, Adobe Sign, or custom?
3. **Quote PDF Generation:** Server-side rendering library (Puppeteer, wkhtmltopdf)?
4. **Activity Tracking:** Real-time or batch sync to CRM?
5. **Win Probability Algorithm:** Rule-based or ML model?
6. **Follow-Up Automation:** Email service integration (SendGrid, Mailgun)?

## Notes

- Sales pipeline visibility is critical for team performance tracking
- Quote generation speed directly impacts sales agent productivity
- Automated follow-ups improve conversion rates without manual effort
- Activity tracking provides audit trail and enables coaching
- Future enhancement: AI-powered quote optimization based on win/loss analysis
- Future enhancement: Predictive lead scoring using ML

## Definition of Done

- [ ] All API endpoints implemented with OpenAPI specification
- [ ] Lead enrichment and qualification logic implemented
- [ ] Quote generation and management implemented with pricing engine
- [ ] Automated follow-up rules implemented
- [ ] Activity tracking implemented
- [ ] Client consent capture implemented
- [ ] Lead closing logic implemented
- [ ] Tick-match processing implemented
- [ ] Sales dashboard metrics implemented
- [ ] CRM integration implemented (status sync, activity sync)
- [ ] Digital Quoting integration implemented
- [ ] Email service integration implemented (quote delivery, follow-ups)
- [ ] Case creation handoff implemented (trigger F003 workflow)
- [ ] Unit tests written with ≥85% coverage
- [ ] Integration tests passing
- [ ] API contract tests passing
- [ ] Performance tests passing
- [ ] Code reviewed and approved
- [ ] Deployed to dev/staging and tested
- [ ] Documentation completed

---

**Document Version:** 1.0  
**Last Updated:** December 9, 2025  
**Author:** Technical Task Writer  
**Status:** Ready for Development
