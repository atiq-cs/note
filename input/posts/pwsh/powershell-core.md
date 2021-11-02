Title: Powershell Core Useful Commands
Lead: Powershell Core Cheat Sheet and Intro
Published: 03/19/2021
Tags:
  - powershell core
  - pwsh
  - system administration
---

_This article formerly titled as 'Powershell Frequently Used Commands. Even though the article is titled "core" it includes some Powershell contents as well.'_

More cmdlets are on the other article: `powershell-core-cmdlets`

## Initialization of Shell
_This section is for my personalized shell; please feel free to skip it._

Initiate a shell using a specified init script (replaced `Env:WinDir/*` symbolic link),

    pwsh -NoExit D:\pwsh\Init.ps1

For Machine Learning customized shell, I try adding an arg,

    pwsh -NoExit D:\pwsh\Init.ps1 ML

## New to pwsh?
- `Clear-Host` is equivalent to cls
- `Get-Location` is alias to `pwd`

ls, cat, popd, pushd etc are supported through aliases.

Write-Host is equivalent to echo. For example,

    echo 'hello'

`ps` is equivalent to Get-Process or to List processes or doing `tasklist`.
Stop-Proccess instead of taskkill

Example of starting pwsh with an initiazation script or calling a script with an arg,

    Start-Process pwsh -ArgumentList '-NoExit', 'Init-App.ps1 foo' -ErrorAction 'stop'

above, `foo` is argument.

Split string based on delimeter,

    $Env:Path –split ';'


### Cmdlets
## File Management
Filter files containing name pattern,

    gci -filter '*word*'

Create directory,

    New-Item D:\work\git -Type Directory

Create file (alternative of Unix `touch`),

    New-Item C:\scripts\new_file.txt -type file

Show files in order of modified time,

    $ Get-ChildItem -Path C:\windows\CPE\Chef\outputs | Where-Object { -not $_.PsIsContainer } |
        Sort-Object LastWriteTime -Descending | Select-Object -first 10 

## Control Panel Cmdlet
Handy cmds follow, [ref](https://docs.microsoft.com/en-us/powershell/module/microsoft.powershell.management/show-controlpanelitem) to access sys properties,

    Show-ControlPanelItem -Name System

Names supported by `Show-ControlPanelItem`,
- Network and Sharing Center
- Device Manager
- Programs and Features
- Default Programs

For, Sound mouse etc we do,
- Sound
- Mouse

## Network Cmdlets
Ping hosts,

    Test-Connection -Count 64 google.com
    Test-Connection -Count 1024 google.com

host `bing.com` does not reply to ICMP requests anymore, hence it's not worth trying `Test-Connection bing.com`.

if help modules are outdated this updates it,

    Update-Help

More network related cmdlets or commands are at [wifi cmd article](network-wifi-cmd)

## Other Cmdlets
Get list of running processes (unique), show full path,

    Get-Process | Select-Object -Unique Path

When Windows Explorer or taskbar has trouble,

    Stop-Process -Name explorer

Find difference between 2 text files,

    Compare-Object (get-content one.txt) (get-content two.txt)

Scheduled Tasks,

    Get-ScheduledTask -TaskName *chef*
    Get-ScheduledTask -TaskPath \
    Get-ScheduledTask -TaskPath \MSIT\


Rename Machine,

    Rename-Computer -NewName JohnPC -Restart

Event Log,

    Show-EventLog


Show console host info,

    $ Get-Host
    Name             : ConsoleHost
    Version          : 6.2.3
    InstanceId       : cddce532-924c-4a66-a671-bbea53caf430
    UI               : System.Management.Automation.Internal.Host.InternalHostUserInterface
    CurrentCulture   : en-US
    CurrentUICulture : en-US
    PrivateData      : Microsoft.PowerShell.ConsoleHost+ConsoleColorProxy
    DebuggerEnabled  : True
    IsRunspacePushed : False
    Runspace         : System.Management.Automation.Runspaces.LocalRunspace

How to uninstall store application i.e., skype

    Get-AppxPackage Microsoft.SkypeApp | Remove-AppxPackage

Get Software List,

    Get-WmiObject -Class Win32_Product -Filter "Name = 'Java 8 (64 bit)'"

How to uninstall application,

    $app = Get-WmiObject -Class Win32_Product -Filter "Name = 'Java 8 (64-bit)'"
    $app.Uninstall()

### Type Conversion
This part also demonstrates how to use some datatype libraries in commands.

Example 1, how do we find ascii number of a bunch of characters?

    $ [int[]][char[]] 'abcd'
    97
    98
    99
    100

Additionally, to find ascii number of single char,
```
$ [int][char] 'z'
122
```

Moroever we can call `Array.Sort` in following way,

    $a = [int[]] @(9,5)
    [Array]::Sort($a)

Because these literatls i.e., 'xxx' in powershell is considered as string literal like python.

    $ 'z'.gettype()
    IsPublic IsSerial Name                                     BaseType
    -------- -------- ----                                     --------
    True     True     String                                   System.Object


#### Mathematics Library
Example usage of .net math library, using old friend the `power` method,

    [Math]::Pow(2,13)

Or finding a square root,

    [Math]::Sqrt(9)

#### String Helpers
nll or empty related where `$ConfigName` is an example variable,

    [string]::IsNullOrEmpty($ConfigName)
    [string]::IsEmpty($ConfigName)
    [string]::Empty($ConfigName)

substring example,

    if ($loc.EndsWith("\")) {
        return $loc.Substring(0, $loc.Length-1)
    }

which is fine to be replaced with,

    if (($lastindex = [int] $loc.lastindexof('\')) -ne -1) {
        return $loc.Substring(0, $lastindex)
    }

## Show OS Version
Using Net Framework Library,

    $ [Environment]::OSVersion
    Platform ServicePack Version      VersionString
    -------- ----------- -------      -------------
    Win32NT             10.0.18362.0 Microsoft Windows NT 10.0.18362.0

    $ [Environment]::OSVersion.Version
    Major  Minor  Build  Revision
    -----  -----  -----  --------
    10     0      18362  0

Additionally, we can do this inspecting `hal.dll`,

    $ [Version](Get-ItemProperty -Path "$($Env:Windir)\System32\hal.dll" -ErrorAction SilentlyContinue).VersionInfo.FileVersion.Split()[0]
    Major  Minor  Build  Revision
    -----  -----  -----  --------
    10     0      18362  356

## Shell Variables
To delete all contents of USB drive (this is dangerous as it deletea all contents and files/dirs from a drive),

    Remove-Item -force l:\*

On pwsh,

    $ $profile
    DocumentsDir\PowerShell\Microsoft.PowerShell_profile.ps1

On Powershell (Windows),

    $ $profile
    DocumentsDir\\WindowsPowerShell\Microsoft.PowerShell_profile.ps1

Access history file,

    $ (Get-PSReadlineOption).HistorySavePath


## Invoking Legacy Powershell Features from pwsh
Say you have a script named `Bluetooth.ps1` that uses Windows features i.e., COM. We invoke old Powershell to execute those.

    Powershell -NoProfile -File Bluetooth.ps1 On

## Setting permission for running pwsh on a new machine
By default, there are lot of warnings and requests for permission. To make things easier, we relax the permission by setting execution policy.
Set execution policy for current user [ref](https://docs.microsoft.com/en-us/powershell/module/microsoft.powershell.security/set-executionpolicy),

    Set-Executionpolicy Unrestricted -scope CurrentUser

In old days, that used to be enough. If you get following,

    PowerShell 6.2.3
    Copyright (c) Microsoft Corporation. All rights reserved.

    https://aka.ms/pscore6-docs
    Type 'help' to get help.

    Security warning
    Run only scripts that you trust. While scripts from the internet can be useful, this script can potentially harm your computer. If you trust this script, use the Unblock-File cmdlet to allow the script to run without this warning
    message. Do you want to run D:\Doc\PowerShell\Microsoft.PowerShell_profile.ps1?
    [D] Do not run  [R] Run once  [S] Suspend  [?] Help (default is "D"): R
    Loading personal and system profiles took 4862ms.

 Try unblocking,

    Unblock-File D:\Doc\PowerShell\Microsoft.PowerShell_profile.ps1

In worst situation, in corporate environments if that still does not work,

    File D:\Doc\PowerShell\Microsoft.PowerShell_profile.ps1 cannot be loaded. The file D:\Doc\PowerShell\Microsoft.PowerShell_profile.ps1 is not digitally signed. You cannot run this script on the current system. For more information about running scripts and setting execution policy, see about_Execution_Policies at https://go.microsoft.com/fwlink/?LinkID=135170.
    At line:1 char:3
    + . 'D:\Doc\PowerShell\Microsoft.PowerShell_profile.ps1'
    +   ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    + CategoryInfo          : SecurityError: (:) [], PSSecurityException
    + FullyQualifiedErrorId : UnauthorizedAccess

we can apply bypass changing Registry,

    Set-ItemProperty -Path HKLM:\Software\Policies\Microsoft\Windows\PowerShell -Name ExecutionPolicy -Value ByPass

In environments like school computers that are running SINC Site, bypassing in Process scope can be useful,

    Set-ExecutionPolicy Bypass -Scope Process

## Variables pwsh vs Powershell

Following are new pwsh variables,

    EnabledExperimentalFeatures    {}

    IsCoreCLR                      True
    IsLinux                        False
    IsMacOS                        False
    isSinglePS                     True
    IsWindows                      True
    LASTEXITCODE                   0

    psConsoleType

The shell modified following previously known Powershell variables,

    OutputEncoding
    PROFILE
    PSCommandPath
    PSEdition
    PSHOME

## continue
rest of the contents yet to be appended..
