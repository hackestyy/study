����������ļ����µ�ĳ�����͵��ļ���ɾ��::@echo off
rem ��������...
for /f "delims=" %%i in ('dir /b /a-d /s "*.swf"') do call someAction
rem �������
pause
����someAction����Ҫִ�еĶ���������������Ǳ�����ǰ�ļ���(�������ļ���)�µ�swf�ļ���
����Щ���뿽�����ı��ĵ��У�����Ϊ.bat�ļ����Ϳ��������ˡ�
������Ҫɾ������ǰ�ļ�(�����ļ���)�������SWF�ļ���������ôд
::@echo off
rem ��������...
rem ɾ���ļ�
for /f "delims=" %%i in ('dir /b /a-d /s "*.swf"') do del %%i
rem ɾ�����
pause