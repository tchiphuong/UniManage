# 🥒 BDD Testing with SpecFlow (Cucumber for .NET)

## 📖 Overview

This project uses **SpecFlow** (the .NET implementation of Cucumber) for Behavior-Driven Development (BDD) testing. SpecFlow allows you to write tests in natural language using **Gherkin syntax**.

## 🎯 Why BDD with SpecFlow?

-   ✅ **Human-readable tests**: Business stakeholders can understand test scenarios
-   ✅ **Living documentation**: Feature files serve as up-to-date documentation
-   ✅ **Collaboration**: Bridge gap between technical and non-technical team members
-   ✅ **Reusable steps**: Step definitions can be reused across multiple scenarios
-   ✅ **Test organization**: Features organized by business functionality

## 🏗️ Project Structure

```
UniManage.Tests/BDD/
├── Features/                    # Gherkin feature files (.feature)
│   ├── UserManagement.feature
│   ├── Authentication.feature
│   └── ApiValidation.feature
├── StepDefinitions/            # Step implementation (.cs)
│   ├── UserManagementSteps.cs
│   └── AuthenticationSteps.cs
├── Hooks/                      # Test lifecycle hooks
│   └── TestHooks.cs
└── Support/                    # Helper classes
    └── TestContext.cs
```

## 📝 Gherkin Syntax

### Basic Structure

```gherkin
Feature: Short description
  As a [role]
  I want [feature]
  So that [benefit]

  Scenario: Scenario description
    Given [precondition]
    When [action]
    Then [expected result]
```

### Keywords

-   **Feature**: High-level description of a software feature
-   **Scenario**: Concrete example of business rule
-   **Given**: Setup/preconditions
-   **When**: Actions/events
-   **Then**: Expected outcomes
-   **And/But**: Additional steps
-   **Background**: Steps executed before each scenario
-   **Scenario Outline**: Template for multiple similar scenarios
-   **Examples**: Data table for Scenario Outline

## 🚀 Running BDD Tests

### Run All BDD Tests

```powershell
dotnet test --filter "FullyQualifiedName~BDD"
```

### Run Specific Feature

```powershell
# Run User Management feature
dotnet test --filter "FullyQualifiedName~UserManagement"

# Run Authentication feature
dotnet test --filter "FullyQualifiedName~Authentication"
```

### Run Tests by Tag

```powershell
# Run only smoke tests
dotnet test --filter "Category=smoke"

# Run validation tests
dotnet test --filter "Category=validation"

# Run multiple tags
dotnet test --filter "Category=users|Category=auth"
```

### Run with SpecFlow Report

```powershell
dotnet test --logger "specflow;LogFileName=TestResults.html"
```

## 🏷️ Tag Categories

### Test Priority

-   `@smoke` - Critical functionality, run on every build
-   `@regression` - Full regression suite
-   `@slow` - Tests that take longer to execute

### Functional Areas

-   `@users` - User management
-   `@auth` - Authentication/Authorization
-   `@api` - API-level tests
-   `@database` - Database-related tests

### Test Types

-   `@positive` - Happy path scenarios
-   `@negative` - Error/edge case scenarios
-   `@validation` - Input validation tests
-   `@security` - Security-related tests
-   `@performance` - Performance tests

### Environment

-   `@dev` - Dev environment only
-   `@staging` - Staging environment
-   `@production` - Production-safe tests

## ✍️ Writing New Tests

### 1. Create Feature File

Create a new `.feature` file in `BDD/Features/`:

```gherkin
Feature: Order Management
  As a sales representative
  I want to manage customer orders
  So that I can process sales efficiently

  @smoke @orders
  Scenario: Create new order
    Given a customer "John Doe" exists
    And the following products are available:
      | Product | Price |
      | Widget  | 10.00 |
      | Gadget  | 20.00 |
    When I create an order for customer "John Doe"
    And I add "Widget" with quantity 2
    Then the order total should be 20.00
    And the order status should be "Pending"
```

### 2. Create Step Definitions

Create corresponding step definitions in `BDD/StepDefinitions/`:

```csharp
[Binding]
public class OrderManagementSteps
{
    private readonly HttpClient _client;
    private readonly TestContext _testContext;

    public OrderManagementSteps(HttpClient client, TestContext testContext)
    {
        _client = client;
        _testContext = testContext;
    }

    [Given(@"a customer ""(.*)"" exists")]
    public async Task GivenACustomerExists(string customerName)
    {
        // Implementation
    }

    [When(@"I create an order for customer ""(.*)""")]
    public async Task WhenICreateAnOrderForCustomer(string customerName)
    {
        // Implementation
    }

    [Then(@"the order total should be (.*)")]
    public void ThenTheOrderTotalShouldBe(decimal expectedTotal)
    {
        // Assertion
    }
}
```

### 3. Run Tests

```powershell
dotnet test --filter "FullyQualifiedName~OrderManagement"
```

## 📊 Data Tables

### Simple Table

```gherkin
Given the following users exist:
  | Username | Email            |
  | john     | john@example.com |
  | jane     | jane@example.com |
```

```csharp
[Given(@"the following users exist:")]
public async Task GivenTheFollowingUsersExist(Table table)
{
    foreach (var row in table.Rows)
    {
        var username = row["Username"];
        var email = row["Email"];
        // Create user
    }
}
```

### Scenario Outline with Examples

```gherkin
Scenario Outline: Login validation
  When I login with username "<username>" and password "<password>"
  Then the result should be "<result>"

  Examples:
    | username | password | result  |
    | valid    | valid    | success |
    | invalid  | invalid  | error   |
```

## 🎣 Hooks

### Available Hooks

```csharp
[BeforeTestRun]       // Once before all tests
[AfterTestRun]        // Once after all tests
[BeforeFeature]       // Before each feature
[AfterFeature]        // After each feature
[BeforeScenario]      // Before each scenario
[AfterScenario]       // After each scenario
[BeforeStep]          // Before each step
[AfterStep]           // After each step
```

### Tag-Specific Hooks

```csharp
[BeforeScenario("@database")]
public void BeforeDatabaseScenario()
{
    // Setup database transaction
}

[AfterScenario("@database")]
public void AfterDatabaseScenario()
{
    // Rollback transaction
}
```

## 💡 Best Practices

### ✅ DO

1. **Write declarative scenarios**

    ```gherkin
    # Good - What, not how
    When I create a new user

    # Bad - Too much detail
    When I click the "New User" button
    And I fill in "Username" with "john"
    And I fill in "Email" with "john@example.com"
    And I click "Submit"
    ```

2. **Use meaningful scenario names**

    ```gherkin
    # Good
    Scenario: Cannot create user with duplicate email

    # Bad
    Scenario: Test 1
    ```

3. **Keep scenarios independent**

    - Each scenario should work in isolation
    - Don't rely on data from previous scenarios

4. **Use Background for common setup**

    ```gherkin
    Background:
      Given I am logged in as an administrator
      And the database is clean
    ```

5. **Tag appropriately**
    ```gherkin
    @smoke @users @positive
    Scenario: Create user successfully
    ```

### ❌ DON'T

1. **Don't write UI tests**

    - Focus on business logic, not UI interactions
    - Test through API, not browser

2. **Don't duplicate scenarios**

    - Use Scenario Outline for similar scenarios

3. **Don't put code in feature files**

    - Keep Gherkin readable for non-technical stakeholders

4. **Don't make scenarios too long**
    - Split complex scenarios into multiple smaller ones

## 📈 Reporting

### Generate HTML Report

```powershell
dotnet test --logger "html;LogFileName=TestResults.html"
```

### Generate JSON Report

```powershell
dotnet test --logger "json;LogFileName=TestResults.json"
```

### CI/CD Integration

```yaml
# GitHub Actions example
- name: Run BDD Tests
  run: |
      dotnet test --filter "Category=smoke" \
        --logger "trx;LogFileName=test-results.trx" \
        --logger "html;LogFileName=test-results.html"

- name: Upload Test Results
  uses: actions/upload-artifact@v2
  with:
      name: test-results
      path: "**/test-results.*"
```

## 🔧 Troubleshooting

### Step Definition Not Found

**Error**: `No matching step definition found for step`

**Solution**:

1. Check regex pattern in `[Given/When/Then]` attribute
2. Rebuild project to generate binding code
3. Check namespace imports

### Ambiguous Step Definitions

**Error**: `Ambiguous step definitions found`

**Solution**:

-   Make step definitions more specific
-   Use different parameters or context

### Background Not Running

**Solution**:

-   Ensure Background is placed before scenarios
-   Check that hooks are properly configured

## 📚 Resources

-   [SpecFlow Documentation](https://docs.specflow.org/)
-   [Gherkin Reference](https://cucumber.io/docs/gherkin/reference/)
-   [SpecFlow Best Practices](https://docs.specflow.org/projects/specflow/en/latest/Guides/BestPractices.html)

## 🤝 Contributing

When adding new BDD tests:

1. Write feature file first (outside-in approach)
2. Run tests to see them fail
3. Implement step definitions
4. Run tests to see them pass
5. Refactor if needed
6. Update this documentation if adding new patterns

## 📞 Support

For BDD testing questions:

-   Check existing feature files for examples
-   Review step definitions for reusable steps
-   Consult the team's BDD champion
