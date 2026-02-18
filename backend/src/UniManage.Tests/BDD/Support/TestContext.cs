using TechTalk.SpecFlow;

namespace UniManage.Tests.BDD.Support;

/// <summary>
/// Shared context for passing data between step definitions
/// </summary>
public class TestContext
{
    private readonly ScenarioContext _scenarioContext;

    public TestContext(ScenarioContext scenarioContext)
    {
        _scenarioContext = scenarioContext;
    }

    #region HTTP Response Management

    public HttpResponseMessage? LastResponse
    {
        get => _scenarioContext.ContainsKey("LastResponse")
            ? _scenarioContext.Get<HttpResponseMessage>("LastResponse")
            : null;
        set => _scenarioContext.Set(value, "LastResponse");
    }

    public T? GetResponseData<T>() where T : class
    {
        return _scenarioContext.ContainsKey("ResponseData")
            ? _scenarioContext.Get<T>("ResponseData")
            : null;
    }

    public void SetResponseData<T>(T data)
    {
        _scenarioContext.Set(data, "ResponseData");
    }

    #endregion

    #region User Management

    public long? CreatedUserId
    {
        get => _scenarioContext.ContainsKey("CreatedUserId")
            ? _scenarioContext.Get<long>("CreatedUserId")
            : null;
        set => _scenarioContext.Set(value, "CreatedUserId");
    }

    public string? CreatedUsername
    {
        get => _scenarioContext.ContainsKey("CreatedUsername")
            ? _scenarioContext.Get<string>("CreatedUsername")
            : null;
        set => _scenarioContext.Set(value, "CreatedUsername");
    }

    #endregion

    #region Generic Data Storage

    public void Set<T>(string key, T value)
    {
        _scenarioContext.Set(value, key);
    }

    public T? Get<T>(string key)
    {
        return _scenarioContext.ContainsKey(key)
            ? _scenarioContext.Get<T>(key)
            : default;
    }

    public bool ContainsKey(string key)
    {
        return _scenarioContext.ContainsKey(key);
    }

    #endregion

    #region Test Data Generation

    public string GenerateUniqueUsername()
    {
        return $"testuser_{Guid.NewGuid():N}";
    }

    public string GenerateUniqueEmail()
    {
        return $"test_{Guid.NewGuid():N}@example.com";
    }

    public string GenerateUniqueCode()
    {
        return $"CODE_{DateTime.Now.Ticks}";
    }

    #endregion
}
