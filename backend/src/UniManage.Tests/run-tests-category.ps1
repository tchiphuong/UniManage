# Run specific test category
param(
    [Parameter(Mandatory=$true)]
    [ValidateSet("Unit", "Integration", "Controllers", "Validators", "Handlers", "Utilities")]
    [string]$Category
)

$filter = switch ($Category) {
    "Unit" { "FullyQualifiedName~UnitTests" }
    "Integration" { "FullyQualifiedName~IntegrationTests" }
    "Controllers" { "FullyQualifiedName~Controllers" }
    "Validators" { "FullyQualifiedName~Validators" }
    "Handlers" { "FullyQualifiedName~Handlers" }
    "Utilities" { "FullyQualifiedName~Utilities" }
}

Write-Host "Running $Category tests..." -ForegroundColor Cyan
dotnet test --filter $filter --logger "console;verbosity=normal"

if ($LASTEXITCODE -eq 0) {
    Write-Host "`n✅ $Category tests passed!" -ForegroundColor Green
} else {
    Write-Host "`n❌ $Category tests failed!" -ForegroundColor Red
    exit $LASTEXITCODE
}
