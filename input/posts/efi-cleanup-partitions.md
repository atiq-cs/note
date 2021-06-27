Title: Cleaning up or resetting a partition table
Lead: Master playbook on partition table troubles on a USB Key / Flash Drive / Hard Drive
Published: 03/18/2013
Tags:
  - efi
  - system boot
  - linux
  - windows
  - partitioning
---
## Recognizing a damaged partition table
There are several reasons why a partition table is damaged. One reason can be experimenting with disks, for example, applying a slightly modified (may be mistakenly) command to make a USB flash drive bootable (for a live OS). It can also be possible by some faulty usb creator software. Additionally, directly writing an ISO using 'dd' command can result a corrupted partition table. The partition can also be damaged or engineered by a virus to meet certain goals. As these operations are mostly run on USB drives or USB keys this post provides instructions to fix such partition tables. Generally these commands are applicable to any kind of hard-disk/usb drive.

![enter image description here][1]

[image acknowledgement][2]

You can have a look at the partition table using `parted` command or `fdisk` command on Linux. On Windows, you can view your partition table using 'Disk Management Console' or using diskpart command (requires admin privilege). For a damaged partition table, if you look at the partition table you will see unexpected partition entries, garbage characters and symbols or combination of both of them. You might even see partition entries that you didn't know existed before in your device. To view list of partitions you can run fdisk command in Linux. An example to use this command on drive sdX is below. You should replace sdX by appropriate drive id i.e., sdc, sdb etc.

    # fdisk -l /dev/sdX

Before proceeding with following procedure be sure to backup data as all data with partitions will be lost after resetting partition table.

To view partition table for drive sdX (replace with appropriate drive id) using parted tool command will be,

    # parted /dev/sdX p

If your partition table is EFI you must use parted tool as fdisk does not yet support EFI partition table yet.

## Cleaning up or resetting the partition table

Before proceeding further, be sure to backup data. Partitions will be lost after resetting partition table. Procedures for both Windows and Linux platform are presented below. If you are using Linux please skip to next Linux section.

### Windows Way

Run command prompt as administrator then run diskpart,

    C:\Windows\system32> diskpart
    Microsoft DiskPart version 10.0.10586
    Copyright (C) 1999-2013 Microsoft Corporation.
    On computer: Qubit-PC

On disk-part prompt, list your disks and select the appropriate one to run clean up operation:

    DISKPART> list disk
      Disk ###  Status         Size     Free     Dyn  Gpt
      --------  -------------  -------  -------  ---  ---
      Disk 0    Online          465 GB  3072 KB
      Disk 1    Online          465 GB    15 MB
      Disk 2    Online          8 GB        25 MB
     
    DISKPART> select disk 2
    Disk 2 is now the selected disk.

For a disk wit GPT partition table you will see a '*' symbol in the output row of the disk. Now please apply `clean` command which will delete all partitions,
This should wipe out all partitions. If this does not work enough you can go a step further. To do that after `clean` command apply following commands,

    DISKPART> convert gpt
    DISKPART> create partition primary
    DISKPART> clean
    DISKPART> convert mbr

Now you can create a new partition and start working with your USB Key

    DISKPART> create partition primary
    DISKPART> format fs=ntfs label="your label" quick
    DISKPART> clean

### Linux Way
Please open a terminal and login as root,

    $ su - root
    Enter root password:

### Using dd to reset partition table on a disk legacy partition table
The dd command is applicable only if the disk contains BIOS(legacy) partition table instead of GPT. Be aware most new systems are rolling out with GPT partition table and EFI firmware. If your system contains a GPT partition table please skip to the next section. Please apply following (caution: data will be lost!) command. Replace sdX with appropriate device id. You can list all devices by running `fdisk -l`. For example, if your flash drive is sdc you have to replace sdX with sdc,

    # dd if=/dev/zero of=/dev/sdX bs=1 seek=446 count=64

This command wipes out the partition table but preserves the MBR. If you want to wipe out your MBR as well you would like to apply following command (first line is the command ran on with root access),

    # dd if=/dev/zero of=/dev/sdX bs=512 count=1
    512+0 records in
    512+0 records out
    512 bytes (512 B) copied, 1.25339 s, 0.4 kB/s

At this point, the device does not have a partition table. So, we create one. Please keep in mind that our next commands require root access as well. As we are using sdc as our example device id of the USB drive it is the third storage device connected to our system. First two hard disks connected to the system are sda and sdb. You should use proper device id in the command according to your device id, 

#### For all partition tables (including GPT)
If your disk contains GPT partition table please use parted rm commands to delete the partitions one by one. Afterwards, proceed with next steps mentioned below.

Let's see information on current partition table,

    # parted /dev/sdc print
    Error: /dev/sdc: unrecognised disk label
    Model:  USB Disk (scsi)
    Disk /dev/sdc: 8054MB
    Sector size (logical/physical): 512B/512B
    Partition Table: unknown

Following commands will create an msdos partition table. Afterwards, we create an ext4 partition entry inside the partition table. The ext4 partition is using the entire space available on the drive (usually desirable for USB flash drive of small size). Please note that, first command is run on linux shell. Rest of the commands are on parted prompt(string after '(parted)') as you can see below,

    # parted /dev/sdc
    GNU Parted 3.1
    Using /dev/sdc
    Welcome to GNU Parted! Type 'help' to view a list of commands.
     
    (parted) mklabel msdos
    (parted) mkpart primary ext4 1 -1
    (parted) toggle 1 boot
    (parted) print                                                           
    Model:  USB Disk (scsi)
    Disk /dev/sdc: 8054MB
    Sector size (logical/physical): 512B/512B
    Partition Table: msdos
    Disk Flags:
     
    Number  Start   End     Size    Type     File system  Flags
     1      1049kB  8053MB  8052MB  primary               boot
     
    (parted) quit                                                            
     
    Information: You may need to update /etc/fstab.

More explanation is here: second command provided to GNU Parted creates an ext4 primary partition starting from sector after mbr to the end. There is a way to pass commands with parted tool. For example, `parted /dev/sdc print` will print device information. You can use this instead of typing `parted` first then typing `print`. These are like shortcut commands. However, we have a note regarding shortcut command. If you are trying to use something like `parted /dev/sdc mkpart primary ext4 1 -1` you know to know some information. If you use the command `parted /dev/sdc mkpart primary ext4 1 -1` it won't work properly. Because while passing commands with parted "-1" means a different thing than the ending sector. If you pass `mkpart` with parted you must provide correct size in MegaBytes instead of "-1" which will work fine.

Our third command passed to gparted sets bootable (active) flag to created partition so that it can be used to boot a OS. Now, let's format the partition now to create ext4 filesystem in it.

    # mkfs -T ext4 /dev/sdX1

where sdX1 should be replaced by sdc1 (which means the first partition on drive sdc) if your flash drive is sdc

Optionally, you can apply a label to the filesystem, it is used by the OS during automount target

    # e2label /dev/sdX1 My_Linux_USB_Drive
    Warning: label too long, truncating.
    When label is too long it truncates.

Please provide your feedback regarding the article. Previous comments are removed which were mostly appreciating remarks (*Thanks to Bill Biffle, Robert Liimatainen, bemma, gusbus, roj, dvt, ponmudi, Leon, cory pollard, RezaKnot, Steve, vtwire and many people around the world*). We are more interested in critical cases where instructions do not work as said.

### FAQ
#### Q1: Roj: my fdisk output is saying my disk is gpt. Why is my dd command failing?
@roj, you are applying dd command on a disk with GPT partition. That is wrong. Follow other ways mentioned in the article for example using `diskpart` or `parted` to remove partitions. `fdisk` is being deprecated and is not well supported on GPT disk.


  [1]: http://3.bp.blogspot.com/-CeoFlB0Q0zU/UacmGoF1YeI/AAAAAAAAAeA/CIXxvWWxGzw/s320/5707_Partitions.gif
  [2]: http://averma82.blogspot.com/2013/05/types-of-hard-drive-partitions-and.html
