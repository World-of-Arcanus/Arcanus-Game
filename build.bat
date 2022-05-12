@echo off

REM Windows Build Script

rmdir /s /q build
mkdir build

robocopy /s data build\data /XD localization

REM Client
robocopy /s Arcanus\bin\Release\netcoreapp3.1 build /XD user moddebug runtimes /XF *.pdb

REM Client Runtimes
mkdir build\runtimes
mkdir build\runtimes\win
mkdir build\runtimes\win-x64
robocopy /s Arcanus\bin\Release\netcoreapp3.1\runtimes\win build\runtimes\win *.*
robocopy /s Arcanus\bin\Release\netcoreapp3.1\runtimes\win-x64 build\runtimes\win-x64 *.*

REM Server
robocopy /s Arcanus.Server\bin\Release\netcoreapp3.1 build /XD user moddebug runtimes linux-x64 /XF *.pdb

REM Monster Editor
REM robocopy Arcanus.MonsterEditor\bin\Release\netcoreapp3.1 build

REM Server Mods
mkdir build\mods
robocopy /s Arcanus.Common\Server\Mods build\mods *.* /XD Unused War

REM Documentation
copy COPYING build\COPYING

echo.
echo Done! The release is located in build\
