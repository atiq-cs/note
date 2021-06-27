Title: WiFi Networking with Powershell and netsh
Published: 11/22/2019
Tags:
  - powershell core
  - network
  - system administration
---
Please be aware of notation below in command outlines. `$` represents a command and rests of the lines following that line are output. Powershell is superset of traditional command prompt. Hence, all usual binaries still run on powershell for example, `takeown`.

# Introduction
We still need to use `netsh` till we have cmdlets support
*To connect to a specific network or SSID*

    netsh wlan connect name='Starbucks WiFi'

*To disconnect from WiFi*

    $ netsh wlan disconnect interface=Wi-Fi
    Disconnection request was completed successfully for interface "Wi-Fi".

*To disable Net WiFi Adapter (requires admin privilege),*

    Disable-NetAdapter -Name Wi-Fi -Confirm:$false

*To disable Net WiFi Adapter (requires admin privilege),*

    Enable-NetAdapter -Name Wi-Fi


To provide more context, new cmdlets and netsh features enable us to control network interfaces of the PC. Here's a netsh example to list WiFi SSIDs,

    $ netsh wlan show profiles

    Profiles on interface Wi-Fi:

    Group policy profiles (read only)
    ---------------------------------

    User profiles
    -------------
        All User Profile     : CSC-Public
        All User Profile     : Starbucks WiFi
        All User Profile     : VTA Free WiFi
        All User Profile     : Philz Tesora
        All User Profile     : PEETS
    .......

This command does not show currently available WiFi Networks. It only shows the networks that are saved in the System because it connected to those in past.

Following shows pretty much the same list probably because most WiFi have clear type Key,

    $ netsh wlan show profiles key=clear

*To view information on the currently connect WiFi network*

    $ Get-NetConnectionProfile
    Name             : Starbucks WiFi  12
    InterfaceAlias   : Wi-Fi
    InterfaceIndex   : 16
    NetworkCategory  : Public
    IPv4Connectivity : Internet
    IPv6Connectivity : NoTraffic

Applying above command on a specific SSID provides us more info,

    $ netsh wlan show profiles name='Starbucks WiFi'

    Profile Starbucks WiFi on interface Wi-Fi:
    =======================================================================

    Applied: All User Profile

    Profile information
    -------------------
        Version                : 1
        Type                   : Wireless LAN
        Name                   : Starbucks WiFi
        Control options        :
            Connection mode    : Connect automatically
            Network broadcast  : Connect only if this network is broadcasting
            AutoSwitch         : Do not switch to other networks
            MAC Randomization  : Disabled

    Connectivity settings
    ---------------------
        Number of SSIDs        : 1
        SSID name              : "Starbucks WiFi"
        Network type           : Infrastructure
        Radio type             : [ Any Radio Type ]
        Vendor extension          : Not present

    Security settings
    -----------------
        Authentication         : Open
        Cipher                 : None
        Security key           : Absent
        Key Index              : 1

    Cost settings
    -------------
        Cost                   : Unrestricted
        Congested              : No
        Approaching Data Limit : No
        Over Data Limit        : No
        Roaming                : No
        Cost Source            : Default

Following is equivalent,

    $ netsh wlan show profiles name='Starbucks WiFi' key=clear

#### Show Interfaces Info
To view currently connected SSID etc,

    $ netsh wlan show interfaces
    There is 1 interface on the system:

    Name                   : Wi-Fi
    Description            : Intel(R) Wi-Fi 6 AX200 160MHz
    GUID                   : 605a0ad1-b96b-4f4e-b5a9-782623d6d797
    Physical address       : 04:ed:33:4c:9e:1f
    State                  : connected
    SSID                   : Nimbus3000
    BSSID                  : 84:bb:69:fa:93:10
    Network type           : Infrastructure
    Radio type             : 802.11ac
    Authentication         : WPA2-Personal
    Cipher                 : CCMP
    Connection mode        : Auto Connect
    Channel                : 40
    Receive rate (Mbps)    : 585
    Transmit rate (Mbps)   : 866.7
    Signal                 : 94%
    Profile                : Qubit

    Hosted network status  : Not available

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

    IPAddress         : fe80::1d40:6866:1418:1efe%22
    InterfaceIndex    : 22
    InterfaceAlias    : vEthernet (Default Switch)
    AddressFamily     : IPv6
    Type              : Unicast
    PrefixLength      : 64
    PrefixOrigin      : WellKnown
    SuffixOrigin      : Link
    AddressState      : Preferred
    ValidLifetime     : Infinite ([TimeSpan]::MaxValue)
    PreferredLifetime : Infinite ([TimeSpan]::MaxValue)
    SkipAsSource      : False
    PolicyStore       : ActiveStore

    IPAddress         : fe80::61e3:ae79:980e:1c5d%8
    InterfaceIndex    : 8
    InterfaceAlias    : Wi-Fi
    AddressFamily     : IPv6
    Type              : Unicast
    PrefixLength      : 64
    ... ...

    ... ...
    IPAddress         : 172.31.98.36
    InterfaceIndex    : 8
    InterfaceAlias    : Wi-Fi
    AddressFamily     : IPv4
    Type              : Unicast
    PrefixLength      : 23
    PrefixOrigin      : Dhcp
    SuffixOrigin      : Dhcp
    AddressState      : Preferred
    ValidLifetime     : 00:38:38
    PreferredLifetime : 00:38:38
    SkipAsSource      : False
    PolicyStore       : ActiveStore

    IPAddress         : 127.0.0.1
    InterfaceIndex    : 1
    InterfaceAlias    : Loopback Pseudo-Interface 1
    AddressFamily     : IPv4
    ... ...


Show available WiFi Networks using `netsh`,

    $ netsh wlan show networks mode=bssid

    Interface name : Wi-Fi
    There are 3 networks currently visible.

    SSID 1 : Qubit_Guest
        Network type            : Infrastructure
        Authentication          : WPA2-Personal
        Encryption              : CCMP
        BSSID 1                 : 20:a6:cd:32:fe:e1
            Signal             : 50%
            Radio type         : 802.11n
            Channel            : 6
            Basic rates (Mbps) : 24
            Other rates (Mbps) : 36 48 54
        BSSID 2                 : 44:48:c1:a5:08:81
            Signal             : 81%
            Radio type         : 802.11n
            Channel            : 6
            Basic rates (Mbps) : 24
            Other rates (Mbps) : 36 48 54
        BSSID 3                ... ...

    SSID 2 : Qubit
        Network type            : Infrastructure
        Authentication          : WPA2-Enterprise
        Encryption              : CCMP
        BSSID 1                 : 44:48:c1:a4:f0:b1
            Signal             : 62%
            Radio type         : 802.11ac
            Channel            : 157
            Basic rates (Mbps) : 24
            Other rates (Mbps) : 36 48 54
        BSSID 2                ... ...

    SSID 3 : hello_kitty
        Network type            : Infrastructure
        Authentication          : WPA2-Personal
        Encryption              : CCMP
        BSSID 1                ... ...

## WiFI Modem Best Practices
Few points,
1. Reboot modem every few months
2. Hopefully carrier provides firmware udpates if not update every few months
3. Keep in touch with carrier's customer service in case of slow speed, give good complaints coz they owe you the speed you deserve.

## References
 1. [ms technet - Get Wireless Network SSID and Password with PowerShell](https://blogs.technet.microsoft.com/heyscriptingguy/2015/11/23/get-wireless-network-ssid-and-password-with-powershell)
 2. [netsh wlan commands](https://marckean.com/2017/03/16/auto-connect-to-wifi-access-points)
 3. [changing autoconnect properties for some networks](https://blogs.technet.microsoft.com/heyscriptingguy/2013/06/15/weekend-scripter-use-powershell-to-find-auto-connect-wireless-networks)
 4. [disabling unnecessary network adapters](https://blogs.technet.microsoft.com/heyscriptingguy/2014/01/13/enabling-and-disabling-network-adapters-with-powershell)
 5. [Some possible default passwords for networks / routers / gateways](https://www.xfinity.com/support/internet/comcast-supported-routers-gateways-adapters)
6. [MS devblogs - Get Wireless Network SSID and Password with PowerShell](https://devblogs.microsoft.com/scripting/get-wireless-network-ssid-and-password-with-powershell)
