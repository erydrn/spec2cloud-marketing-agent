# Technical Task: Digital Marketing Lead Capture - Frontend

**GitHub Issue:** [#7](https://github.com/erydrn/spec2cloud-marketing-agent/issues/7)

## Task Information

**Task ID:** 006  
**Task Name:** Digital Marketing Lead Capture - Frontend UI  
**Feature:** F001 - Digital Marketing Lead Capture  
**Priority:** High  
**Complexity:** Medium  
**Estimated Effort:** 6-8 days  
**Dependencies:**
- Task 002: Frontend Scaffolding (React/Next.js app, UI components, authentication)
- Task 005: Digital Marketing Backend API (lead capture endpoints, TypeScript SDK)

## Description

Build the frontend user interface for the Digital Marketing team and New Business Support team to monitor, review, and manage captured leads. This includes dashboards for lead pipeline visibility, lead detail views, manual lead entry forms, lead assignment management, and reporting views for marketing channel performance.

The frontend consumes the backend API (Task 005) via an auto-generated TypeScript SDK and provides an intuitive interface for lead management workflows.

## Technical Requirements

### 1. UI Components & Pages

#### 1.1 Lead Dashboard Page (`/leads/dashboard`)
Display overview of lead pipeline with key metrics:
- **Metrics Cards:**
  - Total leads captured (today, this week, this month)
  - Leads by quality score (A/B/C counts with percentages)
  - Leads by status (CAPTURED, ASSIGNED, HOLD, DUPLICATE)
  - Leads by source channel (DIGITAL_ADS, SEO, BUSINESS_DEV, etc.)
  - Average processing time (capture to assignment)
  - Conversion funnel: Captured → Qualified → Assigned → Won

- **Lead List View (Data Table):**
  - Columns: Lead ID, Name, Email, Phone, Source, Quality Score, Status, Property Type, Created Date, Assigned To
  - Sortable columns (click header to sort)
  - Filterable columns (dropdown filters for Source, Quality Score, Status)
  - Search bar (search by name, email, phone, property address)
  - Pagination (50 leads per page)
  - Row click: Navigate to lead detail page

- **Filters & Search:**
  - Date range picker (filter by created date)
  - Source filter (multi-select: DIGITAL_ADS, SEO, etc.)
  - Quality score filter (multi-select: A, B, C)
  - Status filter (multi-select: CAPTURED, ASSIGNED, HOLD, DUPLICATE)
  - Property type filter (multi-select: PURCHASE, SALE, REMORTGAGE)
  - Assigned team filter (SALES, NEW_BUSINESS, HOLD)
  - "Clear All Filters" button

#### 1.2 Lead Detail Page (`/leads/[leadId]`)
Display comprehensive lead information:
- **Header Section:**
  - Lead ID, Quality Score badge (A/B/C with color coding)
  - Status badge (CAPTURED, ASSIGNED, etc.)
  - Created date and last updated date
  - "Edit Lead" button, "Reassign" button, "Requalify" button

- **Contact Information Card:**
  - Full name, email, phone, alternate phone
  - Click-to-call phone numbers (tel: links)
  - Click-to-email address (mailto: link)

- **Property Details Card:**
  - Property type (PURCHASE, SALE, REMORTGAGE)
  - Property address (formatted with line breaks)
  - Property value (formatted as currency £XXX,XXX)
  - Property type (flat, house, etc.)
  - Timeline (URGENT, 1-3 MONTHS, etc.)

- **Qualification Results Card:**
  - Contact completeness: ✓ or ✗
  - Property type identified: ✓ or ✗
  - Geographic valid: ✓ or ✗
  - Transaction value valid: ✓ or ✗
  - Duplicate check: ✓ or "Duplicate of Lead #XXXXX" (link to original)

- **Scoring Details Card:**
  - Data completeness score: XX/50
  - Transaction value score: XX/30
  - Timeline urgency score: XX/20
  - Total score: XX/100
  - Quality grade: A/B/C

- **Routing Details Card:**
  - Assigned to: Team name or agent name
  - Assigned team: SALES, NEW_BUSINESS, HOLD
  - Assignment timestamp
  - Assignment method: AUTO or MANUAL

- **Referral Source Card (if applicable):**
  - Referral type, reference code, partner name

- **Additional Information Card:**
  - Free-form additional data (JSON display or key-value pairs)

- **Activity History Timeline:**
  - List of events: CAPTURED, QUALIFIED, ROUTED, ASSIGNED, UPDATED
  - Each event shows: timestamp, event type, event data, performed by (system or user)
  - Reverse chronological order (newest first)

#### 1.3 Manual Lead Entry Form (`/leads/new`)
Form for manually entering leads (e.g., phone inquiries, walk-ins):
- **Form Fields (matching API schema):**
  - Source (dropdown: DIGITAL_ADS, SEO, BUSINESS_DEV, ORGANIC, STRATEGIC, EMAIL, WEB_FORM)
  - Campaign ID (optional text input)
  - Contact Information:
    - First Name* (required)
    - Last Name* (required)
    - Email* (required, email validation)
    - Phone* (required, UK phone format validation)
    - Alternate Phone (optional)
  - Property Type* (required dropdown: PURCHASE, SALE, REMORTGAGE, OTHER)
  - Property Details:
    - Address (text area)
    - Postcode* (required, UK postcode format validation)
    - Property Value (currency input £)
    - Property Type (dropdown: flat, house, bungalow, etc.)
  - Timeline (dropdown: URGENT, 1-3 MONTHS, 3-6 MONTHS, 6-12 MONTHS, UNSURE)
  - Referral Source (expandable section):
    - Type (dropdown: estate_agent, broker, partner)
    - Reference Code
    - Partner Name
  - Additional Info (text area, optional)

- **Form Validation:**
  - Real-time validation on blur (show error messages below fields)
  - Email format validation (regex)
  - UK phone format validation (regex: +44... or 0...)
  - UK postcode format validation (regex)
  - Required field indicators (*)
  - Submit button disabled until all required fields valid

- **Form Actions:**
  - "Submit Lead" button (primary action)
  - "Cancel" button (navigate back to dashboard)

- **Success/Error Handling:**
  - On success: Show toast notification "Lead captured successfully! Lead ID: XXXXX", redirect to lead detail page
  - On error: Show error message below form with details (e.g., "Email already exists - duplicate of Lead #XXXXX")
  - On duplicate (409): Show warning dialog with option to view existing lead or submit anyway (override)

#### 1.4 Lead Assignment Management Page (`/leads/assignments`)
For team leads to manage lead assignments:
- **Team Workload Overview:**
  - List of team members (SALES team) with current active lead count
  - Visual indicator of workload (progress bar or color-coded)
  - Sort by workload (ascending/descending)

- **Bulk Reassignment Tool:**
  - Multi-select leads from table
  - Dropdown to select target team member
  - "Reassign Selected" button
  - Confirmation dialog with reason input

- **Assignment Rules Configuration (Future):**
  - Configure geographic specialization per team member
  - Set availability status (AVAILABLE, ON_LEAVE, BUSY)

#### 1.5 Marketing Channel Performance Page (`/reports/channels`)
Analytics dashboard for marketing team:
- **Channel Performance Metrics (Cards):**
  - For each channel (DIGITAL_ADS, SEO, BUSINESS_DEV, etc.):
    - Total leads captured
    - Quality score distribution (% Grade A, B, C)
    - Average data completeness score
    - Conversion rate to assigned (% leads routed to Sales Team)

- **Charts:**
  - Leads by source (pie chart or bar chart)
  - Leads over time by source (line chart, last 30 days)
  - Quality score distribution by source (stacked bar chart)

- **Filters:**
  - Date range picker (last 7 days, last 30 days, last 90 days, custom)
  - Source filter (multi-select)

### 2. API Integration (TypeScript SDK)

#### 2.1 SDK Generation
- Generate TypeScript SDK from backend OpenAPI 3.0 specification (Task 005)
- SDK should be auto-generated using tools like openapi-generator or swagger-codegen
- SDK includes:
  - Type-safe client for all API endpoints
  - Request/response TypeScript interfaces
  - Error handling types

#### 2.2 API Client Usage
All API calls should use the generated SDK:

**Example: Fetch leads for dashboard**
```typescript
import { LeadsApi, Configuration } from '@/sdk';

const config = new Configuration({ basePath: process.env.NEXT_PUBLIC_API_BASE_URL });
const leadsApi = new LeadsApi(config);

// Fetch leads with filters
const response = await leadsApi.getLeads({
  status: 'ASSIGNED',
  qualityScore: 'A',
  source: 'DIGITAL_ADS',
  page: 1,
  pageSize: 50
});

const leads = response.data;
```

**Example: Create new lead**
```typescript
const createResponse = await leadsApi.captureLeads({
  leadCaptureRequest: {
    source: 'WEB_FORM',
    contactInfo: {
      firstName: 'John',
      lastName: 'Doe',
      email: 'john@example.com',
      phone: '+447700900000'
    },
    propertyType: 'PURCHASE',
    // ...
  }
});

const newLead = createResponse.data;
```

#### 2.3 Error Handling
Handle API errors gracefully:
- 400 Bad Request: Show validation errors in form
- 409 Conflict (Duplicate): Show dialog with existing lead details
- 500 Internal Server Error: Show generic error message with correlation ID for support
- Network errors: Show "Connection lost" message with retry button

#### 2.4 Loading States
Display loading indicators during API calls:
- Data table: Show skeleton loaders for rows
- Forms: Disable submit button and show spinner
- Detail page: Show skeleton loaders for cards

### 3. State Management

#### 3.1 Client-Side State (React Context or Zustand)
Manage application state:
- **Lead List State:**
  - Leads array (fetched from API)
  - Filters (source, quality score, status, date range)
  - Pagination (current page, page size, total count)
  - Sort order (column, direction)
  - Loading state (boolean)
  - Error state (string or null)

- **Lead Detail State:**
  - Current lead object
  - Loading state
  - Error state

- **Form State:**
  - Form values (managed by form library like react-hook-form)
  - Validation errors
  - Submission state (idle, submitting, success, error)

#### 3.2 Server-Side Rendering (Next.js)
Use Next.js server-side rendering for initial page load performance:
- Dashboard page: Fetch initial leads on server-side, hydrate client-side for filters/pagination
- Lead detail page: Fetch lead details on server-side, show immediately on page load

### 4. UI/UX Requirements

#### 4.1 Design System
Use consistent design system (e.g., Material-UI, Ant Design, or custom):
- Typography: Consistent font sizes, weights, colors
- Color palette: Primary, secondary, success, warning, error, info colors
- Spacing: Consistent padding/margin scale (4px, 8px, 16px, 24px, etc.)
- Components: Buttons, cards, tables, forms, badges, tooltips, dialogs

#### 4.2 Responsive Design
Support desktop and tablet views (mobile optional):
- Desktop (≥1024px): Full layout with sidebar navigation
- Tablet (768-1023px): Collapsed sidebar, responsive table
- Mobile (<768px): Stacked layout (optional, low priority)

#### 4.3 Accessibility (WCAG 2.1 Level AA)
- Semantic HTML elements (e.g., `<button>`, `<nav>`, `<main>`)
- ARIA labels for interactive elements
- Keyboard navigation support (tab order, focus indicators)
- Color contrast ratios meet AA standards
- Form labels and error messages associated with inputs

#### 4.4 Performance
- Code splitting: Lazy load pages and large components
- Image optimization: Use Next.js Image component for optimized images
- Data table virtualization: Render only visible rows for large datasets (optional, if performance issues)
- Debounce search input (300ms delay before API call)

### 5. Authentication & Authorization

#### 5.1 Authentication
- User must be authenticated to access lead management pages
- Use authentication context from Task 002 (e.g., NextAuth, Azure AD, Auth0)
- Redirect unauthenticated users to login page

#### 5.2 Authorization
- Role-based access control:
  - **Marketing Manager:** View dashboard, reports (read-only)
  - **New Business Support:** View all pages, manual lead entry, reassignment
  - **Sales Team Member:** View assigned leads only, cannot reassign
  - **Admin:** Full access (all pages, all actions)

- Implement permission checks:
  - Hide "Edit", "Reassign" buttons if user lacks permission
  - API should also enforce authorization (frontend checks are for UX only)

### 6. Notifications & Real-Time Updates (Optional Enhancement)

#### 6.1 Toast Notifications
Display toast notifications for user actions:
- Lead captured successfully
- Lead updated successfully
- Lead reassigned successfully
- Error messages (e.g., "Failed to capture lead. Please try again.")

#### 6.2 Real-Time Updates (Future)
Use WebSockets or Server-Sent Events for real-time dashboard updates:
- New leads appear in dashboard automatically
- Lead status changes reflect immediately
- Notification badge for new leads assigned to user

### 7. Testing & Quality

#### 7.1 Component Tests (React Testing Library)
- Test lead dashboard: renders metrics, table, filters
- Test lead detail page: renders all cards with correct data
- Test manual entry form: validation, submission, error handling
- Test assignment management: reassignment flow

#### 7.2 Integration Tests
- Test full user flow: Dashboard → Lead Detail → Edit → Save → Verify changes
- Test form submission: Fill form → Submit → Verify API call → Verify redirect
- Test error handling: Trigger API error → Verify error message displayed

#### 7.3 E2E Tests (Playwright or Cypress)
- Test critical user journeys:
  - User logs in → Views dashboard → Filters leads → Opens lead detail
  - User creates new lead → Submits form → Verifies lead appears in dashboard
  - User reassigns lead → Verifies new assignment in detail page

## Acceptance Criteria

1. **Lead Dashboard:**
   - [ ] Dashboard displays key metrics (total leads, quality scores, status breakdown)
   - [ ] Data table shows all leads with sortable/filterable columns
   - [ ] Filters work correctly (source, quality score, status, date range)
   - [ ] Search bar filters leads by name, email, phone, address
   - [ ] Pagination works (50 leads per page, navigate pages)
   - [ ] Row click navigates to lead detail page

2. **Lead Detail Page:**
   - [ ] All lead information displayed in organized cards
   - [ ] Contact info, property details, qualification results, scoring, routing visible
   - [ ] Activity history timeline shows all events in reverse chronological order
   - [ ] "Edit Lead", "Reassign", "Requalify" buttons functional
   - [ ] Duplicate leads show link to original lead

3. **Manual Lead Entry Form:**
   - [ ] All form fields render correctly with proper labels and placeholders
   - [ ] Real-time validation for email, phone, postcode formats
   - [ ] Required field validation (submit disabled until valid)
   - [ ] Form submission calls API with correct data
   - [ ] Success: Toast notification shown, redirect to lead detail page
   - [ ] Error: Error message displayed below form
   - [ ] Duplicate (409): Warning dialog shown with link to existing lead

4. **Lead Assignment Management:**
   - [ ] Team workload overview displays all team members with active lead counts
   - [ ] Bulk reassignment tool allows multi-select and reassignment
   - [ ] Reassignment confirmation dialog shows with reason input
   - [ ] Successful reassignment updates lead assignments immediately

5. **Marketing Channel Performance:**
   - [ ] Channel metrics cards display for all sources
   - [ ] Charts render correctly (pie chart, line chart, stacked bar chart)
   - [ ] Date range filter updates charts and metrics
   - [ ] Source filter updates charts and metrics

6. **API Integration:**
   - [ ] TypeScript SDK generated from OpenAPI spec
   - [ ] All API calls use SDK with type-safe interfaces
   - [ ] Loading states displayed during API calls (skeleton loaders, spinners)
   - [ ] Error handling implemented (400, 409, 500 with user-friendly messages)

7. **Authentication & Authorization:**
   - [ ] Unauthenticated users redirected to login
   - [ ] Role-based access control enforced (buttons hidden for unauthorized users)
   - [ ] Sales team members only see assigned leads
   - [ ] Marketing managers have read-only access

8. **UI/UX:**
   - [ ] Consistent design system applied (typography, colors, spacing)
   - [ ] Responsive design works on desktop and tablet
   - [ ] Accessibility: Keyboard navigation, ARIA labels, color contrast meets AA
   - [ ] Toast notifications appear for user actions

9. **Performance:**
   - [ ] Dashboard loads within 2 seconds
   - [ ] Lead detail page loads within 1 second
   - [ ] Form submission completes within 3 seconds
   - [ ] Code splitting implemented for lazy loading

10. **Testing:**
    - [ ] Component tests written for all major components (≥85% coverage)
    - [ ] Integration tests passing for critical flows
    - [ ] E2E tests passing for user journeys (login → dashboard → detail → form)

## Testing Requirements

### Unit Tests (≥85% Coverage)
- Lead dashboard component (renders metrics, table, filters)
- Lead detail page component (renders all cards)
- Manual entry form component (validation, submission)
- Assignment management component (workload display, reassignment)
- Channel performance component (metrics, charts)
- API client utilities (error handling, loading states)

### Integration Tests
- Dashboard page: Fetch leads from API → Render table → Click row → Navigate to detail
- Manual entry form: Fill form → Submit → API call → Redirect to detail page
- Lead detail page: Fetch lead from API → Render cards → Click edit → Navigate to edit form

### E2E Tests (Playwright/Cypress)
1. **Lead Management Flow:**
   - User logs in → Navigates to dashboard → Filters by quality score A → Clicks lead → Views detail page → Clicks edit → Updates phone number → Saves → Verifies change
2. **Manual Entry Flow:**
   - User logs in → Navigates to new lead form → Fills all fields → Submits → Verifies lead appears in dashboard
3. **Assignment Flow:**
   - Team lead logs in → Navigates to assignments page → Selects leads → Reassigns to team member → Verifies assignment updated

### Test Scenarios
1. **Dashboard Filtering:** Apply multiple filters (source, quality score, date range) → Verify only matching leads displayed
2. **Form Validation:** Submit form with invalid email → Verify error message shown, submit disabled
3. **Duplicate Handling:** Submit lead with existing email → Verify 409 error, dialog shown with link to existing lead
4. **Loading States:** Trigger API call → Verify skeleton loaders displayed during fetch
5. **Error Handling:** Simulate 500 error from API → Verify error message displayed with correlation ID

## Dependencies

### Upstream Dependencies
- **Task 002 (Frontend Scaffolding):** React/Next.js app, UI component library, authentication setup, routing
- **Task 005 (Backend API):** Lead capture endpoints, OpenAPI specification for SDK generation

### External Dependencies
- **TypeScript SDK Generator:** openapi-generator or swagger-codegen for SDK generation from OpenAPI spec
- **UI Component Library:** Material-UI, Ant Design, or custom design system
- **Form Library:** react-hook-form or Formik for form state management and validation
- **Chart Library:** Chart.js, Recharts, or D3.js for analytics charts
- **State Management:** React Context, Zustand, or Redux for client-side state
- **Testing Libraries:** React Testing Library, Playwright/Cypress for E2E tests

## Technical Decisions Required

1. **UI Component Library:** Which library to use (Material-UI, Ant Design, custom)?
2. **State Management:** React Context, Zustand, Redux, or other?
3. **Form Library:** react-hook-form, Formik, or native React state?
4. **Chart Library:** Chart.js, Recharts, D3.js, or other?
5. **SDK Generation Tool:** openapi-generator, swagger-codegen, or manual SDK?
6. **E2E Testing Framework:** Playwright or Cypress?
7. **Real-Time Updates:** Should we implement WebSockets for live dashboard updates in Phase 1?

## Notes

- Frontend should be fully type-safe using TypeScript interfaces from generated SDK
- Prioritize user experience for New Business Support team (primary users)
- Dashboard performance is critical; optimize data table rendering for large datasets (1000+ leads)
- Consider implementing data table export (CSV/Excel) for reporting (future enhancement)
- Future enhancement: Bulk lead import from CSV file
- Future enhancement: Advanced search with autocomplete
- Future enhancement: Customizable dashboard widgets (drag-and-drop)

## Definition of Done

- [ ] All pages and components implemented (Dashboard, Detail, Form, Assignments, Reports)
- [ ] TypeScript SDK generated from backend OpenAPI spec
- [ ] API integration implemented with error handling and loading states
- [ ] Authentication and authorization implemented (role-based access control)
- [ ] Responsive design implemented (desktop and tablet)
- [ ] Accessibility requirements met (WCAG 2.1 Level AA)
- [ ] Component tests written with ≥85% coverage
- [ ] Integration tests passing for critical flows
- [ ] E2E tests passing for user journeys
- [ ] Performance optimizations implemented (code splitting, lazy loading)
- [ ] Toast notifications implemented for user actions
- [ ] Code reviewed and approved
- [ ] Deployed to dev/staging environment and tested
- [ ] User acceptance testing completed with New Business Support team
- [ ] Documentation updated (user guide, developer guide)

---

**Document Version:** 1.0  
**Last Updated:** December 9, 2025  
**Author:** Technical Task Writer  
**Status:** Ready for Development
