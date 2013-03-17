echo on
set server_name=EngineeringServices
set executable=EAServices.exe
rem set folder_name=ManufactureServices

rem 查看服务是否存在，如果存在就删除
for /f "skip=3 tokens=4" %%i in ('sc query %server_name%') do set "zf=%%i" &goto :next
:next
if /i "%zf%"=="" (echo 该服务已被卸载！
) else (
if /i "%zf%"=="RUNNING" ( 
net stop %server_name%
)
sc delete %server_name%
)

rem cd c:\
rem rmdir /S /Q %folder_name%
rem mkdir %folder_name%
rem xcopy C:\EM_Mode\%folder_name%\*.* C:\%folder_name% /E /R /Y


rem open port
netsh firewall set icmpsetting 8 enable 
netsh firewall set portopening all 9001 enable
netsh firewall set portopening all 9050 enable

rem 直接关闭防火墙
rem netsh firewall set opmode disable
rem 安装启动服务
rem copy EAServices.exe C:\Windows\System32 /Y
SC create %server_name%  binpath=%~dp0%executable% displayname=%server_name% start=auto
Sc start %server_name%

exit
