Title: Legacy Powershell Useful Commands
Published: 07/18/2020
Tags:
  - legacy powershell
  - system administration
---
*This article applies to legacy powershell (powershell 5.0 or earlier) Check edition via Powershell variable: `$PSEdition` (should show "Desktop for legacy/classic powershell), classic powershell is also bundled into the Windows OS.*

*Please be aware of notation below in command outlines. `$` represents a command and rests of the lines following that line are output. The tradition is to utilize the "> " prompt for Windows Command Shell, however, I come from a Unix background and I defy that.*

### Control Panel Cmdlet
Handy cmds follow, [ref](https://docs.microsoft.com/en-us/powershell/module/microsoft.powershell.management/show-controlpanelitem) to access sys properties,

    Show-ControlPanelItem -Name System

For example, open Power Options Window,

    Show-ControlPanelItem -Name 'Power Options'

Following can be invoked using `Show-ControlPanelItem` specifying name argument,
- System
- Programs and Features, old: `appwiz.cpl`
- Network and Sharing Center (previous: `control ncpa.cpl`)
- Device Manager
- Default Programs
- Power Options, old: `powercfg.cpl`
- User Accounts
- Taskbar and Navigation


For, Sound mouse etc we do,
- Sound
- Mouse

To access advanced system properties (old: `sysdm.cpl`), use,

    $ SystemPropertiesAdvanced


**Refs**

- [MSFT TechNet - Accessing the Control Panel via the Commandline](https://social.technet.microsoft.com/wiki/contents/articles/4486.accessing-the-control-panel-via-the-commandline.aspx)
- [MSFT Win32 Shell - Canonical Names of Control Panel Items](https://learn.microsoft.com/en-us/windows/win32/shell/controlpanel-canonical-names)
- [tenforums - Control Panel Item Cmds](https://www.tenforums.com/tutorials/86339-list-commands-open-control-panel-items-windows-10-a.html)

### Service Management
Get list of services currently running,

    Get-Service | Where-Object {$_.Status -eq "Running"}

Start a service,

    Start-Service -Name VPNAgent

Stop one,

    Stop-Service -Name VPNAgent

Get Software List,

    Get-WmiObject -Class Win32_Product -Filter "Name = 'Java 8 (64 bit)'"


### Changing Window Size
To change Window ize we change buffer size first, because Window Size depends on it.
```powershell
$cUI = (Get-Host).UI.RawUI
$b = $cUI.BufferSize
$b.Width = $width
$b.Height = $history_size
$cUI.BufferSize = $b

# change window height and width
$b = $cUI.WindowSize
$b.Width = $width
$b.Height = $height
$cUI.WindowSize = $b
```

There's oneliner to do it as well,

    (Get-Host).UI.RawUI.BufferSize = New-Object System.Management.Automation.Host.Size -Property @{Width=$width; Height=$history_size}
    (Get-Host).UI.RawUI.WindowSize = New-Object System.Management.Automation.Host.Size -Property @{Width=$width; Height=$height}

To change the title of the console we do,

    $cUI.WindowTitle = $title

ref, [MSFT Docs Automation](https://docs.microsoft.com/en-us/dotnet/api/system.management.automation.host.pshostuserinterface.rawui) provides more details on the UI properties for automation on powershell.

#### Programs and Feature Management
Software Management: **Get Software List**


    Get-WmiObject -Class Win32_Product -Filter "Name = 'Java 8 (64 bit)'"

How to uninstall application,

    $app = Get-WmiObject -Class Win32_Product -Filter "Name = 'Java 8 (64-bit)'"
    $app.Uninstall()

### Miscellaneous
**Arguments to the Shell**

These basic stuffs might come handy,

Passing all arguments, untouched to another script,

```powershell
$saArgs = $args[0 .. ($args.Count)]
& "$Env:HOME\ss.ps1" $saArgs
```
