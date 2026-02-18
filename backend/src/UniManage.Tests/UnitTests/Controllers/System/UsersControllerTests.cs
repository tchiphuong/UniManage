using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UniManage.Api.Controllers.System;
using UniManage.Application.Commands.System.User;
using UniManage.Application.Queries.System.User;
using UniManage.Model.Common;
using UniManage.Tests.Fixtures;

namespace UniManage.Tests.UnitTests.Controllers.System;

/// <summary>
/// Unit tests for UsersController
/// Tests controller routing, parameter binding, and mediator integration
/// </summary>
public class UsersControllerTests : TestBase
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly UsersController _controller;

    public UsersControllerTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _controller = new UsersController(_mediatorMock.Object);

        // Setup HttpContext for HeaderInfo
        _controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext()
        };
    }

    #region GET: /api/v1/users - List Users

    [Fact]
    public async Task GetUserList_WithValidQuery_ReturnsOkWithPagedResult()
    {
        // Arrange
        var query = new GetUserListQuery
        {
            Keyword = "test",
            PageIndex = 1,
            PageSize = 20
        };

        var expectedResponse = new ApiResponse<PagedResult<GetUserListQuery.Response>>
        {
            ReturnCode = 0,
            Message = "Success",
            Data = new PagedResult<GetUserListQuery.Response>
            {
                Items = new List<GetUserListQuery.Response>
                {
                    new GetUserListQuery.Response
                    {
                        Id = 1,
                        Username = "testuser",
                        DisplayName = "Test User",
                        Email = "test@example.com"
                    }
                },
                Paging = new PagingInfo
                {
                    PageIndex = 1,
                    PageSize = 20,
                    TotalItems = 1
                }
            }
        };

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<GetUserListQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _controller.GetUserList(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Result.Should().BeOfType<OkObjectResult>();

        var okResult = result.Result as OkObjectResult;
        okResult!.Value.Should().Be(expectedResponse);

        _mediatorMock.Verify(
            m => m.Send(It.Is<GetUserListQuery>(q =>
                q.Keyword == "test" &&
                q.PageIndex == 1 &&
                q.PageSize == 20 &&
                q.HeaderInfo != null),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task GetUserList_WithNullQuery_CreatesNewQueryAndReturnsOk()
    {
        // Arrange
        var expectedResponse = new ApiResponse<PagedResult<GetUserListQuery.Response>>
        {
            ReturnCode = 0,
            Data = new PagedResult<GetUserListQuery.Response>
            {
                Items = new List<GetUserListQuery.Response>(),
                Paging = new PagingInfo()
            }
        };

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<GetUserListQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _controller.GetUserList(null!, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Result.Should().BeOfType<OkObjectResult>();

        _mediatorMock.Verify(
            m => m.Send(It.Is<GetUserListQuery>(q => q.HeaderInfo != null),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task GetUserList_WithValidationError_ReturnsValidationResponse()
    {
        // Arrange
        var query = new GetUserListQuery { PageIndex = -1 }; // Invalid

        var expectedResponse = new ApiResponse<PagedResult<GetUserListQuery.Response>>
        {
            ReturnCode = 400,
            Message = "Validation failed",
            Errors = new List<string> { "PageIndex must be greater than 0" }
        };

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<GetUserListQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _controller.GetUserList(query, CancellationToken.None);

        // Assert
        var okResult = result.Result as OkObjectResult;
        var response = okResult!.Value as ApiResponse<PagedResult<GetUserListQuery.Response>>;

        response.Should().NotBeNull();
        response!.ReturnCode.Should().Be(400);
        response.Errors.Should().NotBeEmpty();
    }

    #endregion

    #region GET: /api/v1/users/{id} - Get User By Id

    [Fact]
    public async Task GetUserById_WithValidId_ReturnsOkWithUser()
    {
        // Arrange
        const long userId = 1;
        var expectedResponse = new ApiResponse<GetUserByIdQuery.Response>
        {
            ReturnCode = 0,
            Message = "Success",
            Data = new GetUserByIdQuery.Response
            {
                Id = userId,
                Username = "testuser",
                DisplayName = "Test User",
                Email = "test@example.com"
            }
        };

        _mediatorMock
            .Setup(m => m.Send(It.Is<GetUserByIdQuery>(q => q.Id == userId), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _controller.GetUserById(userId, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Result.Should().BeOfType<OkObjectResult>();

        var okResult = result.Result as OkObjectResult;
        var response = okResult!.Value as ApiResponse<GetUserByIdQuery.Response>;

        response.Should().NotBeNull();
        response!.Data.Should().NotBeNull();
        response.Data!.Id.Should().Be(userId);
    }

    [Fact]
    public async Task GetUserById_WithNonExistentId_ReturnsNotFound()
    {
        // Arrange
        const long userId = 999;
        var expectedResponse = new ApiResponse<GetUserByIdQuery.Response>
        {
            ReturnCode = 404,
            Message = "User not found"
        };

        _mediatorMock
            .Setup(m => m.Send(It.Is<GetUserByIdQuery>(q => q.Id == userId), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _controller.GetUserById(userId, CancellationToken.None);

        // Assert
        var okResult = result.Result as OkObjectResult;
        var response = okResult!.Value as ApiResponse<GetUserByIdQuery.Response>;

        response.Should().NotBeNull();
        response!.ReturnCode.Should().Be(404);
    }

    #endregion

    #region POST: /api/v1/users - Create User

    [Fact]
    public async Task CreateUser_WithValidCommand_ReturnsOkWithUserId()
    {
        // Arrange
        var command = new CreateUserCommand
        {
            Username = "newuser",
            DisplayName = "New User",
            Email = "newuser@example.com",
            Password = "Test@12345"
        };

        var expectedResponse = new ApiResponse<CreateUserCommand.Response>
        {
            ReturnCode = 0,
            Message = "User created successfully",
            Data = new CreateUserCommand.Response { Id = 1 }
        };

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<CreateUserCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _controller.CreateUser(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Result.Should().BeOfType<OkObjectResult>();

        var okResult = result.Result as OkObjectResult;
        var response = okResult!.Value as ApiResponse<CreateUserCommand.Response>;

        response.Should().NotBeNull();
        response!.ReturnCode.Should().Be(0);
        response.Data.Should().NotBeNull();
        response.Data!.Id.Should().BeGreaterThan(0);

        _mediatorMock.Verify(
            m => m.Send(It.Is<CreateUserCommand>(c =>
                c.Username == "newuser" &&
                c.Email == "newuser@example.com" &&
                c.HeaderInfo != null),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task CreateUser_WithDuplicateUsername_ReturnsValidationError()
    {
        // Arrange
        var command = new CreateUserCommand
        {
            Username = "existinguser",
            DisplayName = "Test",
            Email = "test@example.com",
            Password = "Test@12345"
        };

        var expectedResponse = new ApiResponse<CreateUserCommand.Response>
        {
            ReturnCode = 400,
            Message = "Validation failed",
            Errors = new List<string> { "Username already exists" }
        };

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<CreateUserCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _controller.CreateUser(command, CancellationToken.None);

        // Assert
        var okResult = result.Result as OkObjectResult;
        var response = okResult!.Value as ApiResponse<CreateUserCommand.Response>;

        response.Should().NotBeNull();
        response!.ReturnCode.Should().Be(400);
        response.Errors.Should().Contain("Username already exists");
    }

    [Fact]
    public async Task CreateUser_WithInvalidEmail_ReturnsValidationError()
    {
        // Arrange
        var command = new CreateUserCommand
        {
            Username = "newuser",
            DisplayName = "Test",
            Email = "invalid-email",
            Password = "Test@12345"
        };

        var expectedResponse = new ApiResponse<CreateUserCommand.Response>
        {
            ReturnCode = 400,
            Message = "Validation failed",
            Errors = new List<string> { "Invalid email format" }
        };

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<CreateUserCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _controller.CreateUser(command, CancellationToken.None);

        // Assert
        var okResult = result.Result as OkObjectResult;
        var response = okResult!.Value as ApiResponse<CreateUserCommand.Response>;

        response.Should().NotBeNull();
        response!.ReturnCode.Should().Be(400);
    }

    #endregion

    #region PUT: /api/v1/users/{id} - Update User

    [Fact]
    public async Task UpdateUser_WithValidCommand_ReturnsOkWithSuccess()
    {
        // Arrange
        const long userId = 1;
        var command = new UpdateUserCommand
        {
            DisplayName = "Updated Name",
            Email = "updated@example.com"
        };

        var expectedResponse = new ApiResponse<UpdateUserCommand.Response>
        {
            ReturnCode = 0,
            Message = "User updated successfully",
            Data = new UpdateUserCommand.Response { Success = true }
        };

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<UpdateUserCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _controller.UpdateUser(userId, command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Result.Should().BeOfType<OkObjectResult>();

        _mediatorMock.Verify(
            m => m.Send(It.Is<UpdateUserCommand>(c =>
                c.Id == userId &&
                c.HeaderInfo != null),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task UpdateUser_WithNonExistentId_ReturnsNotFound()
    {
        // Arrange
        const long userId = 999;
        var command = new UpdateUserCommand
        {
            DisplayName = "Updated Name"
        };

        var expectedResponse = new ApiResponse<UpdateUserCommand.Response>
        {
            ReturnCode = 404,
            Message = "User not found"
        };

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<UpdateUserCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _controller.UpdateUser(userId, command, CancellationToken.None);

        // Assert
        var okResult = result.Result as OkObjectResult;
        var response = okResult!.Value as ApiResponse<UpdateUserCommand.Response>;

        response.Should().NotBeNull();
        response!.ReturnCode.Should().Be(404);
    }

    #endregion

    #region DELETE: /api/v1/users/{id} - Delete User

    [Fact]
    public async Task DeleteUser_WithValidId_ReturnsOkWithSuccess()
    {
        // Arrange
        const long userId = 1;
        var expectedResponse = new ApiResponse<bool>
        {
            ReturnCode = 0,
            Message = "User deleted successfully",
            Data = true
        };

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<DeleteUserCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _controller.DeleteUser(userId, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Result.Should().BeOfType<OkObjectResult>();

        _mediatorMock.Verify(
            m => m.Send(It.Is<DeleteUserCommand>(c =>
                c.Id == userId &&
                c.HeaderInfo != null),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }

    #endregion

    #region Edge Cases & Error Handling

    [Fact]
    public async Task GetUserById_WithZeroId_SendsQueryWithZero()
    {
        // Arrange
        const long userId = 0;
        var expectedResponse = new ApiResponse<GetUserByIdQuery.Response>
        {
            ReturnCode = 404,
            Message = "Invalid user ID"
        };

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<GetUserByIdQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _controller.GetUserById(userId, CancellationToken.None);

        // Assert
        _mediatorMock.Verify(
            m => m.Send(It.Is<GetUserByIdQuery>(q => q.Id == 0),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task CreateUser_WithNegativeId_ReturnsError()
    {
        // Test that mediator handles business logic validation
        // Controller just passes through
        var command = new CreateUserCommand
        {
            Username = "test",
            DisplayName = "Test",
            Email = "test@example.com",
            Password = "Test@12345"
        };

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<CreateUserCommand>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new InvalidOperationException("Database error"));

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(
            async () => await _controller.CreateUser(command, CancellationToken.None));
    }

    #endregion
}
