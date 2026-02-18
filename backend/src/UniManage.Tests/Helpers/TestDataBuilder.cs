namespace UniManage.Tests.Helpers;

/// <summary>
/// Builder for creating test data objects
/// </summary>
public static class TestDataBuilder
{
    /// <summary>
    /// Create a test user model
    /// </summary>
    public static class Users
    {
        public static object CreateValid(string? username = null, string? email = null)
        {
            return new
            {
                Username = username ?? $"test_user_{Guid.NewGuid():N}",
                DisplayName = "Test User",
                Email = email ?? $"test_{Guid.NewGuid():N}@example.com",
                Password = "Test@12345",
                Status = "active"
            };
        }

        public static object CreateInvalid()
        {
            return new
            {
                Username = "", // Invalid: empty
                DisplayName = "",
                Email = "invalid-email",
                Password = "123" // Too short
            };
        }
    }

    /// <summary>
    /// Create test menu data
    /// </summary>
    public static class Menus
    {
        public static object CreateValid(string? code = null, string? parentCode = null)
        {
            return new
            {
                Code = code ?? $"MENU_{Guid.NewGuid():N}",
                NameVi = "Test Menu",
                NameEn = "Test Menu",
                ParentCode = parentCode,
                Sort = 1,
                Icon = "icon-test",
                Url = "/test",
                Status = "active"
            };
        }
    }

    /// <summary>
    /// Create test role data
    /// </summary>
    public static class Roles
    {
        public static object CreateValid(string? code = null)
        {
            return new
            {
                Code = code ?? $"ROLE_{Guid.NewGuid():N}",
                NameVi = "Test Role",
                NameEn = "Test Role",
                Description = "Test role description",
                Status = "active"
            };
        }
    }

    /// <summary>
    /// Create test country data
    /// </summary>
    public static class Countries
    {
        public static object CreateValid(string? code = null)
        {
            return new
            {
                Code = code ?? $"TST",
                NameVi = "Test Country",
                NameEn = "Test Country",
                CurrencyCode = "USD",
                Status = "active"
            };
        }
    }
}
