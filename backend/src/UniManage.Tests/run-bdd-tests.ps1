# Run BDD tests with SpecFlow
param(
    [ValidateSet("All", "Smoke", "Users", "Auth", "Validation", "Positive", "Negative")]
    [string]$Suite = "All",
    
    [switch]$WithReport
)

Write-Host "🥒 Running BDD Tests with SpecFlow" -ForegroundColor Cyan
Write-Host "Suite: $Suite" -ForegroundColor Yellow
Write-Host ""

# Build filter based on suite
$filter = switch ($Suite) {
    "All" { "FullyQualifiedName~BDD" }
    "Smoke" { "Category=smoke" }
    "Users" { "Category=users" }
    "Auth" { "Category=auth" }
    "Validation" { "Category=validation" }
    "Positive" { "Category=positive" }
    "Negative" { "Category=negative" }
}

# Prepare test command
$testCommand = "dotnet test --filter `"$filter`" --logger `"console;verbosity=normal`""

# Add HTML report if requested
if ($WithReport) {
    $reportPath = "TestResults/BDD-Report-$(Get-Date -Format 'yyyyMMdd-HHmmss').html"
    $testCommand += " --logger `"html;LogFileName=$reportPath`""
    Write-Host "📊 HTML report will be generated at: $reportPath" -ForegroundColor Yellow
    Write-Host ""
}

# Run tests
Write-Host "▶️  Executing tests..." -ForegroundColor Cyan
Invoke-Expression $testCommand

# Check results
if ($LASTEXITCODE -eq 0) {
    Write-Host ""
    Write-Host "✅ All BDD tests passed!" -ForegroundColor Green
    
    if ($WithReport) {
        Write-Host ""
        Write-Host "📊 Opening test report..." -ForegroundColor Yellow
        Start-Process $reportPath
    }
} else {
    Write-Host ""
    Write-Host "❌ Some BDD tests failed!" -ForegroundColor Red
    Write-Host "Check the output above for details." -ForegroundColor Yellow
    exit $LASTEXITCODE
}
