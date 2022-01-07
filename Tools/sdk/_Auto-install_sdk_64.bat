cd /d %~dp0
copy .\*.dll %windir%\SysWoW64\
%windir%\SysWoW64\regsvr32 %windir%\SysWoW64\zkemkeeper.dll