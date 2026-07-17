@echo off
echo Cleaning empty folders...
for /f "delims=" %%d in ('dir /ad/b/s ^| sort /r') do rd "%%d" 2>nul
echo Done!
pause