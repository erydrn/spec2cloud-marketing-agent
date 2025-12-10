# Technical Task: Case Creation Automation - Backend

**GitHub Issue:** [#10](https://github.com/erydrn/spec2cloud-marketing-agent/issues/10)

## Task Information

**Task ID:** 009  
**Task Name:** Case Creation Automation - Backend API  
**Feature:** F003 - Case Creation Automation  
**Priority:** Critical  
**Complexity:** Very High  
**Estimated Effort:** 12-15 days  
**Dependencies:**
- Task 001: Backend Scaffolding
- Task 007: Sales Lead Backend (handoff trigger from F002)
- CMS (Case Management System) API access
- OCR/AI Document Intelligence service

## Description

Build backend API services to automate instruction form processing, data extraction using OCR/AI, client/company record management in CRM, case creation in CMS, Client Care Letter (CCL) generation, and legal team assignment. This service eliminates manual data entry and accelerates case setup from instruction to assignment.

## Technical Requirements

### 1. API Endpoints (OpenAPI 3.0 Schema Required)

#### 1.1 Instruction Form Processing Endpoints

**POST /api/v1/cases/instructions/submit**
- Submit instruction form for processing (email attachment, web upload, portal submission)
- Request body:
  ```yaml
  InstructionSubmitRequest:
    type: object
    required: [submissionSource, formDocument]
    properties:
      submissionSource: string (EMAIL, WEB_FORM, INTRODUCER_PORTAL, DIGITAL_QUOTING, MANUAL_UPLOAD)
      formDocument:
        type: object
        properties:
          fileName: string
          fileType: string (PDF, DOCX, JPG, PNG)
          fileSize: number
          fileContent: string (base64 encoded)
          fileUrl: string (alternative to fileContent, if stored externally)
      leadId: string (if linked to existing lead from F002)
      referralCode: string (tick-match or introducer code)
      submittedBy: string (email or user_id)
  ```
- Response (202 Accepted):
  ```yaml
  InstructionSubmitResponse:
    type: object
    properties:
      instructionId: string (UUID)
      status: string (RECEIVED, QUEUED)
      estimatedProcessingTime: string (e.g., "5 minutes")
  ```

**GET /api/v1/cases/instructions/{instructionId}**
- Retrieve instruction processing status and extracted data
- Response includes: status, extracted data, confidence scores, flagged issues

**GET /api/v1/cases/instructions**
- Retrieve all instructions with filters: `?status=IN_PROGRESS&submissionSource=EMAIL&dateFrom=xxx`
- For New Business team inbox management

**PATCH /api/v1/cases/instructions/{instructionId}**
- Manually correct extracted data (for low confidence or errors)
- Request body: Partial InstructionData object

**POST /api/v1/cases/instructions/{instructionId}/reprocess**
- Re-run data extraction with manual corrections or improved OCR

**DELETE /api/v1/cases/instructions/{instructionId}**
- Delete instruction (for duplicates or invalid submissions)

#### 1.2 Data Extraction Endpoints

**POST /api/v1/cases/instructions/{instructionId}/extract**
- Trigger data extraction from instruction form (internal, called automatically)
- Uses Azure AI Document Intelligence or similar OCR service
- Response: Extracted data with confidence scores

**GET /api/v1/cases/instructions/{instructionId}/extraction-report**
- Retrieve detailed extraction report (fields extracted, confidence scores, flagged issues)

#### 1.3 Client & Company Management Endpoints

**POST /api/v1/cases/clients/search-duplicates**
- Search for duplicate clients in CRM
- Request body:
  ```yaml
  DuplicateSearchRequest:
    type: object
    properties:
      firstName: string
      lastName: string
      dateOfBirth: string (ISO date)
      email: string
      phone: string
      address: string
  ```
- Response: Array of potential duplicates with similarity scores

**POST /api/v1/cases/clients**
- Create new client record in CRM
- Request body: ClientData object
- Response (201 Created): Client record with CRM client ID

**PATCH /api/v1/cases/clients/{clientId}**
- Update existing client record

**POST /api/v1/cases/companies/search-duplicates**
- Search for duplicate companies in CRM
- Request body: Company name and address

**POST /api/v1/cases/companies**
- Create new company record in CRM (for corporate clients)

#### 1.4 Case Creation Endpoints

**POST /api/v1/cases**
- Create case in CMS
- Request body:
  ```yaml
  CaseCreateRequest:
    type: object
    required: [caseType, clientId, propertyAddress]
    properties:
      caseType: string (PURCHASE, SALE, REMORTGAGE)
      clientId: string (CRM client ID)
      propertyDetails:
        type: object
        properties:
          address: string
          postcode: string
          propertyValue: number
          propertyType: string
      parties:
        type: object
        properties:
          buyers: array of ClientData
          sellers: array of ClientData
          estateAgent: CompanyData
          mortgageBroker: CompanyData
      financial:
        type: object
        properties:
          purchasePrice: number
          depositAmount: number
          mortgageAmount: number
          cashAmount: number
      timeline:
        type: object
        properties:
          instructionDate: string (ISO date)
          targetCompletionDate: string (ISO date)
      referral:
        type: object
        properties:
          source: string
          referenceCode: string
          partnerName: string
          commissionRate: number
      serviceLevel: string (STANDARD, PREMIUM, EXPRESS)
      specialHandlingFlags: array of string (LEASEHOLD, NEW_BUILD, AUCTION, SHARED_OWNERSHIP)
      leadId: string (link to F002 lead if applicable)
      quoteId: string (link to accepted quote if applicable)
  ```
- Response (201 Created):
  ```yaml
  CaseCreateResponse:
    type: object
    properties:
      caseId: string (CMS case ID)
      caseReference: string (firm's case reference number)
      status: string (CREATED)
      clientCareLetterGenerated: boolean
      assignedSolicitor: string
  ```

**GET /api/v1/cases/{caseId}**
- Retrieve case details from CMS

**PATCH /api/v1/cases/{caseId}**
- Update case details

#### 1.5 Client Care Letter (CCL) Generation Endpoints

**POST /api/v1/cases/{caseId}/client-care-letter**
- Generate Client Care Letter for case
- Request body (optional): Template overrides, custom messages
- Response: CCL document (PDF), stored in document management system

**POST /api/v1/cases/{caseId}/client-care-letter/send**
- Send CCL to client via email
- Request body: `{ "recipientEmail": "string", "message": "string" }`
- Response: Email delivery status

**GET /api/v1/cases/{caseId}/client-care-letter**
- Retrieve generated CCL (PDF download)

#### 1.6 Legal Team Assignment Endpoints

**POST /api/v1/cases/{caseId}/assign**
- Assign case to legal team/solicitor
- Request body (optional for auto-assignment):
  ```yaml
  AssignmentRequest:
    type: object
    properties:
      solicitorId: string (manual assignment)
      teamId: string (manual team assignment)
      assignmentReason: string (MANUAL, AUTO, OVERRIDE)
  ```
- If no solicitor specified, uses assignment algorithm
- Response: Assignment details (solicitor, team, notification sent)

**GET /api/v1/cases/teams/workload**
- Retrieve team/solicitor workload for assignment algorithm
- Response: Array of team members with active case counts, capacity, specializations

**PATCH /api/v1/cases/teams/{teamId}/capacity**
- Update team capacity settings (for assignment algorithm)

#### 1.7 Inbox Management Endpoints

**GET /api/v1/cases/inbox**
- Retrieve New Business team inbox (all instructions grouped by status)
- Query: `?status=NEW,IN_PROGRESS,ERROR&sortBy=createdAt&order=desc`
- Response: Array of instruction items with summary data

**GET /api/v1/cases/inbox/metrics**
- Retrieve inbox metrics (SLA compliance, throughput, processing times)
- Response:
  ```yaml
  InboxMetrics:
    type: object
    properties:
      totalInstructions: number
      processingInProgress: number
      completedLast24Hours: number
      averageProcessingTime: string (e.g., "12 minutes")
      slaCompliance: number (percentage within 24 hour target)
      errorRate: number (percentage requiring manual intervention)
  ```

### 2. Data Processing Requirements

#### 2.1 Instruction Form Classification
Automatically classify form type using AI:
- Analyze form structure, fields, content
- Classify as: PURCHASE, SALE, REMORTGAGE, CUSTOM
- Confidence score for classification
- If confidence <70%, flag for manual review

#### 2.2 Data Extraction (OCR/AI)
Use Azure AI Document Intelligence or similar to extract:
- **Client Information:**
  - Full name (parse into firstName, lastName)
  - Date of birth (parse various formats)
  - Email address
  - Phone numbers (multiple)
  - Current address (parse into structured components)
- **Property Details:**
  - Property address (parse into line1, line2, city, county, postcode)
  - Property price/value
  - Tenure (Freehold, Leasehold)
  - Property type (Flat, House, Bungalow, etc.)
- **Transaction Type:**
  - Purchase, Sale, Remortgage
  - Purchase and Sale (simultaneous)
- **Related Parties:**
  - Estate agent (name, address, contact)
  - Mortgage broker (name, contact)
  - Co-buyers/sellers (names)
- **Timeline:**
  - Target completion date
  - Any key milestone dates
- **Referral Information:**
  - Source (introducer, estate agent, broker)
  - Tick-match code
  - Partner details
- **Financial Details:**
  - Deposit amount
  - Mortgage amount
  - Funding source

#### 2.3 Data Normalization
Normalize extracted data:
- **Phone Numbers:** E.164 format (+44...)
- **Postcodes:** Royal Mail PAF format (uppercase, space before last 3 chars)
- **Addresses:** Structure into line1, line2, city, county, postcode using Royal Mail PAF lookup
- **Dates:** Parse various formats (DD/MM/YYYY, MM-DD-YYYY, "15th December 2025") to ISO 8601
- **Currency:** Remove £, commas; convert to number
- **Names:** Title case, trim whitespace
- **Emails:** Lowercase, trim whitespace

#### 2.4 Data Validation
Validate extracted data against business rules:
- **Required Fields:** Client name, property address, transaction type
- **Email Format:** Regex validation
- **Phone Format:** UK phone regex
- **Postcode Format:** UK postcode regex
- **Date Validity:** Target completion date must be in future
- **Transaction Value:** Must be within serviceable range (e.g., £50k - £5M)
- **Service Area:** Postcode must be within firm's coverage area

Confidence scoring:
- Each extracted field has confidence score (0-100)
- If field confidence <70%, flag for manual review
- Overall form confidence = average of all field confidences
- If overall confidence <80%, route to manual review queue

#### 2.5 Client & Company Duplicate Detection
Search CRM for existing records before creating new:

**Client Duplicate Detection:**
- **Exact Match:** Email match OR (firstName + lastName + DOB match)
- **Fuzzy Match:** Name similarity >85% (Levenshtein distance) + address similarity >80%
- **Confidence Levels:**
  - >95% confidence: Likely duplicate, prompt to merge
  - 80-95%: Possible duplicate, show for review
  - <80%: Unlikely duplicate, proceed with creation

**Company Duplicate Detection:**
- Exact Match: Company name + postcode match
- Fuzzy Match: Company name similarity >90% + address similarity >85%

On duplicate detection:
- Flag instruction for manual review
- Display potential duplicate records side-by-side
- Allow user to merge with existing or create new (with justification)

#### 2.6 Case Creation Logic
Create case in CMS with:
- **Case Reference Number:** Auto-generated (firm's numbering scheme, e.g., "CONV-2025-12345")
- **Case Type & Sub-Type:** Purchase/Sale/Remortgage + Freehold/Leasehold/New Build/etc.
- **Client & Property Details:** All extracted and normalized data
- **Financial Summary:** Purchase price, deposit, mortgage, fees
- **Timeline:** Instruction date, target completion, key milestones
- **Fee Structure:** Based on service level and transaction value
- **Billing Arrangements:** Fixed fee, hourly, capped fee
- **Service Level:** Standard, Premium, Express
- **Special Handling Flags:** Leasehold, New Build, Auction, Shared Ownership, First-Time Buyer
- **Referral Details:** Source, tick-match code, commission rate
- **Document Attachments:** Original instruction form, supporting documents
- **Linked Records:** Lead ID (if from F002), Quote ID (if from quote acceptance)

#### 2.7 Client Care Letter (CCL) Generation
Generate CCL using template with merged data:
- **Template Selection:** Based on case type (purchase/sale/remortgage)
- **Data Merge:** Populate template with client name, case reference, solicitor name, fees, etc.
- **Content Sections:**
  - Welcome message with case reference
  - Assigned solicitor/case handler contact details
  - Service description and scope
  - Fee breakdown (itemized)
  - Payment terms and schedule
  - Expected timeline and milestones
  - Document checklist (what client needs to provide)
  - Terms of business
  - Privacy policy
  - Contact information and support channels
- **Rendering:** Generate PDF with firm branding (logo, colors, fonts)
- **Storage:** Store in document management system, linked to case
- **Delivery:** Email to client with tracked delivery

#### 2.8 Legal Team Assignment Algorithm
Assign case to solicitor/team based on:
1. **Case Type:** Purchase, Sale, Remortgage (teams may specialize)
2. **Transaction Complexity:**
   - Simple (Freehold, no chain): Junior/standard team
   - Complex (Leasehold, new build, auction): Senior/specialist team
3. **Team Capacity:** Current active case count per team member
4. **Holiday/Absence Schedule:** Exclude team members on leave
5. **Geographic Specialization:** If configured, prefer team members with postcode area expertise
6. **Service Level:** Premium/Express cases to experienced handlers
7. **Client Type:** Returning clients to previous handler (if available)

**Assignment Algorithm:**
1. Filter available solicitors (status=AVAILABLE, not on leave)
2. Filter by specialization (case type, complexity, geographic)
3. Calculate workload score = active_cases / capacity_limit
4. Assign to solicitor with lowest workload score
5. If workload equal, use round-robin (last assigned timestamp)

**Manual Override:**
- Allow New Business team to manually assign to specific solicitor
- Require justification for manual assignment

**Post-Assignment Actions:**
- Update case status to "Assigned - Awaiting Documentation"
- Send email notification to assigned solicitor with case summary
- Create initial task list for solicitor (onboarding checklist)
- Send welcome email to client from assigned solicitor

### 3. Data Storage (CosmosDB)

#### 3.1 Instructions Container
Schema:
```json
{
  "id": "uuid",
  "partitionKey": "submissionSource",
  "instructionId": "uuid",
  "submissionSource": "EMAIL | WEB_FORM | INTRODUCER_PORTAL | DIGITAL_QUOTING | MANUAL_UPLOAD",
  "status": "RECEIVED | QUEUED | PROCESSING | EXTRACTED | VALIDATED | COMPLETED | ERROR | MANUAL_REVIEW",
  "formDocument": {
    "fileName": "string",
    "fileType": "string",
    "fileSize": "number",
    "fileUrl": "string (blob storage URL)",
    "fileHash": "string (for deduplication)"
  },
  "formClassification": {
    "type": "PURCHASE | SALE | REMORTGAGE | CUSTOM",
    "confidence": "number (0-100)",
    "requiresReview": "boolean"
  },
  "extractedData": {
    "clients": [
      {
        "firstName": "string",
        "lastName": "string",
        "dateOfBirth": "ISO date",
        "email": "string",
        "phone": "string",
        "address": "object",
        "confidenceScore": "number"
      }
    ],
    "propertyDetails": {
      "address": "string",
      "addressComponents": "object",
      "price": "number",
      "tenure": "string",
      "propertyType": "string",
      "confidenceScore": "number"
    },
    "transactionType": "string",
    "relatedParties": "object",
    "timeline": "object",
    "referral": "object",
    "financial": "object",
    "overallConfidence": "number"
  },
  "validationResults": {
    "allRequiredFieldsPresent": "boolean",
    "emailValid": "boolean",
    "phoneValid": "boolean",
    "postcodeValid": "boolean",
    "dateValid": "boolean",
    "transactionValueValid": "boolean",
    "serviceAreaValid": "boolean",
    "validationErrors": ["string"]
  },
  "duplicateCheck": {
    "clientDuplicatesFound": ["object (CRM client IDs + similarity scores)"],
    "companyDuplicatesFound": ["object"],
    "requiresReview": "boolean"
  },
  "caseId": "string (after case creation)",
  "caseReference": "string",
  "leadId": "string (if linked to F002 lead)",
  "quoteId": "string (if from quote acceptance)",
  "processingStartedAt": "datetime",
  "processingCompletedAt": "datetime",
  "processingDuration": "number (seconds)",
  "manualReviewRequired": "boolean",
  "manualReviewReason": "string",
  "reviewedBy": "string (user_id)",
  "reviewedAt": "datetime",
  "createdAt": "datetime",
  "updatedAt": "datetime"
}
```

#### 3.2 Cases Container (CMS Mirror/Cache)
Store case summary for quick access (full case data in CMS):
```json
{
  "id": "uuid",
  "partitionKey": "caseReference",
  "caseId": "string (CMS case ID)",
  "caseReference": "string",
  "caseType": "PURCHASE | SALE | REMORTGAGE",
  "status": "CREATED | ASSIGNED | ONBOARDING | ACTIVE | COMPLETED | CLOSED",
  "clientId": "string (CRM client ID)",
  "clientName": "string",
  "propertyAddress": "string",
  "propertyValue": "number",
  "targetCompletionDate": "ISO date",
  "serviceLevel": "STANDARD | PREMIUM | EXPRESS",
  "assignedSolicitor": "string (solicitor_id)",
  "assignedTeam": "string (team_id)",
  "assignedAt": "datetime",
  "cclGenerated": "boolean",
  "cclSent": "boolean",
  "instructionId": "string (link to instruction)",
  "leadId": "string (link to F002 lead)",
  "quoteId": "string (link to accepted quote)",
  "createdAt": "datetime",
  "updatedAt": "datetime"
}
```

### 4. Integration Requirements

#### 4.1 OCR/AI Document Intelligence Integration
- **Azure AI Document Intelligence (recommended):**
  - Use Form Recognizer for structured forms
  - Use Document Intelligence for unstructured documents
  - Custom model training for firm-specific form templates
- **Alternative:** AWS Textract, Google Cloud Vision API
- Implement retry logic for transient failures
- Store extraction confidence scores for audit

#### 4.2 CMS (Case Management System) Integration
- RESTful API or SOAP integration (depends on CMS)
- **Key Operations:**
  - Create case with all details
  - Retrieve case details
  - Update case status
  - Attach documents to case
  - Assign case to solicitor
- **CMS Systems (examples):** Eclipse, Proclaim, Leap, Osprey, InfoTrack
- Handle CMS downtime gracefully: Queue case creation requests, retry with exponential backoff

#### 4.3 CRM Integration
- Create/update client and company records
- Search for duplicate records
- Link case to client opportunity (if from F002 lead)
- Update opportunity status to "Case Created"

#### 4.4 Document Management System Integration
- Store original instruction forms
- Store generated CCL PDFs
- Retrieve documents by case ID or instruction ID
- Support Azure Blob Storage, SharePoint, or firm's DMS

#### 4.5 Email Service Integration
- Send CCL to clients
- Send assignment notifications to solicitors
- Send welcome emails from assigned solicitor
- Track email delivery and opens

#### 4.6 Address Validation Service
- Royal Mail Postcode Address File (PAF) for UK addresses
- Validate and normalize postcodes
- Parse unstructured addresses into components

### 5. Business Logic & Validation

#### 5.1 Error Handling
- **Extraction Failures:** If OCR fails, flag for manual processing
- **Validation Failures:** List all validation errors, allow correction
- **CRM Failures:** Retry 3 times with exponential backoff, then queue for manual retry
- **CMS Failures:** Critical path; retry indefinitely until success, alert team after 1 hour
- **Duplicate Conflicts:** Never create duplicate client/company without explicit user override

#### 5.2 SLA Tracking
- Target: Process instruction to case creation within 24 hours
- Track processing time for each instruction
- Flag instructions exceeding 12 hours for escalation
- Dashboard metrics: % within SLA, average processing time

#### 5.3 Audit Trail
Log all actions:
- Instruction received (timestamp, source, submitted by)
- Extraction completed (timestamp, confidence scores)
- Validation results (passed/failed, errors)
- Duplicate check results
- Client/company created (CRM IDs)
- Case created (CMS case ID, reference)
- CCL generated and sent
- Case assigned (solicitor, timestamp)
- Manual interventions (user, action, timestamp)

## Acceptance Criteria

1. **Instruction Form Processing:**
   - [ ] Instructions received via email, web form, introducer portal
   - [ ] Forms classified automatically (PURCHASE, SALE, REMORTGAGE)
   - [ ] Classification confidence >90% for standard forms
   - [ ] Low confidence forms flagged for manual review

2. **Data Extraction:**
   - [ ] All required fields extracted from typed forms (>95% accuracy)
   - [ ] Handwritten forms extracted with >85% accuracy
   - [ ] Confidence scores calculated for each field
   - [ ] Low confidence fields flagged for manual verification
   - [ ] Overall form confidence >80% for auto-processing

3. **Data Normalization & Validation:**
   - [ ] Phone numbers normalized to E.164 format
   - [ ] Postcodes normalized to Royal Mail PAF format
   - [ ] Addresses parsed into structured components
   - [ ] Dates parsed to ISO 8601
   - [ ] Validation rules enforced (required fields, formats, business rules)
   - [ ] Validation errors listed clearly for correction

4. **Duplicate Detection:**
   - [ ] Client duplicates detected by email, name+DOB
   - [ ] Fuzzy matching identifies similar names/addresses (>85% similarity)
   - [ ] Potential duplicates flagged with confidence scores
   - [ ] User can merge with existing or create new with justification
   - [ ] Duplicate rate <2% after detection

5. **CRM Integration:**
   - [ ] New client records created in CRM
   - [ ] New company records created in CRM
   - [ ] Duplicate search queries CRM before creation
   - [ ] All party records linked with appropriate relationships

6. **Case Creation:**
   - [ ] Cases created in CMS with all extracted data
   - [ ] Case reference numbers generated per firm's scheme
   - [ ] Case type, sub-type, and special handling flags set correctly
   - [ ] Original instruction form attached to case
   - [ ] Linked to lead and quote (if applicable)
   - [ ] Case creation completes within 30 seconds

7. **Client Care Letter:**
   - [ ] CCL generated automatically upon case creation
   - [ ] CCL populated with correct client, case, and solicitor details
   - [ ] Fee breakdown itemized correctly
   - [ ] PDF rendered with firm branding
   - [ ] CCL sent to client via email within 1 hour of case creation
   - [ ] Email delivery tracked

8. **Legal Team Assignment:**
   - [ ] Cases assigned based on case type, complexity, workload
   - [ ] Assignment algorithm balances workload across team
   - [ ] Manual assignment override available
   - [ ] Assigned solicitor notified via email immediately
   - [ ] Initial task list created for solicitor

9. **Inbox Management:**
   - [ ] New Business team can view all instructions by status
   - [ ] Filters work (status, source, date range)
   - [ ] Manual corrections can be made to extracted data
   - [ ] Instructions can be reprocessed after corrections
   - [ ] Metrics displayed (throughput, SLA compliance, error rate)

10. **Performance & Quality:**
    - [ ] Instruction processing completes within 5 minutes (extraction to validation)
    - [ ] End-to-end processing (instruction to case assignment) completes within 24 hours
    - [ ] Automation rate ≥80% (no manual intervention required)
    - [ ] Data extraction accuracy ≥95% for required fields
    - [ ] Unit tests achieve ≥85% coverage
    - [ ] Integration tests pass for all critical flows

## Testing Requirements

### Unit Tests (≥85% Coverage)
- Form classification logic
- Data extraction parsing and normalization
- Validation rule enforcement
- Duplicate detection algorithms (exact and fuzzy matching)
- Assignment algorithm (workload balancing, specialization matching)
- CCL template rendering

### Integration Tests
- End-to-end instruction processing: Submit form → Extract → Validate → Create client → Create case → Generate CCL → Assign solicitor
- OCR service integration: Submit form → Extract data → Verify accuracy
- CMS integration: Create case → Verify case in CMS
- CRM integration: Create client → Search duplicate → Verify in CRM
- Email service: Send CCL → Verify delivery

### API Contract Tests
- OpenAPI schema validation for all endpoints

### Performance Tests
- Process 100 instructions concurrently, verify <5 minute average processing time
- Create 50 cases concurrently in CMS, verify <30 second average

### Test Scenarios
1. **Standard Form (Typed):** Submit typed purchase form → Verify >95% extraction accuracy → Verify case created
2. **Handwritten Form:** Submit scanned handwritten form → Verify >85% extraction accuracy → Flag low confidence fields for review
3. **Duplicate Client:** Submit instruction with existing client email → Verify duplicate detected → Prompt to merge
4. **CMS Failure:** Submit instruction when CMS down → Verify retry logic → Verify case created after CMS restored
5. **Manual Correction:** Submit form with extraction error → Manually correct → Reprocess → Verify case created with corrected data

## Dependencies

### Upstream Dependencies
- **Task 001:** Backend scaffolding
- **Task 007:** Sales Lead Backend (quote acceptance triggers instruction submission)

### Downstream Dependencies
- **OCR/AI Service:** Azure AI Document Intelligence or alternative
- **CMS:** API access for case creation (Eclipse, Proclaim, Leap, etc.)
- **CRM:** API access for client/company management
- **Document Management System:** Azure Blob Storage, SharePoint, or DMS API
- **Email Service:** For CCL delivery and notifications
- **Address Validation Service:** Royal Mail PAF or Postcode.io

### External Services
- **Azure AI Document Intelligence:** For OCR/data extraction
- **CMS API:** For case creation
- **CRM API:** For client/company records
- **Royal Mail PAF:** For address validation and normalization

## Technical Decisions Required

1. **OCR Service Selection:** Azure AI Document Intelligence, AWS Textract, Google Vision API?
2. **Custom Model Training:** Train custom OCR model for firm-specific forms?
3. **CMS Integration:** Which CMS platform? RESTful or SOAP?
4. **Duplicate Detection Threshold:** Similarity percentage for flagging duplicates?
5. **Assignment Algorithm Complexity:** Simple round-robin or advanced workload + specialization?
6. **CCL Template Engine:** Handlebars, Jinja2, or CMS built-in?
7. **PDF Generation:** Puppeteer, wkhtmltopdf, or CMS built-in?

## Notes

- Data extraction accuracy is critical; invest in OCR training and validation
- Duplicate detection prevents CRM pollution; prioritize accuracy
- CMS integration is the critical path; implement robust retry and error handling
- CCL generation and delivery must be reliable for regulatory compliance
- Future enhancement: AI-powered data extraction quality improvement over time
- Future enhancement: Predictive timeline estimation based on historical case data

## Definition of Done

- [ ] All API endpoints implemented with OpenAPI specification
- [ ] Instruction form processing workflow implemented
- [ ] OCR/AI data extraction integrated
- [ ] Data normalization and validation implemented
- [ ] Duplicate detection implemented (exact and fuzzy matching)
- [ ] CRM integration implemented (client/company creation, duplicate search)
- [ ] CMS integration implemented (case creation)
- [ ] CCL generation implemented with template rendering
- [ ] Email service integration implemented (CCL delivery, notifications)
- [ ] Legal team assignment algorithm implemented
- [ ] Inbox management endpoints implemented
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
