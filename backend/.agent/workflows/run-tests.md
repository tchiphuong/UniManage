# Run Tests

Run and manage tests for the UniManage system (backend and frontend).

## Steps

### 1. Determine Test Scope

Ask the user:

-   Which tests to run? (Unit, Integration, All)
-   Which layer? (Backend, Frontend, Both)
-   Specific feature or module?
-   Run in watch mode?

### 2. Backend Tests (.NET)

**Test Structure:**

```
UniManage.Tests/
├── Unit/              # Unit tests (isolated)
│   ├── Validators/    # FluentValidation tests
│   ├── Utilities/     # Helper class tests
│   └── Handlers/      # Handler logic tests
├── Integration/       # Integration tests (with DB)
│   ├── Commands/      # Command handler tests
│   ├── Queries/       # Query handler tests
│   └── Api/          # API endpoint tests
└── TestFixtures/      # Shared test setup
```

**Run all backend tests:**

```bash
cd backend/src/UniManage.Tests
dotnet test

# With coverage
dotnet test --collect:"XPlat Code Coverage"

# Specific test
dotnet test --filter "FullyQualifiedName~CreateUserCommandTests"

# By category
dotnet test --filter "Category=Unit"
```

**Run tests in watch mode:**

```bash
dotnet watch test
```

### 3. Write Unit Tests (Backend)

**Validator Test Example:**

```csharp
using FluentValidation.TestHelper;
using Xunit;

namespace UniManage.Tests.Unit.Validators;

public class CreateUserValidatorTests
{
    private readonly CreateUserValidator _validator;

    public CreateUserValidatorTests()
    {
        _validator = new CreateUserValidator();
    }

    [Fact]
    public void Username_WhenEmpty_ShouldHaveValidationError()
    {
        // Arrange
        var command = new CreateUserCommand { Username = "" };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Username)
              .WithErrorMessage("Username is required");
    }

    [Theory]
    [InlineData("ab")]           // Too short
    [InlineData("a" + new string('b', 51))] // Too long
    [InlineData("user@123")]     // Invalid characters
    public void Username_WhenInvalid_ShouldHaveValidationError(string username)
    {
        // Arrange
        var command = new CreateUserCommand { Username = username };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Username);
    }

    [Theory]
    [InlineData("user123")]
    [InlineData("john-doe")]
    [InlineData("admin_user")]
    public void Username_WhenValid_ShouldNotHaveValidationError(string username)
    {
        // Arrange
        var command = new CreateUserCommand
        {
            Username = username,
            Email = "test@example.com",
            Password = "Password123!",
            DisplayName = "Test User"
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x.Username);
    }
}
```

**Utility Test Example:**

```csharp
using Xunit;
using UniManage.Core.Utilities;

namespace UniManage.Tests.Unit.Utilities;

public class PasswordHelperTests
{
    [Fact]
    public void HashPassword_ShouldReturnDifferentHashForSamePassword()
    {
        // Arrange
        var password = "TestPassword123!";

        // Act
        var hash1 = PasswordHelper.HashPassword(password);
        var hash2 = PasswordHelper.HashPassword(password);

        // Assert
        Assert.NotEqual(hash1, hash2); // BCrypt uses salt
    }

    [Fact]
    public void VerifyPassword_WithCorrectPassword_ShouldReturnTrue()
    {
        // Arrange
        var password = "TestPassword123!";
        var hash = PasswordHelper.HashPassword(password);

        // Act
        var result = PasswordHelper.VerifyPassword(password, hash);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void VerifyPassword_WithWrongPassword_ShouldReturnFalse()
    {
        // Arrange
        var password = "TestPassword123!";
        var wrongPassword = "WrongPassword456!";
        var hash = PasswordHelper.HashPassword(password);

        // Act
        var result = PasswordHelper.VerifyPassword(wrongPassword, hash);

        // Assert
        Assert.False(result);
    }

    [Theory]
    [InlineData("Password123!")]    // Valid
    [InlineData("Abcd1234!")]       // Valid
    [InlineData("MyP@ssw0rd")]      // Valid
    public void IsValidPassword_WithValidPassword_ShouldReturnTrue(string password)
    {
        // Act
        var result = PasswordHelper.IsValidPassword(password);

        // Assert
        Assert.True(result);
    }

    [Theory]
    [InlineData("short")]           // Too short
    [InlineData("nouppercase123!")] // No uppercase
    [InlineData("NOLOWERCASE123!")] // No lowercase
    [InlineData("NoNumber!")]       // No number
    public void IsValidPassword_WithInvalidPassword_ShouldReturnFalse(string password)
    {
        // Act
        var result = PasswordHelper.IsValidPassword(password);

        // Assert
        Assert.False(result);
    }
}
```

### 4. Write Integration Tests (Backend)

**Setup Test Database:**

```csharp
using Microsoft.Extensions.Configuration;
using Xunit;

namespace UniManage.Tests.Integration;

public class TestDatabaseFixture : IAsyncLifetime
{
    public IConfiguration Configuration { get; private set; }

    public TestDatabaseFixture()
    {
        Configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.Test.json")
            .Build();
    }

    public async Task InitializeAsync()
    {
        // Setup test database
        // Run migrations if needed
    }

    public async Task DisposeAsync()
    {
        // Cleanup test database
    }
}

[CollectionDefinition("Database")]
public class DatabaseCollection : ICollectionFixture<TestDatabaseFixture>
{
    // This class has no code, and is never created.
}
```

**Command Handler Test:**

```csharp
using Xunit;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace UniManage.Tests.Integration.Commands;

[Collection("Database")]
public class CreateUserCommandTests
{
    private readonly IMediator _mediator;
    private readonly TestDatabaseFixture _fixture;

    public CreateUserCommandTests(TestDatabaseFixture fixture)
    {
        _fixture = fixture;

        // Setup DI container with test configuration
        var services = new ServiceCollection();
        services.AddMediatR(typeof(CreateUserCommandHandler).Assembly);
        // Add other dependencies...

        var serviceProvider = services.BuildServiceProvider();
        _mediator = serviceProvider.GetRequiredService<IMediator>();
    }

    [Fact]
    public async Task CreateUser_WithValidData_ShouldSucceed()
    {
        // Arrange
        var command = new CreateUserCommand
        {
            Username = $"testuser_{Guid.NewGuid():N}",
            DisplayName = "Test User",
            Email = $"test_{Guid.NewGuid():N}@example.com",
            Password = "Password123!",
            HeaderInfo = new HeaderInfo
            {
                Username = "TestSystem",
                IpAddress = "127.0.0.1"
            }
        };

        // Act
        var result = await _mediator.Send(command);

        // Assert
        Assert.Equal(0, result.ReturnCode);
        Assert.NotNull(result.Data);
        Assert.True(result.Data.Id > 0);
    }

    [Fact]
    public async Task CreateUser_WithDuplicateUsername_ShouldFail()
    {
        // Arrange - Create first user
        var username = $"testuser_{Guid.NewGuid():N}";
        var command1 = new CreateUserCommand
        {
            Username = username,
            DisplayName = "Test User 1",
            Email = $"test1_{Guid.NewGuid():N}@example.com",
            Password = "Password123!",
            HeaderInfo = new HeaderInfo { Username = "TestSystem" }
        };
        await _mediator.Send(command1);

        // Arrange - Try to create duplicate
        var command2 = new CreateUserCommand
        {
            Username = username, // Same username
            DisplayName = "Test User 2",
            Email = $"test2_{Guid.NewGuid():N}@example.com",
            Password = "Password123!",
            HeaderInfo = new HeaderInfo { Username = "TestSystem" }
        };

        // Act
        var result = await _mediator.Send(command2);

        // Assert
        Assert.Equal(CoreApiReturnCode.ValidationError, result.ReturnCode);
        Assert.Contains("Username is already taken", result.Errors);
    }
}
```

**Query Handler Test:**

```csharp
[Collection("Database")]
public class GetUserListQueryTests
{
    private readonly IMediator _mediator;

    [Fact]
    public async Task GetUserList_WithKeyword_ShouldReturnFilteredResults()
    {
        // Arrange - Create test users
        await CreateTestUser("john_doe", "John Doe");
        await CreateTestUser("jane_smith", "Jane Smith");
        await CreateTestUser("bob_jones", "Bob Jones");

        var query = new GetUserListQuery
        {
            Keyword = "john",
            PageIndex = 1,
            PageSize = 20,
            HeaderInfo = new HeaderInfo { Username = "TestSystem" }
        };

        // Act
        var result = await _mediator.Send(query);

        // Assert
        Assert.Equal(0, result.ReturnCode);
        Assert.NotNull(result.Data);
        Assert.True(result.Data.Items.Count > 0);
        Assert.Contains(result.Data.Items, u => u.Username.Contains("john"));
    }

    [Fact]
    public async Task GetUserList_WithPagination_ShouldReturnCorrectPage()
    {
        // Arrange
        var query = new GetUserListQuery
        {
            PageIndex = 2,
            PageSize = 10,
            HeaderInfo = new HeaderInfo { Username = "TestSystem" }
        };

        // Act
        var result = await _mediator.Send(query);

        // Assert
        Assert.Equal(0, result.ReturnCode);
        Assert.NotNull(result.Data?.Paging);
        Assert.Equal(2, result.Data.Paging.PageIndex);
        Assert.Equal(10, result.Data.Paging.PageSize);
    }

    private async Task CreateTestUser(string username, string displayName)
    {
        var command = new CreateUserCommand
        {
            Username = username,
            DisplayName = displayName,
            Email = $"{username}@example.com",
            Password = "Password123!",
            HeaderInfo = new HeaderInfo { Username = "TestSystem" }
        };
        await _mediator.Send(command);
    }
}
```

### 5. Frontend Tests (React/TypeScript)

**Test Structure:**

```
frontend/uni-manage/__tests__/
├── components/
│   ├── common/
│   ├── layout/
│   └── features/
├── services/
├── hooks/
└── utils/
```

**Setup testing library:**

```bash
npm install --save-dev @testing-library/react @testing-library/jest-dom @testing-library/user-event vitest
```

**Run frontend tests:**

```bash
cd frontend/uni-manage

# Run all tests
npm test

# Run in watch mode
npm test -- --watch

# Run with coverage
npm test -- --coverage

# Run specific test
npm test UserForm.test.tsx
```

### 6. Component Tests (Frontend)

**Setup test utilities:**

```tsx
// __tests__/test-utils.tsx
import { render } from "@testing-library/react";
import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import { NextIntlClientProvider } from "next-intl";

const messages = {
    Common: {
        save: "Save",
        cancel: "Cancel",
    },
};

export function renderWithProviders(component: React.ReactElement) {
    const queryClient = new QueryClient({
        defaultOptions: {
            queries: { retry: false },
            mutations: { retry: false },
        },
    });

    return render(
        <QueryClientProvider client={queryClient}>
            <NextIntlClientProvider locale="en" messages={messages}>
                {component}
            </NextIntlClientProvider>
        </QueryClientProvider>
    );
}
```

**Form Component Test:**

```tsx
// __tests__/components/features/users/UserForm.test.tsx
import { screen, fireEvent, waitFor } from "@testing-library/react";
import userEvent from "@testing-library/user-event";
import { renderWithProviders } from "@/__tests__/test-utils";
import { UserForm } from "@/components/features/users/UserForm";
import { UserService } from "@/services/UserService";

// Mock the service
vi.mock("@/services/UserService");

describe("UserForm", () => {
    beforeEach(() => {
        vi.clearAllMocks();
    });

    it("should render form fields", () => {
        renderWithProviders(<UserForm />);

        expect(screen.getByLabelText("Username")).toBeInTheDocument();
        expect(screen.getByLabelText("Email")).toBeInTheDocument();
        expect(screen.getByLabelText("Password")).toBeInTheDocument();
        expect(screen.getByRole("button", { name: "Submit" })).toBeInTheDocument();
    });

    it("should show validation errors for empty fields", async () => {
        const user = userEvent.setup();
        renderWithProviders(<UserForm />);

        const submitButton = screen.getByRole("button", { name: "Submit" });
        await user.click(submitButton);

        await waitFor(() => {
            expect(screen.getByText("Username is required")).toBeInTheDocument();
            expect(screen.getByText("Email is required")).toBeInTheDocument();
        });
    });

    it("should show validation error for invalid email", async () => {
        const user = userEvent.setup();
        renderWithProviders(<UserForm />);

        const emailInput = screen.getByLabelText("Email");
        await user.type(emailInput, "invalid-email");
        await user.tab(); // Blur

        await waitFor(() => {
            expect(screen.getByText("Invalid email format")).toBeInTheDocument();
        });
    });

    it("should submit form with valid data", async () => {
        const user = userEvent.setup();
        const mockCreate = vi.fn().mockResolvedValue({
            returnCode: 0,
            data: { id: 1 },
            message: "User created",
        });
        UserService.create = mockCreate;

        const onSuccess = vi.fn();
        renderWithProviders(<UserForm onSuccess={onSuccess} />);

        await user.type(screen.getByLabelText("Username"), "testuser");
        await user.type(screen.getByLabelText("Email"), "test@example.com");
        await user.type(screen.getByLabelText("Password"), "Password123!");

        const submitButton = screen.getByRole("button", { name: "Submit" });
        await user.click(submitButton);

        await waitFor(() => {
            expect(mockCreate).toHaveBeenCalledWith({
                username: "testuser",
                email: "test@example.com",
                password: "Password123!",
            });
            expect(onSuccess).toHaveBeenCalled();
        });
    });

    it("should show loading state during submission", async () => {
        const user = userEvent.setup();
        const mockCreate = vi
            .fn()
            .mockImplementation(() => new Promise((resolve) => setTimeout(resolve, 1000)));
        UserService.create = mockCreate;

        renderWithProviders(<UserForm />);

        await user.type(screen.getByLabelText("Username"), "testuser");
        await user.type(screen.getByLabelText("Email"), "test@example.com");
        await user.type(screen.getByLabelText("Password"), "Password123!");

        const submitButton = screen.getByRole("button", { name: "Submit" });
        await user.click(submitButton);

        expect(submitButton).toBeDisabled();
    });
});
```

**Service Test:**

```tsx
// __tests__/services/UserService.test.tsx
import { UserService } from "@/services/UserService";
import { httpClient } from "@/lib/http-client";

vi.mock("@/lib/http-client");

describe("UserService", () => {
    beforeEach(() => {
        vi.clearAllMocks();
    });

    describe("getList", () => {
        it("should call API with correct parameters", async () => {
            const mockResponse = {
                data: {
                    returnCode: 0,
                    data: {
                        items: [{ id: 1, username: "test" }],
                        paging: { pageIndex: 1, pageSize: 20, totalItems: 1 },
                    },
                },
            };
            httpClient.get = vi.fn().mockResolvedValue(mockResponse);

            await UserService.getList({ keyword: "test", pageIndex: 1 });

            expect(httpClient.get).toHaveBeenCalledWith("/users", {
                params: { keyword: "test", pageIndex: 1 },
            });
        });
    });

    describe("create", () => {
        it("should call API with correct data", async () => {
            const mockResponse = {
                data: {
                    returnCode: 0,
                    data: { id: 1 },
                    message: "User created",
                },
            };
            httpClient.post = vi.fn().mockResolvedValue(mockResponse);

            const data = {
                username: "testuser",
                email: "test@example.com",
                password: "Password123!",
            };

            await UserService.create(data);

            expect(httpClient.post).toHaveBeenCalledWith("/users", data);
        });
    });
});
```

### 7. Run Test Coverage

**Backend:**

```bash
cd backend/src/UniManage.Tests

# Generate coverage report
dotnet test --collect:"XPlat Code Coverage"

# Generate HTML report (requires ReportGenerator)
reportgenerator -reports:"**/coverage.cobertura.xml" -targetdir:"coverage-report" -reporttypes:Html

# Open report
start coverage-report/index.html
```

**Frontend:**

```bash
cd frontend/uni-manage

# Generate coverage
npm test -- --coverage

# Open report
open coverage/index.html
```

**Coverage targets:**

-   Unit tests: > 80%
-   Integration tests: > 70%
-   Overall: > 75%

### 8. Continuous Integration

**GitHub Actions workflow:**

```yaml
# .github/workflows/test.yml
name: Run Tests

on:
    push:
        branches: [main, develop]
    pull_request:
        branches: [main, develop]

jobs:
    backend-tests:
        runs-on: ubuntu-latest
        steps:
            - uses: actions/checkout@v3

            - name: Setup .NET
              uses: actions/setup-dotnet@v3
              with:
                  dotnet-version: "8.0.x"

            - name: Restore dependencies
              run: dotnet restore
              working-directory: backend/src

            - name: Build
              run: dotnet build --no-restore
              working-directory: backend/src

            - name: Run tests
              run: dotnet test --no-build --verbosity normal --collect:"XPlat Code Coverage"
              working-directory: backend/src/UniManage.Tests

            - name: Upload coverage
              uses: codecov/codecov-action@v3
              with:
                  files: "**/coverage.cobertura.xml"

    frontend-tests:
        runs-on: ubuntu-latest
        steps:
            - uses: actions/checkout@v3

            - name: Setup Node.js
              uses: actions/setup-node@v3
              with:
                  node-version: "20"

            - name: Install dependencies
              run: npm ci
              working-directory: frontend/uni-manage

            - name: Run tests
              run: npm test -- --coverage
              working-directory: frontend/uni-manage

            - name: Upload coverage
              uses: codecov/codecov-action@v3
              with:
                  files: frontend/uni-manage/coverage/coverage-final.json
```

### 9. Verify Test Quality

**Checklist:**

-   [ ] Tests are isolated (no dependencies between tests)
-   [ ] Tests are deterministic (same result every time)
-   [ ] Tests are fast (< 1s per unit test)
-   [ ] Tests have clear names (describe what they test)
-   [ ] Tests follow AAA pattern (Arrange, Act, Assert)
-   [ ] Edge cases are covered
-   [ ] Error cases are covered
-   [ ] Mocks are used appropriately
-   [ ] Test data is realistic

### 10. Checklist

✅ Backend unit tests written
✅ Backend integration tests written
✅ Frontend component tests written
✅ Frontend service tests written
✅ All tests passing
✅ Coverage meets targets (>75%)
✅ Tests run in CI/CD pipeline
✅ Test data cleaned up after tests
✅ Tests are maintainable
✅ Tests document behavior

## Summary

This workflow provides comprehensive testing with:

-   Unit tests for isolated logic
-   Integration tests with database
-   Component tests for UI
-   Service tests for API integration
-   Coverage reporting
-   CI/CD integration
-   Quality guidelines
