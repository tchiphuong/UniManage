# Run all tests
Write-Host "Running all tests..." -ForegroundColor Cyan
dotnet test --logger "console;verbosity=normal"

if ($LASTEXITCODE -eq 0) {
    Write-Host "`n✅ All tests passed!" -ForegroundColor Green
} else {
    Write-Host "`n❌ Some tests failed!" -ForegroundColor Red
    exit $LASTEXITCODE
}
