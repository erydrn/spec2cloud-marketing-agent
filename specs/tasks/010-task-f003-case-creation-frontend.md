# Technical Task: Case Creation Automation - Frontend

**GitHub Issue:** [#11](https://github.com/erydrn/spec2cloud-marketing-agent/issues/11)

## Task Information

**Task ID:** 010  
**Task Name:** Case Creation Automation - Frontend UI  
**Feature:** F003 - Case Creation Automation  
**Priority:** Critical  
**Complexity:** High  
**Estimated Effort:** 8-10 days  
**Dependencies:**
- Task 002: Frontend Scaffolding
- Task 009: Case Creation Backend API (TypeScript SDK)

## Description

Build frontend UI for New Business Support team to manage instruction form inbox, review extracted data, correct errors, handle duplicate detection, and monitor case creation workflow. This interface provides visibility and control over the automated case creation process.

## Technical Requirements

### 1. UI Components & Pages

#### 1.1 Instruction Inbox Dashboard (`/cases/inbox`)
- **Inbox Overview Cards:**
  - Total Instructions (Today, This Week)
  - In Progress (count)
  - Completed Last 24 Hours
  - Requiring Manual Review (alert badge)
  - SLA Compliance (% within 24 hours)
  - Average Processing Time

- **Instruction List (Data Table):**
  - Columns: Instruction ID, Submission Source, Form Type, Status, Confidence Score, Submitted At, Processing Time, Actions
  - Status badges: RECEIVED, PROCESSING, EXTRACTED, MANUAL_REVIEW, COMPLETED, ERROR
  - Color-coded confidence scores: Green (>80%), Yellow (70-80%), Red (<70%)
  - Filters: Status, Source, Date Range, Confidence Score
  - Search: By client name, property address
  - Sort: By submission date, processing time, confidence score
  - Row click: Navigate to instruction detail page

- **Quick Actions:**
  - "Upload Form" button (manual upload)
  - "Bulk Actions" dropdown (reprocess selected, delete selected)
  - "Export List" button (CSV)

#### 1.2 Instruction Detail & Review Page (`/cases/inbox/[instructionId]`)
- **Header:**
  - Instruction ID, Status badge
  - Submission source, Submitted date
  - Overall confidence score (progress bar)
  - "Reprocess" button, "Approve & Create Case" button, "Delete" button

- **Original Document Viewer:**
  - Display original instruction form (PDF/image viewer)
  - Zoom controls, download button

- **Extracted Data Review (Side-by-Side Layout):**
  - Left: Original document viewer
  - Right: Extracted data form with editable fields

- **Extracted Data Form:**
  - **Client Information Section:**
    - First Name, Last Name (with confidence score badges)
    - Date of Birth
    - Email, Phone
    - Address (structured: line1, line2, city, county, postcode)
    - Each field shows confidence score, editable inline
    - Low confidence fields highlighted in yellow
  
  - **Property Details Section:**
    - Property Address (structured)
    - Property Value
    - Tenure (dropdown: Freehold, Leasehold)
    - Property Type (dropdown: Flat, House, Bungalow, etc.)
  
  - **Transaction Details Section:**
    - Transaction Type (dropdown: Purchase, Sale, Remortgage)
    - Target Completion Date (date picker)
    - Deposit Amount, Mortgage Amount
  
  - **Related Parties Section:**
    - Estate Agent (name, contact)
    - Mortgage Broker (name, contact)
    - Co-buyers/sellers (array input)
  
  - **Referral Information Section:**
    - Referral Source
    - Tick-Match Code
    - Partner Name

- **Validation Status:**
  - Checklist of validation results:
    - [ ] All required fields present
    - [ ] Email valid
    - [ ] Phone valid
    - [ ] Postcode valid
    - [ ] Date valid
    - [ ] Transaction value in range
    - [ ] Service area valid
  - Red X or Green checkmark for each

- **Duplicate Check Results:**
  - If duplicates found, show warning banner
  - List potential duplicate clients/companies with similarity scores
  - Side-by-side comparison view (extracted data vs. existing record)
  - Actions: "Merge with Existing" button, "Create New Anyway" button (with justification input)

- **Actions:**
  - "Save Corrections" button (updates extracted data)
  - "Approve & Create Case" button (triggers case creation)
  - "Flag for Review" button (escalate to supervisor)

#### 1.3 Case Creation Status Page (`/cases/[caseId]/creation-status`)
Real-time status view during case creation:
- **Progress Steps:**
  - [ ] Client record created in CRM
  - [ ] Company records created in CRM
  - [ ] Case created in CMS
  - [ ] Client Care Letter generated
  - [ ] CCL sent to client
  - [ ] Case assigned to solicitor
  - [ ] Solicitor notified

- **Each step shows:**
  - Status icon (pending, in progress, completed, error)
  - Timestamp when completed
  - Error message if failed

- **On Success:**
  - "Case Created Successfully!" message
  - Case reference number (large, prominent)
  - Link to case detail page
  - "Return to Inbox" button

- **On Error:**
  - Error message with details
  - "Retry" button
  - "Contact Support" button with correlation ID

#### 1.4 Manual Form Upload Page (`/cases/inbox/upload`)
For manually uploading instruction forms:
- **File Upload Widget:**
  - Drag-and-drop area
  - "Browse Files" button
  - Supported formats: PDF, DOCX, JPG, PNG
  - Max file size: 10 MB
  - Multiple file upload support

- **Form Metadata:**
  - Submission Source (dropdown: Manual Upload, Email, Web Form, etc.)
  - Lead ID (optional, link to existing lead from F002)
  - Referral Code (optional)
  - Notes (text area)

- **Actions:**
  - "Upload & Process" button
  - "Cancel" button

- **Upload Progress:**
  - Progress bar during upload
  - "Processing..." spinner after upload
  - Redirect to instruction detail page when extraction complete

#### 1.5 Duplicate Management Modal
When duplicate detected:
- **Modal Title:** "Potential Duplicate Detected"

- **Comparison View (Two Columns):**
  - Left: Extracted data from new instruction
  - Right: Existing CRM record
  - Highlight differences in yellow

- **Similarity Score:**
  - Overall similarity: XX%
  - Name similarity: XX%
  - Address similarity: XX%

- **Actions:**
  - "Merge with Existing" button (primary)
    - Dropdown: Which fields to update from new instruction
  - "Create New Record" button (secondary, warning style)
    - Requires justification text area

#### 1.6 Inbox Metrics Dashboard (`/cases/inbox/metrics`)
For New Business managers:
- **Processing Metrics:**
  - Total instructions processed (today, week, month)
  - Average processing time (chart over time)
  - SLA compliance % (target: 24 hours)
  - Automation rate (% processed without manual intervention)
  - Error rate (% requiring manual review)

- **Charts:**
  - Instructions by submission source (pie chart)
  - Processing time distribution (histogram)
  - Daily throughput (line chart, last 30 days)
  - Confidence score distribution (bar chart)

- **Team Performance:**
  - Table: Team member, Instructions processed, Average processing time, Error rate

### 2. API Integration (TypeScript SDK)

Use generated SDK from Task 009:

**Example: Fetch inbox**
```typescript
import { CasesApi } from '@/sdk';

const casesApi = new CasesApi(config);
const response = await casesApi.getInbox({ 
  status: 'MANUAL_REVIEW', 
  sortBy: 'createdAt', 
  order: 'desc' 
});
const instructions = response.data;
```

**Example: Update extracted data**
```typescript
await casesApi.updateInstruction({
  instructionId: instructionId,
  instructionData: {
    extractedData: {
      clients: [{ firstName: 'John', lastName: 'Smith', ... }],
      propertyDetails: { ... }
    }
  }
});
```

**Example: Create case**
```typescript
const caseResponse = await casesApi.createCase({
  caseCreateRequest: {
    caseType: 'PURCHASE',
    clientId: clientId,
    propertyDetails: { ... },
    // ...
  }
});
const caseId = caseResponse.data.caseId;
```

### 3. State Management

Use React Context or Zustand for:
- **Inbox State:** Instructions list, filters, pagination
- **Instruction Detail State:** Current instruction, extracted data (for inline editing)
- **Case Creation State:** Creation progress steps, status

### 4. UI/UX Requirements

#### 4.1 Side-by-Side Review Interface
- Split screen: Original document on left, extracted data on right
- Synchronize scrolling (optional)
- Highlight fields on document when focused in form

#### 4.2 Inline Editing
- Click field to edit
- Show confidence score badge next to each field
- Color code: Green (>80%), Yellow (70-80%), Red (<70%)
- Save button appears when edits made

#### 4.3 Real-Time Processing Updates
- Use WebSocket or polling to update case creation progress
- Animated progress indicators
- Toast notifications for completion/errors

#### 4.4 Error Handling
- Display validation errors inline (below fields)
- Show CRM/CMS integration errors with retry options
- Provide clear error messages with actionable steps

### 5. Testing Requirements

#### Unit Tests (≥85% Coverage)
- Inbox filtering and sorting logic
- Extracted data form validation
- Duplicate comparison logic
- Case creation progress tracking

#### Integration Tests
- Fetch inbox → Display instructions → Filter by status → Verify filtered list
- View instruction → Edit field → Save → Verify updated
- Approve instruction → Trigger case creation → Verify case created

#### E2E Tests
1. **Instruction Review Flow:** User opens instruction → Reviews extracted data → Corrects field → Saves → Approves → Case created
2. **Duplicate Handling:** User opens instruction → Duplicate detected → Compares records → Merges with existing → Case created
3. **Manual Upload:** User uploads form → Processing completes → Reviews data → Approves → Case created

## Acceptance Criteria

1. **Inbox Dashboard:**
   - [ ] Metrics cards display accurate counts
   - [ ] Instruction list displays with correct columns
   - [ ] Filters work (status, source, date range)
   - [ ] Search filters instructions by name, address
   - [ ] Row click navigates to detail page

2. **Instruction Detail & Review:**
   - [ ] Original document displays correctly (PDF/image viewer)
   - [ ] Extracted data form displays with all fields
   - [ ] Confidence scores displayed for each field
   - [ ] Low confidence fields highlighted
   - [ ] Fields editable inline
   - [ ] Validation status checklist accurate
   - [ ] Duplicate check results displayed if found
   - [ ] Save corrections updates data
   - [ ] Approve triggers case creation

3. **Duplicate Management:**
   - [ ] Duplicate warning banner displayed when detected
   - [ ] Comparison modal shows side-by-side view
   - [ ] Similarity scores displayed
   - [ ] Merge action updates existing record
   - [ ] Create new action requires justification

4. **Case Creation Status:**
   - [ ] Progress steps display in real-time
   - [ ] Each step shows status and timestamp
   - [ ] Success message with case reference displayed
   - [ ] Error messages clear with retry option

5. **Manual Upload:**
   - [ ] File upload widget accepts supported formats
   - [ ] Upload progress displayed
   - [ ] Processing starts automatically after upload
   - [ ] Redirects to detail page when complete

6. **Inbox Metrics:**
   - [ ] Processing metrics accurate
   - [ ] Charts render correctly
   - [ ] Team performance table displays

7. **API Integration:**
   - [ ] All API calls use generated SDK
   - [ ] Error handling displays user-friendly messages
   - [ ] Loading states displayed

8. **Testing:**
   - [ ] Unit tests ≥85% coverage
   - [ ] Integration tests pass
   - [ ] E2E tests pass

## Dependencies

### Upstream Dependencies
- **Task 002:** Frontend scaffolding
- **Task 009:** Case Creation Backend API (TypeScript SDK)

### External Dependencies
- **PDF Viewer Component:** react-pdf or similar
- **Form Library:** react-hook-form
- **File Upload Component:** react-dropzone

## Technical Decisions Required

1. **PDF Viewer Library:** react-pdf, pdfjs, or other?
2. **Real-Time Updates:** WebSocket or polling for case creation progress?
3. **Inline Editing:** Contenteditable or input switching?

## Notes

- Side-by-side review interface is critical for data accuracy
- Duplicate detection UI must be intuitive to prevent errors
- Real-time case creation progress improves user experience
- Future enhancement: AI-powered field suggestions during manual correction
- Future enhancement: Bulk instruction processing with batch approval

## Definition of Done

- [ ] All pages and components implemented
- [ ] TypeScript SDK integrated
- [ ] PDF viewer functional
- [ ] Inline editing functional
- [ ] Duplicate management flow complete
- [ ] Case creation progress tracking complete
- [ ] Manual upload functional
- [ ] Inbox metrics dashboard complete
- [ ] Unit tests ≥85% coverage
- [ ] Integration tests passing
- [ ] E2E tests passing
- [ ] Code reviewed and approved
- [ ] Deployed to dev/staging and tested
- [ ] User acceptance testing completed

---

**Document Version:** 1.0  
**Last Updated:** December 9, 2025  
**Author:** Technical Task Writer  
**Status:** Ready for Development
