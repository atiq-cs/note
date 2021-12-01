Title: Fedora Linux Motley
Lead: Fedora Linux Time Machine / Knowledge base
Published: 10/24/2021
Tags:
  - linux
  - fedora
---
_this is still a draft, sections need to be organized, divided into separate posts for certain topics_

First section is for Fedora 25.

New fedora uses in memory swap, swap partitions are no more required.

To find devices,

    lsblk


How to find unallocated disk partition with parted or another tool?

  parted /dev/sdb free

Creating bootable USB fedora

    clean
    conver gpt

Create .ssh dir to connect using ssh key,

    sudo su - atiq
    mkdir .ssh
    chmod 700 .ssh
    touch .ssh/authorized_keys
    chmod 600 .ssh/authorized_keys

Fedora Media Writer,

**refs**
- https://docs.fedoraproject.org/en-US/quick-docs/installing-chromium-or-google-chrome-browsers
- https://note.iqubit.xyz/posts/google-chrome-linux


**How to install manual pages for C/C++**

    dnf install man-pages libstdc++-docs


**Install TP Link Wireless Driver packages in fedora**
(probably from 10 years ago, however, principle still might apply)

Our WIFI adapter model is TL-WN722N
info for linux driver at [ubunut.com](https://help.ubuntu.com/community/HardwareSupportComponentsWirelessNetworkCardsTP-Link#USB)

`lsusb` command says it has ar9271 chip. Fedora supports it by following `ar9271.fw`,

    # rpm -q --whatprovides /usr/lib/firmware/ar9271.fw 
    linux-firmware-20120206-0.3.git06c8f81.fc17.noarch

It has the AR9170 chipset as forum says.

* list loaded modules
 
      less /proc/modules

* see module info

      ath9k_htc
      ath9k_common
      ath9k_hw
      provides
      firmware:       htc_9271.fw
      firmware:       htc_7010.fw

      ath

current fedora driver & firmware: `ar9170-firmware-2009.05.28-4.fc17.noarch`

When a problem occurs update following packages
- wireless-tools
- ar9170-firmware-2009.05.28-4.fc17.noarch (most probably it is working for tp link tl-wn722n)
- ath_info (provides information)

install cmd,

    dnf -y update ar9170-firmware wireless-tools linux-firmware

listing firmware packages,

    # rpm -qa | grep firmware
    ar9170-firmware-2009.05.28-4.fc17.noarch
    iwl6000g2b-firmware-17.168.5.2-2.fc17.noarch
    iwl5000-firmware-8.83.5.1_1-2.fc17.noarch
    atmel-firmware-1.3-9.fc17.noarch
    linux-firmware-20120206-0.3.git06c8f81.fc17.noarch
    alsa-firmware-1.0.25-1.fc17.noarch
    iwl1000-firmware-39.31.5.1-2.fc17.noarch
    iwl100-firmware-39.31.5.1-3.fc17.noarch
    ql2500-firmware-5.06.05-1.fc17.noarch
    iwl4965-firmware-228.61.2.24-4.fc17.noarch
    rt73usb-firmware-1.8-9.fc17.noarch
    netxen-firmware-4.0.534-5.fc17.noarch
    iwl6000-firmware-9.221.4.1-3.fc17.noarch
    iwl6050-firmware-41.28.5.1-4.fc17.noarch
    iwl6000g2a-firmware-17.168.5.3-2.fc17.noarch
    ivtv-firmware-20080701-22.noarch
    rt61pci-firmware-1.2-9.fc17.noarch
    iwl3945-firmware-15.32.2.9-6.fc17.noarch
    libertas-usb8388-firmware-5.110.22.p23-6.fc17.noarch
    aic94xx-firmware-30-3.fc17.noarch
    ipw2100-firmware-1.3-13.fc17.noarch
    ipw2200-firmware-3.1-6.fc17.noarch
    alsa-tools-firmware-1.0.25-2.fc17.x86_64
    iwl5150-firmware-8.24.2.2-3.fc17.noarch
    zd1211-firmware-1.4-6.fc17.noarch

**How to install avro-keyboard in fedora**
(from 2016 or earlier)

Install scim and development files. We need devel files for compiling scim-avro
Run following commands as root
yum -y install scim-devel scim

clone scim-avro latest version.

    git clone https://github.com/mominul/scim-avro

    cd scim-avro/
    chmod a+x configure
    ./configure
    make
    sudo make install

As a regular run following command `im-chooser`

some cmds from online,

    cd 
    chmod a+x configure
    ./configure
    cd po
    msginit --input=scim-avro.pot --output-file=ja.po --locale=ja


In old times, we used to `groupinstall bengali-support` for Bangla lang support. ref, [scim-avro](https://github.com/mominul/scim-avro)


**install video players in fedora core**
adding codecs/ plugins for totemplayer,

     dnf -y install gstreamer-plugins-good gstreamer-plugins-good-extras gstreamer-plugins-ugly