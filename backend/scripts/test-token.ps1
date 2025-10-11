# Test token from IdentityServer
Write-Host "Testing IdentityServer..." -ForegroundColor Cyan

$body = @{
    client_id = "postman-test"
    client_secret = "secret"
    grant_type = "password"
    username = "admin"
    password = "admin123"
    scope = "openid profile email unimanage.api offline_access"
}

try {
    $response = Invoke-RestMethod -Uri "http://localhost:5001/connect/token" -Method POST -Body $body -ContentType "application/x-www-form-urlencoded"
    Write-Host "SUCCESS! Token retrieved" -ForegroundColor Green
    Write-Host "Access Token:" $response.access_token.Substring(0, 50)
    Write-Host "Expires in:" $response.expires_in "seconds"
    $response | ConvertTo-Json
}
catch {
    Write-Host "FAILED!" -ForegroundColor Red
    Write-Host $_.Exception.Message
}
