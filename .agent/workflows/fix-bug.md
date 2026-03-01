# Fix Bug

Systematically diagnose and fix a bug in the UniManage system.

## Steps

### 1. Reproduce the Bug

**Gather information from user report:**

-   What is the expected behavior?
-   What actually happens?
-   What steps trigger the bug?
-   Error messages or screenshots?
-   Which environment? (dev, staging, production)
-   User role and permissions?

**Reproduce locally:**

```bash
# Start backend
cd backend/src/UniManage.Api
dotnet run

# Start frontend
cd frontend/uni-manage
npm run dev

# Follow exact steps to reproduce
# Document what you observe
```

### 2. Identify the Root Cause

**Check logs first:**

**Backend logs:**

```bash
# Check today's logs
cat backend/logs/$(date +%Y-%m-%d)/error-*.log

# Search for specific error
grep -r "error text" backend/logs/

# Check specific API logs
cat backend/logs/$(date +%Y-%m-%d)/Users/Users-Create.log
```

**Frontend console:**

-   Open browser DevTools (F12)
-   Check Console tab for JavaScript errors
-   Check Network tab for failed API calls
-   Check Application tab for localStorage issues

**Database issues:**

```sql
-- Check for constraint violations
SELECT * FROM sys.messages WHERE message_id = LAST_ERROR_NUMBER();

-- Check for deadlocks
SELECT * FROM sys.dm_exec_requests WHERE blocking_session_id <> 0;

-- Check recent errors
SELECT * FROM sys.dm_exec_query_stats ORDER BY last_execution_time DESC;
```

**Common bug locations:**

**Backend:**

-   Handler logic errors
-   Validator issues (FluentValidation)
-   Database query errors (Dapper)
-   Missing null checks
-   Concurrency conflicts (DataRowVersion)
-   Transaction handling

**Frontend:**

-   Form validation not matching backend
-   API response handling
-   State management issues
-   Race conditions with async calls
-   Missing error handling

**Database:**

-   Missing indexes (slow queries)
-   Constraint violations
-   Data type mismatches
-   Missing foreign keys

### 3. Create a Test Case

**Backend integration test:**

Location: `UniManage.Tests/Integration/{Feature}Tests.cs`

```csharp
[Fact]
public async Task CreateUser_WithDuplicateEmail_ShouldReturnError()
{
    // Arrange
    var command = new CreateUserCommand
    {
        Username = "testuser",
        Email = "existing@example.com",
        Password = "Password123!"
    };

    // Act
    var result = await _mediator.Send(command);

    // Assert
    Assert.Equal(CoreApiReturnCode.ValidationError, result.ReturnCode);
    Assert.Contains("Email already exists", result.Errors);
}
```

**Frontend unit test:**

Location: `frontend/uni-manage/__tests__/{Component}.test.tsx`

```tsx
import { render, screen, fireEvent } from "@testing-library/react";
import { UserForm } from "@/components/features/users/UserForm";

describe("UserForm", () => {
    it("should show validation error for invalid email", async () => {
        render(<UserForm />);

        const emailInput = screen.getByLabelText("Email");
        fireEvent.change(emailInput, { target: { value: "invalid-email" } });
        fireEvent.blur(emailInput);

        expect(await screen.findByText("Invalid email format")).toBeInTheDocument();
    });
});
```

### 4. Fix the Bug

**Backend fixes:**

**Example: Fix validation error**

```csharp
#region Validator

public sealed class CreateUserValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .Must(ValidationHelper.IsValidEmail).WithMessage("Invalid email format")
            // ADD: Check for duplicate email
            .MustAsync(async (email, cancel) => !await IsEmailExistsAsync(email))
            .WithMessage("Email already exists");
    }

    private static async Task<bool> IsEmailExistsAsync(string email)
    {
        using (var dbContext = new DbContext())
        {
            return await dbContext.ExecuteScalarAsync<bool>(
                "SELECT CASE WHEN EXISTS(SELECT 1 FROM sy_users WHERE Email = @Email) THEN 1 ELSE 0 END",
                new { Email = email });
        }
    }
}

#endregion
```

**Example: Fix null reference**

```csharp
public async Task<ApiResponse<Response>> Handle(GetUserQuery request, CancellationToken ct)
{
    using (var dbContext = new DbContext())
    {
        var user = await dbContext.QuerySingleOrDefaultAsync<UserDto>(
            "SELECT * FROM sy_users WHERE Id = @Id",
            new { Id = request.Id },
            ct);

        // FIX: Check for null before accessing
        if (user == null)
        {
            return ResponseHelper.NotFound<Response>("User not found");
        }

        return ResponseHelper.Success(user, "User retrieved");
    }
}
```

**Example: Fix concurrency issue**

```csharp
var rowsAffected = await dbContext.ExecuteAsync(
    @"UPDATE sy_users
      SET DisplayName = @DisplayName, UpdatedBy = @UpdatedBy, UpdatedAt = GETDATE()
      WHERE Id = @Id AND DataRowVersion = @RowVersion",
    new { Id = id, DisplayName = name, UpdatedBy = user, RowVersion = rowVersion },
    ct);

// FIX: Handle concurrency conflict
if (rowsAffected == 0)
{
    // Check if record exists
    var exists = await dbContext.ExecuteScalarAsync<bool>(
        "SELECT CASE WHEN EXISTS(SELECT 1 FROM sy_users WHERE Id = @Id) THEN 1 ELSE 0 END",
        new { Id = id });

    if (!exists)
        return ResponseHelper.NotFound<Response>("User not found");
    else
        return ResponseHelper.Error<Response>("User was modified by another user. Please refresh and try again.");
}
```

**Frontend fixes:**

**Example: Fix form validation**

```tsx
// BEFORE: Validation not matching backend
const schema = z.object({
    email: z.string().email(),
});

// AFTER: Match backend validation
const schema = z.object({
    email: z
        .string()
        .min(1, "Email is required")
        .email("Invalid email format")
        .max(255, "Email must be less than 255 characters"),
});
```

**Example: Fix API error handling**

```tsx
// BEFORE: Not handling errors
const { data } = useQuery({
    queryKey: ["users"],
    queryFn: UserService.getList,
});

// AFTER: Proper error handling
const { data, error, isError } = useQuery({
    queryKey: ["users"],
    queryFn: UserService.getList,
    retry: 3,
    retryDelay: 1000,
});

if (isError) {
    return (
        <div className="text-danger">{error.response?.data?.message || "Failed to load users"}</div>
    );
}
```

**Example: Fix race condition**

```tsx
// BEFORE: Race condition with multiple API calls
useEffect(() => {
    fetchUsers();
    fetchDepartments();
}, []);

// AFTER: Use TanStack Query to handle concurrent calls
const { data: users } = useQuery({ queryKey: ["users"], queryFn: UserService.getList });
const { data: departments } = useQuery({
    queryKey: ["departments"],
    queryFn: DepartmentService.getList,
});
```

**Database fixes:**

**Example: Add missing index**

```sql
-- Find slow query
SELECT TOP 10
    qs.execution_count,
    qs.total_elapsed_time / 1000000 AS total_elapsed_time_sec,
    SUBSTRING(qt.text, (qs.statement_start_offset/2)+1,
        ((CASE qs.statement_end_offset
            WHEN -1 THEN DATALENGTH(qt.text)
            ELSE qs.statement_end_offset
        END - qs.statement_start_offset)/2)+1) AS statement_text
FROM sys.dm_exec_query_stats qs
CROSS APPLY sys.dm_exec_sql_text(qs.sql_handle) qt
ORDER BY qs.total_elapsed_time DESC;

-- FIX: Add missing index
CREATE INDEX IX_Users_Email ON sy_users(Email);
```

**Example: Fix constraint violation**

```sql
-- If foreign key constraint fails, add missing data
INSERT INTO hr_departments (DepartmentCode, DepartmentName, CreatedBy, CreatedAt)
VALUES ('IT', 'Information Technology', 'SYSTEM', GETDATE());

-- Then update the foreign key
UPDATE hr_employees
SET DepartmentId = (SELECT Id FROM hr_departments WHERE DepartmentCode = 'IT')
WHERE DepartmentCode = 'IT';
```

### 5. Test the Fix

**Test locally:**

```bash
# Run backend tests
cd backend/src/UniManage.Tests
dotnet test

# Run frontend tests
cd frontend/uni-manage
npm test

# Manual testing
# - Reproduce original steps
# - Verify bug is fixed
# - Test edge cases
# - Test related features
```

**Test in staging:**

```bash
# Deploy to staging
/deploy-fullstack

# Verify in staging environment
# - Test with production-like data
# - Test with different user roles
# - Monitor logs for errors
```

### 6. Code Review

**Create Pull Request with:**

-   Clear description of bug
-   Steps to reproduce (before fix)
-   Explanation of root cause
-   Description of fix
-   Test cases added
-   Screenshots/logs showing fix works

**PR template:**

```markdown
## Bug Description

[Describe the bug]

## Root Cause

[Explain what caused the bug]

## Fix

[Describe how you fixed it]

## Testing

-   [ ] Added unit tests
-   [ ] Added integration tests
-   [ ] Tested manually in dev
-   [ ] Tested in staging
-   [ ] Verified logs show no errors

## Screenshots

[Before/after screenshots]

## Related Issues

Fixes #123
```

### 7. Deploy to Production

Follow the deployment workflow:

```bash
/deploy-fullstack
```

**Monitor closely after deployment:**

-   Watch logs for 30 minutes
-   Check error rates
-   Verify fix works in production
-   Be ready to rollback if issues

### 8. Document the Bug

**Add to bug tracking system:**

-   Bug ID
-   Severity
-   Root cause
-   Fix description
-   Files changed
-   Deployment date

**Update knowledge base:**

-   Add to common issues document
-   Update troubleshooting guide
-   Share learnings with team

### 9. Prevent Future Bugs

**Add preventive measures:**

**Backend:**

-   Add validation if missing
-   Add null checks
-   Add error handling
-   Add logging for debugging
-   Add unit tests

**Frontend:**

-   Add TypeScript types
-   Add form validation
-   Add error boundaries
-   Add loading states
-   Add unit tests

**Database:**

-   Add indexes for performance
-   Add constraints for data integrity
-   Add documentation for complex queries

### 10. Verify Checklist

✅ Bug reproduced locally
✅ Root cause identified
✅ Logs checked
✅ Test case created
✅ Fix implemented
✅ Tests passing
✅ Code reviewed
✅ Deployed to staging
✅ Verified in staging
✅ Deployed to production
✅ Monitored after deployment
✅ Documentation updated
✅ Preventive measures added

## Common Bug Patterns

### Backend Bugs

1. **Null Reference**: Not checking for null before accessing
2. **Validation**: Frontend and backend validation don't match
3. **Concurrency**: Not handling DataRowVersion conflicts
4. **Transaction**: Not wrapping multiple operations in transaction
5. **SQL Injection**: Not using parameterized queries (rare with Dapper)
6. **Memory Leak**: Not disposing DbContext

### Frontend Bugs

1. **State Management**: Component state not updating
2. **Race Condition**: Multiple async calls interfering
3. **Form Validation**: Not matching backend rules
4. **Error Handling**: Not catching API errors
5. **Type Safety**: Using `any` instead of proper types
6. **Memory Leak**: Not cleaning up useEffect

### Database Bugs

1. **Performance**: Missing indexes on frequently queried columns
2. **Data Integrity**: Missing or incorrect foreign keys
3. **Deadlock**: Multiple transactions locking same resources
4. **Constraint Violation**: Data doesn't meet constraint rules

## Summary

This workflow provides a systematic approach to:

-   Reproduce and diagnose bugs
-   Identify root causes through logs and debugging
-   Implement proper fixes
-   Add test cases
-   Deploy safely with monitoring
-   Document and prevent future occurrences
