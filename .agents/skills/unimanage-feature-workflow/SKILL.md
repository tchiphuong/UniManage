---
name: unimanage-feature-workflow
description: Step-by-step workflow for building a complete feature/module in UniManage (Backend CQRS + Frontend HeroUI v3 + E2E Test + Documentation). Use when asked to "build page X" or "create module Y".
---

# UniManage Feature Implementation Workflow

## When to use this skill

- User asks to "build page X", "create module Y", or "implement feature Z"
- Building a complete CRUD screen end-to-end
- Adding a new module to the system

## Naming Conventions (MANDATORY)

| Layer           | Convention                    | Example                                |
| --------------- | ----------------------------- | -------------------------------------- |
| Database tables | Full prefix                   | `SyUsers`, `HrEmployees`               |
| Entity classes  | Full prefix                   | `SyUsers`, `CdCategories`              |
| API URLs        | **Drop prefix**, plural nouns | `/api/v1/users`, `/api/v1/employees`   |
| Frontend routes | **Drop prefix**               | `app/system/users/page.tsx`            |
| Frontend types  | Keep prefix for DB mapping    | `export interface SyUserModel { ... }` |

## The 5-Step Workflow

### Step 1: Backend Investigation & CQRS Setup

**Before writing any code**, investigate existing backend:

```
1. grep_search for existing Controller (e.g., UsersController.cs)
2. Check which CRUD endpoints exist (GET, POST, PUT, DELETE)
3. Read Command/Query files to understand Request/Response structure
4. Identify missing endpoints that need to be created
```

**When creating new Commands/Queries:**

- Read `@.agents/skills/unimanage-backend/SKILL.md` for architecture reference
- Copy patterns from `@.agents/skills/unimanage-backend/examples/`
- ALL Commands/Queries MUST implement `ILoggableCommand`
- NO manual logging code in Handlers
- Validators in consolidated files: `{Module}QueriesValidators.cs`, `{Module}CommandValidators.cs`
- List queries MUST extend `BaseListQuery` and return `PagedResponse<T>`

**Checklist before moving to Step 2:**

- [ ] GET list (paginated) endpoint exists
- [ ] GET by id/uuid endpoint exists
- [ ] POST create endpoint exists
- [ ] PUT update endpoint exists
- [ ] DELETE endpoint exists
- [ ] All implement `ILoggableCommand`
- [ ] Validators consolidated in separate files
- [ ] Controller is thin (only routing + `_mediator.Send()`)

### Step 2: Frontend Types & API Service

**Create/update type definitions** in `types/` directory:

```typescript
// types/system.ts
export interface SyUserModel {
  uuid: string;
  username: string;
  email: string;
  status: string;
  // ... map exactly to backend Response DTO
}
```

**Create API service** in `services/` directory:

```typescript
// services/system/user.service.ts
class UserService {
  async getUsers(params): Promise<ApiResponse<PagedResult<SyUserModel>>> { ... }
  async getUserById(uuid): Promise<ApiResponse<SyUserModel>> { ... }
  async createUser(data): Promise<ApiResponse<any>> { ... }
  async updateUser(uuid, data): Promise<ApiResponse<any>> { ... }
  async deleteUsers(uuids): Promise<ApiResponse<any>> { ... }
}
```

**Rules:**

- Service must use the shared `apiClient` (handles JWT token injection and error handling)
- Type interfaces must match backend Response DTOs exactly
- Use `ApiResponse<T>` and `PagedResult<T>` generic wrappers
- The API success condition is `returnCode === 0` (NOT `200`).

### Step 3: Frontend UI (HeroUI v3)

**Read `@.agents/skills/heroui-v3/SKILL.md`** before building UI.

**UI Component Wrapper Pattern (MANDATORY)**:

- **NEVER** import UI components (Button, Input, Table, etc.) directly from `@heroui/react` in your feature pages.
- **ALWAYS** create or use a wrapper component in the `components/common/` folder (e.g., `components/common/button.tsx`).
- Configure default props (like `variant`, `size`, `color`) inside these wrapper components to maintain a consistent design system across the app.
- Then, import the component from `@/components/common` into your page.

**Feature-Specific Components (Co-location Pattern)**:

- Do NOT clutter the global `components/` folder with module-specific logic.
- **ALWAYS** place feature-specific components (e.g., Modals, Forms, custom Cards) inside `app/{module}/{entity}/components/`.
- Example: `app/system/users/components/user-form-modal.tsx`.

**Page structure** (`app/{module}/{entity}/page.tsx`):

```
┌─────────────────────────────────────────┐
│ Toolbar                                 │
│  [Search Input]  [Status Filter]  [+Add]│
├─────────────────────────────────────────┤
│ Data Table (server-side pagination)     │
│  Column1 | Column2 | Status | Actions  │
│  ...     | ...     | Chip   | Edit/Del │
│                                         │
│  [Pagination: < 1 2 3 ... >]           │
└─────────────────────────────────────────┘
```

**Required components:**

1. **Toolbar**: Search input (debounced), status filter dropdown, "Add New" button
2. **Data Table**: HeroUI `Table` with server-side pagination via `Pagination` component
3. **Form Modal**: Shared for Create and Edit, using `react-hook-form` + `zod` validation
4. **Delete Modal**: Confirmation dialog with danger styling

**Code patterns:**

- `useState` for filters, pagination, modal state
- `useCallback` for fetch functions
- `useEffect` to trigger fetch on filter/page changes
- `useDebounce` for search input (prevent API spam)
- **MANDATORY API Handling**: ALWAYS use `useApiHandler()` when calling API services. Prefer `executeApiCall()` for simple call-and-return patterns (reduces boilerplate). Use `handleResponse` / `handleError` only when you need fine-grained control. Do NOT parse API errors manually.
- **JWT Token Parsing**: Use `getTokenUsername(token)` from `@/lib/utils` to decode JWT and extract username. Do NOT manually call `atob()` + `JSON.parse()` in individual hooks.

### Step 4: E2E Testing (Playwright)

**Create test file** in `e2e-tests/` (e.g., `system.users.spec.ts`).

**Standards:**

- Use **Page Object Model (POM)** — separate UI interaction logic from test specs
- Login credentials from `process.env` (never hardcode)
- Test data cleanup after each test run

**Required test coverage:**

```
✅ View list page (table renders, pagination works)
✅ Search/filter functionality
✅ Create new record (fill form, submit, verify in table)
✅ Edit existing record (open modal, change data, save)
✅ Delete record (confirm dialog, verify removed)
✅ Form validation (submit empty form, verify error messages)
```

### Step 5: Documentation

**Create user manual** in `docs/manuals/` (e.g., `UserManagement_Manual.md`).

**Content structure:**

1. Feature overview and purpose
2. Step-by-step operation guide for each action
3. Field descriptions and validation rules
4. Common errors and troubleshooting

## Decision Tree

```
User asks "Build page X"
│
├── Backend exists?
│   ├── YES → Step 1: Investigate existing endpoints
│   └── NO  → Step 1: Create full CQRS (Command + Query + Controller)
│
├── Step 2: Create types + service (always needed for new pages)
│
├── Step 3: Build UI
│   ├── Simple CRUD? → Table + Modal pattern
│   └── Complex? → Discuss with user first
│
├── Step 4: Write E2E tests
│
└── Step 5: Write documentation
```
