#! /bin/bash

# Generate ProtoBuf files
mono CodeGenerator.exe Packet.proto

# Clean cito output directory
rm -r cito/output

# Create output directories
mkdir cito/output
mkdir cito/output/JsTa
mkdir cito/output/JsTa/js

# Compile JavaScript files
mono CitoAssets.exe data Assets.ci.cs
mono CiTo.exe -D CITO -D JS -D JSTA -l js-ta -o cito/output/JsTa/js/Assets.js Assets.ci.cs
mono CiTo.exe -D CITO -D JS -D JSTA -l js-ta -o cito/output/JsTa/js/Arcanus.js \
	$(ls Arcanus.Common/Client/*.ci.cs) \
	$(ls Arcanus.Common/Client/Mods/*.ci.cs) \
	$(ls Arcanus.Common/Client/MainMenu/*.ci.cs) \
	$(ls Arcanus.Common/Client/Misc/*.ci.cs) \
	$(ls Arcanus.Common/Client/SimpleServer/*.ci.cs) \
	$(ls Arcanus.Common/Client/UI/*.ci.cs) \
	$(ls Arcanus.Common/Client/UI/Screens/*.ci.cs) \
	$(ls Arcanus.Common/Client/UI/Widgets/*.ci.cs) \
	$(ls Arcanus.Common/Common/*.ci.cs) \
	Packet.Serializer.ci.cs

# Copy skeleton files
cp -r cito/platform/JsTa/* cito/output/JsTa/
