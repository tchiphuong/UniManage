using UniManage.Core.Utilities;

namespace UniManage.Tests.UnitTests.Utilities;

/// <summary>
/// Unit tests for ValidationHelper utility
/// </summary>
public class ValidationHelperTests
{
    #region IsValidEmail Tests

    [Theory]
    [InlineData("user@example.com", true)]
    [InlineData("user.name@example.com", true)]
    [InlineData("user+tag@example.co.uk", true)]
    [InlineData("user123@subdomain.example.com", true)]
    [InlineData("user_name@example.com", true)]
    public void IsValidEmail_WithValidEmails_ReturnsTrue(string email, bool expected)
    {
        // Act
        var result = ValidationHelper.IsValidEmail(email);

        // Assert
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData("invalid", false)]
    [InlineData("invalid@", false)]
    [InlineData("@invalid.com", false)]
    [InlineData("invalid@.com", false)]
    [InlineData("invalid@domain", false)]
    [InlineData("", false)]
    [InlineData(null, false)]
    [InlineData("user name@example.com", false)] // Space
    [InlineData("user@domain .com", false)] // Space
    public void IsValidEmail_WithInvalidEmails_ReturnsFalse(string? email, bool expected)
    {
        // Act
        var result = ValidationHelper.IsValidEmail(email!);

        // Assert
        result.Should().Be(expected);
    }

    #endregion

    #region IsValidPhoneNumber Tests

    [Theory]
    [InlineData("0123456789", true)] // 10 digits
    [InlineData("0987654321", true)]
    [InlineData("+84123456789", true)] // International format
    [InlineData("84123456789", true)]
    public void IsValidPhoneNumber_WithValidPhones_ReturnsTrue(string phone, bool expected)
    {
        // Act
        var result = ValidationHelper.IsValidPhoneNumber(phone);

        // Assert
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData("123", false)] // Too short
    [InlineData("abcdefghij", false)] // Letters
    [InlineData("012-345-6789", false)] // With dashes (if not allowed)
    [InlineData("", false)]
    [InlineData(null, false)]
    public void IsValidPhoneNumber_WithInvalidPhones_ReturnsFalse(string? phone, bool expected)
    {
        // Act
        var result = ValidationHelper.IsValidPhoneNumber(phone!);

        // Assert
        result.Should().Be(expected);
    }

    #endregion

    #region IsValidCode Tests

    [Theory]
    [InlineData("USER123", true)]
    [InlineData("user_code", true)]
    [InlineData("user.code", true)]
    [InlineData("USER-CODE", true)]
    [InlineData("ABC123", true)]
    public void IsValidCode_WithValidCodes_ReturnsTrue(string code, bool expected)
    {
        // Act
        var result = ValidationHelper.IsValidCode(code);

        // Assert
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData("user code", false)] // Space
    [InlineData("user@code", false)] // Special char
    [InlineData("user#123", false)] // Special char
    [InlineData("", false)]
    [InlineData(null, false)]
    public void IsValidCode_WithInvalidCodes_ReturnsFalse(string? code, bool expected)
    {
        // Act
        var result = ValidationHelper.IsValidCode(code!);

        // Assert
        result.Should().Be(expected);
    }

    #endregion

    #region IsValidUserCode Tests

    [Theory]
    [InlineData("john123", true)]
    [InlineData("john_doe", true)]
    [InlineData("johndoe", true)]
    [InlineData("JOHN123", true)]
    public void IsValidUserCode_WithValidUserCodes_ReturnsTrue(string userCode, bool expected)
    {
        // Act
        var result = ValidationHelper.IsValidUserCode(userCode);

        // Assert
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData("john doe", false)] // Space
    [InlineData("john@doe", false)] // Special char
    [InlineData("john#123", false)] // Special char
    [InlineData("ab", false)] // Too short
    [InlineData("", false)]
    public void IsValidUserCode_WithInvalidUserCodes_ReturnsFalse(string userCode, bool expected)
    {
        // Act
        var result = ValidationHelper.IsValidUserCode(userCode);

        // Assert
        result.Should().Be(expected);
    }

    #endregion

    #region ToFieldErrorModels Tests

    [Fact]
    public void ToFieldErrorModels_WithValidationFailures_ReturnsFieldErrors()
    {
        // Arrange
        var failures = new[]
        {
            new FluentValidation.Results.ValidationFailure("Username", "Username is required"),
            new FluentValidation.Results.ValidationFailure("Email", "Email is invalid"),
            new FluentValidation.Results.ValidationFailure("Email", "Email already exists")
        };

        // Act
        var errors = ValidationHelper.ToFieldErrorModels(failures);

        // Assert
        errors.Should().HaveCount(2); // 2 fields: Username, Email
        errors.Should().ContainSingle(e => e.Field == "Username");
        errors.Should().ContainSingle(e => e.Field == "Email");

        var emailErrors = errors.First(e => e.Field == "Email");
        emailErrors.Messages.Should().HaveCount(2);
    }

    [Fact]
    public void ToFieldErrorModels_WithEmptyFailures_ReturnsEmptyList()
    {
        // Arrange
        var failures = Array.Empty<FluentValidation.Results.ValidationFailure>();

        // Act
        var errors = ValidationHelper.ToFieldErrorModels(failures);

        // Assert
        errors.Should().BeEmpty();
    }

    #endregion

    #region Complex Validation Scenarios

    [Theory]
    [InlineData("admin@company.com", "0912345678", "ADMIN001", true)]
    [InlineData("user@company.com", "0987654321", "USER123", true)]
    public void ValidateCompleteUserInfo_WithValidData_AllValidationsPass(
        string email, string phone, string userCode, bool expected)
    {
        // Act
        var emailValid = ValidationHelper.IsValidEmail(email);
        var phoneValid = ValidationHelper.IsValidPhoneNumber(phone);
        var codeValid = ValidationHelper.IsValidUserCode(userCode);

        // Assert
        emailValid.Should().Be(expected);
        phoneValid.Should().Be(expected);
        codeValid.Should().Be(expected);
    }

    [Fact]
    public void ValidateMultipleFields_WithMixedValidity_ReturnsCorrectResults()
    {
        // Arrange
        var email = "invalid-email";
        var phone = "0123456789";
        var code = "invalid code";

        // Act
        var emailValid = ValidationHelper.IsValidEmail(email);
        var phoneValid = ValidationHelper.IsValidPhoneNumber(phone);
        var codeValid = ValidationHelper.IsValidCode(code);

        // Assert
        emailValid.Should().BeFalse();
        phoneValid.Should().BeTrue();
        codeValid.Should().BeFalse();
    }

    #endregion
}
