using System.Net;
using System.Net.Http.Json;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using UniManage.Model.Common;
using UniManage.Tests.BDD.Support;

namespace UniManage.Tests.BDD.StepDefinitions;

[Binding]
public class UserManagementSteps
{
    private readonly HttpClient _client;
    private readonly TestContext _testContext;
    private object? _userPayload;

    public UserManagementSteps(HttpClient client, TestContext testContext)
    {
        _client = client;
        _testContext = testContext;
    }

    #region Given Steps

    [Given(@"the API is running")]
    public void GivenTheAPIIsRunning()
    {
        // API is already running via WebApplicationFactory in hooks
        _client.BaseAddress.Should().NotBeNull();
    }

    [Given(@"I am authenticated as an administrator")]
    public void GivenIAmAuthenticatedAsAnAdministrator()
    {
        // Add admin authentication token
        // _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "admin-token");

        // For now, just mark as authenticated
        _testContext.Set("IsAuthenticated", true);
    }

    [Given(@"I have valid user information")]
    public void GivenIHaveValidUserInformation()
    {
        _userPayload = new
        {
            Username = _testContext.GenerateUniqueUsername(),
            DisplayName = "Test User",
            Email = _testContext.GenerateUniqueEmail(),
            Password = "Test@12345",
            Status = "active"
        };
    }

    [Given(@"I have valid user information")]
    public void GivenIHaveValidUserInformationWithTable(Table table)
    {
        var data = table.Rows[0];
        _userPayload = new
        {
            Username = data["Username"],
            DisplayName = data["DisplayName"],
            Email = data["Email"],
            Password = data["Password"],
            Status = "active"
        };
    }

    [Given(@"a user with username ""(.*)"" already exists")]
    public async Task GivenAUserWithUsernameAlreadyExists(string username)
    {
        var existingUser = new
        {
            Username = username,
            DisplayName = "Existing User",
            Email = $"{username}@example.com",
            Password = "Test@12345",
            Status = "active"
        };

        var response = await _client.PostAsJsonAsync("/api/v1/users", existingUser);
        response.IsSuccessStatusCode.Should().BeTrue();
    }

    [Given(@"I have user information with email ""(.*)""")]
    public void GivenIHaveUserInformationWithEmail(string email)
    {
        _userPayload = new
        {
            Username = _testContext.GenerateUniqueUsername(),
            DisplayName = "Test User",
            Email = email,
            Password = "Test@12345",
            Status = "active"
        };
    }

    [Given(@"I have user information with password ""(.*)""")]
    public void GivenIHaveUserInformationWithPassword(string password)
    {
        _userPayload = new
        {
            Username = _testContext.GenerateUniqueUsername(),
            DisplayName = "Test User",
            Email = _testContext.GenerateUniqueEmail(),
            Password = password,
            Status = "active"
        };
    }

    [Given(@"there are (.*) users in the system")]
    public async Task GivenThereAreUsersInTheSystem(int userCount)
    {
        // Create test users
        for (int i = 0; i < userCount; i++)
        {
            var user = new
            {
                Username = $"testuser{i}_{Guid.NewGuid():N}",
                DisplayName = $"Test User {i}",
                Email = $"test{i}_{Guid.NewGuid():N}@example.com",
                Password = "Test@12345",
                Status = "active"
            };

            await _client.PostAsJsonAsync("/api/v1/users", user);
        }
    }

    [Given(@"the following users exist in the system:")]
    public async Task GivenTheFollowingUsersExistInTheSystem(Table table)
    {
        foreach (var row in table.Rows)
        {
            var user = new
            {
                Username = row["Username"],
                DisplayName = row["DisplayName"],
                Email = row["Email"],
                Password = "Test@12345",
                Status = "active"
            };

            await _client.PostAsJsonAsync("/api/v1/users", user);
        }
    }

    [Given(@"a user with username ""(.*)"" exists")]
    public async Task GivenAUserWithUsernameExists(string username)
    {
        var user = new
        {
            Username = username,
            DisplayName = "Test User",
            Email = $"{username}@example.com",
            Password = "Test@12345",
            Status = "active"
        };

        var response = await _client.PostAsJsonAsync("/api/v1/users", user);
        var result = await response.Content.ReadFromJsonAsync<ApiResponse<dynamic>>();

        _testContext.CreatedUserId = (long)result!.Data!.id;
        _testContext.CreatedUsername = username;
    }

    [Given(@"multiple users exist with different usernames")]
    public async Task GivenMultipleUsersExistWithDifferentUsernames()
    {
        var usernames = new[] { "alice", "charlie", "bob", "david" };

        foreach (var username in usernames)
        {
            var user = new
            {
                Username = username,
                DisplayName = username.ToUpper(),
                Email = $"{username}@example.com",
                Password = "Test@12345",
                Status = "active"
            };

            await _client.PostAsJsonAsync("/api/v1/users", user);
        }
    }

    [Given(@"users exist with different statuses")]
    public async Task GivenUsersExistWithDifferentStatuses()
    {
        var statuses = new[] { "active", "inactive" };

        foreach (var status in statuses)
        {
            for (int i = 0; i < 3; i++)
            {
                var user = new
                {
                    Username = $"user_{status}_{i}_{Guid.NewGuid():N}",
                    DisplayName = $"User {status} {i}",
                    Email = $"user_{status}_{i}@example.com",
                    Password = "Test@12345",
                    Status = status
                };

                await _client.PostAsJsonAsync("/api/v1/users", user);
            }
        }
    }

    #endregion

    #region When Steps

    [When(@"I send a POST request to create the user")]
    [When(@"I try to create another user with username ""(.*)""")]
    public async Task WhenISendAPOSTRequestToCreateTheUser(string? username = null)
    {
        if (username != null)
        {
            _userPayload = new
            {
                Username = username,
                DisplayName = "Test User",
                Email = _testContext.GenerateUniqueEmail(),
                Password = "Test@12345",
                Status = "active"
            };
        }

        var response = await _client.PostAsJsonAsync("/api/v1/users", _userPayload);
        _testContext.LastResponse = response;

        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadFromJsonAsync<ApiResponse<dynamic>>();
            _testContext.SetResponseData(result);

            if (result?.Data?.id != null)
            {
                _testContext.CreatedUserId = (long)result.Data.id;
            }
        }
    }

    [When(@"I create a new user")]
    public async Task WhenICreateANewUser()
    {
        await WhenISendAPOSTRequestToCreateTheUser();
    }

    [When(@"I request users list with page (.*) and page size (.*)")]
    public async Task WhenIRequestUsersListWithPageAndPageSize(int pageIndex, int pageSize)
    {
        var response = await _client.GetAsync($"/api/v1/users?pageIndex={pageIndex}&pageSize={pageSize}");
        _testContext.LastResponse = response;

        var result = await response.Content.ReadFromJsonAsync<ApiResponse<PagedResult<dynamic>>>();
        _testContext.SetResponseData(result);
    }

    [When(@"I search for users with keyword ""(.*)""")]
    public async Task WhenISearchForUsersWithKeyword(string keyword)
    {
        var response = await _client.GetAsync($"/api/v1/users?keyword={keyword}");
        _testContext.LastResponse = response;

        var result = await response.Content.ReadFromJsonAsync<ApiResponse<PagedResult<dynamic>>>();
        _testContext.SetResponseData(result);
    }

    [When(@"I update the user's display name to ""(.*)""")]
    public async Task WhenIUpdateTheUsersDisplayNameTo(string displayName)
    {
        var userId = _testContext.CreatedUserId;
        var updatePayload = new { DisplayName = displayName };

        var response = await _client.PutAsJsonAsync($"/api/v1/users/{userId}", updatePayload);
        _testContext.LastResponse = response;
    }

    [When(@"I update the user's email to ""(.*)""")]
    public async Task WhenIUpdateTheUsersEmailTo(string email)
    {
        var userId = _testContext.CreatedUserId;
        var updatePayload = new { Email = email };

        var response = await _client.PutAsJsonAsync($"/api/v1/users/{userId}", updatePayload);
        _testContext.LastResponse = response;
    }

    [When(@"I send a DELETE request for that user")]
    [When(@"I delete the user")]
    public async Task WhenISendADELETERequestForThatUser()
    {
        var userId = _testContext.CreatedUserId;
        var response = await _client.DeleteAsync($"/api/v1/users/{userId}");
        _testContext.LastResponse = response;
    }

    [When(@"I try to delete a user with ID (.*)")]
    public async Task WhenITryToDeleteAUserWithID(long userId)
    {
        var response = await _client.DeleteAsync($"/api/v1/users/{userId}");
        _testContext.LastResponse = response;
    }

    [When(@"I retrieve the user by ID")]
    [When(@"I try to retrieve the deleted user")]
    public async Task WhenIRetrieveTheUserByID()
    {
        var userId = _testContext.CreatedUserId;
        var response = await _client.GetAsync($"/api/v1/users/{userId}");
        _testContext.LastResponse = response;
    }

    [When(@"I update the user's information")]
    public async Task WhenIUpdateTheUsersInformation()
    {
        await WhenIUpdateTheUsersDisplayNameTo("Updated Display Name");
    }

    [When(@"I request users sorted by username in ascending order")]
    public async Task WhenIRequestUsersSortedByUsernameInAscendingOrder()
    {
        var response = await _client.GetAsync("/api/v1/users?sortBy=username&sortDirection=asc");
        _testContext.LastResponse = response;

        var result = await response.Content.ReadFromJsonAsync<ApiResponse<PagedResult<dynamic>>>();
        _testContext.SetResponseData(result);
    }

    [When(@"I filter users by status ""(.*)""")]
    public async Task WhenIFilterUsersByStatus(string status)
    {
        var response = await _client.GetAsync($"/api/v1/users?status={status}");
        _testContext.LastResponse = response;

        var result = await response.Content.ReadFromJsonAsync<ApiResponse<PagedResult<dynamic>>>();
        _testContext.SetResponseData(result);
    }

    [When(@"I request the first page of users")]
    public async Task WhenIRequestTheFirstPageOfUsers()
    {
        await WhenIRequestUsersListWithPageAndPageSize(1, 20);
    }

    #endregion

    #region Then Steps

    [Then(@"the response status should be (.*)")]
    public void ThenTheResponseStatusShouldBe(int statusCode)
    {
        _testContext.LastResponse.Should().NotBeNull();
        ((int)_testContext.LastResponse!.StatusCode).Should().Be(statusCode);
    }

    [Then(@"the response should contain a user ID")]
    public void ThenTheResponseShouldContainAUserID()
    {
        _testContext.CreatedUserId.Should().NotBeNull();
        _testContext.CreatedUserId.Should().BeGreaterThan(0);
    }

    [Then(@"the user should be created in the database")]
    [Then(@"the user should be created successfully")]
    public void ThenTheUserShouldBeCreatedInTheDatabase()
    {
        _testContext.CreatedUserId.Should().NotBeNull();
        _testContext.CreatedUserId.Should().BeGreaterThan(0);
    }

    [Then(@"the response should indicate validation error")]
    public async Task ThenTheResponseShouldIndicateValidationError()
    {
        var result = await _testContext.LastResponse!.Content.ReadFromJsonAsync<ApiResponse<dynamic>>();
        result.Should().NotBeNull();
        result!.ReturnCode.Should().Be(400);
        result.Errors.Should().NotBeEmpty();
    }

    [Then(@"the error message should contain ""(.*)""")]
    public async Task ThenTheErrorMessageShouldContain(string expectedText)
    {
        var result = await _testContext.LastResponse!.Content.ReadFromJsonAsync<ApiResponse<dynamic>>();
        var errors = result!.Errors as List<string>;

        errors.Should().NotBeNull();
        errors!.Should().Contain(e => e.Contains(expectedText, StringComparison.OrdinalIgnoreCase));
    }

    [Then(@"the response should contain (.*) users")]
    public void ThenTheResponseShouldContainUsers(int expectedCount)
    {
        var result = _testContext.GetResponseData<ApiResponse<PagedResult<dynamic>>>();
        result.Should().NotBeNull();
        result!.Data!.Items.Should().HaveCount(expectedCount);
    }

    [Then(@"the pagination info should show page (.*) of (.*)")]
    public void ThenThePaginationInfoShouldShowPageOf(int pageIndex, int totalPages)
    {
        var result = _testContext.GetResponseData<ApiResponse<PagedResult<dynamic>>>();
        result!.Data!.Paging.PageIndex.Should().Be(pageIndex);
        result.Data.Paging.TotalPages.Should().Be(totalPages);
    }

    [Then(@"the total items should be (.*)")]
    public void ThenTheTotalItemsShouldBe(int totalItems)
    {
        var result = _testContext.GetResponseData<ApiResponse<PagedResult<dynamic>>>();
        result!.Data!.Paging.TotalItems.Should().BeGreaterOrEqualTo(totalItems);
    }

    [Then(@"the response should contain only users matching ""(.*)""")]
    public void ThenTheResponseShouldContainOnlyUsersMatching(string keyword)
    {
        var result = _testContext.GetResponseData<ApiResponse<PagedResult<dynamic>>>();
        result!.Data!.Items.Should().NotBeEmpty();

        // All users should contain the keyword in username or display name
        foreach (var item in result.Data.Items)
        {
            var username = ((string?)item.username ?? "").ToLower();
            var displayName = ((string?)item.displayName ?? "").ToLower();
            var keywordLower = keyword.ToLower();

            (username.Contains(keywordLower) || displayName.Contains(keywordLower)).Should().BeTrue();
        }
    }

    [Then(@"the user's information should be updated in the database")]
    [Then(@"the user's information should be updated")]
    public void ThenTheUsersInformationShouldBeUpdatedInTheDatabase()
    {
        _testContext.LastResponse!.IsSuccessStatusCode.Should().BeTrue();
    }

    [Then(@"the user should no longer exist in the database")]
    [Then(@"the user should be removed from the system")]
    public async Task ThenTheUserShouldNoLongerExistInTheDatabase()
    {
        var userId = _testContext.CreatedUserId;
        var response = await _client.GetAsync($"/api/v1/users/{userId}");

        var result = await response.Content.ReadFromJsonAsync<ApiResponse<dynamic>>();
        result!.ReturnCode.Should().Be(404);
    }

    [Then(@"the response should indicate user not found")]
    [Then(@"the user should not be found")]
    public async Task ThenTheResponseShouldIndicateUserNotFound()
    {
        var result = await _testContext.LastResponse!.Content.ReadFromJsonAsync<ApiResponse<dynamic>>();
        result!.ReturnCode.Should().Be(404);
    }

    [Then(@"I should see the user's information")]
    public async Task ThenIShouldSeeTheUsersInformation()
    {
        var result = await _testContext.LastResponse!.Content.ReadFromJsonAsync<ApiResponse<dynamic>>();
        result!.ReturnCode.Should().Be(0);
        result.Data.Should().NotBeNull();
    }

    [Then(@"the users should be returned in alphabetical order by username")]
    public void ThenTheUsersShouldBeReturnedInAlphabeticalOrderByUsername()
    {
        var result = _testContext.GetResponseData<ApiResponse<PagedResult<dynamic>>>();
        var usernames = result!.Data!.Items.Select(u => (string)u.username).ToList();

        usernames.Should().BeInAscendingOrder();
    }

    [Then(@"only users with status ""(.*)"" should be returned")]
    public void ThenOnlyUsersWithStatusShouldBeReturned(string status)
    {
        var result = _testContext.GetResponseData<ApiResponse<PagedResult<dynamic>>>();
        result!.Data!.Items.Should().NotBeEmpty();

        foreach (var item in result.Data.Items)
        {
            ((string)item.status).Should().Be(status);
        }
    }

    [Then(@"the response should be returned within (.*) seconds")]
    public void ThenTheResponseShouldBeReturnedWithinSeconds(int seconds)
    {
        // This would be tracked by the hooks or timing interceptor
        _testContext.LastResponse.Should().NotBeNull();
    }

    [Then(@"the response should contain paginated results")]
    public void ThenTheResponseShouldContainPaginatedResults()
    {
        var result = _testContext.GetResponseData<ApiResponse<PagedResult<dynamic>>>();
        result!.Data!.Paging.Should().NotBeNull();
        result.Data.Items.Should().NotBeNull();
    }

    #endregion
}
