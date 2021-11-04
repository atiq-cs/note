Title: Powershell Core Cmdlets
Published: 12/10/2020
Tags:
  - powershell core
  - pwsh
  - system administration
---

### Start-Process
Here are some handy cmds (Application Run Examples),

`Start` is an alias of `Start-Process`

    Start DevEnv /Edit, Stream-Converter.ps1
    Start TeamViewer
    Start CVpn-Client

    Start Atom

where I link `CVpn-Client` to `C:\Program Files (x86)\Cisco\Cisco AnyConnect Secure Mobility Client\vpnui.exe`,

and, atom is usually linked to `C:\Users\atiq\AppData\Local\atom\atom.exe`

Syntax for microsoft store apps,

    Start Microsoft-Edge:https://google.com
    Start Skype:

For passing arguments to an application we can either add it right after the app name with a seperating space,

    Start Notepad++ file_path

Or, specify it in `ArgumentList`,

    Start Notepad++ -ArgumentList file_path

However, it's tricky if passed argument for example, `file_path` above contains a space character.

To make it work, we need to double quote them [ref](https://stackoverflow.com/questions/22840882/powershell-opening-file-path-with-spaces),

    Start Notepad++ -ArgumentList "`"D:\Cool Soft\my awesome file.txt`""

### Get-Process
A simple run gives a technical summary of the process

    $ Get-Process pwsh

    NPM(K)    PM(M)      WS(M)     CPU(s)      Id  SI ProcessName
    ------    -----      -----     ------      --  -- -----------
        80   117.16     170.47      25.61   21628   1 pwsh

To get number of instances of a process

    function GetProcessInstanceNumber([string] $process) {
        @(Get-Process $process -ErrorAction 0).Count
    }

## Service Management
Get list of services currently running,

    Get-Service | Where-Object {$_.Status -eq "Running"}

Start a service,

    Start-Service -Name VPNAgent

Stop one,

    Stop-Service -Name VPNAgent

Useful for bluetooth service,

    Start-Service bthserv
    Stop-Service -Force bthserv
