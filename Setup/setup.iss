#define AppName    "WeekNumber"
#define AppExe     AppName + ".exe"
#define UpdaterExe AppName + ".Updater.exe"

[Setup]
AppName={#AppName}
AppVersion=1.0
DefaultDirName={autopf}\{#AppName}
DefaultGroupName={#AppName}
OutputBaseFilename={#AppName}_Setup
Compression=lzma
SolidCompression=yes
WizardStyle=modern
; Installer benötigt Admin-Rechte (Program Files)
PrivilegesRequired=admin
CloseApplications=yes
RestartApplications=yes

[Icons]
; Autostart
Name: "{userstartup}\{#AppName}"; Filename: "{app}\{#AppExe}"

[Files]
Source: "publish\app\*";     DestDir: "{app}"; Flags: ignoreversion recursesubdirs
;Source: "publish\updater\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs

[Run]
; wird von [Setup] CloseApplications=yes übernommen
; 1. Laufende Instanz beenden
;Filename: "{app}\{#UpdaterExe}"; \
;  Parameters: "--stop {#AppExe}"; \
;  StatusMsg: "Beende laufende Instanz..."; \
;  Flags: runhidden waituntilterminated

; 2. Autostart + Tray nach Installation
;Filename: "{app}\{#UpdaterExe}"; \
  Parameters: "--post-install ""{app}\{#AppExe}"""; \
  StatusMsg: "Konfiguriere Anwendung..."; \
  Flags: runhidden waituntilterminated
  
; App starten
Filename: "{app}\{#AppExe}"; \
  Description: "{#AppName} starten"; \
  Flags: nowait postinstall skipifsilent

;[UninstallRun]
; Autostart-Eintrag beim Deinstallieren entfernen
;Filename: "{app}\{#UpdaterExe}"; \
  Parameters: "--uninstall {#AppName}"; \
  Flags: runhidden waituntilterminated
