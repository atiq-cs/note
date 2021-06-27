Title: Command Line for Utilities and Tools
Published: 11/29/2019
Tags:
  - windows command prompt
  - system administration
---
Please be aware of notation below in command outlines. `$` represents a command and rests of the lines following that line are output. Powershell is superset of traditional command prompt. Hence, all usual binaries still run on powershell for exxample, `takeown`.

## Windows OS Tools
### Balanced Power options
Turn hibernation off (run from elevated PS),

    powercfg.exe /h off

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
An example use of rar (in my system referring to `D:\ProgData\WinRAR\rar.exe`),

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
