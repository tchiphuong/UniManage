@echo off
title UniManage - Run All Services
echo ==========================================
echo Starting UniManage Services...
echo ==========================================

echo [0/3] Building backend solution to prevent DLL access collision...
dotnet build %~dp0backend\src\UniManage.sln
if %errorlevel% neq 0 (
    echo Build failed! Aborting...
    pause
    exit /b %errorlevel%
)
echo.

echo [1/3] Starting IdentityServer...
start "UniManage.IdentityServer" cmd /k "title IdentityServer && cd /d %~dp0backend\src\03.Apps\UniManage.IdentityServer && dotnet run"

echo [2/3] Starting WebApi...
start "UniManage.WebApi" cmd /k "title WebApi && cd /d %~dp0backend\src\03.Apps\UniManage.WebApi && dotnet run"

echo [3/3] Starting Frontend (Next.js)...
start "UniManage.Frontend" cmd /k "title Frontend && cd /d %~dp0frontend\uni-manage && yarn dev"

echo.
echo All services have been launched in separate windows!
echo - IdentityServer (Auth)
echo - WebApi (Backend)
echo - Frontend (Next.js)
echo.
echo Please check the new terminal windows for logs.
pause
