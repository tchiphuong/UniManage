# UniManage API Tests

Comprehensive test suite for UniManage API project, covering unit tests, integration tests, BDD tests, and end-to-end testing scenarios.

## 🏗️ Test Structure

```
UniManage.Tests/
├── BDD/                         # Behavior-Driven Development tests
│   ├── Features/               # Gherkin feature files
│   │   ├── UserManagement.feature
│   │   ├── Authentication.feature
│   │   └── ApiValidation.feature
│   ├── StepDefinitions/        # Step implementations
│   │   └── UserManagementSteps.cs
│   ├── Hooks/                  # Test lifecycle hooks
│   │   └── TestHooks.cs
│   └── Support/                # Helper classes
│       └── TestContext.cs
├── Fixtures/                    # Test base classes and fixtures
│   ├── TestBase.cs             # Base class for all tests
│   ├── DbContextFixture.cs     # Database test fixture
│   └── UniManageWebApplicationFactory.cs  # Integration test factory
├── Helpers/                     # Test utilities and helpers
│   ├── ApiResponseAssertions.cs  # Custom assertions for API responses
│   └── TestDataBuilder.cs      # Test data builders
├── UnitTests/                   # Unit tests
│   ├── Controllers/            # Controller tests
│   │   └── System/
│   │       └── UsersControllerTests.cs
│   ├── Handlers/               # Handler tests (business logic)
│   ├── Validators/             # Validator tests
│   │   └── System/
│   │       └── CreateUserCommandValidatorTests.cs
│   └── Utilities/              # Utility class tests
│       ├── PasswordHelperTests.cs
│       └── ValidationHelperTests.cs
└── IntegrationTests/           # Integration tests
    └── System/
        └── UsersApiIntegrationTests.cs
```

## 🚀 Running Tests

### Run All Tests

```powershell
dotnet test
```

### Run Unit Tests Only

```powershell
dotnet test --filter "FullyQualifiedName~UnitTests"
```

### Run Integration Tests Only

```powershell
dotnet test --filter "FullyQualifiedName~IntegrationTests"
```

### Run BDD Tests Only

```powershell
dotnet test --filter "FullyQualifiedName~BDD"

# Or use the convenient script
.\run-bdd-tests.ps1 -Suite Smoke

# Interactive menu
.\run-bdd-interactive.ps1
```

### Run Specific Test Category

```powershell
# Unit tests only
dotnet test --filter "FullyQualifiedName~UnitTests"

# Integration tests only
dotnet test --filter "FullyQualifiedName~IntegrationTests"

# Controller tests only
dotnet test --filter "FullyQualifiedName~Controllers"

# Validator tests only
dotnet test --filter "FullyQualifiedName~Validators"
```

### Run with Coverage

```powershell
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover
```

### Run Specific Test

```powershell
dotnet test --filter "FullyQualifiedName=UniManage.Tests.UnitTests.Controllers.System.UsersControllerTests.GetUserList_WithValidQuery_ReturnsOkWithPagedResult"
```

## 📝 Test Categories

### 1. Unit Tests - Controllers

Tests controller routing, parameter binding, and mediator integration. Controllers should be thin, so these tests focus on:

-   ✅ Request routing
-   ✅ Parameter binding from route/query/body
-   ✅ HeaderInfo assignment
-   ✅ Mediator call verification
-   ✅ Response type checking

**Example:**

```csharp
[Fact]
public async Task GetUserList_WithValidQuery_ReturnsOkWithPagedResult()
{
    // Arrange
    var query = new GetUserListQuery { Keyword = "test", PageIndex = 1, PageSize = 20 };
    _mediatorMock.Setup(m => m.Send(It.IsAny<GetUserListQuery>(), It.IsAny<CancellationToken>()))
        .ReturnsAsync(expectedResponse);

    // Act
    var result = await _controller.GetUserList(query, CancellationToken.None);

    // Assert
    result.Result.Should().BeOfType<OkObjectResult>();
    _mediatorMock.Verify(m => m.Send(It.IsAny<GetUserListQuery>(), It.IsAny<CancellationToken>()), Times.Once);
}
```

### 2. Unit Tests - Validators

Tests FluentValidation rules including:

-   ✅ Required field validation
-   ✅ Length validation
-   ✅ Format validation (email, phone, etc.)
-   ✅ Complex business rules
-   ✅ Async database validations

**Example:**

```csharp
[Theory]
[InlineData("invalid")]
[InlineData("invalid@")]
[InlineData("@invalid.com")]
public void Validate_WithInvalidEmail_ShouldHaveValidationError(string email)
{
    var command = new CreateUserCommand { Email = email };
    var result = _validator.TestValidate(command);
    result.ShouldHaveValidationErrorFor(x => x.Email);
}
```

### 3. Unit Tests - Handlers

Tests business logic and error handling:

-   ✅ Successful operations
-   ✅ Error scenarios
-   ✅ Database operations (with mock DbContext)
-   ✅ Logging
-   ✅ Transaction handling

### 4. Unit Tests - Utilities

Tests core utility classes:

-   ✅ PasswordHelper (hash, verify, generate)
-   ✅ ValidationHelper (email, phone, code validation)
-   ✅ StringHelper (slug, camelCase, etc.)
-   ✅ DateTimeHelper
-   ✅ And more...

### 5. Integration Tests

End-to-end API tests with real HTTP calls:

-   ✅ Full request/response cycle
-   ✅ Authentication/Authorization
-   ✅ Database operations
-   ✅ Complete user journeys
-   ✅ Error handling

**Example:**

```csharp
[Fact]
public async Task CreateUser_WithValidData_ReturnsCreatedUser()
{
    var newUser = new { Username = "testuser", Email = "test@example.com", Password = "Test@12345" };
    var response = await _client.PostAsJsonAsync("/api/v1/users", newUser);

    response.StatusCode.Should().Be(HttpStatusCode.OK);
    var result = await response.Content.ReadFromJsonAsync<ApiResponse<CreateUserResponse>>();
    result!.ReturnCode.Should().Be(0);
    result.Data!.Id.Should().BeGreaterThan(0);
}
```

### 6. BDD Tests (SpecFlow/Cucumber)

Behavior-Driven Development tests written in Gherkin:

-   ✅ Human-readable test scenarios
-   ✅ Living documentation
-   ✅ Business stakeholder collaboration
-   ✅ Reusable step definitions

**Example:**

```gherkin
Feature: User Management
  As a system administrator
  I want to manage users in the system
  So that I can control who has access

  @smoke @users
  Scenario: Create a new user successfully
    Given I have valid user information
    When I send a POST request to create the user
    Then the response status should be 200
    And the response should contain a user ID
    And the user should be created in the database
```

**See [BDD/README.md](BDD/README.md) for complete BDD testing guide.**

## 🔧 Custom Assertions

We provide custom assertions for cleaner test code:

```csharp
using UniManage.Tests.Helpers;

// Success assertions
response.ShouldBeSuccess("User created successfully");

// Error assertions
response.ShouldHaveError(404, "User not found");
response.ShouldHaveValidationErrors("username", "email");

// Paged response assertions
response.ShouldBeValidPagedResponse(pageIndex: 1, pageSize: 20);
response.ShouldHaveItemCount(5);

// Status code assertions
response.ShouldBeNotFound();
response.ShouldBeForbidden();
response.ShouldBeUnauthorized();
```

## 🏭 Test Data Builders

Use TestDataBuilder for consistent test data:

```csharp
using UniManage.Tests.Helpers;

// Create valid test user
var user = TestDataBuilder.Users.CreateValid();

// Create invalid test user
var invalidUser = TestDataBuilder.Users.CreateInvalid();

// Create valid menu
var menu = TestDataBuilder.Menus.CreateValid(code: "TEST_MENU");

// Create valid role
var role = TestDataBuilder.Roles.CreateValid();
```

## 📊 Test Coverage Goals

| Component   | Target Coverage |
| ----------- | --------------- |
| Controllers | 90%+            |
| Validators  | 100%            |
| Handlers    | 85%+            |
| Utilities   | 95%+            |
| Overall     | 80%+            |

## 🐛 Debugging Tests

### Run test in debug mode in VS Code

1. Set breakpoint in test
2. Right-click test → "Debug Test"

### View test output

```powershell
dotnet test --logger "console;verbosity=detailed"
```

### Common Issues

**Issue:** Integration tests fail with "Database not found"

-   **Solution:** Ensure test database exists or configure in-memory database

**Issue:** Async validation tests don't work

-   **Solution:** Use `TestValidateAsync` instead of `TestValidate`

**Issue:** Mock setup not matching

-   **Solution:** Use `It.IsAny<T>()` for flexible matching or exact parameter matching

## 📚 Best Practices

### ✅ DO

-   Use descriptive test names: `MethodName_Scenario_ExpectedResult`
-   Arrange-Act-Assert pattern
-   Test one thing per test
-   Use Theory for multiple similar test cases
-   Mock external dependencies
-   Clean up test data in integration tests

### ❌ DON'T

-   Test implementation details
-   Create test interdependencies
-   Hardcode test data that could change
-   Skip cleanup in integration tests
-   Test framework code (ASP.NET, FluentValidation, etc.)

## 🔗 Related Documentation

-   [FluentValidation Testing](https://docs.fluentvalidation.net/en/latest/testing.html)
-   [FluentAssertions Documentation](https://fluentassertions.com/introduction)
-   [xUnit Documentation](https://xunit.net/)
-   [Moq Quickstart](https://github.com/moq/moq4/wiki/Quickstart)

## 📈 Continuous Integration

Tests run automatically on:

-   Every commit to `main` and `develop` branches
-   Every pull request
-   Nightly builds

## 🤝 Contributing

When adding new features:

1. Write tests first (TDD approach)
2. Ensure all tests pass
3. Maintain coverage above 80%
4. Update this README if adding new test categories

## 📞 Support

For test-related questions, contact the development team or check the project documentation.
