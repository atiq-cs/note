Title: Command Line for Utilities and Tools
Published: 11/29/2019
Tags:
  - windows command shell
  - system administration
---
*Please be aware of notation below in command outlines. `$` represents a command and rests of the lines following that line are output. The tradition is to utilize the "> " prompt for Windows Command Shell, however, I come from a Unix background and I defy that.*

\* Netsh tool examples are bit long, hence it is moved at the bottom.

### Balanced Power options
Turn hibernation off (run from elevated PS),

    powercfg /h off

List Power plans,

    $ powercfg list
    Existing Power Schemes (* Active)
    -----------------------------------
    Power Scheme GUID: 381b4222-f694-41f0-9685-ff5bb260df2e  (Balanced) *
    Power Scheme GUID: 433299bd-efc0-474e-a61e-4a940c85e632  (Timers off (Presentation))
    Power Scheme GUID: 496d75e1-8cba-4d72-b778-535d67c976ea  (Airplane)


How to export Balanced plan (we copy paste id from output of above command),
```
powercfg -Export D:\Efficient_BP.pow 381b4222-f694-41f0-9685-ff5bb260df2e
```

automate importing registry (requires privilege) ref,

    reg import 'D:\Soft\reg files soft settings\app_paths\KeePass.reg'

### WinRar
An example use of rar,

    rar a -m5 -psa1 -ep1 I:\Doc\docs.rar I:\Doc\Office_Docs\

_without `-ep1` it makes a big directory tree_

## gnuwin32 tools
similar to Unix,

    "D:\ProgramFiles_x86\GnuWin32\bin\gzip.exe" -d .\bitarray-ex-0.8.1.tar.gz
    "D:\ProgramFiles_x86\GnuWin32\bin\tar.exe" -xvf .\bitarray-ex-0.8.1.tar
    "D:\ProgramFiles_x86\GnuWin32\bin\tar.exe" --version

## External Applications
Here are some command line examples for External Applications.

using `tftp` to uplod a file into ftp server,

     tftp -i 192.168.1.10 PUT 'E:\LinkSys E2000\images\FW_E2000_1.0.04.007_US_20101201_code.bin' FW_E2000_1.0.04.007_US_20101201_code.bin

I used to use this one for 'ATA Automation'.

Identify image format info using image magick's `identiy` (referring to `C:\Program Files\ImageMagick-6.8.8-Q16\identify.exe` in my system as an example),

    identify "F:\Documents\Images\From Previous Cell-Phone\2011-09\01092011.jpg"
    identify -verbose "F:\Sourcecodes\Web\ASP .Net\Icons\saos_favicon.ico"

Identify image format info using image magick's `convert` tool (referring to `C:\Program Files\ImageMagick-6.8.8-Q16\convert.exe` in my system as an example),

    convert -background transparent

`ps2pdf` tool (location in my system `C:\Program Files\MiKTex\miktex\bin\x64\ps2pdf.exe`) from Miktex from example,

    ps2pdf D:\Source\ml-95-ripper.ps

## Video Applications
**mkvextract**
Extract subtitle using `mkvextract` (referring to `C:\Program Files (x86)\MKVToolNix\mkvextract.exe` in my system),

    mkvextract tracks D:\Movies\Fences.mkv "2:D:\Movies\Fences.srt"

`mkvmerge` (in my system `C:\Program Files (x86)\MKVToolNix\mkvmerge.exe`) show info on mkv file,

    mkvmerge -i D:\Movies\Fences.mkv


### Network Shell (Netsh)
Network shell (netsh) is a command-line utility that allows you to configure and display the status of various network communications server roles and components after they are installed on computers running Windows Server. ref, [MSFT Windows Server - Network Shell](https://learn.microsoft.com/en-us/windows-server/networking/technologies/netsh/netsh)

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
        All User Profile     : PEETS
    .......

This command does not show currently available WiFi Networks. It only shows the networks that are saved in the System because it connected to those in past.

Following shows pretty much the same list probably because most WiFi have clear type Key,

    $ netsh wlan show profiles key=clear

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


**Show Network Interfaces Information**

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
