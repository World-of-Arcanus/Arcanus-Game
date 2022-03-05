@echo off

REM Windows Build Script

rmdir /s /q build
mkdir build

robocopy /s data build\data /XD localization

REM Client
robocopy /s Arcanus\bin\Release\netcoreapp3.1 build /XD user moddebug runtimes /XF *.pdb

REM Server
robocopy /s Arcanus.Server\bin\Release\netcoreapp3.1 build /XD user moddebug runtimes /XF *.pdb

REM Monster Editor
REM robocopy Arcanus.MonsterEditor\bin\Release\netcoreapp3.1 build

REM Server Mods
mkdir build\mods
robocopy /s Arcanus.Common\Server\Mods build\mods *.* /XD Unused War

REM Documentation
copy COPYING build\COPYING

echo.
echo Done! The release is located in build\
