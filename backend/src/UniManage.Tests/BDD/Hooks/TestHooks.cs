using BoDi;
using TechTalk.SpecFlow;
using UniManage.Tests.Fixtures;

namespace UniManage.Tests.BDD.Hooks;

/// <summary>
/// SpecFlow hooks for test lifecycle management
/// Executed before/after scenarios and features
/// </summary>
[Binding]
public class TestHooks
{
    private readonly IObjectContainer _objectContainer;
    private UniManageWebApplicationFactory? _factory;
    private HttpClient? _client;

    public TestHooks(IObjectContainer objectContainer)
    {
        _objectContainer = objectContainer;
    }

    #region BeforeTestRun / AfterTestRun

    [BeforeTestRun]
    public static void BeforeTestRun()
    {
        Console.WriteLine("🚀 Starting BDD Test Run...");
        Console.WriteLine($"Test Run Time: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");

        // Global setup - database migrations, seeding, etc.
    }

    [AfterTestRun]
    public static void AfterTestRun()
    {
        Console.WriteLine("✅ BDD Test Run Completed");

        // Global cleanup
    }

    #endregion

    #region BeforeFeature / AfterFeature

    [BeforeFeature]
    public static void BeforeFeature(FeatureContext featureContext)
    {
        Console.WriteLine($"\n📋 Feature: {featureContext.FeatureInfo.Title}");

        // Feature-level setup
    }

    [AfterFeature]
    public static void AfterFeature()
    {
        Console.WriteLine("Feature completed\n");

        // Feature-level cleanup
    }

    #endregion

    #region BeforeScenario / AfterScenario

    [BeforeScenario]
    public void BeforeScenario(ScenarioContext scenarioContext)
    {
        Console.WriteLine($"  🎬 Scenario: {scenarioContext.ScenarioInfo.Title}");

        // Create web application factory for integration tests
        _factory = new UniManageWebApplicationFactory();
        _client = _factory.CreateClient();

        // Register in DI container for step definitions
        _objectContainer.RegisterInstanceAs(_factory);
        _objectContainer.RegisterInstanceAs(_client);

        // Store scenario start time
        scenarioContext.Set(DateTime.Now, "StartTime");
    }

    [AfterScenario]
    public void AfterScenario(ScenarioContext scenarioContext)
    {
        var startTime = scenarioContext.Get<DateTime>("StartTime");
        var duration = DateTime.Now - startTime;

        var status = scenarioContext.TestError == null ? "✅ PASSED" : "❌ FAILED";
        Console.WriteLine($"  {status} (Duration: {duration.TotalSeconds:F2}s)");

        if (scenarioContext.TestError != null)
        {
            Console.WriteLine($"  Error: {scenarioContext.TestError.Message}");
        }

        // Cleanup
        _client?.Dispose();
        _factory?.Dispose();
    }

    #endregion

    #region Tag-Specific Hooks

    [BeforeScenario("@database")]
    public void BeforeDatabaseScenario()
    {
        Console.WriteLine("  🗄️ Setting up database for scenario...");
        // Setup test database, begin transaction, etc.
    }

    [AfterScenario("@database")]
    public void AfterDatabaseScenario()
    {
        Console.WriteLine("  🗄️ Cleaning up database...");
        // Rollback transaction, cleanup test data
    }

    [BeforeScenario("@authenticated")]
    public void BeforeAuthenticatedScenario(HttpClient client)
    {
        Console.WriteLine("  🔐 Setting up authentication...");
        // Add authentication token to HTTP client
        // client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "test-token");
    }

    [BeforeScenario("@slow")]
    public void BeforeSlowScenario()
    {
        Console.WriteLine("  🐢 Slow scenario - increased timeout...");
        // Increase timeout for slow scenarios
    }

    #endregion

    #region Step Hooks

    [BeforeStep]
    public void BeforeStep(ScenarioContext scenarioContext)
    {
        // Optional: Log each step execution
        var stepInfo = scenarioContext.StepContext.StepInfo;
        // Console.WriteLine($"    → {stepInfo.StepDefinitionType}: {stepInfo.Text}");
    }

    [AfterStep]
    public void AfterStep(ScenarioContext scenarioContext)
    {
        // Capture screenshots or logs on step failure
        if (scenarioContext.TestError != null)
        {
            var stepInfo = scenarioContext.StepContext.StepInfo;
            Console.WriteLine($"    ⚠️ Step failed: {stepInfo.Text}");
        }
    }

    #endregion
}
