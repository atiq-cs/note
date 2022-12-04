Title: Powershell Useful Commands
Lead: Powershell Cheat Sheet and Useful Commands/Cmdlets
Published: 03/19/2021
Tags:
  - powershell
  - legacy powershell
  - system administration
---

*This article mostly applies to Powershell version 6 or later (formerly known as Powershell Core). Check edition via Powershell variable: `$PSEdition` (should show core for modern powershell)*

More cmdlets are on the other article: `powershell-cmdlets`

### Initialization of Shell
_This section is for my personalized shell; please feel free to skip it._

Initiate a shell using a specified init script,

    pwsh -NoExit D:\pwsh\Init.ps1

For shells with specialized purposes, we have,

    New-Shell.ps1 -Type Python

to support Machine Learning and Data Science stack.

Or, for frontend stack,

    New-Shell.ps1 -Type Node


### New to Powershell?
- `Clear-Host` is equivalent to cls
- `Get-Location` is alias to `pwd`

ls, cat, popd, pushd etc are supported through aliases.

Write-Host is equivalent to echo. For example,

    echo 'hello'

`ps` is equivalent to Get-Process or to List processes or doing `tasklist`.
Stop-Proccess instead of taskkill

Example of starting Powershell with an initiazation script or calling a script with an arg,

    Start-Process pwsh -ArgumentList '-NoExit', 'Init-App.ps1 foo' -ErrorAction 'stop'

above, `foo` is argument.

Split string based on delimeter,

    $Env:Path –Split ';'

Access current user's Path variable on the system,

```powershell
(Get-ItemPropertyValue -Path HKCU:\Environment -Name Path) -Split(';')
```


### File System Management
Filter files containing name pattern,

    Get-ChildItem -Filter '*word*'

Create directory,

    New-Item D:\work\git -Type Directory

Create file (alternative of Unix cmd, `touch`),

    New-Item C:\scripts\new_file.txt -Type File

Show files in order of modified time,

    $ Get-ChildItem -Path C:\windows\CPE\Chef\outputs | Where-Object { -not $_.PsIsContainer } |
        Sort-Object LastWriteTime -Descending | Select-Object -first 10

List files in order of size,


    $ gci 'D:\Movies\pq\movies' | Sort-Object Length -Descending

List items in order of last modified time


    $ gci E:\Media\upload | Sort-Object LastWriteTime -Descending


### Network Connections
Ping hosts,

    Test-Connection -Count 64 google.com
    Test-Connection -Count 1024 google.com

host `bing.com` does not reply to ICMP requests anymore, hence it's not worth trying `Test-Connection bing.com`.

if help modules are outdated this updates it,

    Update-Help

More network related cmdlets or commands are at [wifi cmd article](../network-wifi-cmd)

### Miscellaneous
**Processes**

Get list of running processes (unique), show full path,

    Get-Process | Select-Object -Unique Path

When Windows Explorer or taskbar has trouble,

    Stop-Process -Name Explorer

Find difference between 2 text files,

    Compare-Object (get-content one.txt) (get-content two.txt)

**Scheduled Tasks**


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

`WmiObject` example can be found on post: [powershell-legacy](powershell-legacy)

### Type Conversion
This part also demonstrates how to use some datatype libraries in commands.

Example 1, how do we find ascii number of a bunch of characters?

    $ [int[]][char[]] 'abcd'
    97
    98
    99
    100

Additionally, to find ascii number of single char,
```powershell
$ [int][char] 'z'
122
```

Moroever we can call `Array.Sort` in following way,

```powershell
$a = [int[]] @(9,5)
[Array]::Sort($a)
```

Because these literatls i.e., 'xxx' in powershell is considered as string literal like python.

    $ 'z'.gettype()
    IsPublic IsSerial Name                                     BaseType
    -------- -------- ----                                     --------
    True     True     String                                   System.Object


#### Mathematics Library
Example usage of .net math library, using old friend the `power` method,
```powershell
[Math]::Pow(2,13)
```

Or finding a square root,
```powershell
[Math]::Sqrt(9)
```

#### String Helpers
String Split,

```powershell
$Env:Path –split ';'
```

`Null` or empty string related examples, where `$ConfigName` is an example variable,

```powershell
[string]::IsNullOrEmpty($ConfigName)
[string]::IsEmpty($ConfigName)
[string]::Empty($ConfigName)
```

Substring example,
```powershell
if ($loc.EndsWith("\")) {
    return $loc.Substring(0, $loc.Length-1)
}
```

which is fine to be replaced with,
```powershell
if (($lastindex = [int] $loc.lastindexof('\')) -ne -1) {
    return $loc.Substring(0, $lastindex)
}
```

### Show OS Version
Using Net Framework Library,

    $ [Environment]::OSVersion
    Platform ServicePack Version      VersionString
    -------- ----------- -------      -------------
    Win32NT             10.0.22621.0 Microsoft Windows NT 10.0.22621.0

    $ [Environment]::OSVersion.Version
    Major  Minor  Build  Revision
    -----  -----  -----  --------
    10     0      22621  0

Additionally, we can do this inspecting `hal.dll`,

    $ [Version](Get-ItemProperty -Path "$($Env:Windir)\System32\hal.dll" -ErrorAction SilentlyContinue).`
        VersionInfo.FileVersion.Split()[0]
    Major  Minor  Build  Revision
    -----  -----  -----  --------
    10     0      22621  819

### Shell Variables
To delete all contents of USB drive (this is dangerous as it deletea all contents and files/dirs from a drive),

    Remove-Item -force l:\*

On Powershell,

    $ $Profile
    DocumentsDir\PowerShell\Microsoft.PowerShell_profile.ps1

On Legacy Powershell (the classic one that comes with Windows),

    $ $Profile
    DocumentsDir\\WindowsPowerShell\Microsoft.PowerShell_profile.ps1

Access history file,

    $ (Get-PSReadlineOption).HistorySavePath


### Invoking Legacy Powershell Features from Powershell
Say you have a script named `Bluetooth.ps1` that uses Windows features i.e., COM. We invoke old Powershell to execute those.

    Powershell -NoProfile -File Bluetooth.ps1 On

### Setting permission for running Powershell on a new machine
By default, there are lot of warnings and requests for permission. To make things easier, we relax the permission by setting execution policy.
Set execution policy for current user. [ref, MSFT - Security Set Execution Policy](https://docs.microsoft.com/en-us/powershell/module/microsoft.powershell.security/set-executionpolicy),

    Set-Executionpolicy Unrestricted -scope CurrentUser

In old days, that used to be enough. If you get following,

    PowerShell 7.3.0   https://aka.ms/pscore6-docs

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

### Variables Powershell vs Legacy Powershell

Following are new Powershell variables,

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

*TODO: check pwsh 7*
