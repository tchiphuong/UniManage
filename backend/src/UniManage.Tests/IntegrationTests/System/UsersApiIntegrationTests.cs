using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using UniManage.Model.Common;
using UniManage.Tests.Fixtures;

namespace UniManage.Tests.IntegrationTests.System;

/// <summary>
/// Integration tests for Users API endpoints
/// Tests full request/response cycle with real HTTP calls
/// </summary>
public class UsersApiIntegrationTests : IClassFixture<UniManageWebApplicationFactory>
{
    private readonly HttpClient _client;
    private readonly UniManageWebApplicationFactory _factory;

    public UsersApiIntegrationTests(UniManageWebApplicationFactory factory)
    {
        _factory = factory;
        _client = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });

        // Add authentication token if needed
        // _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "test-token");
    }

    #region GET /api/v1/users - List Users

    [Fact]
    public async Task GetUserList_WithDefaultParameters_ReturnsOkWithPagedResult()
    {
        // Act
        var response = await _client.GetAsync("/api/v1/users");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var result = await response.Content.ReadFromJsonAsync<ApiResponse<PagedResult<UserDto>>>();
        result.Should().NotBeNull();
        result!.ReturnCode.Should().Be(0);
        result.Data.Should().NotBeNull();
        result.Data!.Items.Should().NotBeNull();
        result.Data.Paging.Should().NotBeNull();
        result.Data.Paging.PageIndex.Should().Be(1);
        result.Data.Paging.PageSize.Should().BeGreaterThan(0);
    }

    [Fact]
    public async Task GetUserList_WithKeyword_ReturnsFilteredResults()
    {
        // Arrange
        var keyword = "admin";

        // Act
        var response = await _client.GetAsync($"/api/v1/users?keyword={keyword}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var result = await response.Content.ReadFromJsonAsync<ApiResponse<PagedResult<UserDto>>>();
        result.Should().NotBeNull();
        result!.Data.Should().NotBeNull();

        if (result.Data!.Items.Any())
        {
            result.Data.Items.Should().AllSatisfy(user =>
            {
                (user.Username.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                 user.DisplayName.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                .Should().BeTrue();
            });
        }
    }

    [Theory]
    [InlineData(1, 10)]
    [InlineData(2, 20)]
    [InlineData(1, 50)]
    public async Task GetUserList_WithPaginationParameters_ReturnsPaginatedResults(int pageIndex, int pageSize)
    {
        // Act
        var response = await _client.GetAsync($"/api/v1/users?pageIndex={pageIndex}&pageSize={pageSize}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var result = await response.Content.ReadFromJsonAsync<ApiResponse<PagedResult<UserDto>>>();
        result.Should().NotBeNull();
        result!.Data!.Paging.PageIndex.Should().Be(pageIndex);
        result.Data.Paging.PageSize.Should().Be(pageSize);
        result.Data.Items.Should().HaveCountLessOrEqualTo(pageSize);
    }

    [Fact]
    public async Task GetUserList_WithInvalidPageIndex_ReturnsValidationError()
    {
        // Arrange
        var invalidPageIndex = -1;

        // Act
        var response = await _client.GetAsync($"/api/v1/users?pageIndex={invalidPageIndex}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK); // Controller returns OK, error in body

        var result = await response.Content.ReadFromJsonAsync<ApiResponse<PagedResult<UserDto>>>();
        result.Should().NotBeNull();
        result!.ReturnCode.Should().Be(400);
        result.Errors.Should().NotBeEmpty();
    }

    [Fact]
    public async Task GetUserList_WithSorting_ReturnsSortedResults()
    {
        // Act
        var response = await _client.GetAsync("/api/v1/users?sortBy=username&sortDirection=asc");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var result = await response.Content.ReadFromJsonAsync<ApiResponse<PagedResult<UserDto>>>();
        result.Should().NotBeNull();

        if (result!.Data!.Items.Count > 1)
        {
            var usernames = result.Data.Items.Select(u => u.Username).ToList();
            usernames.Should().BeInAscendingOrder();
        }
    }

    #endregion

    #region GET /api/v1/users/{id} - Get User By Id

    [Fact]
    public async Task GetUserById_WithValidId_ReturnsOkWithUser()
    {
        // Arrange
        const long existingUserId = 1; // Assuming user with ID 1 exists

        // Act
        var response = await _client.GetAsync($"/api/v1/users/{existingUserId}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var result = await response.Content.ReadFromJsonAsync<ApiResponse<UserDetailDto>>();
        result.Should().NotBeNull();
        result!.ReturnCode.Should().Be(0);
        result.Data.Should().NotBeNull();
        result.Data!.Id.Should().Be(existingUserId);
    }

    [Fact]
    public async Task GetUserById_WithNonExistentId_ReturnsNotFound()
    {
        // Arrange
        const long nonExistentUserId = 999999;

        // Act
        var response = await _client.GetAsync($"/api/v1/users/{nonExistentUserId}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK); // Returns OK with error in body

        var result = await response.Content.ReadFromJsonAsync<ApiResponse<UserDetailDto>>();
        result.Should().NotBeNull();
        result!.ReturnCode.Should().Be(404);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public async Task GetUserById_WithInvalidId_ReturnsError(long invalidId)
    {
        // Act
        var response = await _client.GetAsync($"/api/v1/users/{invalidId}");

        // Assert
        var result = await response.Content.ReadFromJsonAsync<ApiResponse<UserDetailDto>>();
        result.Should().NotBeNull();
        result!.ReturnCode.Should().NotBe(0);
    }

    #endregion

    #region POST /api/v1/users - Create User

    [Fact]
    public async Task CreateUser_WithValidData_ReturnsCreatedUser()
    {
        // Arrange
        var newUser = new
        {
            Username = $"testuser_{Guid.NewGuid():N}",
            DisplayName = "Test User",
            Email = $"test_{Guid.NewGuid():N}@example.com",
            Password = "Test@12345",
            EmployeeCode = $"EMP{DateTime.Now.Ticks}",
            Status = "active"
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/v1/users", newUser);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var result = await response.Content.ReadFromJsonAsync<ApiResponse<CreateUserResponse>>();
        result.Should().NotBeNull();
        result!.ReturnCode.Should().Be(0);
        result.Data.Should().NotBeNull();
        result.Data!.Id.Should().BeGreaterThan(0);
    }

    [Fact]
    public async Task CreateUser_WithDuplicateUsername_ReturnsValidationError()
    {
        // Arrange - First create a user
        var username = $"testuser_{Guid.NewGuid():N}";
        var user1 = new
        {
            Username = username,
            DisplayName = "Test User 1",
            Email = $"test1_{Guid.NewGuid():N}@example.com",
            Password = "Test@12345",
            Status = "active"
        };

        await _client.PostAsJsonAsync("/api/v1/users", user1);

        // Try to create another user with same username
        var user2 = new
        {
            Username = username, // Duplicate
            DisplayName = "Test User 2",
            Email = $"test2_{Guid.NewGuid():N}@example.com",
            Password = "Test@12345",
            Status = "active"
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/v1/users", user2);

        // Assert
        var result = await response.Content.ReadFromJsonAsync<ApiResponse<CreateUserResponse>>();
        result.Should().NotBeNull();
        result!.ReturnCode.Should().Be(400);
        result.Errors.Should().NotBeEmpty();
        result.Errors.Should().Contain(e => e.Contains("username", StringComparison.OrdinalIgnoreCase));
    }

    [Fact]
    public async Task CreateUser_WithInvalidEmail_ReturnsValidationError()
    {
        // Arrange
        var user = new
        {
            Username = $"testuser_{Guid.NewGuid():N}",
            DisplayName = "Test User",
            Email = "invalid-email-format", // Invalid
            Password = "Test@12345",
            Status = "active"
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/v1/users", user);

        // Assert
        var result = await response.Content.ReadFromJsonAsync<ApiResponse<CreateUserResponse>>();
        result.Should().NotBeNull();
        result!.ReturnCode.Should().Be(400);
        result.Errors.Should().NotBeEmpty();
    }

    [Fact]
    public async Task CreateUser_WithWeakPassword_ReturnsValidationError()
    {
        // Arrange
        var user = new
        {
            Username = $"testuser_{Guid.NewGuid():N}",
            DisplayName = "Test User",
            Email = $"test_{Guid.NewGuid():N}@example.com",
            Password = "123", // Too weak
            Status = "active"
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/v1/users", user);

        // Assert
        var result = await response.Content.ReadFromJsonAsync<ApiResponse<CreateUserResponse>>();
        result.Should().NotBeNull();
        result!.ReturnCode.Should().Be(400);
        result.Errors.Should().Contain(e => e.Contains("password", StringComparison.OrdinalIgnoreCase));
    }

    [Fact]
    public async Task CreateUser_WithMissingRequiredFields_ReturnsValidationError()
    {
        // Arrange
        var user = new
        {
            Username = "",
            DisplayName = "",
            Email = "",
            Password = ""
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/v1/users", user);

        // Assert
        var result = await response.Content.ReadFromJsonAsync<ApiResponse<CreateUserResponse>>();
        result.Should().NotBeNull();
        result!.ReturnCode.Should().Be(400);
        result.Errors.Should().HaveCountGreaterOrEqualTo(4);
    }

    #endregion

    #region PUT /api/v1/users/{id} - Update User

    [Fact]
    public async Task UpdateUser_WithValidData_ReturnsSuccess()
    {
        // Arrange - First create a user
        var username = $"testuser_{Guid.NewGuid():N}";
        var createUser = new
        {
            Username = username,
            DisplayName = "Original Name",
            Email = $"test_{Guid.NewGuid():N}@example.com",
            Password = "Test@12345",
            Status = "active"
        };

        var createResponse = await _client.PostAsJsonAsync("/api/v1/users", createUser);
        var createResult = await createResponse.Content.ReadFromJsonAsync<ApiResponse<CreateUserResponse>>();
        var userId = createResult!.Data!.Id;

        // Update the user
        var updateUser = new
        {
            DisplayName = "Updated Name",
            Email = $"updated_{Guid.NewGuid():N}@example.com",
            Status = "active"
        };

        // Act
        var response = await _client.PutAsJsonAsync($"/api/v1/users/{userId}", updateUser);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var result = await response.Content.ReadFromJsonAsync<ApiResponse<UpdateUserResponse>>();
        result.Should().NotBeNull();
        result!.ReturnCode.Should().Be(0);
    }

    [Fact]
    public async Task UpdateUser_WithNonExistentId_ReturnsNotFound()
    {
        // Arrange
        const long nonExistentUserId = 999999;
        var updateUser = new
        {
            DisplayName = "Updated Name",
            Email = "updated@example.com"
        };

        // Act
        var response = await _client.PutAsJsonAsync($"/api/v1/users/{nonExistentUserId}", updateUser);

        // Assert
        var result = await response.Content.ReadFromJsonAsync<ApiResponse<UpdateUserResponse>>();
        result.Should().NotBeNull();
        result!.ReturnCode.Should().Be(404);
    }

    #endregion

    #region DELETE /api/v1/users/{id} - Delete User

    [Fact]
    public async Task DeleteUser_WithValidId_ReturnsSuccess()
    {
        // Arrange - First create a user
        var username = $"testuser_{Guid.NewGuid():N}";
        var createUser = new
        {
            Username = username,
            DisplayName = "To Be Deleted",
            Email = $"test_{Guid.NewGuid():N}@example.com",
            Password = "Test@12345",
            Status = "active"
        };

        var createResponse = await _client.PostAsJsonAsync("/api/v1/users", createUser);
        var createResult = await createResponse.Content.ReadFromJsonAsync<ApiResponse<CreateUserResponse>>();
        var userId = createResult!.Data!.Id;

        // Act
        var response = await _client.DeleteAsync($"/api/v1/users/{userId}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var result = await response.Content.ReadFromJsonAsync<ApiResponse<bool>>();
        result.Should().NotBeNull();
        result!.ReturnCode.Should().Be(0);
        result.Data.Should().BeTrue();
    }

    [Fact]
    public async Task DeleteUser_WithNonExistentId_ReturnsNotFound()
    {
        // Arrange
        const long nonExistentUserId = 999999;

        // Act
        var response = await _client.DeleteAsync($"/api/v1/users/{nonExistentUserId}");

        // Assert
        var result = await response.Content.ReadFromJsonAsync<ApiResponse<bool>>();
        result.Should().NotBeNull();
        result!.ReturnCode.Should().Be(404);
    }

    #endregion

    #region Complex Scenarios

    [Fact]
    public async Task CompleteUserLifecycle_CreateUpdateDelete_AllSucceed()
    {
        // 1. Create user
        var username = $"lifecycle_{Guid.NewGuid():N}";
        var createUser = new
        {
            Username = username,
            DisplayName = "Lifecycle Test",
            Email = $"lifecycle_{Guid.NewGuid():N}@example.com",
            Password = "Test@12345",
            Status = "active"
        };

        var createResponse = await _client.PostAsJsonAsync("/api/v1/users", createUser);
        createResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        var createResult = await createResponse.Content.ReadFromJsonAsync<ApiResponse<CreateUserResponse>>();
        var userId = createResult!.Data!.Id;

        // 2. Get user
        var getResponse = await _client.GetAsync($"/api/v1/users/{userId}");
        getResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        // 3. Update user
        var updateUser = new
        {
            DisplayName = "Updated Lifecycle",
            Email = $"updated_{Guid.NewGuid():N}@example.com"
        };
        var updateResponse = await _client.PutAsJsonAsync($"/api/v1/users/{userId}", updateUser);
        updateResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        // 4. Delete user
        var deleteResponse = await _client.DeleteAsync($"/api/v1/users/{userId}");
        deleteResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        // 5. Verify deletion
        var verifyResponse = await _client.GetAsync($"/api/v1/users/{userId}");
        var verifyResult = await verifyResponse.Content.ReadFromJsonAsync<ApiResponse<UserDetailDto>>();
        verifyResult!.ReturnCode.Should().Be(404);
    }

    #endregion

    #region Helper DTOs

    private class UserDto
    {
        public long Id { get; set; }
        public string Username { get; set; } = default!;
        public string DisplayName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Status { get; set; } = default!;
    }

    private class UserDetailDto : UserDto
    {
        public string? EmployeeCode { get; set; }
        public DateTime? CreatedAt { get; set; }
    }

    private class CreateUserResponse
    {
        public long Id { get; set; }
    }

    private class UpdateUserResponse
    {
        public bool Success { get; set; }
    }

    #endregion
}
