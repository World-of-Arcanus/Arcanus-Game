@echo off

REM Linux Build Script

rmdir /s /q build-linux
mkdir build-linux

robocopy /s data build-linux\data /XD localization

REM Server
robocopy /s Arcanus.Server\bin\Release\net6.0\linux-x64 build-linux /XF *.pdb web.config

REM Server Mods
mkdir build-linux\mods
robocopy /s Arcanus.Common\Server\Mods build-linux\mods *.* /XD Unused War

REM Documentation
copy COPYING build-linux\COPYING

echo.
echo Done! The release is located in build-linux\
