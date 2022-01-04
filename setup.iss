; Script generated by the Inno Setup Script Wizard.
; SEE THE DOCUMENTATION FOR DETAILS ON CREATING INNO SETUP SCRIPT FILES!

[Setup]
; NOTE: The value of AppId uniquely identifies this application.
; Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
AppId={{119E2FCB-5CDD-4C24-BCB2-56A824E2BF0A}
AppName=Arcanus
AppVerName=Arcanus
AppPublisher=Arcanus development team
AppPublisherURL=http://www.worldofarcanus.com/
AppSupportURL=http://www.worldofarcanus.com/
AppUpdatesURL=http://www.worldofarcanus.com/
DefaultDirName={pf}\Arcanus
DefaultGroupName=Arcanus
AllowNoIcons=yes
OutputBaseFilename=ArcanusSetup
Compression=lzma
SolidCompression=yes
OutputDir=output2
WizardImageFile=extra/setup_WizardImage.bmp
WizardSmallImageFile=extra/setup_WizardSmallImage.bmp

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"
Name: "french"; MessagesFile: "compiler:Languages\French.isl"
Name: "german"; MessagesFile: "compiler:Languages\German.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"

[Files]
Source: "output\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs
; NOTE: Don't use "Flags: ignoreversion" on any shared system files

[Icons]
Name: "{group}\{cm:UninstallProgram,Arcanus}"; Filename: "{uninstallexe}"
Name: "{group}\Arcanus"; Filename: "{app}\Arcanus.exe"
Name: "{group}\Configuration"; Filename: "{app}\UserData"
Name: "{commondesktop}\Arcanus"; Filename: "{app}\Arcanus.exe"; IconFilename: "{app}\data\local\md.ico"; Tasks: desktopicon

[Registry]
Root: HKCR; Subkey: ".mdlink"; ValueType: string; ValueName: ""; ValueData: "Arcanus"; Flags: uninsdeletevalue
Root: HKCR; Subkey: "Arcanus"; ValueType: string; ValueName: ""; ValueData: "Arcanus multiplayer link"; Flags: uninsdeletekey
Root: HKCR; Subkey: "Arcanus\DefaultIcon"; ValueType: string; ValueName: ""; ValueData: "{app}\Arcanus.exe,0"
Root: HKCR; Subkey: "Arcanus\shell\open\command"; ValueType: string; ValueName: ""; ValueData: """{app}\Arcanus.exe"" ""%1"""

Root: HKCR; Subkey: "md"; ValueType: string; ValueName: ""; ValueData: "URL:Arcanus"; Flags: uninsdeletekey
Root: HKCR; Subkey: "md"; ValueType: string; ValueName: "URL Protocol"; ValueData: ""; Flags: uninsdeletekey
Root: HKCR; Subkey: "md\DefaultIcon"; ValueType: string; ValueName: ""; ValueData: "{app}\Arcanus.exe,0"
Root: HKCR; Subkey: "md\shell\open\command"; ValueType: string; ValueName: ""; ValueData: """{app}\Arcanus.exe"" ""%1"""
