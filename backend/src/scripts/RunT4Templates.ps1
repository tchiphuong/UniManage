# Script to run T4 templates with proper assembly references
# Requires: dotnet tool install -g dotnet-t4

param(
    [switch]$All,
    [switch]$CoreResource,
    [switch]$DatabaseModels,
    [switch]$CoreLanguage
)

$ErrorActionPreference = "Stop"

Write-Host "=== T4 Template Generator ===" -ForegroundColor Cyan
Write-Host ""

# Paths
$scriptDir = Split-Path -Parent $MyInvocation.MyCommand.Path
$srcDir = Split-Path -Parent $scriptDir
$coreDir = Join-Path $srcDir "UniManage.Core"
$resourceDir = Join-Path $srcDir "UniManage.Resource"

# Find NuGet packages directory
$nugetPackagesDir = Join-Path $env:USERPROFILE ".nuget\packages"
if (-not (Test-Path $nugetPackagesDir)) {
    Write-Host "ERROR: NuGet packages directory not found: $nugetPackagesDir" -ForegroundColor Red
    exit 1
}

# Find required assemblies
$sqlClientPath = Get-ChildItem -Path $nugetPackagesDir -Recurse -Filter "Microsoft.Data.SqlClient.dll" | 
    Where-Object { $_.FullName -like "*net6.0*" -or $_.FullName -like "*netstandard2.0*" } | 
    Select-Object -First 1

$newtonsoftPath = Get-ChildItem -Path $nugetPackagesDir -Recurse -Filter "Newtonsoft.Json.dll" | 
    Where-Object { $_.FullName -like "*net6.0*" -or $_.FullName -like "*netstandard2.0*" } | 
    Select-Object -First 1

if (-not $sqlClientPath) {
    Write-Host "ERROR: Microsoft.Data.SqlClient.dll not found in NuGet packages" -ForegroundColor Red
    Write-Host "Run: dotnet add package Microsoft.Data.SqlClient" -ForegroundColor Yellow
    exit 1
}

if (-not $newtonsoftPath) {
    Write-Host "ERROR: Newtonsoft.Json.dll not found in NuGet packages" -ForegroundColor Red
    Write-Host "Run: dotnet add package Newtonsoft.Json" -ForegroundColor Yellow
    exit 1
}

Write-Host "Found assemblies:" -ForegroundColor Green
Write-Host "  SqlClient: $($sqlClientPath.FullName)" -ForegroundColor Gray
Write-Host "  Newtonsoft: $($newtonsoftPath.FullName)" -ForegroundColor Gray
Write-Host ""

function Run-T4Template {
    param(
        [string]$TemplatePath,
        [string]$Name
    )
    
    Write-Host "Processing: $Name" -ForegroundColor Yellow
    Write-Host "  Template: $TemplatePath" -ForegroundColor Gray
    
    if (-not (Test-Path $TemplatePath)) {
        Write-Host "  ERROR: Template not found!" -ForegroundColor Red
        return $false
    }
    
    # Check if dotnet-t4 is installed
    $t4Tool = Get-Command "t4" -ErrorAction SilentlyContinue
    if (-not $t4Tool) {
        Write-Host "  ERROR: t4 tool not installed. Run: dotnet tool install -g dotnet-t4" -ForegroundColor Red
        return $false
    }
    
    # Run T4 template
    try {
        $outputPath = $TemplatePath -replace "\.tt$", ".cs"
        
        & t4 `
            -r="$($sqlClientPath.FullName)" `
            -r="$($newtonsoftPath.FullName)" `
            -o="$outputPath" `
            "$TemplatePath"
        
        if ($LASTEXITCODE -eq 0) {
            Write-Host "  SUCCESS: Generated $outputPath" -ForegroundColor Green
            return $true
        } else {
            Write-Host "  FAILED: Exit code $LASTEXITCODE" -ForegroundColor Red
            return $false
        }
    }
    catch {
        Write-Host "  ERROR: $_" -ForegroundColor Red
        return $false
    }
}

# Run templates based on parameters
$success = $true

if ($All -or $CoreResource) {
    $template = Join-Path $resourceDir "CoreResource.tt"
    if (-not (Run-T4Template -TemplatePath $template -Name "CoreResource")) {
        $success = $false
    }
    Write-Host ""
}

if ($All -or $DatabaseModels) {
    $template = Join-Path $coreDir "Database\Generator\DatabaseModels.tt"
    if (-not (Run-T4Template -TemplatePath $template -Name "DatabaseModels")) {
        $success = $false
    }
    Write-Host ""
}

if ($All -or $CoreLanguage) {
    $template = Join-Path $coreDir "Database\Generator\CoreLanguage.tt"
    if (-not (Run-T4Template -TemplatePath $template -Name "CoreLanguage")) {
        $success = $false
    }
    Write-Host ""
}

if (-not ($All -or $CoreResource -or $DatabaseModels -or $CoreLanguage)) {
    Write-Host "Usage:" -ForegroundColor Yellow
    Write-Host "  .\RunT4Templates.ps1 -All              # Run all templates"
    Write-Host "  .\RunT4Templates.ps1 -CoreResource     # Run CoreResource.tt only"
    Write-Host "  .\RunT4Templates.ps1 -DatabaseModels   # Run DatabaseModels.tt only"
    Write-Host "  .\RunT4Templates.ps1 -CoreLanguage     # Run CoreLanguage.tt only"
    Write-Host ""
    Write-Host "First install: dotnet tool install -g dotnet-t4" -ForegroundColor Cyan
}

if ($success) {
    Write-Host "=== All templates processed successfully ===" -ForegroundColor Green
    exit 0
} else {
    Write-Host "=== Some templates failed ===" -ForegroundColor Red
    exit 1
}
