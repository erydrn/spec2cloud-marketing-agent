# Technical Task: Client Onboarding - Backend

**GitHub Issue:** [#12](https://github.com/erydrn/spec2cloud-marketing-agent/issues/12)

## Task Information

**Task ID:** 011  
**Task Name:** Client Onboarding - Backend API  
**Feature:** F004 - Client Onboarding  
**Priority:** Critical  
**Complexity:** Very High  
**Estimated Effort:** 12-15 days  
**Dependencies:**
- Task 001: Backend Scaffolding
- Task 009: Case Creation Backend (cases created in F003 flow to F004)
- ID verification service API access
- AML screening service API access

## Description

Build backend API services for automated client onboarding including CCL receipt tracking, client questionnaire delivery and completion, identity verification, AML checks, source of funds verification, and bank Terms of Business review. This service ensures compliance and readiness before legal work begins.

## Technical Requirements

### 1. API Endpoints (OpenAPI 3.0 Schema Required)

#### 1.1 Onboarding Management Endpoints

**POST /api/v1/onboarding/{caseId}/initiate**
- Initiate onboarding process for case
- Triggers CCL delivery, questionnaire delivery, client portal provisioning
- Response: Onboarding checklist with initial status

**GET /api/v1/onboarding/{caseId}**
- Retrieve onboarding status and checklist
- Response includes: CCL status, questionnaire status, ID verification status, AML status, source of funds status, TOB review status, completion percentage

**GET /api/v1/onboarding/cases-pending**
- Retrieve cases pending onboarding completion
- Filters: `?stage=ID_VERIFICATION&daysOpen>5&assignedSolicitor=xxx`

#### 1.2 Client Care Letter Endpoints

**POST /api/v1/onboarding/{caseId}/ccl/track-receipt**
- Track CCL receipt (electronic signature, email confirmation)
- Request body: `{ "receiptMethod": "ESIGNATURE|EMAIL", "signatureData": "base64", "receiptDate": "ISO datetime" }`
- Updates case status to "CCL Signed"

**GET /api/v1/onboarding/{caseId}/ccl/status**
- Check CCL delivery and receipt status
- Response: `{ "sent": boolean, "sentAt": datetime, "viewed": boolean, "viewedAt": datetime, "signed": boolean, "signedAt": datetime }`

#### 1.3 Client Questionnaire Endpoints

**POST /api/v1/onboarding/{caseId}/questionnaire/deliver**
- Send questionnaire to client via email with portal link
- Request body: `{ "questionnaireType": "PURCHASE|SALE|REMORTGAGE", "recipientEmail": "string" }`

**GET /api/v1/onboarding/{caseId}/questionnaire**
- Retrieve questionnaire questions and answers
- Response: Questionnaire object with questions array and completion status

**POST /api/v1/onboarding/{caseId}/questionnaire/submit**
- Submit completed questionnaire (by client via portal or agent on behalf)
- Request body: `{ "answers": { "questionId": "answer" }, "completedBy": "CLIENT|AGENT" }`
- Triggers data extraction and case update

**PATCH /api/v1/onboarding/{caseId}/questionnaire/answers/{questionId}**
- Update specific questionnaire answer

#### 1.4 Identity Verification Endpoints

**POST /api/v1/onboarding/{caseId}/identity/upload-documents**
- Upload ID documents (passport, driver's license, proof of address)
- Request body:
  ```yaml
  IDUploadRequest:
    type: object
    properties:
      documentType: string (PASSPORT, DRIVERS_LICENSE, PROOF_OF_ADDRESS, NATIONAL_INSURANCE)
      documentFile: string (base64 encoded)
      clientId: string (for joint buyers/sellers, multiple uploads)
  ```

**POST /api/v1/onboarding/{caseId}/identity/verify**
- Trigger automated ID verification (OCR extraction + authenticity check)
- Uses ID verification service (Onfido, Yoti, etc.)
- Response: Verification result with confidence score

**GET /api/v1/onboarding/{caseId}/identity/status**
- Retrieve ID verification status for all clients
- Response: Array of client ID verification results with status (PENDING, VERIFIED, FAILED, MANUAL_REVIEW)

**POST /api/v1/onboarding/{caseId}/identity/manual-review**
- Submit manual review decision for low-confidence verification
- Request body: `{ "clientId": "string", "decision": "APPROVED|REJECTED", "reviewerComments": "string" }`

#### 1.5 AML Check Endpoints

**POST /api/v1/onboarding/{caseId}/aml/run-checks**
- Run AML checks for all clients in case
- Checks: Sanctions lists (OFAC, UN, EU, UK), PEP screening, adverse media search
- Response: AML check results with risk scores

**GET /api/v1/onboarding/{caseId}/aml/results**
- Retrieve AML check results
- Response:
  ```yaml
  AMLResults:
    type: object
    properties:
      overallRiskScore: number (0-100)
      riskCategory: string (LOW, MEDIUM, HIGH)
      sanctionsHits: array (list of matches)
      pepHits: array
      adverseMediaHits: array
      requiresMLROReview: boolean
      mlroDecision: string (APPROVED, REJECTED, PENDING)
      checksCompletedAt: datetime
  ```

**POST /api/v1/onboarding/{caseId}/aml/mlro-review**
- Submit MLRO review decision for high-risk cases
- Request body: `{ "decision": "APPROVED|REJECTED|REQUEST_ENHANCED_DD", "comments": "string", "conditions": ["string"] }`

#### 1.6 Source of Funds Endpoints

**POST /api/v1/onboarding/{caseId}/source-of-funds/declare**
- Client declares source of funds
- Request body:
  ```yaml
  SourceOfFundsRequest:
    type: object
    properties:
      sources:
        type: array
        items:
          type: object
          properties:
            sourceType: string (SAVINGS, PROPERTY_SALE, MORTGAGE, GIFT, INHERITANCE, BUSINESS_PROCEEDS, OTHER)
            amount: number
            supportingDocuments: array of string (base64 files)
            additionalInfo: string
  ```

**POST /api/v1/onboarding/{caseId}/source-of-funds/verify**
- Verify declared source of funds against uploaded documents
- Validation rules: Documents match amounts, dates current (<6 months), gift donors verified
- Response: Verification result (VERIFIED, ADDITIONAL_INFO_REQUIRED, REJECTED)

**GET /api/v1/onboarding/{caseId}/source-of-funds/status**
- Retrieve source of funds verification status

#### 1.7 Bank Terms of Business (TOB) Endpoints

**POST /api/v1/onboarding/{caseId}/bank-tob/retrieve**
- Retrieve lender's Terms of Business
- Request body: `{ "lenderName": "string" }`
- Checks internal library for approved TOB, retrieves if available

**POST /api/v1/onboarding/{caseId}/bank-tob/review**
- Submit TOB review (auto-accept if standard, flag if non-standard)
- Request body: `{ "tobType": "STANDARD|NON_STANDARD", "reviewRequired": boolean, "solicitorNotes": "string" }`

**POST /api/v1/onboarding/{caseId}/bank-tob/approve**
- Approve non-standard TOB (requires solicitor authorization)
- Request body: `{ "approvedBy": "solicitor_id", "approvalNotes": "string" }`

#### 1.8 Onboarding Completion Endpoints

**POST /api/v1/onboarding/{caseId}/complete**
- Mark onboarding as complete (all checklist items done)
- Validates all requirements met
- Updates case status to "Onboarding Complete - Ready for Legal Work"
- Creates initial task list for conveyancing work

**GET /api/v1/onboarding/dashboard**
- Retrieve onboarding dashboard metrics
- Response: Cases by onboarding stage, average onboarding time, compliance pass rates, blockers

### 2. Data Processing Requirements

#### 2.1 CCL Receipt Tracking
- Track CCL delivery via email service (sent, viewed timestamps)
- Capture electronic signature or email confirmation reply
- Validate signature date and authenticity
- Store signed CCL in document management system
- Flag CCL as "pending" if not signed within 5 business days, send reminder

#### 2.2 Questionnaire Processing
- Deliver questionnaire based on case type (PURCHASE, SALE, REMORTGAGE)
- Questions dynamically generated based on case details
- Track completion percentage (questions answered / total questions)
- Extract key information from answers to update case record:
  - Personal circumstances (employment, residency status)
  - Property-specific info (fixtures, utilities, occupancy)
  - Financial source confirmation
  - Special requests or concerns
- Flag incomplete questionnaires after 7 days, send reminder

#### 2.3 Identity Verification Logic
- Use ID verification service API (Onfido, Yoti, etc.)
- **Document OCR:** Extract name, DOB, document number from ID
- **Face Matching:** Match photo ID to selfie (optional)
- **Authenticity Check:** Validate security features, expiry date
- **Data Consistency:** Match ID data to client record
- **Confidence Scoring:** 0-100 score for verification quality
- **Thresholds:**
  - >85%: Auto-approve
  - 70-85%: Manual review by compliance team
  - <70%: Request re-upload or alternative ID

#### 2.4 AML Check Logic
- Integrate with AML screening service (Dow Jones, Refinitiv, ComplyAdvantage)
- **Checks Performed:**
  - Sanctions lists: OFAC, UN, EU, UK HMT
  - PEP screening: Politically Exposed Persons
  - Adverse media: Criminal convictions, financial crime
  - Credit reference (optional)
  - Company registry search for corporate clients
- **Risk Scoring Algorithm:**
  - Base score from client profile (age, occupation, location)
  - +20 points for each sanctions hit
  - +15 points for PEP status
  - +10 points for adverse media hit
  - +10 points for high transaction value (>£1M)
  - +5 points for cash payment >£10k
  - +5 points for complex structure (multiple parties, offshore entities)
- **Risk Categories:**
  - Low (<30): Auto-approve, standard monitoring
  - Medium (30-70): Flag for solicitor review, enhanced monitoring
  - High (>70): Escalate to MLRO, senior approval required, refuse if unmitigable
- **False Positive Handling:** Allow user to mark hits as false positives with justification

#### 2.5 Source of Funds Verification Logic
- **Validation Rules:**
  - Total declared sources ≥ (deposit + fees)
  - Each source has supporting documents uploaded
  - Documents dated within 6 months (bank statements, completion statements)
  - Gift amounts match donor bank statements
  - Gift letters signed and dated
  - Donor identity verified for gifts >£10k
- **Gift Verification (Enhanced):**
  - Donor relationship to client confirmed
  - Donor ID and address verified
  - Donor bank statement shows funds available
  - Gift letter confirms not a loan (irrevocable gift)

#### 2.6 Bank TOB Review Logic
- Maintain internal library of approved standard TOBs from major lenders
- Auto-accept if lender TOB matches approved standard
- Flag non-standard TOBs for solicitor review:
  - Unusual retention clauses
  - Clawback provisions
  - Additional fees or charges
  - Non-standard indemnities required
- Solicitor must approve non-standard TOB before proceeding

#### 2.7 Onboarding Completion Logic
- **Checklist Requirements:**
  - [ ] Signed CCL received
  - [ ] Client questionnaire completed (100%)
  - [ ] ID documents verified (all clients, confidence >85%)
  - [ ] AML checks passed (risk category LOW or MEDIUM with approval)
  - [ ] Source of funds verified (total sources ≥ required amount)
  - [ ] Bank TOB reviewed (if mortgage case)
- **Completion Trigger:**
  - When all items checked, automatically update case status
  - Notify assigned solicitor via email
  - Create initial task list for conveyancing work based on case type
  - Set expected milestone dates (exchange, completion)

### 3. Data Storage (CosmosDB)

#### 3.1 Onboarding Container
Schema:
```json
{
  "id": "uuid",
  "partitionKey": "caseId",
  "caseId": "string",
  "onboardingStatus": "INITIATED | IN_PROGRESS | COMPLETED | BLOCKED",
  "completionPercentage": "number (0-100)",
  "checklist": {
    "cclSigned": "boolean",
    "questionnaireCompleted": "boolean",
    "idVerified": "boolean",
    "amlPassed": "boolean",
    "sourceOfFundsVerified": "boolean",
    "bankTobReviewed": "boolean"
  },
  "ccl": {
    "sent": "boolean",
    "sentAt": "datetime",
    "viewed": "boolean",
    "viewedAt": "datetime",
    "signed": "boolean",
    "signedAt": "datetime",
    "signatureData": "string"
  },
  "questionnaire": {
    "delivered": "boolean",
    "deliveredAt": "datetime",
    "completed": "boolean",
    "completedAt": "datetime",
    "completedBy": "CLIENT | AGENT",
    "answers": "object"
  },
  "identityVerification": [
    {
      "clientId": "string",
      "documentType": "PASSPORT | DRIVERS_LICENSE | PROOF_OF_ADDRESS",
      "uploadedAt": "datetime",
      "verificationStatus": "PENDING | VERIFIED | FAILED | MANUAL_REVIEW",
      "confidenceScore": "number",
      "verifiedAt": "datetime",
      "verificationService": "string (e.g., Onfido)",
      "extractedData": "object",
      "reviewedBy": "string (compliance_officer_id if manual review)"
    }
  ],
  "amlChecks": {
    "status": "PENDING | IN_PROGRESS | COMPLETED | FAILED",
    "overallRiskScore": "number (0-100)",
    "riskCategory": "LOW | MEDIUM | HIGH",
    "sanctionsHits": ["object"],
    "pepHits": ["object"],
    "adverseMediaHits": ["object"],
    "requiresMLROReview": "boolean",
    "mlroDecision": "APPROVED | REJECTED | PENDING",
    "mlroComments": "string",
    "checksCompletedAt": "datetime"
  },
  "sourceOfFunds": {
    "declared": "boolean",
    "declaredAt": "datetime",
    "sources": [
      {
        "sourceType": "SAVINGS | PROPERTY_SALE | MORTGAGE | GIFT | INHERITANCE | BUSINESS_PROCEEDS | OTHER",
        "amount": "number",
        "verified": "boolean",
        "verificationNotes": "string"
      }
    ],
    "totalDeclared": "number",
    "verified": "boolean",
    "verifiedAt": "datetime"
  },
  "bankTob": {
    "lenderName": "string",
    "tobType": "STANDARD | NON_STANDARD",
    "reviewed": "boolean",
    "reviewedAt": "datetime",
    "reviewedBy": "string (solicitor_id)",
    "approved": "boolean",
    "approvalNotes": "string"
  },
  "blockers": [
    {
      "blockerType": "string (e.g., LOW_CONFIDENCE_ID, HIGH_AML_RISK, INSUFFICIENT_FUNDS)",
      "blockerDescription": "string",
      "createdAt": "datetime",
      "resolvedAt": "datetime"
    }
  ],
  "initiatedAt": "datetime",
  "completedAt": "datetime",
  "onboardingDuration": "number (hours)"
}
```

### 4. Integration Requirements

#### 4.1 ID Verification Service Integration
- **Onfido (recommended):** REST API for document upload, verification, face matching
- **Yoti:** Alternative provider
- Implement webhook for async verification results
- Store verification reports in document management system

#### 4.2 AML Screening Service Integration
- **Dow Jones Risk & Compliance:** API for sanctions, PEP, adverse media screening
- **Refinitiv World-Check:** Alternative provider
- **ComplyAdvantage:** AI-powered screening
- Batch screening API for multiple clients
- Real-time alerts for ongoing monitoring (future enhancement)

#### 4.3 Email Service Integration
- Send questionnaires, reminders, completion confirmations
- Track email opens and link clicks

#### 4.4 Document Management System Integration
- Store signed CCL, ID documents, source of funds documents
- Retrieve documents by case ID

#### 4.5 CMS Integration
- Update case status when onboarding complete
- Create initial task list for solicitor

### 5. Business Logic & Validation

#### 5.1 Compliance Rules
- All clients in case must complete ID verification
- AML checks required for all clients
- Source of funds must total ≥ (deposit + fees + stamp duty)
- Gifts >£10k require donor verification
- High AML risk requires MLRO approval before proceeding

#### 5.2 SLA Tracking
- Target: Complete onboarding within 5 business days of case creation
- Track time spent at each stage
- Flag cases exceeding 3 days for escalation

## Acceptance Criteria

1. **Onboarding Initiation:**
   - [ ] Onboarding initiated automatically upon case creation
   - [ ] CCL delivered to client
   - [ ] Questionnaire delivered to client
   - [ ] Client portal access provisioned

2. **CCL Tracking:**
   - [ ] CCL delivery tracked (sent, viewed, signed)
   - [ ] Electronic signature captured and validated
   - [ ] Signed CCL stored in DMS
   - [ ] Reminder sent if not signed within 5 days

3. **Questionnaire:**
   - [ ] Correct questionnaire type delivered (PURCHASE, SALE, REMORTGAGE)
   - [ ] Questions dynamically generated
   - [ ] Completion tracked (percentage)
   - [ ] Answers extracted and update case record
   - [ ] Reminder sent if incomplete after 7 days

4. **Identity Verification:**
   - [ ] Documents uploaded successfully
   - [ ] Automated verification runs (OCR, authenticity, face matching)
   - [ ] Confidence scores calculated
   - [ ] Auto-approve if >85% confidence
   - [ ] Manual review queue for 70-85% confidence
   - [ ] Re-upload requested if <70% confidence

5. **AML Checks:**
   - [ ] Sanctions, PEP, adverse media screening performed
   - [ ] Risk score calculated correctly
   - [ ] Low risk (<30) auto-approved
   - [ ] Medium risk (30-70) flagged for solicitor review
   - [ ] High risk (>70) escalated to MLRO
   - [ ] False positives manageable

6. **Source of Funds:**
   - [ ] Clients declare sources with documents
   - [ ] Validation rules enforced (amounts, dates, gift verification)
   - [ ] Total sources verified ≥ required amount
   - [ ] Gift donors verified for >£10k gifts

7. **Bank TOB:**
   - [ ] Lender TOB retrieved from library
   - [ ] Standard TOBs auto-accepted
   - [ ] Non-standard TOBs flagged for solicitor review
   - [ ] Solicitor approval captured

8. **Onboarding Completion:**
   - [ ] All checklist items validated before completion
   - [ ] Case status updated to "Onboarding Complete"
   - [ ] Solicitor notified
   - [ ] Initial task list created

9. **Performance & Compliance:**
   - [ ] ID verification completes within 60 seconds
   - [ ] AML checks complete within 2 minutes
   - [ ] End-to-end onboarding completes within 5 business days (>90% cases)
   - [ ] Unit tests ≥85% coverage
   - [ ] Integration tests pass

## Testing Requirements

### Unit Tests (≥85% Coverage)
- CCL receipt validation
- Questionnaire answer extraction
- ID verification confidence scoring
- AML risk scoring algorithm
- Source of funds validation rules
- TOB review logic
- Onboarding completion checklist validation

### Integration Tests
- End-to-end onboarding: Initiate → CCL signed → Questionnaire completed → ID verified → AML passed → Source of funds verified → TOB reviewed → Complete
- ID verification service: Upload document → Verify → Verify result returned
- AML screening service: Run checks → Verify risk score calculated
- Email service: Send questionnaire → Track delivery

### Test Scenarios
1. **Happy Path:** All onboarding steps complete successfully within 3 days
2. **Low Confidence ID:** ID verification returns <85% confidence → Manual review required
3. **High AML Risk:** AML check returns risk score 75 → MLRO review required
4. **Insufficient Source of Funds:** Declared sources < required → Additional documents requested
5. **Non-Standard TOB:** Lender TOB not in library → Solicitor review required

## Dependencies

### Upstream Dependencies
- **Task 001:** Backend scaffolding
- **Task 009:** Case Creation Backend (cases flow from F003)

### Downstream Dependencies
- **ID Verification Service:** Onfido, Yoti, or alternative
- **AML Screening Service:** Dow Jones, Refinitiv, ComplyAdvantage
- **Email Service:** For questionnaire delivery, reminders
- **Document Management System:** For storing documents
- **CMS:** For case status updates

## Technical Decisions Required

1. **ID Verification Provider:** Onfido, Yoti, or other?
2. **AML Screening Provider:** Dow Jones, Refinitiv, ComplyAdvantage?
3. **Face Matching:** Mandatory or optional for ID verification?
4. **Enhanced Due Diligence Threshold:** What risk score triggers EDD?
5. **Gift Verification Threshold:** Above what amount require donor verification?

## Notes

- ID verification and AML compliance are legally critical; prioritize accuracy
- Source of funds verification prevents money laundering; thorough validation required
- Onboarding duration directly impacts time-to-completion; optimize for speed
- Future enhancement: Biometric verification (fingerprint, facial recognition)
- Future enhancement: Continuous AML monitoring during transaction
- Future enhancement: Integration with Open Banking for source of funds verification

## Definition of Done

- [ ] All API endpoints implemented with OpenAPI specification
- [ ] CCL tracking implemented
- [ ] Questionnaire delivery and processing implemented
- [ ] ID verification integration implemented (Onfido or similar)
- [ ] AML screening integration implemented (Dow Jones or similar)
- [ ] Source of funds verification implemented
- [ ] Bank TOB review implemented
- [ ] Onboarding completion logic implemented
- [ ] Email service integration implemented
- [ ] Document management integration implemented
- [ ] CMS integration implemented (status updates)
- [ ] Unit tests ≥85% coverage
- [ ] Integration tests passing
- [ ] Performance tests passing
- [ ] Code reviewed and approved
- [ ] Deployed to dev/staging and tested
- [ ] Security review completed (data encryption, PII handling)
- [ ] Compliance review completed (AML, GDPR)

---

**Document Version:** 1.0  
**Last Updated:** December 9, 2025  
**Author:** Technical Task Writer  
**Status:** Ready for Development
