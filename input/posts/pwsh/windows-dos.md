Title: Useful Windows Commands
Published: 11/19/2019
Tags:
  - windows command prompt
  - system administration
---
Please be aware of notation below in command outlines. `$` represents a command and rests of the lines following that line are output. Powershell is superset of traditional command prompt. Hence, all usual binaries still run on powershell for exxample, `takeown`.

**Win Supported Run Cmds**

[ToDo: move this to a separate one.]

    desk.cpl
    services.msc
    sysdm.cpl


## Misc
Customized ping command, waits 20 seconds for reply and keep pinging non-stop [ping ref](https://docs.microsoft.com/en-us/windows-server/administration/windows-commands/ping),

    ping -w 20000 -t myserver.company.com

Enable or disable Linux Subsystem feature,

    Enable-WindowsOptionalFeature -Online -FeatureName Microsoft-Windows-Subsystem-Linux
    Disable-WindowsOptionalFeature -Online -FeatureName Microsoft-Windows-Subsystem-Linux

Get host name,

    HOSTNAME

associating file type for extension `.fob`,

    cmd /c assoc .fob=foobarfile
    cmd /c ftype foobarfile=powershell.exe -File `"C:\path\to\your.ps1`" `"%1`"

Find files with hidden attributes,

    dir /A:H general-solving\lintcode

Modify files to remove hidden attributes using attrib

    attrib -h my-hidden-file.txt

**Robust copy**
`robocopy` is good for copying large numbeer of files (to handle so many different cases: large files, network interruptions, file names). An example cmd,

    robocopy /s /z /v /fp /eta 'D:\Movies' 'H:\' Office.*
    robocopy /s /z /v /fp /eta 'D:\TV Series' 'H:\' Fringe*

Above cmds copy all files from first one (source dir) matching the pattern.

Exmple of copy over network,

    robocopy /s /z /v /fp /eta '\\ENLIGHTENSOUL\Bangla\3RD Person Singular Number' 'i:\'

**xcopy examples**
(prefer `robocopy` over this one), some variations below,

    xcopy /C /F /H e:\c O:
    xcopy /s /C /F /H e:\C++ O:\C++
    xcopy /s /C /F /H "e:\FC back up" "o:\FC back up"
    xcopy /s /C /F /H "e:\Net Workspace" "o:\Net Workspace"
    xcopy /s /C /F /H f:\* Q:\*

Retrieving all cmd history,

    doskey /history > copy_command.txt

delete everything inside a directory,

    rd /s /q JobDetails_data

**mklink**
To create a symbolic link named MyDocs from the root directory to the \Users\User1\Documents directory,

    mklink /d \MyDocs \Users\User1\Documents

Creates a Directory Junction instead using mklink,

    $ mklink /j C:\Users\Atique\AppData\Local\Microsoft\Office\15.0\OfficeFileCache H:\OfficeFileCache
    Junction created for C:\Users\Atique\AppData\Local\Microsoft\Office\15.0\OfficeFileCache <<===>> H:\OfficeFileCache

**Network share commands**
view curently shared directories,

    net share

to delete share (requires admin privilege),

    $ net share NewDrama /delete
    Movies      D:\Cool Movies
    Softs       D:\Sourcecodes\Web\Softs

To share a directory,

	net share listshare="D:\Docs"

**registry**
For example usage `regsvr32` of check [codeproject - Windows Media Player Native Subtitle Plugin](https://www.codeproject.com/Articles/766246/Windows-Media-Player-Native-Subtitle-Plugin-Cplusp)

## References
- [MS Docs - Windows Commands](https://docs.microsoft.com/en-us/windows-server/administration/windows-commands/commands-by-server-role)
