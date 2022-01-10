#! /bin/bash
# Linux build script

rm -R -f output
mkdir output

cp -R data output

# Dll
cp Arcanus.Common/bin/Release/Arcanus.Common.dll output

# Scripting API
cp Arcanus.ScriptingApi/bin/Release/Arcanus.ScriptingApi.dll output

# Game Client
cp Arcanus/bin/Release/*.exe output

# Server
cp Arcanus.Server/bin/Release/*.exe output

# Monster editor
cp Arcanus.MonsterEditor/bin/Release/*.exe output

# Server Mods
cp -R Arcanus.Common/Server/Mods output

# Third-party libraries
cp Lib/* output

rm -f output/*vshost.exe
cp COPYING.md output/credits.txt

# pause
