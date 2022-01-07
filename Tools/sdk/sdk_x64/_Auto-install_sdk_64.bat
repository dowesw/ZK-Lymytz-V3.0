cd /d %~dp0
copy .\*.dll %windir%\System32\
%windir%\SysWoW64\regsvr32 zkemkeeper.dll
copy .\*.dll %windir%\SysWoW64\