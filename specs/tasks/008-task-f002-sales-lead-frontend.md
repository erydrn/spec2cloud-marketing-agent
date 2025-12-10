# Technical Task: Sales Lead Management - Frontend

**GitHub Issue:** [#9](https://github.com/erydrn/spec2cloud-marketing-agent/issues/9)

## Task Information

**Task ID:** 008  
**Task Name:** Sales Lead Management - Frontend UI  
**Feature:** F002 - Sales Lead Management  
**Priority:** High  
**Complexity:** High  
**Estimated Effort:** 8-10 days  
**Dependencies:**
- Task 002: Frontend Scaffolding
- Task 006: Digital Marketing Frontend (shared components and patterns)
- Task 007: Sales Lead Backend API (TypeScript SDK)

## Description

Build frontend UI for Sales Team to manage their sales pipeline, qualify leads, generate and send quotes, track activities, capture client consent, and close leads. This interface provides Sales Agents with a comprehensive workspace to convert leads into cases efficiently.

## Technical Requirements

### 1. UI Components & Pages

#### 1.1 Sales Pipeline Dashboard (`/sales/pipeline`)
Main workspace for sales agents:

- **Pipeline Overview Cards:**
  - Active Leads (count)
  - Quotes Sent This Week (count)
  - Conversion Rate This Month (percentage)
  - Pipeline Value (total estimated £XXX,XXX)
  - Overdue Follow-Ups (count with alert badge)

- **Pipeline Kanban Board:**
  - Columns: NEW | CONTACTED | QUOTED | NEGOTIATING | WON | LOST
  - Each column shows lead cards:
    - Lead name, property address, quality score badge
    - Days in stage indicator
    - Next action date (if scheduled)
    - Quick actions: View, Call, Email
  - Drag-and-drop to move between stages (updates lead status)
  - Color-coded priority: Red (overdue), Yellow (due today), Green (future)

- **Alternative: List View Toggle**
  - Data table with columns: Lead Name, Property, Quality Score, Status, Days Open, Last Contact, Next Action, Assigned To
  - Sortable and filterable
  - Switch between Kanban and List views

- **Quick Actions Toolbar:**
  - "New Manual Lead" button (reuse from F001)
  - "Bulk Actions" dropdown (reassign, schedule follow-up)
  - "Export Pipeline" button (CSV export)

#### 1.2 Lead Detail Page - Sales View (`/sales/leads/[leadId]`)
Enhanced lead detail with sales-specific sections:

- **Header:**
  - Lead name, quality score, sales status badge
  - Quick action buttons: Call, Email, Generate Quote, Schedule Task, Close Lead

- **Contact Card:** (reuse from F001)
  - With call/email links

- **Property Card:** (reuse from F001)
  - With enriched details (tenure, bedrooms, property type)

- **Client Circumstances Card:**
  - First-time buyer? (Yes/No)
  - Chain status (No Chain, Upward, Downward, Both)
  - Financing type (Cash, Mortgage, Mixed)
  - Mortgage approved? (Yes/No/Pending)
  - Target completion date
  - Editable fields (click to edit inline)

- **Qualification Checklist Card:**
  - Checklist items with checkboxes:
    - [ ] Contact information verified
    - [ ] Property details confirmed
    - [ ] Client readiness assessed
    - [ ] Special circumstances identified
    - [ ] Service level determined
  - Service level dropdown (Standard, Premium, Express)
  - Win probability score: XX% (calculated, displayed as progress bar)
  - Expected close date picker
  - Qualification notes text area
  - "Save Qualification" button

- **Quotes Card:**
  - List of all quotes (including revisions)
  - Each quote shows: Version, Created Date, Total Cost, Status (SENT, VIEWED, ACCEPTED, EXPIRED)
  - Actions: View PDF, Resend, Revise, Extend Validity
  - "Generate New Quote" button (prominent)

- **Activity Timeline Card:**
  - Reverse chronological list of activities (calls, emails, meetings, notes)
  - Each activity: Type icon, Date/Time, Outcome, Notes
  - "Log Activity" button (opens modal)

- **Competing Quotes Card:**
  - Input fields to add competitor quote details (Competitor name, Price, Service level)
  - "Add Competitor Quote" button

#### 1.3 Quote Generation Modal (`/sales/leads/[leadId]/quotes/new`)
Modal or slide-over panel for quote creation:

- **Service Package Selection:**
  - Radio buttons: Standard, Premium, Express
  - Each option shows included services and base price

- **Transaction Details:**
  - Transaction type (auto-filled from lead)
  - Transaction value (editable currency input)

- **Additional Services:**
  - Checkboxes: Searches (£300), Insurance (£XX), Indemnity (£XX)
  - Dynamic pricing update as services added

- **Promotional Code:**
  - Text input with "Apply" button
  - Validation message (valid/invalid)

- **Custom Adjustments:**
  - Discount percentage input
  - Discount reason text area
  - "Requires Approval" badge if discount >10%

- **Pricing Summary:**
  - Base Fee: £XXX
  - Additional Services: £XXX
  - Subtotal: £XXX
  - Discount: -£XXX
  - VAT: £XXX
  - **Total: £XXX** (bold, large font)

- **Actions:**
  - "Generate Quote" button (creates draft)
  - "Generate & Send" button (creates and emails immediately)
  - "Cancel" button

#### 1.4 Quote Detail View (`/sales/quotes/[quoteId]`)
Full quote details page:

- **Quote Header:**
  - Quote ID, Version, Status badge
  - Created Date, Sent Date, Viewed Date (if applicable)
  - Validity period (expires on DATE)

- **Quote Details:**
  - Service package, transaction type, transaction value
  - Itemized pricing breakdown (table)
  - Promotional code applied (if any)
  - Discount details (if any)

- **Actions:**
  - "Download PDF" button
  - "Send to Client" button (opens email modal)
  - "Revise Quote" button (creates new version)
  - "Extend Validity" button (adds 7 days)
  - "Mark as Accepted" button (manual acceptance)

- **Delivery Status:**
  - Email sent to: [email]
  - Sent at: [timestamp]
  - Opened at: [timestamp] (if tracked)
  - Portal accessed at: [timestamp] (if applicable)

#### 1.5 Activity Logging Modal
Modal for logging calls, emails, meetings, notes:

- **Activity Type:** Radio buttons (Call, Email, Meeting, Note)

- **Activity Date/Time:** Datetime picker (defaults to now)

- **Duration:** Number input (minutes) - for Call/Meeting only

- **Outcome:** Dropdown (Connected, Voicemail, No Answer, Positive, Negative) - for Call only

- **Notes:** Text area (required)

- **Next Action:** Dropdown (Call Back, Send Email, Schedule Meeting, No Action)

- **Next Action Date:** Date picker (if next action selected)

- **Actions:**
  - "Save Activity" button
  - "Cancel" button

#### 1.6 Quote Acceptance / Consent Capture Page (`/sales/quotes/[quoteId]/accept`)
For capturing client consent (can be accessed via client portal or agent on behalf):

- **Quote Summary:**
  - Service details and total cost (read-only)

- **Consent Method:** Radio buttons
  - Online Portal (e-signature)
  - Email Confirmation
  - Phone Verbal
  - In-Person

- **Consent Fields (conditional on method):**
  - **Online Portal:**
    - E-signature canvas (draw signature)
    - "I agree to terms and conditions" checkbox
  - **Email Confirmation:**
    - Email address to send confirmation request
  - **Phone Verbal:**
    - Recording reference input
    - Agent attestation checkbox
  - **In-Person:**
    - File upload for signed document

- **Initial Case Information:**
  - Property address (editable)
  - Target completion date (date picker)
  - Additional parties (text inputs for co-buyers/sellers)

- **Request Documentation:**
  - Checkboxes: ID, Proof of Funds, Mortgage Offer
  - Custom message text area

- **Actions:**
  - "Capture Consent & Create Case" button (primary)
  - "Cancel" button

- **Success Message:**
  - "Consent captured! Case #XXXXX has been created. Client will receive confirmation email."

#### 1.7 Lead Closing Modal
Modal for closing leads:

- **Outcome:** Radio buttons (Won, Lost, On Hold, Invalid)

- **Reason:** Dropdown (conditional on outcome)
  - **Lost reasons:** Price Too High, Timeline Misaligned, Service Not Suitable, Competitor, Unresponsive, Transaction Cancelled, Other
  - **On Hold reason:** Client Postponed, Awaiting Third Party, Internal Capacity

- **Competitor Details:** Text input (if reason = Competitor)

- **Follow-Up Date:** Date picker (if outcome = On Hold)

- **Closing Notes:** Text area

- **Actions:**
  - "Close Lead" button (red, warning style)
  - "Cancel" button

- **Confirmation Dialog:**
  - "Are you sure you want to close this lead? This action cannot be undone."

#### 1.8 Team Performance Dashboard (`/sales/team-performance`)
For team leads:

- **Team Metrics Cards:**
  - Total Team Pipeline Value
  - Team Conversion Rate
  - Average Days to Close
  - Total Quotes Sent This Month

- **Agent Performance Table:**
  - Columns: Agent Name, Active Leads, Quotes Sent, Leads Won, Conversion Rate, Avg Days to Close
  - Sortable columns
  - Color-coded performance indicators (green = above target, red = below)

- **Charts:**
  - Leads by stage (stacked bar chart by agent)
  - Conversion funnel (team-wide)
  - Win/loss reasons (pie chart)

- **Filters:**
  - Date range picker
  - Team filter (if multiple teams)

### 2. API Integration (TypeScript SDK)

#### 2.1 SDK Usage
All API calls via generated SDK from Task 007:

**Example: Fetch assigned leads**
```typescript
import { SalesApi } from '@/sdk';

const salesApi = new SalesApi(config);
const response = await salesApi.getAssignedLeads({ 
  status: 'NEW', 
  sortBy: 'createdAt', 
  order: 'desc' 
});
const leads = response.data;
```

**Example: Generate quote**
```typescript
const quoteResponse = await salesApi.createQuote({
  leadId: leadId,
  quoteRequest: {
    servicePackage: 'STANDARD',
    transactionType: 'PURCHASE',
    transactionValue: 350000,
    additionalServices: ['SEARCHES', 'INSURANCE'],
    promotionalCode: 'SAVE10'
  }
});
const quote = quoteResponse.data;
```

**Example: Log activity**
```typescript
await salesApi.createActivity({
  leadId: leadId,
  activityRequest: {
    activityType: 'CALL',
    activityDate: new Date().toISOString(),
    duration: 15,
    outcome: 'CONNECTED',
    notes: 'Discussed timeline and answered questions about process',
    nextAction: 'SEND_EMAIL',
    nextActionDate: '2025-12-15'
  }
});
```

#### 2.2 Real-Time Updates (Optional)
Implement WebSocket or polling for live updates:
- New leads assigned to agent appear in pipeline automatically
- Quote status changes (viewed, accepted) update in real-time
- Activity logs from other agents appear immediately

### 3. State Management

Use React Context or Zustand for:
- **Pipeline State:** Leads grouped by stage, drag-and-drop state
- **Lead Detail State:** Current lead, qualification checklist, activities
- **Quote State:** Draft quote pricing calculations
- **User State:** Authenticated agent profile, preferences

### 4. UI/UX Requirements

#### 4.1 Design Patterns
- **Kanban Board:** Smooth drag-and-drop with visual feedback
- **Inline Editing:** Click-to-edit for enrichment fields (save on blur)
- **Modal Forms:** Quote generation, activity logging, lead closing
- **Toast Notifications:** Success/error messages for actions
- **Loading States:** Skeleton loaders for data fetching

#### 4.2 Keyboard Shortcuts
- `N`: New manual lead
- `Q`: Generate quote (on lead detail page)
- `C`: Log call
- `E`: Send email
- `/`: Focus search bar

#### 4.3 Mobile Responsiveness
- Pipeline: Switch to list view on tablet/mobile
- Lead detail: Stack cards vertically on mobile
- Quote generation: Full-screen modal on mobile

### 5. Testing Requirements

#### Unit Tests (≥85% Coverage)
- Pipeline board: Drag-and-drop state management
- Quote calculator: Pricing calculations with discounts and services
- Activity logger: Form validation and submission
- Consent capture: Validation for each consent method

#### Integration Tests
- Full lead processing flow: View lead → Qualify → Generate quote → Send → Accept → Verify case creation
- Quote generation: Fill form → Submit → Verify API call → Verify quote appears in list
- Activity logging: Log call → Verify timeline updated

#### E2E Tests
1. **Sales Pipeline Flow:** Agent logs in → Views pipeline → Drags lead to "Quoted" → Verifies status updated
2. **Quote Generation:** Agent opens lead → Clicks "Generate Quote" → Fills form → Sends quote → Verifies email sent notification
3. **Quote Acceptance:** Agent opens quote → Clicks "Capture Consent" → Fills consent form → Submits → Verifies case created
4. **Lead Closing:** Agent opens lead → Clicks "Close Lead" → Selects "Lost - Competitor" → Submits → Verifies lead closed

## Acceptance Criteria

1. **Sales Pipeline Dashboard:**
   - [ ] Overview cards display accurate metrics
   - [ ] Kanban board displays leads grouped by stage
   - [ ] Drag-and-drop updates lead status via API
   - [ ] List view toggles correctly
   - [ ] Filters and search work correctly

2. **Lead Detail - Sales View:**
   - [ ] All cards display correct lead data
   - [ ] Enrichment fields editable inline
   - [ ] Qualification checklist saveable
   - [ ] Win probability calculated and displayed
   - [ ] Activity timeline shows all activities

3. **Quote Generation:**
   - [ ] Modal displays with service package options
   - [ ] Pricing calculates correctly with services and discounts
   - [ ] Promotional codes validated
   - [ ] Approval badge shown for discounts >10%
   - [ ] Quote generated successfully via API

4. **Quote Management:**
   - [ ] Quote list displays all versions
   - [ ] Quote detail shows full breakdown
   - [ ] PDF download works
   - [ ] Email send modal functional
   - [ ] Quote status tracked (sent, viewed, accepted)

5. **Activity Logging:**
   - [ ] Modal displays for all activity types
   - [ ] Conditional fields based on activity type
   - [ ] Activities saved and appear in timeline
   - [ ] Next action scheduled correctly

6. **Consent Capture:**
   - [ ] Consent form displays for all methods
   - [ ] E-signature canvas functional
   - [ ] Case creation triggered on submit
   - [ ] Success message with case ID shown

7. **Lead Closing:**
   - [ ] Closing modal displays with outcomes
   - [ ] Conditional fields based on outcome
   - [ ] Confirmation dialog shown
   - [ ] Lead status updated to CLOSED

8. **Team Performance:**
   - [ ] Team metrics accurate
   - [ ] Agent table displays performance data
   - [ ] Charts render correctly
   - [ ] Filters update data

9. **API Integration:**
   - [ ] All API calls use generated SDK
   - [ ] Error handling displays user-friendly messages
   - [ ] Loading states displayed during API calls

10. **Testing:**
    - [ ] Unit tests achieve ≥85% coverage
    - [ ] Integration tests pass
    - [ ] E2E tests pass for critical flows

## Dependencies

### Upstream Dependencies
- **Task 002:** Frontend scaffolding
- **Task 006:** Digital Marketing Frontend (shared components)
- **Task 007:** Sales Lead Backend API (TypeScript SDK)

### External Dependencies
- **UI Components:** Material-UI, Ant Design, or custom
- **Drag-and-Drop Library:** react-beautiful-dnd or dnd-kit
- **Form Library:** react-hook-form
- **Chart Library:** Chart.js or Recharts
- **E-Signature Component:** react-signature-canvas

## Technical Decisions Required

1. **Kanban Library:** react-beautiful-dnd or dnd-kit?
2. **E-Signature Implementation:** Custom canvas or third-party component?
3. **Real-Time Updates:** WebSocket or polling? Priority for Phase 1?
4. **PDF Generation:** Client-side (jsPDF) or server-side?
5. **Email Tracking:** Implement or defer to backend?

## Notes

- Kanban board is critical for sales team workflow; ensure smooth UX
- Quote generation must be fast and intuitive (minimize clicks)
- Activity logging should be quick (keyboard shortcuts help)
- Consent capture is legally important; ensure clear audit trail
- Future enhancement: AI-powered quote suggestions based on win/loss data
- Future enhancement: Integrated VoIP calling from interface

## Definition of Done

- [ ] All pages and components implemented
- [ ] TypeScript SDK integrated
- [ ] Kanban board with drag-and-drop functional
- [ ] Quote generation and management complete
- [ ] Activity logging complete
- [ ] Consent capture complete
- [ ] Lead closing complete
- [ ] Team performance dashboard complete
- [ ] API integration with error handling
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
