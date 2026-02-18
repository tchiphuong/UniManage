# Run tests with coverage report
Write-Host "Running tests with coverage..." -ForegroundColor Cyan

dotnet test `
    /p:CollectCoverage=true `
    /p:CoverletOutputFormat=opencover `
    /p:CoverletOutput=./coverage/ `
    --logger "console;verbosity=normal"

if ($LASTEXITCODE -eq 0) {
    Write-Host "`n✅ Tests completed with coverage!" -ForegroundColor Green
    Write-Host "Coverage report generated at: ./coverage/" -ForegroundColor Yellow
} else {
    Write-Host "`n❌ Tests failed!" -ForegroundColor Red
    exit $LASTEXITCODE
}
