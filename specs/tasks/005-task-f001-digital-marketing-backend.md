# Technical Task: Digital Marketing Lead Capture - Backend

**GitHub Issue:** [#6](https://github.com/erydrn/spec2cloud-marketing-agent/issues/6)

## Task Information

**Task ID:** 005  
**Task Name:** Digital Marketing Lead Capture - Backend API  
**Feature:** F001 - Digital Marketing Lead Capture  
**Priority:** High  
**Complexity:** High  
**Estimated Effort:** 8-10 days  
**Dependencies:**
- Task 001: Backend Scaffolding (API Gateway, Azure Functions, CosmosDB setup)
- Marketing platform API credentials and access
- CRM system API access

## Description

Build the backend API services for capturing leads from multiple marketing channels (Digital Ads, SEO, Business Development, Organic, Strategic), performing automated data extraction, normalization, qualification, lead scoring, deduplication, and intelligent routing to Sales Team or New Business Support Team.

This backend service acts as the ingestion layer for all marketing-originated leads and provides the foundational data quality and routing logic for downstream sales processes.

## Technical Requirements

### 1. API Endpoints (OpenAPI 3.0 Schema Required)

#### 1.1 Lead Capture Endpoints

**POST /api/v1/leads/capture**
- Accepts lead data from any marketing channel
- Request body schema:
  ```yaml
  LeadCaptureRequest:
    type: object
    required: [source, contactInfo, propertyType]
    properties:
      source:
        type: string
        enum: [DIGITAL_ADS, SEO, BUSINESS_DEV, ORGANIC, STRATEGIC, EMAIL, WEB_FORM]
        description: Marketing channel source
      campaignId:
        type: string
        description: Campaign identifier for tracking
      contactInfo:
        type: object
        required: [email or phone]
        properties:
          firstName: string
          lastName: string
          email: string (email format)
          phone: string (UK format)
          alternatePhone: string
      propertyType:
        type: string
        enum: [PURCHASE, SALE, REMORTGAGE, OTHER]
      propertyDetails:
        type: object
        properties:
          address: string
          postcode: string
          propertyValue: number
          propertyType: string (flat, house, bungalow, etc.)
      timeline:
        type: string
        enum: [URGENT, 1_3_MONTHS, 3_6_MONTHS, 6_12_MONTHS, UNSURE]
      referralSource:
        type: object
        properties:
          type: string (estate_agent, broker, partner)
          referenceCode: string
          partnerName: string
      additionalInfo:
        type: object
        description: Free-form additional data
  ```
- Response (201 Created):
  ```yaml
  LeadCaptureResponse:
    type: object
    properties:
      leadId: string (UUID)
      status: string (CAPTURED, QUEUED)
      qualityScore: string (A, B, C)
      assignedTo: string (team name or HOLD)
      message: string
  ```

**POST /api/v1/leads/bulk-capture**
- Batch import for marketing platform integrations
- Request: Array of LeadCaptureRequest objects
- Response: Array of LeadCaptureResponse with individual results

#### 1.2 Lead Management Endpoints

**GET /api/v1/leads/{leadId}**
- Retrieve full lead details including processing history

**PATCH /api/v1/leads/{leadId}**
- Update lead data (for corrections or enrichment)

**GET /api/v1/leads**
- Query leads with filters: `?status=ASSIGNED&qualityScore=A&source=DIGITAL_ADS&dateFrom=2025-01-01`
- Pagination support: `?page=1&pageSize=50`

**GET /api/v1/leads/{leadId}/history**
- Retrieve lead processing audit trail (capture, qualification, routing decisions)

#### 1.3 Lead Qualification Endpoints

**POST /api/v1/leads/{leadId}/requalify**
- Re-run qualification and scoring logic (for manual corrections)

**GET /api/v1/leads/quality-metrics**
- Return quality score distribution and data completeness metrics

#### 1.4 Lead Routing Endpoints

**POST /api/v1/leads/{leadId}/reassign**
- Manual reassignment to different team/agent
- Request body: `{ "assignedTo": "team_id", "reason": "string" }`

**GET /api/v1/teams/workload**
- Return current workload distribution for routing algorithm

### 2. Data Processing Requirements

#### 2.1 Lead Data Normalization
- Normalize phone numbers to UK standard format (E.164: +44...)
- Validate and normalize email addresses (lowercase, trim whitespace)
- Standardize postcodes using Royal Mail PAF format (e.g., "SW1A 1AA")
- Parse and structure property addresses into components (line1, line2, city, county, postcode)
- Convert currency values to numeric format (remove £, commas)
- Parse and normalize dates to ISO 8601 format
- Trim and sanitize all text fields (remove special characters, HTML tags)

#### 2.2 Lead Qualification Rules
System must implement qualification checklist and flag results:
- **Contact Completeness:** Email OR phone required (pass/fail)
- **Property Type Identification:** Must map to PURCHASE/SALE/REMORTGAGE (pass/fail)
- **Geographic Validation:** Postcode must be in serviceable area (configurable postcode list/radius)
- **Transaction Value Range:** Must be within min/max bounds (configurable, e.g., £50k - £5M)
- **Duplicate Detection:** Check against existing leads by email, phone, and property address (fuzzy matching with 85% threshold)

#### 2.3 Lead Scoring Algorithm
Calculate quality score (A/B/C) based on weighted factors:
- **Data Completeness (50% weight):**
  - All required fields present: 50 points
  - Property address complete: 20 points
  - Timeline specified: 15 points
  - Additional contact info (alternate phone): 15 points
- **Transaction Value (30% weight):**
  - > £500k: 30 points
  - £250k - £500k: 20 points
  - £100k - £250k: 10 points
  - < £100k: 5 points
- **Timeline Urgency (20% weight):**
  - URGENT: 20 points
  - 1-3 MONTHS: 15 points
  - 3-6 MONTHS: 10 points
  - 6-12 MONTHS: 5 points
  - UNSURE: 0 points

**Score Mapping:**
- **Grade A:** ≥ 75 points (high-quality, complete leads)
- **Grade B:** 50-74 points (moderate quality, some gaps)
- **Grade C:** < 50 points (low quality, major gaps)

#### 2.4 Lead Routing Logic
Route leads based on qualification and score:
- **Grade A + Complete Data:** → Sales Team (immediate assignment using round-robin)
- **Grade B + Incomplete Data:** → New Business Support Team (data completion queue)
- **Grade C or Out-of-Area:** → Hold Queue (manual review)
- **Duplicate Detected:** → Merge with existing lead, notify current owner, DO NOT create new record

Sales Team assignment algorithm:
- Retrieve team members with status = AVAILABLE
- Calculate current workload (active leads count per member)
- Assign to member with lowest workload
- If workload equal, use round-robin rotation (last assigned timestamp)
- Support geographic specialization (if member has postcode area preference, prioritize)

#### 2.5 Lead Deduplication
Duplicate detection logic:
- **Exact Match:** Email match (case-insensitive) OR phone match (normalized)
- **Fuzzy Match:** Property address similarity > 85% using Levenshtein distance
- **Time Window:** Check leads from last 90 days

On duplicate detection:
- Append new submission data to existing lead record
- Update lead status if more recent data available
- Increment "duplicate_submission_count" field
- Create activity log entry with new submission details
- Notify assigned owner via notification service
- Return existing lead ID in API response

#### 2.6 Lead Enrichment (Optional Enhancement)
Where possible, enrich lead data:
- Validate phone number format and carrier lookup
- Validate email domain (MX record check)
- Lookup property data from public APIs (if address provided)
- Search CRM for existing client records (by email/phone)

### 3. Data Storage (CosmosDB)

#### 3.1 Leads Container
Schema:
```json
{
  "id": "uuid",
  "partitionKey": "source",
  "leadId": "uuid",
  "source": "DIGITAL_ADS | SEO | ...",
  "campaignId": "string",
  "status": "CAPTURED | QUALIFIED | ASSIGNED | DUPLICATE | HOLD",
  "qualityScore": "A | B | C",
  "contactInfo": {
    "firstName": "string",
    "lastName": "string",
    "email": "string",
    "phone": "string",
    "alternatePhone": "string"
  },
  "propertyType": "PURCHASE | SALE | REMORTGAGE | OTHER",
  "propertyDetails": {
    "address": "string",
    "addressComponents": {
      "line1": "string",
      "line2": "string",
      "city": "string",
      "county": "string",
      "postcode": "string"
    },
    "propertyValue": "number",
    "propertyType": "string"
  },
  "timeline": "URGENT | 1_3_MONTHS | ...",
  "referralSource": { "type": "string", "referenceCode": "string", "partnerName": "string" },
  "qualificationResults": {
    "contactComplete": "boolean",
    "propertyTypeIdentified": "boolean",
    "geographicValid": "boolean",
    "transactionValueValid": "boolean",
    "isDuplicate": "boolean",
    "duplicateOfLeadId": "string"
  },
  "scoringDetails": {
    "dataCompletenessScore": "number",
    "transactionValueScore": "number",
    "timelineScore": "number",
    "totalScore": "number"
  },
  "routingDetails": {
    "assignedTo": "string (team_id or agent_id)",
    "assignedTeam": "SALES | NEW_BUSINESS | HOLD",
    "assignmentTimestamp": "datetime",
    "assignmentMethod": "AUTO | MANUAL"
  },
  "enrichmentData": {
    "phoneValidated": "boolean",
    "emailValidated": "boolean",
    "existingClientId": "string"
  },
  "additionalInfo": "object",
  "createdAt": "datetime",
  "updatedAt": "datetime",
  "processedAt": "datetime",
  "duplicateSubmissionCount": "number",
  "_etag": "string"
}
```

#### 3.2 Lead History Container (Audit Trail)
Schema:
```json
{
  "id": "uuid",
  "partitionKey": "leadId",
  "leadId": "string",
  "eventType": "CAPTURED | QUALIFIED | ROUTED | ASSIGNED | UPDATED | DUPLICATE_DETECTED",
  "eventData": "object (details of the event)",
  "performedBy": "string (system or user_id)",
  "timestamp": "datetime"
}
```

### 4. Integration Requirements

#### 4.1 CRM Integration
- Create/update contact records in CRM when lead is captured
- Create opportunity record linked to contact
- Sync lead status updates to CRM
- Query CRM for duplicate client detection
- API client for CRM (e.g., Dynamics 365, Salesforce)

#### 4.2 Lead Systems Integration
- Bi-directional sync with firm's lead management system
- Push new leads to lead system
- Pull lead status updates from lead system

#### 4.3 Digital Quoting Integration
- Push Grade A qualified leads to Digital Quoting tool for auto-quote generation
- Include lead details required for quote (property type, value, timeline)

#### 4.4 Introducer Portal Integration
- Receive leads submitted via Introducer Portal
- Validate tick-match codes and referral references
- Link leads to partner accounts

#### 4.5 Notification Service
- Send email notifications to assigned agents when leads are routed
- Send notifications to existing lead owners when duplicates detected
- Support webhook notifications to external systems

### 5. Business Logic & Validation

#### 5.1 Input Validation
- All required fields must be present (source, contactInfo, propertyType)
- Email must pass regex validation and domain check
- Phone must match UK format (+44... or 0...)
- Property value must be positive number
- Timeline must be valid enum value
- Postcode must match UK format regex

#### 5.2 Error Handling
- Return 400 Bad Request for validation failures with field-level errors
- Return 409 Conflict for duplicate leads with existing lead ID
- Return 500 Internal Server Error for system failures with correlation ID for tracing
- Implement retry logic with exponential backoff for external service calls (CRM, lead systems)
- Dead-letter queue for failed processing (manual intervention)

#### 5.3 Configuration Management
- Serviceable postcode areas (configurable list or radius from office locations)
- Transaction value min/max bounds
- Quality score thresholds and weights
- Team member availability and workload limits
- Duplicate detection time window and similarity threshold

## Acceptance Criteria

1. **Lead Capture:**
   - [ ] API accepts lead submissions from all defined channels
   - [ ] All required fields are validated before processing
   - [ ] Invalid submissions return 400 with field-level error messages
   - [ ] Successfully captured leads return 201 with lead ID and quality score

2. **Data Normalization:**
   - [ ] Phone numbers normalized to E.164 format
   - [ ] Postcodes normalized to Royal Mail PAF format
   - [ ] Email addresses validated and normalized (lowercase)
   - [ ] Property addresses parsed into structured components
   - [ ] At least 95% of structured data sources normalized correctly

3. **Lead Qualification:**
   - [ ] Contact completeness check passes when email OR phone present
   - [ ] Property type identified for 100% of leads
   - [ ] Geographic validation correctly identifies in-area vs. out-of-area
   - [ ] Transaction value validation enforces configured min/max bounds
   - [ ] Duplicate detection identifies duplicates with >85% accuracy

4. **Lead Scoring:**
   - [ ] Quality scores (A/B/C) calculated correctly based on weighted algorithm
   - [ ] Grade A leads have ≥75 points total
   - [ ] Grade B leads have 50-74 points
   - [ ] Grade C leads have <50 points
   - [ ] Scoring details stored in database for audit

5. **Lead Routing:**
   - [ ] Grade A leads with complete data routed to Sales Team
   - [ ] Grade B leads with incomplete data routed to New Business Support Team
   - [ ] Grade C or out-of-area leads routed to Hold Queue
   - [ ] Duplicate leads merged with existing records, no new record created
   - [ ] Sales Team assignment uses round-robin with workload balancing
   - [ ] Routing completes within 30 seconds of capture

6. **Lead Deduplication:**
   - [ ] Exact email matches detected 100% of the time
   - [ ] Exact phone matches detected 100% of the time
   - [ ] Fuzzy address matches detected with >85% similarity threshold
   - [ ] Duplicate detection checks last 90 days of leads
   - [ ] Duplicate submissions merged correctly without data loss
   - [ ] Assigned owners notified when duplicates detected

7. **CRM Integration:**
   - [ ] New contact records created in CRM for all leads
   - [ ] Opportunity records created and linked to contacts
   - [ ] Lead status updates synced to CRM within 30 seconds
   - [ ] Existing client records identified by email/phone search
   - [ ] CRM sync failures logged and retried automatically

8. **Performance:**
   - [ ] Lead capture processing completes within 2 minutes
   - [ ] API handles 500+ concurrent lead submissions without degradation
   - [ ] Lead routing completes within 30 seconds
   - [ ] API response time <500ms for capture endpoint (p95)

9. **Error Handling:**
   - [ ] Validation errors return 400 with clear error messages
   - [ ] Duplicate conflicts return 409 with existing lead ID
   - [ ] System errors return 500 with correlation ID
   - [ ] Failed CRM calls retry with exponential backoff (3 attempts)
   - [ ] Unprocessable leads moved to dead-letter queue

10. **Audit & Compliance:**
    - [ ] All lead processing events logged to history container
    - [ ] Audit trail includes capture, qualification, routing, and assignment events
    - [ ] GDPR consent captured and stored with lead data
    - [ ] Data encrypted at rest and in transit
    - [ ] Lead data retention follows configured policy

## Testing Requirements

### Unit Tests (≥85% Coverage)
- Lead normalization functions (phone, email, postcode, address parsing)
- Qualification rule evaluations (contact completeness, geographic validation, etc.)
- Scoring algorithm calculations (weighted scoring, grade assignment)
- Routing logic (round-robin, workload balancing, duplicate handling)
- Deduplication matching algorithms (exact and fuzzy matching)
- Input validation functions

### Integration Tests
- End-to-end lead capture flow: POST /api/v1/leads/capture → qualification → scoring → routing → database write
- CRM integration: Lead capture triggers contact and opportunity creation in CRM
- Digital Quoting integration: Grade A leads pushed to quoting tool
- Duplicate detection: Submit duplicate lead → verify merge with existing record
- Bulk capture: Submit batch of 100 leads → verify all processed correctly

### API Contract Tests
- OpenAPI schema validation for all endpoints
- Request/response schema validation
- Error response format validation (400, 409, 500)

### Performance Tests
- Load test: 500 concurrent lead submissions, verify <2 minute processing time
- Stress test: 1000 concurrent submissions, verify graceful degradation
- Latency test: Verify p95 API response time <500ms

### Test Scenarios
1. **Happy Path:** Submit complete Grade A lead → verify routing to Sales Team with round-robin assignment
2. **Incomplete Lead:** Submit Grade B lead with missing property address → verify routing to New Business Support Team
3. **Out-of-Area:** Submit lead with non-serviceable postcode → verify routing to Hold Queue
4. **Duplicate Email:** Submit lead with existing email → verify merge with existing lead, no new record
5. **Duplicate Address:** Submit lead with similar property address (>85%) → verify duplicate detection
6. **Invalid Data:** Submit lead with invalid email and phone → verify 400 error with field-level messages
7. **CRM Failure:** Submit lead when CRM is down → verify retry logic and eventual success
8. **Bulk Import:** Submit 100 leads via bulk endpoint → verify all processed with correct routing

## Dependencies

### Upstream Dependencies
- **Task 001 (Backend Scaffolding):** API Gateway, Azure Functions runtime, CosmosDB setup, deployment pipeline
- **Marketing Platform APIs:** Access credentials for Google Ads, Meta, LinkedIn (for future channel integrations)
- **Email Server:** SMTP/API access for email-based lead capture
- **Website Infrastructure:** Web form submission endpoints

### Downstream Dependencies
- **CRM System:** API access for contact/opportunity creation (e.g., Dynamics 365, Salesforce)
- **Lead Management System:** API access for bi-directional lead sync
- **Digital Quoting Tool:** API access for pushing qualified leads
- **Introducer Portal:** API access for receiving referral submissions
- **Notification Service:** Email/webhook service for agent notifications

### External Services
- **Email Validation Service:** For email domain verification (e.g., Kickbox, ZeroBounce)
- **Phone Validation Service:** For UK phone number validation (e.g., Twilio Lookup)
- **Address Lookup Service:** Royal Mail PAF or Postcode.io for address normalization
- **Property Data Service (Optional):** For property detail enrichment (e.g., Zoopla, Rightmove APIs)

## Technical Decisions Required

1. **CRM Platform Selection:** Which CRM system will be integrated (Dynamics 365, Salesforce, custom)?
2. **Duplicate Detection Algorithm:** Exact matching only, or include fuzzy matching for addresses? Similarity threshold?
3. **Routing Algorithm Complexity:** Simple round-robin or advanced workload balancing with geographic specialization?
4. **Lead Enrichment Services:** Which external services for phone/email validation and property data lookup?
5. **Real-Time vs. Batch Processing:** Should lead processing be fully real-time or batched for high-volume scenarios?
6. **Dead-Letter Queue Strategy:** Manual review process for failed leads? Auto-retry policy?
7. **Rate Limiting:** Should API implement rate limiting per source/campaign to prevent abuse?
8. **Data Retention Policy:** How long to retain leads in various statuses (captured, lost, converted)?

## Notes

- This backend service is the entry point for all marketing-originated leads and must maintain high availability (99.9% uptime)
- Lead processing latency directly impacts sales response time; optimize for speed while maintaining data quality
- Deduplication is critical to prevent duplicate outreach to prospects; prioritize accuracy
- Consider implementing feature flags for gradual rollout of routing algorithm changes
- Implement comprehensive logging for troubleshooting lead processing issues
- Future enhancement: Machine learning model for lead quality prediction based on historical conversion data
- Future enhancement: Real-time marketing channel performance analytics

## Definition of Done

- [ ] All API endpoints implemented with OpenAPI specification
- [ ] Lead capture, normalization, qualification, scoring, and routing logic implemented
- [ ] Deduplication logic implemented with exact and fuzzy matching
- [ ] CosmosDB schema implemented with leads and history containers
- [ ] CRM integration implemented (contact/opportunity creation, duplicate search)
- [ ] Lead Systems and Digital Quoting integrations implemented
- [ ] Notification service integration implemented (email notifications to agents)
- [ ] Unit tests written with ≥85% code coverage
- [ ] Integration tests passing for end-to-end flows
- [ ] API contract tests passing (OpenAPI schema validation)
- [ ] Performance tests passing (500 concurrent submissions, <2 min processing)
- [ ] Error handling and retry logic implemented
- [ ] Configuration management implemented (serviceable areas, scoring weights, etc.)
- [ ] Logging and monitoring implemented (Application Insights)
- [ ] Code reviewed and approved
- [ ] API documentation generated from OpenAPI spec
- [ ] Deployed to dev/staging environment and tested
- [ ] Security review completed (data encryption, input validation)
- [ ] GDPR compliance verified (consent capture, data retention)

---

**Document Version:** 1.0  
**Last Updated:** December 9, 2025  
**Author:** Technical Task Writer  
**Status:** Ready for Development
