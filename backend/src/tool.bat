@echo off
chcp 65001 >nul
cd /d "%~dp0"
title UniManage Code Generator Tool

echo ========================================================
echo        UNIMANAGE CODE GENERATOR (CRUD + EXCEL)
echo ========================================================
echo.
choice /c YN /m "Update T4 Entities from database before starting?"
if errorlevel 2 goto SkipT4

echo.
echo [1/3] Updating T4 Entities...
call build_all.bat 5
echo.

:SkipT4
cls
echo [2/3] Running Code Generator...
dotnet run --project 02.Tools\UniManage.Tools.CodeGen\UniManage.Tools.CodeGen.csproj

echo.
choice /c YN /m "Update T4 again to apply newly generated Resources?"
if errorlevel 2 goto SkipEndT4
echo [3/3] Updating T4 Entities...
call build_all.bat 5

:SkipEndT4
echo.
pause
