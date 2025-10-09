-- =============================================
-- Example: Encrypt Password for appsettings.json
-- =============================================

/*
HƯỚNG DẪN:

1. Set environment variable (PowerShell):
   $env:UNIMANAGE_ENCRYPTION_SEED = "YourSecretSeed2024"

2. Mã hóa password:
   cd d:\Coding\dot_NET\UniManage\UniManage\src\UniManage.Core
   dotnet run -- encrypt "uni_manager@2024"

3. Copy encrypted value vào appsettings.json:
   {
       "Database": {
           "Password": "ENC:J5xK8mQ3..."
       }
   }

4. Test connection:
   dotnet run --project Database/Test/DatabaseConnectionTest.cs
*/

-- =============================================
-- Sample appsettings.json Configuration
-- =============================================

{
    "Database": {
        "Server": "TCPHUONG\\SQLEXPRESS",
        "DefaultDatabase": "UniManage",
        "LogDatabase": "UniManageLog",
        "UserId": "uni_manager",
        "Password": "uni_manager@2024",  -- Plain text (Development)
        -- OR
        "Password": "ENC:YourEncryptedPasswordHere==",  -- Encrypted (Production)
        "TrustServerCertificate": true,
        "MultipleActiveResultSets": true,
        "ConnectionTimeout": 30
    }
}

-- =============================================
-- Security Best Practices
-- =============================================

-- 1. Development: Plain text OK
-- appsettings.Development.json:
{
    "Database": {
        "Password": "uni_manager@2024"
    }
}

-- 2. Production: MUST encrypt
-- appsettings.json:
{
    "Database": {
        "Password": "ENC:encrypted_value_here"
    }
}

-- 3. Set UNIMANAGE_ENCRYPTION_SEED on production server
-- PowerShell (Windows):
[Environment]::SetEnvironmentVariable("UNIMANAGE_ENCRYPTION_SEED", "YourSecretSeed2024", "Machine")

-- Bash (Linux):
echo 'export UNIMANAGE_ENCRYPTION_SEED="YourSecretSeed2024"' >> ~/.bashrc

-- =============================================
-- Troubleshooting
-- =============================================

-- Error: "Failed to decrypt configuration value"
-- Solution: Check environment variable
echo $env:UNIMANAGE_ENCRYPTION_SEED  -- PowerShell
echo $UNIMANAGE_ENCRYPTION_SEED      -- Bash

-- Re-encrypt with correct seed:
dotnet run -- encrypt "uni_manager@2024"
