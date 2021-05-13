; Script generated by the Inno Setup Script Wizard.
; SEE THE DOCUMENTATION FOR DETAILS ON CREATING INNO SETUP SCRIPT FILES!

[Setup]
; NOTE: The value of AppId uniquely identifies this application. Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
AppId={{D946D6A2-48F7-4B69-A73E-3A2C27718A51}
AppName=GenTicks
AppVersion=1.0
;AppVerName=GenTicks 1.0
AppPublisher=MGUTU, Inc.
DefaultDirName={autopf}\GenTicks
ChangesAssociations=yes
DisableProgramGroupPage=yes
; Uncomment the following line to run in non administrative install mode (install for current user only.)
;PrivilegesRequired=lowest
OutputDir=setup
OutputBaseFilename=setup
Compression=lzma
SolidCompression=yes
WizardStyle=modern

[Dirs]
Name:"{autopf}\GenTicks"; Permissions: users-full
[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"
Name: "russian"; MessagesFile: "compiler:Languages\Russian.isl"

[Types]
Name: "full"; Description:"������ ���������"
Name: "custom"; Description: "���������� ���������"; Flags:iscustom

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked
Name: "CreateOrUpdateDB"; Description: "Create\Update Database | ��������\���������� ��"; Flags: unchecked

[Files]
Source: "bin\Debug\netcoreapp3.1\GenerationTicketsWPF.exe"; DestDir: "{app}"; Flags: ignoreversion; Components: program
Source: "bin\Debug\netcoreapp3.1\*.dll"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs; Components: program
Source: "bin\Debug\netcoreapp3.1\Shablon.docx"; DestDir: "{app}"; Flags: ignoreversion nocompression; Components: program
Source: "bin\Debug\netcoreapp3.1\*.json"; DestDir: "{app}"; Flags: ignoreversion; Components: program
Source: "SQLQuery_CreateDB.sql"; DestDir: "{app}"; Flags: ignoreversion; Components: program
Source: "UpDBscript.bat"; DestDir: "{app}"; Flags: ignoreversion; Components: program
Source: "bin\Debug\netcoreapp3.1\*.pdb"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs; Components: program
; NOTE: Don't use "Flags: ignoreversion" on any shared system files

[Registry]
Root: HKA; Subkey: "Software\Classes\.myp\OpenWithProgids"; ValueType: string; ValueName: "GenTicksFile.myp"; ValueData: ""; Flags: uninsdeletevalue
Root: HKA; Subkey: "Software\Classes\GenTicksFile.myp"; ValueType: string; ValueName: ""; ValueData: "GenTicks File"; Flags: uninsdeletekey
Root: HKA; Subkey: "Software\Classes\GenTicksFile.myp\DefaultIcon"; ValueType: string; ValueName: ""; ValueData: "{app}\GenerationTicketsWPF.exe,0"
Root: HKA; Subkey: "Software\Classes\GenTicksFile.myp\shell\open\command"; ValueType: string; ValueName: ""; ValueData: """{app}\GenerationTicketsWPF.exe"" ""%1"""
Root: HKA; Subkey: "Software\Classes\Applications\GenerationTicketsWPF.exe\SupportedTypes"; ValueType: string; ValueName: ".myp"; ValueData: ""

[Icons]
Name: "{autoprograms}\GenTicks"; Filename: "{app}\GenerationTicketsWPF.exe"
Name: "{autodesktop}\GenTicks"; Filename: "{app}\GenerationTicketsWPF.exe"; Tasks: desktopicon

[Run]
Filename: "{app}\GenerationTicketsWPF.exe"; Flags: nowait postinstall skipifsilent; Description: "{cm:LaunchProgram,GenTicks}"
Filename: "{app}\UpDBscript.bat"; WorkingDir: "{app}"; Flags: waituntilterminated runascurrentuser; Description: "��������\���������� ��"; Tasks: CreateOrUpdateDB

[Components]
Name: "program"; Description: "���������"; Types: full
