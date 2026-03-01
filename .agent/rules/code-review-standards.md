# Code Review Standards

**Activation**: Model Decision - Apply when reviewing code or preparing code for review

This rule ensures consistent, thorough code reviews that maintain code quality.

## Review Checklist

### General Quality

-   [ ] Code follows project conventions and patterns
-   [ ] Code is readable and well-organized
-   [ ] No commented-out code (remove or explain why)
-   [ ] No console.log or debugging code in production
-   [ ] No hardcoded values (use constants/config)
-   [ ] No sensitive data exposed (passwords, API keys)

### Backend (.NET/C#)

**Architecture & Patterns:**

-   [ ] Command/Query inherits BaseCommand/BaseQuery
-   [ ] Handler follows CQRS principles
-   [ ] Repository pattern used correctly (if applicable)
-   [ ] Proper separation of concerns

**Code Quality:**

-   [ ] Regions properly organized (#region Command, #region Validator, #region Handler)
-   [ ] ResponseHelper used for all API responses
-   [ ] Utilities used instead of reimplementing (PasswordHelper, ValidationHelper, etc.)
-   [ ] No duplicate code (DRY principle)
-   [ ] Method names are descriptive and follow conventions

**Database & Data Access:**

-   [ ] Using statements for DbContext disposal
-   [ ] Parameterized queries (no SQL injection risk)
-   [ ] Transactions used for Commands
-   [ ] No SELECT \* (specify columns)
-   [ ] Proper indexing for queries
-   [ ] DataRowVersion checked for updates

**Validation:**

-   [ ] FluentValidation validator created
-   [ ] All required fields validated
-   [ ] Business rules enforced
-   [ ] Async validation for database checks (MustAsync)
-   [ ] Cascade mode used appropriately

**Error Handling:**

-   [ ] Try-catch blocks in Handlers
-   [ ] Proper error messages
-   [ ] Transactions rolled back on error
-   [ ] Errors logged with UniLogManager

**Logging:**

-   [ ] CoreLogModel initialized with HeaderInfo
-   [ ] Parameters logged (without sensitive data)
-   [ ] Result logged on success
-   [ ] Errors logged on failure
-   [ ] UniLogManager.WriteApiLog() called in finally

**API Design:**

-   [ ] RESTful URLs (nouns, not verbs)
-   [ ] Correct HTTP methods (GET, POST, PUT, DELETE)
-   [ ] HeaderInfo assigned in Controller
-   [ ] Consistent response format

**Security:**

-   [ ] PasswordHelper used for password hashing
-   [ ] Input validation prevents injection attacks
-   [ ] Authorization checks in place
-   [ ] Sensitive data masked in logs

**Performance:**

-   [ ] Efficient database queries
-   [ ] No N+1 query problems
-   [ ] Async/await used properly
-   [ ] Large datasets paginated

### Frontend (React/TypeScript)

**Architecture & Patterns:**

-   [ ] Component structure appropriate (common/layout/features)
-   [ ] Services used for API calls
-   [ ] TanStack Query for data fetching
-   [ ] React Hook Form + Zod for forms

**Code Quality:**

-   [ ] TypeScript types defined (no `any`)
-   [ ] Imports organized (React → libraries → internal)
-   [ ] Component naming follows PascalCase
-   [ ] Props interface defined
-   [ ] Hooks follow rules of hooks

**HeroUI v3:**

-   [ ] Imports from @heroui/react (not NextUI)
-   [ ] Uses onPress instead of onClick
-   [ ] Proper prop names (isDisabled, isRequired, etc.)
-   [ ] Accessibility attributes (aria-label, etc.)

**Forms:**

-   [ ] React Hook Form used
-   [ ] Zod schema matches backend validation
-   [ ] Error messages displayed
-   [ ] Loading states during submission
-   [ ] Success/error notifications shown

**Data Fetching:**

-   [ ] TanStack Query used
-   [ ] Query keys defined in query-keys.ts
-   [ ] Loading states handled
-   [ ] Error states handled
-   [ ] Cache invalidation on mutations

**Error Handling:**

-   [ ] API errors caught and displayed
-   [ ] Error boundaries for critical components
-   [ ] Fallback UI for errors
-   [ ] Toast notifications for user feedback

**State Management:**

-   [ ] useState for local state
-   [ ] TanStack Query for server state
-   [ ] Context for global state (if needed)
-   [ ] No prop drilling

**Styling:**

-   [ ] Tailwind utilities used
-   [ ] Responsive design (md:, lg: breakpoints)
-   [ ] Dark mode compatible
-   [ ] Consistent spacing and colors

**Internationalization:**

-   [ ] next-intl used for all text
-   [ ] Translation keys defined
-   [ ] No hardcoded strings

**Performance:**

-   [ ] Components memoized if needed (React.memo)
-   [ ] Expensive calculations use useMemo
-   [ ] Event handlers use useCallback
-   [ ] Images optimized

**Accessibility:**

-   [ ] ARIA labels present
-   [ ] Keyboard navigation works
-   [ ] Focus management correct
-   [ ] Screen reader compatible

### Database (SQL Server)

**Schema Design:**

-   [ ] Table naming follows conventions (PascalCase, plural)
-   [ ] Column naming follows conventions (PascalCase, suffixes)
-   [ ] Primary key is Id (IDENTITY)
-   [ ] Audit columns present (CreatedBy, CreatedAt, UpdatedBy, UpdatedAt, DataRowVersion)
-   [ ] Appropriate data types (NVARCHAR for Vietnamese, DATETIME2(3))

**Constraints:**

-   [ ] Primary key constraint named correctly (PK\_{Table})
-   [ ] Foreign keys named correctly (FK*{Table}*{ReferencedTable})
-   [ ] Unique constraints for unique columns
-   [ ] Check constraints for business rules
-   [ ] Proper cascade rules (CASCADE, SET NULL, NO ACTION)

**Indexes:**

-   [ ] Indexes on foreign keys
-   [ ] Indexes on frequently queried columns
-   [ ] Index naming follows convention (IX*{Table}*{Column})
-   [ ] No over-indexing

**Migrations:**

-   [ ] Wrapped in transaction
-   [ ] TRY/CATCH block present
-   [ ] Idempotent (IF EXISTS/NOT EXISTS checks)
-   [ ] Rollback script available
-   [ ] Documentation updated

**Queries:**

-   [ ] No SELECT \*
-   [ ] Parameterized (prevent SQL injection)
-   [ ] Efficient (proper JOINs, WHERE clauses)
-   [ ] Indexed columns in WHERE/ORDER BY

## Review Process

### As Code Author

**Before requesting review:**

1. Self-review your changes
2. Run all tests locally
3. Check for console errors/warnings
4. Verify code follows conventions
5. Update documentation
6. Write clear PR description

**PR Description Template:**

```markdown
## What

[Brief description of changes]

## Why

[Reason for changes]

## How

[Technical explanation]

## Testing

-   [ ] Unit tests added/updated
-   [ ] Integration tests added/updated
-   [ ] Tested manually in dev
-   [ ] Tested in staging (if applicable)

## Screenshots

[Add screenshots for UI changes]

## Checklist

-   [ ] Code follows project standards
-   [ ] Tests passing
-   [ ] Documentation updated
-   [ ] No console errors
-   [ ] Self-reviewed
```

### As Reviewer

**Review checklist:**

1. **Understand the context** - Read PR description, related issues
2. **Check architecture** - Does it fit the overall design?
3. **Review logic** - Is it correct and efficient?
4. **Check tests** - Are there adequate tests?
5. **Verify conventions** - Does it follow project standards?
6. **Test locally** - Pull and run the code
7. **Check edge cases** - What could go wrong?

**Feedback guidelines:**

-   Be respectful and constructive
-   Explain the "why" behind suggestions
-   Distinguish between must-fix and nice-to-have
-   Suggest alternatives when possible
-   Praise good code

**Feedback examples:**

✅ **Good feedback:**

> "This validator is missing a check for duplicate email. Consider adding:
>
> ```csharp
> .MustAsync(async (email, ct) => !await IsEmailExistsAsync(email))
> .WithMessage("Email already exists");
> ```
>
> This prevents duplicate accounts."

❌ **Bad feedback:**

> "This is wrong."

✅ **Good feedback:**

> "Nice use of DatabaseHelper.QueryPagingAsync here! This handles pagination consistently with the rest of the app."

**Review labels:**

-   🔴 **Critical** - Must fix before merge (security, bugs, breaking changes)
-   🟡 **Important** - Should fix (conventions, best practices)
-   🟢 **Optional** - Nice to have (refactoring, suggestions)
-   💡 **Question** - Need clarification

## Common Issues to Watch For

### Backend

1. **Missing validation** - No validator or incomplete rules
2. **SQL injection risk** - String concatenation instead of parameters
3. **Missing error handling** - No try-catch in Handler
4. **Memory leaks** - Not disposing DbContext
5. **Missing logging** - No CoreLogModel or UniLogManager
6. **Hardcoded values** - Should be in config
7. **N+1 queries** - Multiple database calls in loop
8. **Missing transaction** - Multiple operations without transaction

### Frontend

1. **Using `any`** - Should use proper TypeScript types
2. **Not handling errors** - Missing error states
3. **Not handling loading** - Missing loading states
4. **Hardcoded text** - Should use next-intl
5. **Direct API calls** - Should use service class + TanStack Query
6. **Prop drilling** - Should use Context or lift state
7. **Missing accessibility** - No ARIA labels
8. **Wrong import** - Importing from @nextui-org instead of @heroui

### Database

1. **Missing indexes** - Slow queries
2. **Missing constraints** - Data integrity issues
3. **No audit columns** - Can't track changes
4. **Wrong data types** - VARCHAR instead of NVARCHAR
5. **Missing transaction** - Migration not wrapped
6. **Not idempotent** - Migration fails on re-run

## Approval Criteria

**Approve if:**

-   ✅ All critical issues resolved
-   ✅ Tests passing
-   ✅ Follows project conventions
-   ✅ Code is maintainable
-   ✅ Documentation updated

**Request changes if:**

-   ❌ Critical issues present (security, bugs)
-   ❌ Tests failing
-   ❌ Major convention violations
-   ❌ Missing required features

**Comment only if:**

-   💡 Optional suggestions
-   💡 Questions for clarification
-   💡 Future improvements

## Post-Review

**After approval:**

1. Squash commits if needed
2. Merge to main branch
3. Delete feature branch
4. Close related issues
5. Deploy to staging/production
6. Monitor for issues

**After merge:**

1. Verify deployment successful
2. Check logs for errors
3. Verify features work as expected
4. Update team on deployment

## Standards Checklist

✅ Code follows CQRS patterns (backend)
✅ Code follows HeroUI v3 conventions (frontend)
✅ All tests passing
✅ No console errors or warnings
✅ Proper error handling
✅ Logging implemented correctly
✅ Security best practices followed
✅ Performance considerations addressed
✅ Accessibility requirements met
✅ Documentation updated
✅ No hardcoded values
✅ No sensitive data exposed
