# Script to generate CoreResource.cs from database
$ErrorActionPreference = "Stop"

$connectionString = "Server=TCPHUONG\SQLEXPRESS;Database=UniManage;User Id=uni_manager;Password=uni_manager@2024;TrustServerCertificate=True;"

Write-Host "Connecting to database..." -ForegroundColor Yellow

try {
    $conn = New-Object System.Data.SqlClient.SqlConnection($connectionString)
    $conn.Open()
    
    Write-Host "[OK] Database connected" -ForegroundColor Green
    
    # Get default language
    $cmd = $conn.CreateCommand()
    $cmd.CommandText = "SELECT TOP 1 LanguageCode FROM sy_languages ORDER BY IsDefault DESC, LanguageCode"
    $defaultLang = $cmd.ExecuteScalar()
    
    if (-not $defaultLang) {
        $defaultLang = "vi-VN"
    }
    
    Write-Host "Default language: $defaultLang" -ForegroundColor Cyan
    
    # Get all resources for default language
    $sqlQuery = "SELECT ResourceKey, ResourceValue FROM sy_resources WHERE LanguageCode = @LanguageCode ORDER BY ResourceKey"
    $cmd.CommandText = $sqlQuery
    $param = $cmd.Parameters.Add("@LanguageCode", [System.Data.SqlDbType]::NVarChar, 10)
    $param.Value = $defaultLang
    
    $reader = $cmd.ExecuteReader()
    
    $resources = @{}
    while ($reader.Read()) {
        $key = $reader.GetString(0)
        $value = if ($reader.IsDBNull(1)) { "" } else { $reader.GetString(1) }
        $resources[$key] = $value
    }
    
    $reader.Close()
    $conn.Close()
    
    Write-Host "[OK] Loaded $($resources.Count) resources" -ForegroundColor Green
    
    # Generate C# file
    $output = @"
namespace UniManage.Resource
{
    /// <summary>
    /// Core resource strings for UniManage application
    /// Auto-generated from database by PowerShell script
    /// Last generated: $(Get-Date -Format "yyyy-MM-dd HH:mm:ss")
    /// </summary>
    public static class CoreResource
    {
"@
    
    foreach ($key in $resources.Keys | Sort-Object) {
        $value = $resources[$key].Replace("`"", "\`"").Replace("`n", "\n").Replace("`r", "")
        $output += @"

        /// <summary>
        /// $key`: $value
        /// </summary>
        public static string $key => "$value";
        
"@
    }
    
    $output += @"
    }
}
"@
    
    $outputPath = Join-Path $PSScriptRoot "CoreResource.cs"
    [System.IO.File]::WriteAllText($outputPath, $output, [System.Text.Encoding]::UTF8)
    
    Write-Host "[OK] CoreResource.cs generated successfully!" -ForegroundColor Green
    Write-Host "  Output: $outputPath" -ForegroundColor Cyan
}
catch {
    Write-Host "[ERROR] $_" -ForegroundColor Red
    Write-Host $_.Exception.Message -ForegroundColor Red
    exit 1
}
