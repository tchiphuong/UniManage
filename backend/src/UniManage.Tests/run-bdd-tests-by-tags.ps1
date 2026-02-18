# Run BDD tests by specific tags
param(
    [Parameter(Mandatory=$true)]
    [string[]]$Tags,
    
    [switch]$WithReport
)

Write-Host "🥒 Running BDD Tests by Tags" -ForegroundColor Cyan
Write-Host "Tags: $($Tags -join ', ')" -ForegroundColor Yellow
Write-Host ""

# Build filter for multiple tags (OR condition)
$tagFilters = $Tags | ForEach-Object { "Category=$_" }
$filter = $tagFilters -join "|"

# Prepare test command
$testCommand = "dotnet test --filter `"$filter`" --logger `"console;verbosity=normal`""

# Add HTML report if requested
if ($WithReport) {
    $reportPath = "TestResults/BDD-Tags-Report-$(Get-Date -Format 'yyyyMMdd-HHmmss').html"
    $testCommand += " --logger `"html;LogFileName=$reportPath`""
}

# Run tests
Write-Host "▶️  Executing tests..." -ForegroundColor Cyan
Invoke-Expression $testCommand

# Check results
if ($LASTEXITCODE -eq 0) {
    Write-Host ""
    Write-Host "✅ All BDD tests passed!" -ForegroundColor Green
    
    if ($WithReport) {
        Start-Process $reportPath
    }
} else {
    Write-Host ""
    Write-Host "❌ Some BDD tests failed!" -ForegroundColor Red
    exit $LASTEXITCODE
}

<#
.SYNOPSIS
Run BDD tests filtered by tags

.DESCRIPTION
Runs SpecFlow BDD tests that match the specified tags.
Multiple tags can be provided and will be combined with OR logic.

.PARAMETER Tags
One or more tags to filter tests (smoke, users, auth, validation, etc.)

.PARAMETER WithReport
Generate and open HTML test report

.EXAMPLE
.\run-bdd-tests-by-tags.ps1 -Tags "smoke"

.EXAMPLE
.\run-bdd-tests-by-tags.ps1 -Tags "users", "auth" -WithReport

.EXAMPLE
.\run-bdd-tests-by-tags.ps1 -Tags "smoke", "positive"
#>
