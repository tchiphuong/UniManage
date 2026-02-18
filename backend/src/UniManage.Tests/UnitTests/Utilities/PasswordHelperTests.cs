using UniManage.Core.Utilities;

namespace UniManage.Tests.UnitTests.Utilities;

/// <summary>
/// Unit tests for PasswordHelper utility
/// </summary>
public class PasswordHelperTests
{
    #region HashPassword Tests

    [Fact]
    public void HashPassword_WithValidPassword_ReturnsHashedString()
    {
        // Arrange
        var password = "TestPassword123";

        // Act
        var hash = PasswordHelper.HashPassword(password);

        // Assert
        hash.Should().NotBeNullOrEmpty();
        hash.Should().NotBe(password);
        hash.Length.Should().BeGreaterThan(30);
    }

    [Fact]
    public void HashPassword_WithSamePassword_ReturnsDifferentHashes()
    {
        // Arrange
        var password = "TestPassword123";

        // Act
        var hash1 = PasswordHelper.HashPassword(password);
        var hash2 = PasswordHelper.HashPassword(password);

        // Assert
        hash1.Should().NotBe(hash2, "BCrypt should use different salts");
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void HashPassword_WithEmptyOrNullPassword_ThrowsException(string? password)
    {
        // Act & Assert
        var act = () => PasswordHelper.HashPassword(password!);
        act.Should().Throw<ArgumentException>();
    }

    #endregion

    #region VerifyPassword Tests

    [Fact]
    public void VerifyPassword_WithCorrectPassword_ReturnsTrue()
    {
        // Arrange
        var password = "TestPassword123";
        var hash = PasswordHelper.HashPassword(password);

        // Act
        var result = PasswordHelper.VerifyPassword(password, hash);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void VerifyPassword_WithIncorrectPassword_ReturnsFalse()
    {
        // Arrange
        var password = "TestPassword123";
        var wrongPassword = "WrongPassword456";
        var hash = PasswordHelper.HashPassword(password);

        // Act
        var result = PasswordHelper.VerifyPassword(wrongPassword, hash);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void VerifyPassword_WithCaseSensitivity_ReturnsFalse()
    {
        // Arrange
        var password = "TestPassword123";
        var wrongCase = "testpassword123";
        var hash = PasswordHelper.HashPassword(password);

        // Act
        var result = PasswordHelper.VerifyPassword(wrongCase, hash);

        // Assert
        result.Should().BeFalse("Password verification should be case-sensitive");
    }

    #endregion

    #region GenerateRandomPassword Tests

    [Fact]
    public void GenerateRandomPassword_WithDefaultLength_Returns12CharPassword()
    {
        // Act
        var password = PasswordHelper.GenerateRandomPassword();

        // Assert
        password.Length.Should().Be(12);
    }

    [Theory]
    [InlineData(8)]
    [InlineData(16)]
    [InlineData(20)]
    public void GenerateRandomPassword_WithSpecifiedLength_ReturnsCorrectLength(int length)
    {
        // Act
        var password = PasswordHelper.GenerateRandomPassword(length);

        // Assert
        password.Length.Should().Be(length);
    }

    [Fact]
    public void GenerateRandomPassword_MultipleGenerations_ReturnsDifferentPasswords()
    {
        // Act
        var password1 = PasswordHelper.GenerateRandomPassword();
        var password2 = PasswordHelper.GenerateRandomPassword();
        var password3 = PasswordHelper.GenerateRandomPassword();

        // Assert
        password1.Should().NotBe(password2);
        password2.Should().NotBe(password3);
        password1.Should().NotBe(password3);
    }

    [Fact]
    public void GenerateRandomPassword_ContainsRequiredCharacterTypes()
    {
        // Act
        var password = PasswordHelper.GenerateRandomPassword(16);

        // Assert
        password.Should().MatchRegex(@"[A-Z]", "Should contain uppercase");
        password.Should().MatchRegex(@"[a-z]", "Should contain lowercase");
        password.Should().MatchRegex(@"[0-9]", "Should contain digit");
    }

    #endregion

    #region IsValidPassword Tests

    [Theory]
    [InlineData("Test@12345", true)]
    [InlineData("Password123", true)]
    [InlineData("MyP@ssw0rd", true)]
    [InlineData("StrongPass99", true)]
    public void IsValidPassword_WithValidPasswords_ReturnsTrue(string password, bool expected)
    {
        // Act
        var result = PasswordHelper.IsValidPassword(password);

        // Assert
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData("123", false)] // Too short
    [InlineData("alllower", false)] // No uppercase or digits
    [InlineData("ALLUPPER", false)] // No lowercase or digits
    [InlineData("NoNumbers", false)] // No digits
    [InlineData("", false)] // Empty
    public void IsValidPassword_WithInvalidPasswords_ReturnsFalse(string password, bool expected)
    {
        // Act
        var result = PasswordHelper.IsValidPassword(password);

        // Assert
        result.Should().Be(expected);
    }

    #endregion

    #region Integration Tests

    [Fact]
    public void PasswordWorkflow_HashAndVerify_WorksCorrectly()
    {
        // Arrange
        var password = "MySecurePassword123";

        // Act - Hash the password
        var hash = PasswordHelper.HashPassword(password);

        // Act - Verify correct password
        var verifyCorrect = PasswordHelper.VerifyPassword(password, hash);

        // Act - Verify incorrect password
        var verifyIncorrect = PasswordHelper.VerifyPassword("WrongPassword", hash);

        // Assert
        hash.Should().NotBeNullOrEmpty();
        verifyCorrect.Should().BeTrue();
        verifyIncorrect.Should().BeFalse();
    }

    [Fact]
    public void GenerateAndValidatePassword_CreatesValidPassword()
    {
        // Act
        var password = PasswordHelper.GenerateRandomPassword();
        var isValid = PasswordHelper.IsValidPassword(password);
        var hash = PasswordHelper.HashPassword(password);
        var verified = PasswordHelper.VerifyPassword(password, hash);

        // Assert
        isValid.Should().BeTrue();
        verified.Should().BeTrue();
    }

    #endregion
}
