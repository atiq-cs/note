Title: Google Chrome on Linux
Lead: How to Install Google Chrome on Different Linux Distributions
Published: 10/24/2021
Tags:
  - linux
---
In new system, i.e, fedora 34/35 etc this is the way,

    sudo dnf config-manager --set-enabled google-chrome
    sudo dnf install google-chrome-stable

If you want to install a beta release of google-chrome apply this command,

    $ sudo dnf install google-chrome-beta

To install the latest development release apply following,

    $ sudo dnf install google-chrome

If you are using other operating systems continue reading.

Generally, there are 3 ways of installing Google Chrome:

*   Using dnf (`apt-get` on Ubuntu), setting up with google's repository in dnf configuration file or ubuntu's aptitude source.lst
*   Locally download chrome rpm or debian package and apply dnf localinstall command
*   last option: Download all dependencies manually and install them one by one, then install google-chrome package.

### Earlier Systems (Fedora Core, RHEL, Cent-OS etc)
To enable google's dnf repository add a repo file inside directory `/etc/dnf.repos.d/` named google-chrome.repo For 64 bit systems contents should look like this,

    [google-chrome]
    name=google-chrome - \$basearch
    baseurl=http://dl.google.com/linux/chrome/rpm/stable/\$basearch
    enabled=1
    gpgcheck=1
    gpgkey=https://dl-ssl.google.com/linux/linux_signing_key.pub
    

Afterwards, if you want to install a stable release of google-chrome apply this command,

    $ sudo dnf install google-chrome-stable

If you have trouble accessing https URLs substitute https using http on the gpgkey field. This is useful when you hit errors like this,

    Error: failure: No more mirrors to try.
    
Or error such as this,

    GPG key retrieval failed: [Errno 14] problem making ssl connection
    

Previously, we used to manually substitute the base url like this (not required anymore),

    [google-chrome]
    name=google-chrome - 64-bit
    baseurl=http://dl.google.com/linux/chrome/rpm/stable/x86_64
    enabled=1
    gpgcheck=1
    gpgkey=http://dl-ssl.google.com/linux/linux_signing_key.pub
    

For 32 bit systems contents should be like this,

    [google-chrome]
    name=google-chrome - 32-bit
    baseurl=http://dl.google.com/linux/chrome/rpm/stable/i386
    enabled=1
    gpgcheck=1
    gpgkey=http://dl-ssl.google.com/linux/linux_signing_key.pub
    

Beaware that, google stopped updating 32 bit builds for Linux. If you install those versions you won't receive updates.

### RHEL/Cent-OS 7
RHEL/CentOS 7 have all required packages available for Google Chrome. Hence, a simple,

    dnf install google-chrome-stable   

command dies the job. However, if you don't have redhat subscription you might get following error,

    libxss.so.1()(64bit) is needed by google-chrome-stable
    

If you enable local repository following instruction at [fftsys - Setup-RHEL-DVD-ISO-as-local-repository-to-install-packages](https://fftsys.azurewebsites.net/tech/how-to-setup-rhel-fedora-cent-os-dvd-or-iso-as-local-repository-to-install-packages) the same previous dnf command completes without any error.

### Using apt-get on debian system (ubuntu etc)
Add signing key

    wget -q -O - https://dl-ssl.google.com/linux/linux_signing_key.pub | sudo apt-key add -
    

Add google repository to the sources list,

    sudo sh -c 'echo "deb http://dl.google.com/linux/chrome/deb/ stable main" >> /etc/apt/sources.list.d/google.list'

Update and install chrome,

    sudo apt-get update
    sudo apt-get install google-chrome-stable
    

For Ubuntu's new release 13.04 if you face following error "Error: Dependency is not satisfiable: libudev0(>=147)" use the beta version of chrome which has already resolved it.

    sudo apt-get install google-chrome-beta
    

Chromium project has details on the issue [here](https://code.google.com/p/chromium/issues/detail?id=226002).

### Chrome on RHEL 6 or Earlier
With RHEL 6 you will run into issues while installing google-chrome. Officially, Google Chrome is not supported on RHEL 6 or earlier since Google Chrome 28. If you are okay with experimental third party script to install google chrome you can follow [tecadmin's article](http://tecadmin.net/install-google-chrome-in-centos-rhel-and-fedora/#). You can install chromium as an alternative. [If Not True Then False](http://www.if-not-true-then-false.com/) provides instructions [if-not-true-then-false's blog - how to install chromium on rhel](http://www.if-not-true-then-false.com/2013/install-chromium-on-centos-red-hat-rhel/). If an updated firefox does the job for you in RHEL old OSs in that case you can try [remi repository](http://rpms.famillecollet.com/) to update firefox. Check [remi famillecollet's site](http://blog.famillecollet.com/pages/Config-en) for info, tecmint also has an article [Installing or Updating Firefox 30 using Remi](http://www.tecmint.com/install-firefox-in-rhelcentos-6-3-fedora-17-16/). If you hate 3rd parties you can simply download the archive from firefox's site, extract it and run (possibly add few links such as in /usr/bin for convenience)

### Installing using direct full package
Download google chrome linux package first from [Chrome's page](https://www.google.com/intl/en/chrome/browser/). Apply this command in the directory where you downloaded [the file](https://dl.google.com/linux/direct/google-chrome-stable_current_x86_64.rpm),

    sudo dnf localinstall google-chrome-stable_current_x86_64.rpm    

If you want to install the package manually the command will be like this:

    sudo dpkg -i google-chrome-stable_current_x86_64.deb
    

#### References
1.  [Install Google Chrome on Fedora 17/16, CentOS/Red Hat (RHEL) 6.3](https://www.if-not-true-then-false.com/2010/install-google-chrome-with-dnf-on-fedora-red-hat-rhel/)

**Commit history**

1. update fedora 34-35 - Oct 26, 2021
2. update fedora section - Dec 19, 2016
3. update - June 29, 2014