using UniManage.Model.Common;

namespace UniManage.Tests.Helpers;

/// <summary>
/// Custom assertions for API responses
/// </summary>
public static class ApiResponseAssertions
{
    /// <summary>
    /// Assert that API response is successful
    /// </summary>
    public static void ShouldBeSuccess<T>(this ApiResponse<T> response, string? message = null)
    {
        response.Should().NotBeNull();
        response.ReturnCode.Should().Be(0, message ?? "Response should be successful");
        response.Errors.Should().BeEmpty();
        response.Data.Should().NotBeNull();
    }

    /// <summary>
    /// Assert that API response has error
    /// </summary>
    public static void ShouldHaveError<T>(this ApiResponse<T> response, int expectedReturnCode, string? messageContains = null)
    {
        response.Should().NotBeNull();
        response.ReturnCode.Should().Be(expectedReturnCode);

        if (messageContains != null)
        {
            response.Message.Should().Contain(messageContains);
        }
    }

    /// <summary>
    /// Assert that API response has validation errors
    /// </summary>
    public static void ShouldHaveValidationErrors<T>(this ApiResponse<T> response, params string[] fieldNames)
    {
        response.Should().NotBeNull();
        response.ReturnCode.Should().Be(400, "Validation errors should return 400");
        response.Errors.Should().NotBeEmpty();

        if (fieldNames.Any())
        {
            foreach (var fieldName in fieldNames)
            {
                response.Errors.Should().Contain(e => e.Contains(fieldName, StringComparison.OrdinalIgnoreCase),
                    $"Should have validation error for field: {fieldName}");
            }
        }
    }

    /// <summary>
    /// Assert that paged response is valid
    /// </summary>
    public static void ShouldBeValidPagedResponse<T>(this ApiResponse<PagedResult<T>> response,
        int? expectedPageIndex = null,
        int? expectedPageSize = null)
    {
        response.ShouldBeSuccess();
        response.Data.Should().NotBeNull();
        response.Data!.Items.Should().NotBeNull();
        response.Data.Paging.Should().NotBeNull();

        if (expectedPageIndex.HasValue)
        {
            response.Data.Paging.PageIndex.Should().Be(expectedPageIndex.Value);
        }

        if (expectedPageSize.HasValue)
        {
            response.Data.Paging.PageSize.Should().Be(expectedPageSize.Value);
        }

        response.Data.Paging.TotalItems.Should().BeGreaterOrEqualTo(0);
        response.Data.Paging.TotalPages.Should().BeGreaterOrEqualTo(0);
    }

    /// <summary>
    /// Assert that paged response items match count
    /// </summary>
    public static void ShouldHaveItemCount<T>(this ApiResponse<PagedResult<T>> response, int expectedCount)
    {
        response.Data.Should().NotBeNull();
        response.Data!.Items.Should().HaveCount(expectedCount);
    }

    /// <summary>
    /// Assert that response is not found (404)
    /// </summary>
    public static void ShouldBeNotFound<T>(this ApiResponse<T> response)
    {
        response.Should().NotBeNull();
        response.ReturnCode.Should().Be(404);
    }

    /// <summary>
    /// Assert that response is forbidden (403)
    /// </summary>
    public static void ShouldBeForbidden<T>(this ApiResponse<T> response)
    {
        response.Should().NotBeNull();
        response.ReturnCode.Should().Be(403);
    }

    /// <summary>
    /// Assert that response is unauthorized (401)
    /// </summary>
    public static void ShouldBeUnauthorized<T>(this ApiResponse<T> response)
    {
        response.Should().NotBeNull();
        response.ReturnCode.Should().Be(401);
    }
}
