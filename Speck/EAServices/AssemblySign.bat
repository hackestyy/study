@echo off
REM  **************************************************
REM  *This file is used to compile WP7 Project        *
REM  **************************************************
set ZTE_INPUT_PARAM=%~1
if /i "%ZTE_INPUT_PARAM%" == "/?"  ( 
echo *************************************************************************
echo    Usage:AssemblySign  PrivateCertStore digitalCert
echo.
echo  	PrivateCertStore: digitalCert Category
echo. 
echo  	digitalCert: the certification      
goto end
)
@echo %1
@echo %2
for /f "delims=" %%i in ('dir /b /a-d /s "*.exe"') do signtool sign /s %1 /n %2 %%i
for /f "delims=" %%i in ('dir /b /a-d /s "*.dll"') do signtool sign /s %1 /n %2 %%i
@echo ǩ�����
:end
pause