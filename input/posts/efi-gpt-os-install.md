Title: Operating System Installation on GPT Disks (EFI boot enabled Systems)
Published: 02/04/2013
Tags:
  - efi
  - system boot
  - linux
  - windows
---

This article includes instruction for both Windows and Linux. However, for knowledge, feel free to explore the article.

## Windows Installation with EFI

Legacy boot enabled OSs install bootloader on first 446 bytes of active primary partition in MBR disks (disks with MBR/Legacy Partition Layout). On EFI disks, EFI enabled operating systems put required EFI files for boot into a reserved EFI partition. EFI bootloader applications are EFI files which should properly be put in System's EFI partition.

As for Windows installations Windows automatically detects whether the system is using EFI or legacy and thus, installation proceeds. Usually there is also an option in modern mainboards: BIOS to enable/disable EFI boot feature. When this feature is enabled for GPT disks Windows will install EFI bootloader instead of legacy/mbr bootloader.

When your hard-disk size is 2 TB or more GPT is better. Legacy mbr layout for hard disks are getting out-dated. The future is GPT which has advantage of creating numerous primary partitions and, has built-in recovery features.

## Finding Out What Partition Layout your Hard disk uses

To know what partition layout is used by your hard disk follow procedure below. If you are using Linux skip Windows section

### Procedure to Find Out Partition Layout on Windows

Fire up a command prompt with admin privilege and run command as illustrated below. Commands are given after '>' symbol. A command is follows by single or several lines of output,

    Microsoft Windows [Version 6.3.9600]
    (c) 2013 Microsoft Corporation. All rights reserved.
     
    C:\Windows\system32> diskpart
    Microsoft DiskPart version 6.3.9600
    Copyright (C) 1999-2013 Microsoft Corporation.
    On computer: Qubit_WS
     
    DISKPART> list disk
      Disk ###  Status         Size     Free     Dyn  Gpt
      --------  -------------  -------  -------  ---  ---
    * Disk 0    Online          931 GB      0 B        *

As you can see a * on Gpt column indicates that the disk uses GPT layout (for EFI boot).

### Procedure to Find Out Partition Layout on Linux

First of all you need to login as root in a terminal.

    $ su - root
    Enter password:     
    #

The prompt '#' indicates that you are logged in as root. Now, if you have multiple drives in your system, to find out the drive id apply following command,

    # fdisk -l

It will give you the list of drives with information from which you will be able to recognize your hard-drive that you want to view partition layout. If you have single hard drive its drive id will be sda. If you have multiple drives second one will be sdb, third sdc and so on. For example, you want to view partition layout info for drive sda apply following command,

    # parted /dev/sda p

If your hard drive uses mbr/legacy layout it will be displayed as layout: msdos on the output of the command you just applied.

If you're decided that you shall go with GPT but you have a hard-disk which has legacy/mbr/msdos layout you should follow procedure presented below,

## Converting MBR disk to GPT disk
You can use an existing Windows or use the command prompt of recovery tool of Windows 7/8 Installation DVD to follow the procedure. You have to start command prompt as administrator if you run from existing Windows and then apply 'diskpart' command. If you are running from recovery console you only to run diskpart,

    C:\Windows\system32> diskpart

On `diskpart` prompt list your disks and select the disk that you want to convert to GPT,

    DISKPART> list disk     
      Disk ###  Status         Size     Free     Dyn  Gpt
      --------  -------------  -------  -------  ---  ---
      Disk 0    Online          465 GB  3072 KB
      Disk 1    Online          465 GB    15 MB
     
    DISKPART> select disk 1     
    Disk 1 is now the selected disk.

Run clean command to delete all partitions, Be sure to backup data from the hard-disk before proceeding with this command,

    DISKPART> clean

This should wipe out all partitions.

Now run convert,

    DISKPART> convert gpt

Where `convert gpt` command converts a disk layout to GPT 'convert mbr' converts to mbr. Before proceeding with installation of Windows or other OS we need to create the EFI and MSR partitions. Following commands perform this,

    DISKPART> create partition efi size=112

which creates an EFI partition of 112 MB. To format the partition,

    DISKPART> format fs=fat32 quick

the efi partition is formatted used fat32 filesystem. EFI requires fat or fat32 filesystem. We also need to create a microsoft reserved partition which can be of size 50 MB

    DISKPART> create partition msr size=50

Now let's go ahead and create a primary partition where Windows will be installed.

    DISKPART> create partition primary size=102400

It creates a partition with size 100 GB. Let's mark it as active partition.

    DISKPART> active

The format operation is optional, which can be done with following command,

    DISKPART> format fs=ntfs label="WinOS" quick

Now we can go ahead with Windows installation. During installation of Windows when partition selection screen appears we have to select the partition of 100 GB for installation. Window will automatically put its efi bootloader files inside EFI partition.

## Installation of Fedora 18 on GPT disks with EFI boot enabled systems (dual boot with Windows)

Fedora 18, 17, 16 releases have by default EFI support. I am not sure about the earlier releases to Fedora 16 about EFI support (I haven't tested in them). If you are creating a bootable USB stick using `livecd-tools` be sure to add these options: `--efi --format --msdos --reset-mbr`. Otherwise, EFI support will not be available with installation from bootable usb.

Installation procedure for Fedora 18 with EFI bootable usb is same to the usual installation except the part where we select the boot partition. During partition selection after selecting the root partition we have to change the mount point of EFI partition that we have created earlier with `/boot/efi`

Rest of the installation procedure is as usual. After the installation is complete, Fedora will be listed on EFI boot managers. Usually, Windows is boot by osbootmgr, Fedora is named as Fedora. Only problem is that I have to manually select Fedora each time otherwise Windows gets booted by default. However, with latest EFI firmware it is possible to choose the default OS to boot. As I see OS selection from EFI BIOS is working with recent Dell Lattitude and some other Models I have tested.
