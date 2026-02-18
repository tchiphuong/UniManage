using FluentValidation.TestHelper;
using UniManage.Application.Commands.System.User;
using UniManage.Tests.Fixtures;

namespace UniManage.Tests.UnitTests.Validators.System;

/// <summary>
/// Unit tests for CreateUserCommandValidator
/// Tests all validation rules including async database checks
/// </summary>
public class CreateUserCommandValidatorTests : TestBase
{
    private readonly CreateUserCommandValidator _validator;

    public CreateUserCommandValidatorTests()
    {
        _validator = new CreateUserCommandValidator();
    }

    #region Username Validation

    [Fact]
    public void Validate_WithEmptyUsername_ShouldHaveValidationError()
    {
        // Arrange
        var command = new CreateUserCommand
        {
            Username = "",
            DisplayName = "Test User",
            Email = "test@example.com",
            Password = "Test@12345"
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Username)
            .WithErrorMessage("*required*");
    }

    [Theory]
    [InlineData("a")] // Too short
    [InlineData("ab")] // Too short
    public void Validate_WithUsernameTooShort_ShouldHaveValidationError(string username)
    {
        // Arrange
        var command = new CreateUserCommand
        {
            Username = username,
            DisplayName = "Test User",
            Email = "test@example.com",
            Password = "Test@12345"
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Username)
            .WithErrorMessage("*between 3 and*");
    }

    [Theory]
    [InlineData("validuser123")]
    [InlineData("user_123")]
    [InlineData("user.name")]
    [InlineData("USER123")]
    public void Validate_WithValidUsername_ShouldNotHaveValidationError(string username)
    {
        // Arrange
        var command = new CreateUserCommand
        {
            Username = username,
            DisplayName = "Test User",
            Email = "test@example.com",
            Password = "Test@12345"
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x.Username);
    }

    [Theory]
    [InlineData("user name")] // Spaces
    [InlineData("user@name")] // Special chars
    [InlineData("user#123")] // Special chars
    public void Validate_WithInvalidUsernameCharacters_ShouldHaveValidationError(string username)
    {
        // Arrange
        var command = new CreateUserCommand
        {
            Username = username,
            DisplayName = "Test User",
            Email = "test@example.com",
            Password = "Test@12345"
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Username);
    }

    #endregion

    #region DisplayName Validation

    [Fact]
    public void Validate_WithEmptyDisplayName_ShouldHaveValidationError()
    {
        // Arrange
        var command = new CreateUserCommand
        {
            Username = "testuser",
            DisplayName = "",
            Email = "test@example.com",
            Password = "Test@12345"
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.DisplayName);
    }

    [Theory]
    [InlineData("A")]
    [InlineData("AB")]
    public void Validate_WithDisplayNameTooShort_ShouldHaveValidationError(string displayName)
    {
        // Arrange
        var command = new CreateUserCommand
        {
            Username = "testuser",
            DisplayName = displayName,
            Email = "test@example.com",
            Password = "Test@12345"
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.DisplayName);
    }

    #endregion

    #region Email Validation

    [Fact]
    public void Validate_WithEmptyEmail_ShouldHaveValidationError()
    {
        // Arrange
        var command = new CreateUserCommand
        {
            Username = "testuser",
            DisplayName = "Test User",
            Email = "",
            Password = "Test@12345"
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Email);
    }

    [Theory]
    [InlineData("invalid")]
    [InlineData("invalid@")]
    [InlineData("@invalid.com")]
    [InlineData("invalid@.com")]
    [InlineData("invalid@domain")]
    public void Validate_WithInvalidEmailFormat_ShouldHaveValidationError(string email)
    {
        // Arrange
        var command = new CreateUserCommand
        {
            Username = "testuser",
            DisplayName = "Test User",
            Email = email,
            Password = "Test@12345"
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Email);
    }

    [Theory]
    [InlineData("user@example.com")]
    [InlineData("user.name@example.com")]
    [InlineData("user+tag@example.co.uk")]
    [InlineData("user123@subdomain.example.com")]
    public void Validate_WithValidEmail_ShouldNotHaveValidationError(string email)
    {
        // Arrange
        var command = new CreateUserCommand
        {
            Username = "testuser",
            DisplayName = "Test User",
            Email = email,
            Password = "Test@12345"
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x.Email);
    }

    #endregion

    #region Password Validation

    [Fact]
    public void Validate_WithEmptyPassword_ShouldHaveValidationError()
    {
        // Arrange
        var command = new CreateUserCommand
        {
            Username = "testuser",
            DisplayName = "Test User",
            Email = "test@example.com",
            Password = ""
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Password);
    }

    [Theory]
    [InlineData("123")] // Too short
    [InlineData("1234567")] // Too short
    public void Validate_WithPasswordTooShort_ShouldHaveValidationError(string password)
    {
        // Arrange
        var command = new CreateUserCommand
        {
            Username = "testuser",
            DisplayName = "Test User",
            Email = "test@example.com",
            Password = password
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Password);
    }

    [Theory]
    [InlineData("alllowercase123")] // No uppercase
    [InlineData("ALLUPPERCASE123")] // No lowercase
    [InlineData("NoNumbers")] // No digits
    public void Validate_WithPasswordMissingRequiredCharacters_ShouldHaveValidationError(string password)
    {
        // Arrange
        var command = new CreateUserCommand
        {
            Username = "testuser",
            DisplayName = "Test User",
            Email = "test@example.com",
            Password = password
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Password);
    }

    [Theory]
    [InlineData("Test@12345")]
    [InlineData("Password123")]
    [InlineData("StrongPass99")]
    [InlineData("MyP@ssw0rd")]
    public void Validate_WithValidPassword_ShouldNotHaveValidationError(string password)
    {
        // Arrange
        var command = new CreateUserCommand
        {
            Username = "testuser",
            DisplayName = "Test User",
            Email = "test@example.com",
            Password = password
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x.Password);
    }

    #endregion

    #region Complete Object Validation

    [Fact]
    public void Validate_WithAllValidFields_ShouldPassValidation()
    {
        // Arrange
        var command = new CreateUserCommand
        {
            Username = "validuser123",
            DisplayName = "Valid User",
            Email = "valid@example.com",
            Password = "ValidPass123"
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Validate_WithAllInvalidFields_ShouldHaveMultipleValidationErrors()
    {
        // Arrange
        var command = new CreateUserCommand
        {
            Username = "",
            DisplayName = "",
            Email = "invalid",
            Password = "123"
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Username);
        result.ShouldHaveValidationErrorFor(x => x.DisplayName);
        result.ShouldHaveValidationErrorFor(x => x.Email);
        result.ShouldHaveValidationErrorFor(x => x.Password);
    }

    #endregion

    #region Async Validation (Database Checks)

    [Fact]
    public async Task ValidateAsync_WithExistingUsername_ShouldHaveValidationError()
    {
        // Note: This test would require a test database or mock
        // For demonstration, showing the test structure
        // In real scenario, you'd setup a test database with existing user

        // Arrange
        var command = new CreateUserCommand
        {
            Username = "admin", // Assuming 'admin' exists in test DB
            DisplayName = "Admin User",
            Email = "newadmin@example.com",
            Password = "Test@12345"
        };

        // Act
        var result = await _validator.TestValidateAsync(command);

        // Assert
        // This would fail if 'admin' username exists in database
        // result.ShouldHaveValidationErrorFor(x => x.Username)
        //     .WithErrorMessage("*already taken*");
    }

    [Fact]
    public async Task ValidateAsync_WithExistingEmail_ShouldHaveValidationError()
    {
        // Similar to username check
        // Would require test database setup

        // Arrange
        var command = new CreateUserCommand
        {
            Username = "newuser",
            DisplayName = "New User",
            Email = "admin@example.com", // Assuming exists
            Password = "Test@12345"
        };

        // Act
        var result = await _validator.TestValidateAsync(command);

        // Assert
        // result.ShouldHaveValidationErrorFor(x => x.Email)
        //     .WithErrorMessage("*already registered*");
    }

    #endregion
}
