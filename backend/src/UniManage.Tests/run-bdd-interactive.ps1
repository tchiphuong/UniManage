#!/usr/bin/env pwsh

# Quick BDD Test Runner - Interactive Menu

Write-Host ""
Write-Host "╔═══════════════════════════════════════════════╗" -ForegroundColor Cyan
Write-Host "║   🥒 SpecFlow BDD Test Runner              ║" -ForegroundColor Cyan
Write-Host "╚═══════════════════════════════════════════════╝" -ForegroundColor Cyan
Write-Host ""

Write-Host "Select test suite to run:" -ForegroundColor Yellow
Write-Host ""
Write-Host "  1. 🔥 Smoke Tests (Critical paths only)" -ForegroundColor White
Write-Host "  2. 👥 User Management Tests" -ForegroundColor White
Write-Host "  3. 🔐 Authentication Tests" -ForegroundColor White
Write-Host "  4. ✅ Validation Tests" -ForegroundColor White
Write-Host "  5. 😊 Positive Scenarios" -ForegroundColor White
Write-Host "  6. ⚠️  Negative Scenarios" -ForegroundColor White
Write-Host "  7. 🌐 All BDD Tests" -ForegroundColor White
Write-Host "  8. 🏷️  Custom Tags..." -ForegroundColor White
Write-Host "  9. 📊 Generate Report Only" -ForegroundColor White
Write-Host "  0. ❌ Exit" -ForegroundColor White
Write-Host ""

$choice = Read-Host "Enter your choice (0-9)"

switch ($choice) {
    "1" {
        Write-Host "`n🔥 Running Smoke Tests..." -ForegroundColor Cyan
        dotnet test --filter "Category=smoke" --logger "console;verbosity=normal"
    }
    "2" {
        Write-Host "`n👥 Running User Management Tests..." -ForegroundColor Cyan
        dotnet test --filter "Category=users" --logger "console;verbosity=normal"
    }
    "3" {
        Write-Host "`n🔐 Running Authentication Tests..." -ForegroundColor Cyan
        dotnet test --filter "Category=auth" --logger "console;verbosity=normal"
    }
    "4" {
        Write-Host "`n✅ Running Validation Tests..." -ForegroundColor Cyan
        dotnet test --filter "Category=validation" --logger "console;verbosity=normal"
    }
    "5" {
        Write-Host "`n😊 Running Positive Scenarios..." -ForegroundColor Cyan
        dotnet test --filter "Category=positive" --logger "console;verbosity=normal"
    }
    "6" {
        Write-Host "`n⚠️  Running Negative Scenarios..." -ForegroundColor Cyan
        dotnet test --filter "Category=negative" --logger "console;verbosity=normal"
    }
    "7" {
        Write-Host "`n🌐 Running All BDD Tests..." -ForegroundColor Cyan
        dotnet test --filter "FullyQualifiedName~BDD" --logger "console;verbosity=normal"
    }
    "8" {
        Write-Host ""
        $tags = Read-Host "Enter tags separated by commas (e.g., smoke,users)"
        $tagArray = $tags -split "," | ForEach-Object { $_.Trim() }
        $filter = ($tagArray | ForEach-Object { "Category=$_" }) -join "|"
        
        Write-Host "`n🏷️  Running tests with tags: $tags" -ForegroundColor Cyan
        dotnet test --filter $filter --logger "console;verbosity=normal"
    }
    "9" {
        Write-Host "`n📊 Generating test report..." -ForegroundColor Cyan
        $reportPath = "TestResults/BDD-Report-$(Get-Date -Format 'yyyyMMdd-HHmmss').html"
        dotnet test --filter "FullyQualifiedName~BDD" --logger "html;LogFileName=$reportPath"
        
        if ($LASTEXITCODE -eq 0) {
            Write-Host "`n✅ Report generated: $reportPath" -ForegroundColor Green
            Start-Process $reportPath
        }
    }
    "0" {
        Write-Host "`nGoodbye! 👋" -ForegroundColor Yellow
        exit 0
    }
    default {
        Write-Host "`n❌ Invalid choice. Please try again." -ForegroundColor Red
        exit 1
    }
}

# Display results
Write-Host ""
if ($LASTEXITCODE -eq 0) {
    Write-Host "✅ Tests completed successfully!" -ForegroundColor Green
} else {
    Write-Host "❌ Some tests failed. Check output above." -ForegroundColor Red
    exit $LASTEXITCODE
}

Write-Host ""
Write-Host "Press any key to exit..."
$null = $Host.UI.RawUI.ReadKey("NoEcho,IncludeKeyDown")
