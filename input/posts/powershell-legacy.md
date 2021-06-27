Title: Legacy Powershell Useful Commands
Published: 07/18/2020
Tags:
  - powershell
  - system administration
---
Please be aware of notation below in command outlines. `$` represents a command; following rest of the lines represent output. Powershell is superset of traditional command prompt. Hence, all usual binaries still run on powershell for example, `takeown`.

**Power Options**
Open Power Options Window,

    Show-ControlPanelItem -Name "Power Options"

## Service Management
Get list of services currently running,

    Get-Service | Where-Object {$_.Status -eq "Running"}

Start a service,

    Start-Service -Name VPNAgent

Stop one,

    Stop-Service -Name VPNAgent

## Scripting using pwsh
These basic stuffs might come handy,

Passing all arguments, untouched to another script,

    $saArgs = $args[0 .. ($args.Count)]
    & "$Env:HOME\ss.ps1" $saArgs


### Changing Window Size
To change Window ize we change buffer size first, because Window Size depends on it.

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

There's oneliner to do it as well,

    (Get-Host).UI.RawUI.BufferSize = New-Object System.Management.Automation.Host.Size -Property @{Width=$width; Height=$history_size}
    (Get-Host).UI.RawUI.WindowSize = New-Object System.Management.Automation.Host.Size -Property @{Width=$width; Height=$height}

To change the title of the console we do,

    $cUI.WindowTitle = $title

[ms docs ref](https://docs.microsoft.com/en-us/dotnet/api/system.management.automation.host.pshostuserinterface.rawui)

## continue
rest of the contents yet to be appended..

