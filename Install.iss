; Script generated by the Inno Setup Script Wizard.
; SEE THE DOCUMENTATION FOR DETAILS ON CREATING INNO SETUP SCRIPT FILES!

[Setup]
; NOTE: The value of AppId uniquely identifies this application.
; Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
PrivilegesRequired=admin
AppId={{1D3C7DBD-BBB7-4B4A-9EEC-87F1DB84CD14}
AppName=Drive Backup
AppVerName=Drive Backup 1.0
AppPublisher=Alex White
AppPublisherURL=http://awnet.homedns.org
AppSupportURL=http://awnet.homedns.org
AppUpdatesURL=http://awnet.homedns.org
DefaultDirName={pf}\Drive Backup
DefaultGroupName=Drive Backup
AllowNoIcons=yes
LicenseFile=C:\Users\Alex White\Desktop\Drive Backup\License.txt
InfoAfterFile=C:\Users\Alex White\Desktop\Drive Backup\Readme.rtf
OutputDir=C:\Users\Alex White\Desktop
OutputBaseFilename=setup
SetupIconFile=C:\Users\Alex White\Pictures\611109878.ico
Compression=lzma
SolidCompression=yes

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked
Name: "quicklaunchicon"; Description: "{cm:CreateQuickLaunchIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[Files]
Source: "C:\Users\Alex White\Desktop\Drive Backup\Drive Backup New\Drive Backup\bin\Release\Drive Backup.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Alex White\Desktop\Drive Backup\Drive Backup New\Drive Backup\bin\Release\Drive Backup.exe.config"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Alex White\Desktop\Drive Backup\Drive Backup New\Drive Backup\bin\Release\Drive Backup.pdb"; DestDir: "{app}";
; NOTE: Don't use "Flags: ignoreversion" on any shared system files

[Icons]
Name: "{commonstartup}\Drive Backup"; Filename: "{app}\Drive Backup.exe"
Name: "{group}\Drive Backup"; Filename: "{app}\Drive Backup.exe"
Name: "{group}\{cm:UninstallProgram,Drive Backup}"; Filename: "{uninstallexe}"
Name: "{commondesktop}\Drive Backup"; Filename: "{app}\Drive Backup.exe"; Tasks: desktopicon
Name: "{userappdata}\Microsoft\Internet Explorer\Quick Launch\Drive Backup"; Filename: "{app}\Drive Backup.exe"; Tasks: quicklaunchicon

[Run]
Filename: "{app}\Drive Backup.exe"; Description: "{cm:LaunchProgram,Drive Backup}"; Flags: nowait postinstall skipifsilent

