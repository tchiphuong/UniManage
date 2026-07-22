@echo off
title UniManage - Stop All Services
echo ==========================================
echo Stopping UniManage Services...
echo ==========================================

echo [1/3] Stopping IdentityServer...
taskkill /FI "WINDOWTITLE eq IdentityServer*" /T /F >nul 2>&1

echo [2/3] Stopping WebApi...
taskkill /FI "WINDOWTITLE eq WebApi*" /T /F >nul 2>&1

echo [3/3] Stopping Frontend...
taskkill /FI "WINDOWTITLE eq Frontend*" /T /F >nul 2>&1

echo.
echo All services have been stopped!
pause
