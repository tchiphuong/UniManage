@echo off
chcp 65001 >nul
setlocal EnableDelayedExpansion

REM =============================
REM Build & Publish Script - UniManage
REM =============================

REM ===== 1. PATH CONFIGURATION (ABSOLUTE) =====
set "ROOT=%~dp0"

echo Checking project paths...

REM Define paths for .csproj files
set "API_PATH=%ROOT%03.Apps\UniManage.WebApi\UniManage.WebApi.csproj"
set "IDS_PATH=%ROOT%03.Apps\UniManage.IdentityServer\UniManage.IdentityServer.csproj"
set "WRK_PATH=%ROOT%03.Apps\UniManage.Worker\UniManage.Worker.csproj"
set "GEN_PATH=%ROOT%04.Tools\UniManage.Tools.CodeGen\UniManage.Tools.CodeGen.csproj"

REM Define Output folders
for /f %%i in ('powershell -command "Get-Date -Format yyyyMMdd_HHmm"') do set "BUILD_TIME=%%i"
set "OUT_ROOT=%ROOT%publish\%BUILD_TIME%"
set "OUT_API=%OUT_ROOT%\api"
set "OUT_IDENTITY=%OUT_ROOT%\identityserver"
set "OUT_WORKER=%OUT_ROOT%\worker"

REM ===== 2. CHECK FILE EXISTENCE =====
set MISSING=0
if not exist "%API_PATH%" ( echo [ERROR] Not found: %API_PATH% & set MISSING=1 )
if not exist "%IDS_PATH%" ( echo [ERROR] Not found: %IDS_PATH% & set MISSING=1 )
if not exist "%WRK_PATH%" ( echo [ERROR] Not found: %WRK_PATH% & set MISSING=1 )

if %MISSING% EQU 1 (
    echo.
    echo [ERROR] Missing project files detected! Please check the folder structure.
    pause
    exit /b
) else (
    echo [OK] All project files found.
)

REM ===== 3. MENU =====
set "IS_AUTO=0"
set "CHOICE=%~1"
if not "%CHOICE%"=="" (
    set "IS_AUTO=1"
    goto SkipMenu
)

:Menu
echo.
echo ===============================================
echo   UNIMANAGE - BUILD TOOL
echo ===============================================
echo   1. Build ALL (API + Identity + Worker)
echo   2. Build API Only
echo   3. Build IdentityServer Only
echo   4. Build Worker Only
echo   5. Transform T4 Only
echo   6. Build Code Generator Tool
echo   ---------------------------------------------
echo   0. Exit
echo ===============================================
set /p CHOICE=Please enter your choice (0-8): 

:SkipMenu
if "%CHOICE%"=="0" goto :EOF
if "%CHOICE%"=="1" goto ProcessAll
if "%CHOICE%"=="2" goto ProcessAPI
if "%CHOICE%"=="3" goto ProcessIdentity
if "%CHOICE%"=="4" goto ProcessWorker
if "%CHOICE%"=="5" goto ProcessT4Only
if "%CHOICE%"=="6" goto ProcessCodeGen

echo Invalid choice!
if "%IS_AUTO%"=="1" exit /b 0
goto Menu

REM =======================================================
REM ===== LOGIC FOR SINGLE OPTIONS
REM =======================================================

:ProcessAll
echo.
echo [FULL] Starting Build ALL...
REM call :TransformT4
echo.
echo [1/3] Building API...
call :BuildAPI
echo.
echo [2/3] Building IdentityServer...
call :BuildIdentity
echo.
echo [3/3] Building Worker...
call :BuildWorker
echo.
echo [DONE] ALL TASKS COMPLETED SUCCESSFULLY!
if "%IS_AUTO%"=="1" exit /b 0
goto Menu

:ProcessAPI
REM call :TransformT4
call :BuildAPI
echo [DONE] API Only.
if "%IS_AUTO%"=="1" exit /b 0
goto Menu

:ProcessIdentity
REM call :TransformT4
call :BuildIdentity
echo [DONE] Identity Only.
if "%IS_AUTO%"=="1" exit /b 0
goto Menu

:ProcessWorker
REM call :TransformT4
call :BuildWorker
echo [DONE] Worker Only.
if "%IS_AUTO%"=="1" exit /b 0
goto Menu

:ProcessT4Only
call :TransformT4
echo [DONE] T4 Transform completed.
if "%IS_AUTO%"=="1" exit /b 0
goto Menu

:ProcessCodeGen
call :BuildCodeGen
echo [DONE] Code Generator Tool built.
if "%IS_AUTO%"=="1" exit /b 0
goto Menu

REM =======================================================
REM ===== WORKER FUNCTIONS (CORE)
REM =======================================================

:TransformT4
set T4_TOOL=dotnet-t4
dotnet tool list -g | findstr "dotnet-t4" >nul
if %ERRORLEVEL% NEQ 0 set T4_TOOL=TextTransform

echo Processing T4 Templates (%T4_TOOL%)...
for /r "%ROOT%" %%F in (*.tt) do (
    if exist "%%F" (
        set PROCESSED=0
        if "!T4_TOOL!"=="dotnet-t4" (
            dotnet t4 "%%F" -o "%%~dpnF.cs" >nul 2>&1
            if !ERRORLEVEL! EQU 0 set PROCESSED=1
        )
        if !PROCESSED! EQU 0 (
            call :FindAndRunTextTransform "%%F"
        )
    )
)
exit /b 0

:FindAndRunTextTransform
set FILE_TT=%~1
for %%V in (2022 2019) do (
    for %%E in (Enterprise Professional Community) do (
        set "PATH_TT=%ProgramFiles%\Microsoft Visual Studio\%%V\%%E\Common7\IDE\TextTransform.exe"
        if exist "!PATH_TT!" (
            "!PATH_TT!" "%FILE_TT%" >nul 2>&1
            exit /b 0
        )
        set "PATH_TT_x86=%ProgramFiles(x86)%\Microsoft Visual Studio\%%V\%%E\Common7\IDE\TextTransform.exe"
        if exist "!PATH_TT_x86!" (
            "!PATH_TT_x86!" "%FILE_TT%" >nul 2>&1
            exit /b 0
        )
    )
)
echo [WARNING] Transform tool not found for: %~nx1
exit /b 1

:BuildAPI
REM /clp:ErrorsOnly to hide warnings, /p:UseSharedCompilation=false to avoid file lock issues
dotnet publish "%API_PATH%" -c Release -o "%OUT_API%" /clp:ErrorsOnly /p:UseSharedCompilation=false
if %ERRORLEVEL% NEQ 0 ( 
    echo [ERROR] Build API Failed!
    pause
    exit /b 1 
)
echo   + [OK] API Published.
exit /b 0

:BuildIdentity
dotnet publish "%IDS_PATH%" -c Release -o "%OUT_IDENTITY%" /clp:ErrorsOnly /p:UseSharedCompilation=false
if %ERRORLEVEL% NEQ 0 ( 
    echo [ERROR] Build Identity Failed!
    pause
    exit /b 1 
)
echo   + [OK] Identity Published.
exit /b 0

:BuildWorker
dotnet publish "%WRK_PATH%" -c Release -o "%OUT_WORKER%" /clp:ErrorsOnly /p:UseSharedCompilation=false
if %ERRORLEVEL% NEQ 0 ( 
    echo [ERROR] Build Worker Failed!
    pause
    exit /b 1 
)
echo   + [OK] Worker Published.
exit /b 0

:BuildCodeGen
set "OUT_GEN=%OUT_ROOT%\codegen"
dotnet publish "%GEN_PATH%" -c Release -o "!OUT_GEN!" /clp:ErrorsOnly /p:UseSharedCompilation=false
if %ERRORLEVEL% NEQ 0 ( 
    echo [ERROR] Build Code Generator Failed!
    pause
    exit /b 1 
)
echo   + [OK] Code Generator Published.
exit /b 0