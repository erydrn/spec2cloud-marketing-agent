# Task: Frontend Scaffolding

**GitHub Issue:** [#2](https://github.com/erydrn/spec2cloud-marketing-agent/issues/2)

## Task Information

**Task ID:** 002  
**Task Name:** Frontend Scaffolding  
**Feature:** Infrastructure (Scaffolding)  
**Priority:** Critical  
**Estimated Complexity:** High  
**Dependencies:** 001-task-backend-scaffolding.md (requires OpenAPI spec)

## Description

Create the foundational frontend infrastructure for the Property Legal Services (PLS) AI Agent System using Next.js 15+ with React, TypeScript, and Tailwind CSS. Implement the app structure, routing, state management, API client generation, and component library setup.

## Technical Requirements

### 1. Next.js Application Setup
- Initialize Next.js project (latest stable version, App Router)
- Location: `src/PLS.Web/`
- Configure TypeScript with strict mode:
  - `tsconfig.json` with path aliases (`@/components`, `@/lib`, `@/api`)
  - Strict type checking enabled
  - No implicit any
- Set up Tailwind CSS v4 with:
  - Custom design tokens (colors, spacing, typography)
  - Dark mode support (class-based strategy)
  - Component-specific utilities
- Configure Next.js features:
  - App Router with server and client components
  - API routes for BFF (Backend-for-Frontend) pattern
  - Image optimization
  - Font optimization (Inter, system fonts)
  - Metadata API for SEO
  - Environment variable management (`.env.local`, `.env.production`)

### 2. Project Structure
```
src/PLS.Web/
├── app/
│   ├── (auth)/              # Authentication routes
│   │   ├── login/
│   │   └── register/
│   ├── (dashboard)/         # Protected dashboard routes
│   │   ├── leads/
│   │   ├── cases/
│   │   ├── documents/
│   │   ├── completion/
│   │   └── archive/
│   ├── api/                 # API routes (BFF pattern)
│   ├── layout.tsx           # Root layout
│   ├── page.tsx             # Landing page
│   └── error.tsx            # Error boundary
├── components/
│   ├── ui/                  # Reusable UI components
│   ├── forms/               # Form components
│   ├── layouts/             # Layout components
│   └── features/            # Feature-specific components
├── lib/
│   ├── api/                 # Generated API client
│   ├── hooks/               # Custom React hooks
│   ├── utils/               # Utility functions
│   ├── validations/         # Zod schemas
│   └── constants/           # Constants and enums
├── public/
│   ├── images/
│   └── icons/
├── styles/
│   └── globals.css
└── types/
    └── index.ts             # Shared type definitions
```

### 3. API Client Generation
- Install OpenAPI code generator (`openapi-typescript-codegen` or `@hey-api/openapi-ts`)
- Generate TypeScript client from backend OpenAPI spec:
  - Automatic type generation for all DTOs
  - Type-safe API methods
  - Request/response interceptors
  - Error handling
- Configure API client:
  - Base URL from environment variables
  - Authentication token injection
  - Correlation ID propagation
  - Retry logic with exponential backoff
  - Request/response logging (development only)
- Place generated client in `lib/api/generated/`
- Create wrapper services for domain-specific API calls:
  - `lib/api/leads.ts` - Lead management APIs
  - `lib/api/cases.ts` - Case management APIs
  - `lib/api/documents.ts` - Document APIs
  - `lib/api/completion.ts` - Completion APIs
  - `lib/api/agents.ts` - Agent status APIs

### 4. State Management
- Implement TanStack Query (React Query) for server state:
  - Query caching and invalidation
  - Optimistic updates
  - Automatic refetching
  - Pagination and infinite scroll
  - Devtools integration (development)
- Configure query client with:
  - Default stale time (5 minutes)
  - Retry policy (3 attempts with exponential backoff)
  - Refetch on window focus
  - Error handling
- Create query hooks for each API endpoint:
  - `useLeads()`, `useLead(id)`
  - `useCases()`, `useCase(id)`
  - `useDocuments(caseId)`
- Set up Zustand for client-side state:
  - UI state (sidebar open/close, modals)
  - User preferences (theme, view mode)
  - Multi-step form state
  - WebSocket connection state
- Create store slices:
  - `stores/uiStore.ts`
  - `stores/userStore.ts`
  - `stores/formStore.ts`

### 5. Authentication Setup
- Integrate authentication library (NextAuth.js or Azure MSAL)
- Configure Azure AD B2C provider:
  - Client ID and tenant from environment variables
  - Redirect URIs for login/logout
  - Token refresh logic
  - Session management
- Implement authentication components:
  - `<SignInButton />` - Trigger sign-in flow
  - `<SignOutButton />` - Sign out and clear session
  - `<UserProfile />` - Display user info
- Create authentication utilities:
  - `lib/auth/session.ts` - Get server-side session
  - `lib/auth/client.ts` - Client-side auth hooks
  - `lib/auth/middleware.ts` - Protect API routes
- Implement route protection:
  - Server Component authentication checks
  - Redirect to login for unauthenticated users
  - Role-based access control (RBAC) utilities

### 6. Component Library Setup
- Install shadcn/ui components:
  - Button, Input, Select, Textarea
  - Card, Sheet, Dialog, Popover
  - Table, Tabs, Accordion
  - Form, Label, Checkbox, RadioGroup
  - Toast notifications
  - Loading spinners and skeletons
- Customize component theme:
  - Brand colors (primary, secondary, accent)
  - Border radius and shadows
  - Typography scale
  - Spacing system
- Create composite components:
  - `<PageHeader />` - Page title and actions
  - `<DataTable />` - Sortable, filterable table
  - `<FormField />` - Labeled input with validation
  - `<StatusBadge />` - Status indicators
  - `<LoadingState />` - Loading placeholders
  - `<EmptyState />` - Empty state illustrations

### 7. Form Management
- Set up React Hook Form for form handling
- Integrate Zod for validation schemas
- Create form validation schemas:
  - `validations/leadSchema.ts` - Lead capture validation
  - `validations/caseSchema.ts` - Case creation validation
  - `validations/documentSchema.ts` - Document upload validation
- Implement reusable form components:
  - `<FormInput />` - Text input with validation
  - `<FormSelect />` - Select dropdown with validation
  - `<FormTextarea />` - Textarea with validation
  - `<FormDatePicker />` - Date picker with validation
  - `<FormCheckbox />` - Checkbox with validation
  - `<FormFileUpload />` - File upload with preview
- Configure form error handling:
  - Field-level error messages
  - Form-level error summary
  - Server error display
  - Optimistic UI updates

### 8. Routing and Navigation
- Implement App Router structure:
  - Route groups for layout isolation
  - Dynamic routes for detail pages
  - Parallel routes for modals
  - Intercepting routes for overlays
- Create navigation components:
  - `<Sidebar />` - Main navigation sidebar
  - `<TopNav />` - Top navigation bar with user menu
  - `<Breadcrumb />` - Breadcrumb trail
  - `<TabNav />` - Tab navigation for detail pages
- Configure route middleware:
  - Authentication checks
  - Role-based access control
  - Audit logging
  - Performance monitoring

### 9. Real-time Communication
- Set up WebSocket client for real-time updates:
  - SignalR client for .NET backend
  - Connection management with reconnection logic
  - Event subscription system
- Implement real-time hooks:
  - `useAgentStatus()` - Subscribe to agent status updates
  - `useCaseUpdates(caseId)` - Subscribe to case updates
  - `useNotifications()` - Subscribe to system notifications
- Create notification system:
  - Toast notifications for real-time events
  - Notification center with history
  - Badge counts for unread items

### 10. Development Tools
- Configure linting and formatting:
  - ESLint with TypeScript rules
  - Prettier with Tailwind plugin
  - Lint-staged for pre-commit hooks
- Set up testing infrastructure:
  - Jest for unit tests
  - React Testing Library for component tests
  - Playwright for E2E tests
  - Mock Service Worker (MSW) for API mocking
- Configure build and deployment:
  - Environment-specific builds
  - Source maps for debugging
  - Bundle analysis
  - Performance budgets
- Set up Storybook (optional):
  - Component documentation
  - Visual testing
  - Design system showcase

### 11. Accessibility (a11y)
- Ensure WCAG 2.1 AA compliance:
  - Semantic HTML elements
  - ARIA labels and roles
  - Keyboard navigation support
  - Focus management
  - Color contrast requirements
  - Screen reader testing
- Implement accessibility utilities:
  - `<VisuallyHidden />` - Screen reader only text
  - `<SkipLink />` - Skip to main content
  - Focus trap for modals
  - Roving tabindex for menus

### 12. Performance Optimization
- Implement code splitting:
  - Route-based code splitting (automatic with App Router)
  - Component lazy loading
  - Dynamic imports for heavy components
- Configure caching strategies:
  - Static generation for marketing pages
  - Incremental Static Regeneration (ISR) for data pages
  - Server-side rendering for authenticated pages
- Optimize assets:
  - Image optimization with Next.js Image
  - Font subsetting and preloading
  - CSS purging with Tailwind
- Implement performance monitoring:
  - Web Vitals tracking
  - Core Web Vitals reporting
  - Custom performance marks

## Acceptance Criteria

1. ✅ Next.js application builds successfully with `npm run build`
2. ✅ Development server starts without errors (`npm run dev`)
3. ✅ TypeScript compilation successful with no type errors
4. ✅ API client successfully generated from OpenAPI spec
5. ✅ Authentication flow functional (login, logout, session management)
6. ✅ All shadcn/ui components installed and themed
7. ✅ Route protection working (redirect to login for protected routes)
8. ✅ Form validation working with Zod schemas
9. ✅ TanStack Query configured with proper caching
10. ✅ WebSocket connection successful (SignalR hub connection)
11. ✅ Responsive layout working on mobile, tablet, desktop
12. ✅ Dark mode toggle functional
13. ✅ Accessibility audit passing (Lighthouse score ≥90)
14. ✅ ESLint and Prettier checks passing
15. ✅ At least 1 page component with ≥1 passing test

## Testing Requirements

### Unit Tests
- ✅ API client wrapper functions
- ✅ Custom hooks (state management, queries)
- ✅ Utility functions (formatters, validators)
- ✅ Form validation schemas

**Minimum Coverage:** ≥80% for lib/ and components/ui/

### Component Tests
- ✅ Form components render and validate correctly
- ✅ UI components render with different props
- ✅ Navigation components handle routing
- ✅ Authentication components show correct state

**Test Framework:** React Testing Library with Jest

### E2E Tests
- ✅ User can sign in and see dashboard
- ✅ User can navigate between pages
- ✅ Forms can be submitted successfully
- ✅ Real-time updates appear in UI

**Test Framework:** Playwright with test fixtures

### Accessibility Tests
- ✅ All interactive elements keyboard accessible
- ✅ Form inputs have proper labels
- ✅ Color contrast meets WCAG AA standards
- ✅ Screen reader navigation logical

**Test Tools:** axe-core, Lighthouse CI

## Dependencies

**Upstream:** 
- 001-task-backend-scaffolding.md (requires OpenAPI spec endpoint)

**Downstream:** 
- All frontend feature implementation tasks

**External Services:**
- Backend API (for API client generation)
- Azure AD B2C (for authentication)
- SignalR hub endpoint (for real-time updates)

## Technical Decisions Required

1. **Authentication Library:** NextAuth.js vs. Azure MSAL (recommend MSAL for Azure AD B2C)
2. **Component Library:** shadcn/ui vs. Radix UI direct (recommend shadcn/ui)
3. **API Client Generator:** openapi-typescript-codegen vs. @hey-api/openapi-ts (recommend @hey-api)
4. **Testing Strategy:** Component tests vs. E2E tests priority (recommend both with 80/20 split)
5. **Deployment Target:** Vercel vs. Azure Static Web Apps vs. Azure Container Apps (recommend Azure SWA for Azure integration)

## Notes

- Follow AGENTS.md frontend guidelines
- Use TypeScript strict mode throughout
- Implement error boundaries for all route segments
- Ensure all API calls include proper error handling
- Document component API with JSDoc comments
- Create ADR for major frontend architecture decisions (state management, authentication)
- Use Conventional Commits for all commits
- Keep bundle size under performance budgets (<250KB initial JS)

## Definition of Done

- [ ] All code merged to main branch
- [ ] All unit and component tests passing with ≥80% coverage
- [ ] E2E tests passing in CI pipeline
- [ ] Accessibility audit score ≥90 (Lighthouse)
- [ ] Performance audit score ≥90 (Lighthouse)
- [ ] README.md updated with frontend setup instructions
- [ ] ADR created for authentication and state management choices
- [ ] Peer review completed with 2 approvals
- [ ] No TypeScript errors or ESLint warnings
- [ ] Application deployed to development environment
