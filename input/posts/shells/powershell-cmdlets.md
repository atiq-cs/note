Title: Powershell Cmdlets
Published: 12/10/2020
Tags:
  - powershell
  - system administration
---
*This article applies to Powershell version 6 or later (formerly also known as Powershell Core).*

### Start-Process
Here are some handy cmds (Application Run Examples),

`Start` is an alias of `Start-Process`

    Start Code --diff, a.md, b.md
    Start Signal

Syntax for AppEx/ MS Store Apps,

    Start Microsoft-Edge:https://google.com
    Start MS-Paint:

For passing arguments to an application we can either add it right after the app name with a seperating space,

    Start Notepad++ file_path

Or, specify it in `ArgumentList`,

    Start Notepad++ -ArgumentList file_path

However, it's tricky if passed argument for example, `file_path` above contains a space character.

To make it work, we need to double quote them [ref, SO - pwsh opening file path with spaces](https://stackoverflow.com/questions/22840882/powershell-opening-file-path-with-spaces),

    Start Notepad++ -ArgumentList "`"D:\Cool Soft\my awesome file.txt`""

### Process Management
A simple run gives a technical summary of the process

    $ Get-Process pwsh

    NPM(K)    PM(M)      WS(M)     CPU(s)      Id  SI ProcessName
    ------    -----      -----     ------      --  -- -----------
        80   117.16     170.47      25.61   21628   1 pwsh

 * annoying what the powershell team chose by default on the implementation

To get number of instances of a process

    function GetProcessInstanceNumber([string] $process) {
        @(Get-Process $process -ErrorAction 0).Count
    }


Get top 30 processes sorted by CPU usage,

    Get-Process | Sort-Object CPU -Descending | Select -First 30 -Property ID,ProcessName,CPU,WorkingSet64 | Format-table -autosize


Same thing with default columns,

    Get-Process | Sort-Object -Property cpu -Descending | Select -First 30

### Service Management
Get list of services currently running,

    Get-Service | Where-Object {$_.Status -eq "Running"}

Start a service,

    Start-Service -Name VPNAgent

Stop one,

    Stop-Service -Name VPNAgent

Useful for bluetooth service,

    Start-Service bthserv
    Stop-Service -Force bthserv


### Network Administration
*To view information on the currently connect WiFi network*

    $ Get-NetConnectionProfile
    Name                     : @Coffeebar Guest WiFi
    InterfaceAlias           : Wi-Fi
    InterfaceIndex           : 16
    NetworkCategory          : Public
    DomainAuthenticationKind : None
    IPv4Connectivity         : Internet
    IPv6Connectivity         : NoTraffic


**Show Network Interfaces Information**
Using powershell cmdlet, we can view information on all network interfaces in the system,

    $ Get-NetAdapter
    Name                      InterfaceDescription                    ifIndex Status       MacAddress             LinkSpeed
    ----                      --------------------                    ------- ------       ----------             ---------
    Ethernet 2                Cisco AnyConnect Secure Mobility Clien…      14 Not Present  00-05-9A-3C-7A-00          0 bps
    Wi-Fi                     Intel(R) Wi-Fi 6 AX200 160MHz                10 Up           04-ED-33-4C-9E-1F       400 Mbps
    vEthernet (Default Switc… Hyper-V Virtual Ethernet Adapter             23 Up           00-15-5D-5F-1A-CB        10 Gbps

To view information on the WiFi network interface,

    $ Get-NetAdapter -Name Wi-Fi
    Name                      InterfaceDescription                    ifIndex Status       MacAddress             LinkSpeed
    ----                      --------------------                    ------- ------       ----------             ---------
    Wi-Fi                     Intel(R) Wi-Fi 6 AX200 160MHz                10 Up           04-ED-33-4C-9E-1F       400 Mbps

Show cmdlets related to net adapter,

    $ gcm -Noun netadapter | select name, modulename

    Name               ModuleName
    ----               ----------
    Disable-NetAdapter NetAdapter
    Enable-NetAdapter  NetAdapter
    Get-NetAdapter     NetAdapter
    Rename-NetAdapter  NetAdapter
    Restart-NetAdapter NetAdapter
    Set-NetAdapter     NetAdapter

Additionally, now, we have cmdlet to show IP Address info without sing `netsh`,

    $ Get-NetIPAddress
    IPAddress         : fe80::f384:25fc:f7ba:30b%7
    InterfaceIndex    : 7
    InterfaceAlias    : Bluetooth Network Connection
    AddressFamily     : IPv6
    Type              : Unicast
    PrefixLength      : 64
    PrefixOrigin      : WellKnown
    SuffixOrigin      : Link
    AddressState      : Deprecated
    ValidLifetime     : Infinite ([TimeSpan]::MaxValue)
    PreferredLifetime : Infinite ([TimeSpan]::MaxValue)
    SkipAsSource      : False
    PolicyStore       : ActiveStore

    IPAddress         : fe80::99c3:e3a9:7b86:99fe%11
    InterfaceIndex    : 11
    InterfaceAlias    : Local Area Connection* 10
    AddressFamily     : IPv6
    Type              : Unicast
    PrefixLength      : 64
    PrefixOrigin      : WellKnown
    SuffixOrigin      : Link
    AddressState      : Deprecated
    ValidLifetime     : Infinite ([TimeSpan]::MaxValue)
    PreferredLifetime : Infinite ([TimeSpan]::MaxValue)
    SkipAsSource      : False
    PolicyStore       : ActiveStore

    IPAddress         : fe80::604:e257:2351:2768%17
    InterfaceIndex    : 17
    InterfaceAlias    : Local Area Connection* 9
    AddressFamily     : IPv6
    Type              : Unicast
    PrefixLength      : 64
    PrefixOrigin      : WellKnown
    SuffixOrigin      : Link
    AddressState      : Deprecated
    ValidLifetime     : Infinite ([TimeSpan]::MaxValue)
    PreferredLifetime : Infinite ([TimeSpan]::MaxValue)
    SkipAsSource      : False
    PolicyStore       : ActiveStore



    ... ...

    ... ...
    IPAddress         : 10.55.200.98
    InterfaceIndex    : 16
    InterfaceAlias    : Wi-Fi
    AddressFamily     : IPv4
    Type              : Unicast
    PrefixLength      : 8
    PrefixOrigin      : Dhcp
    SuffixOrigin      : Dhcp
    AddressState      : Preferred
    ValidLifetime     : 20:23:37
    PreferredLifetime : 20:23:37
    SkipAsSource      : False
    PolicyStore       : ActiveStore

    IPAddress         : 127.0.0.1
    InterfaceIndex    : 1
    InterfaceAlias    : Loopback Pseudo-Interface 1
    AddressFamily     : IPv4
    Type              : Unicast
    PrefixLength      : 8
    PrefixOrigin      : WellKnown
    SuffixOrigin      : WellKnown
    AddressState      : Preferred
    ValidLifetime     : Infinite ([TimeSpan]::MaxValue)
    PreferredLifetime : Infinite ([TimeSpan]::MaxValue)
    SkipAsSource      : False
    PolicyStore       : ActiveStore
