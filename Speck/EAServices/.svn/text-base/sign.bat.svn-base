批处理遍历文件夹下的某种类型的文件并删除::@echo off
rem 正在搜索...
for /f "delims=" %%i in ('dir /b /a-d /s "*.swf"') do call someAction
rem 搜索完毕
pause
其中someAction是你要执行的动作。上面的命令是遍历当前文件夹(包含子文件夹)下的swf文件。
把这些代码拷贝到文本文档中，保存为.bat文件，就可以运行了。
比如我要删除掉当前文件(含子文件夹)里的所有SWF文件，可以这么写
::@echo off
rem 正在搜索...
rem 删除文件
for /f "delims=" %%i in ('dir /b /a-d /s "*.swf"') do del %%i
rem 删除完毕
pause