REM Windows build script

del /s /q output
mkdir output

xcopy /s data output\data\

REM Dll
xcopy /s Arcanus.Common\bin\Release\Arcanus.Common.dll output\

REM Scripting API
xcopy /s Arcanus.ScriptingApi\bin\Release\Arcanus.ScriptingApi.dll output\

REM Game Client
xcopy /s /y Arcanus\bin\Release\*.exe output\

REM Server
xcopy /s /y Arcanus.Server\bin\Release\*.exe output\

REM Monster editor
xcopy /s /y Arcanus.MonsterEditor\bin\Release\*.exe output\

REM Server Mods
mkdir output\Mods
xcopy /s Arcanus.Common\Server\Mods output\Mods\

REM Third-party libraries
xcopy /y /s Lib\*.* output\

del output\*vshost.exe
copy COPYING.md output\credits.txt

REM pause
