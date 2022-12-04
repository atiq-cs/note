Title: Switch among multiple Git Config/Profiles using Powershell
Lead: use git cli to switch among multiple Git Config/Profiles
Published: 11/02/2020
Tags:
  - powershell
  - version control
  - git
---

## Overview
Overview of the script,

- verify user name stuff, arguments
- switch profile based on first command line argument

## final script

```powershell
[CmdletBinding()]
param(
    [ValidateSet('USER_NAME_1', 'USER_NAME_2')]
    [Parameter(Mandatory=$true)] [string] $ProfileName,
    [Parameter(Mandatory=$true)] [string[]] $CredUserNames
)


<#
.SYNOPSIS
    Switch to given git config profile
.DESCRIPTION
    Sets for both git repo local and global,
    - user name
    - user email
    - credential user name
#>
function Main() {
    # Expecting switch between 2 users
    if ($CredUserNames.Length -lt 2) {
        throw [ArgumentException] ('Unexpected number of gUser Names!')
    }

    switch ($ProfileName) {
        "USER_NAME_1" {
            $gitUserName = git config --get user.name
            $shellUserName = $($Home.SubString($Home.LastIndexOf('\')+1))

            # Expecting git user name to start with Windows Login UserName
            # For example,
            #   windows username Mak will match with
            #   git user full name 'Atiq Rahman'
            if (-not ($gitUserName).StartsWith($shellUserName, [System.StringComparison]::`
                InvariantCultureIgnoreCase)) {
                throw [ArgumentException] ('Unexpected user name ' + $gitUserName + '!')
            }

            $gitUserEmail = git config --get user.email
            # global
            git config --global user.name "$gitUserName"
            git config --global user.email $gitUserEmail

            git config credential.username $CredUserNames[0]
            $Env:GITHUB_TOKEN = 'gxp_clylM69Sjj0OZz5JOKX98TzcIE2xBo1MjNTO'
            Break
        }
        "USER_NAME_2" {
            $gitUserName = git config --get user.name
            # last name of user 2, just for validation
            $userNameSuffix = 'USER2_SUFFIX'

            if (-not ($gitUserName).EndsWith($userNameSuffix)) {
                throw [ArgumentException] ('Unexpected user name ' + $gitUserName + '!')
            }

            $gitUserEmail = git config --get user.email
            # global
            git config --global user.name "$gitUserName"
            git config --global user.email $gitUserEmail

            git config credential.username $CredUserNames[1]
            $Env:GITHUB_TOKEN = 'gxp_84770a857cb1482de0e8af7c39a06de4ccf001eeb'
            Break
        }
        Default { "Unexpected $ProfileName!" }
    }

    VerifyGitProfile

    'Remember to set your GITHUB_TOKEN before invoking ''deploy'''
}

function VerifyGitProfile() {
    'Final Git Configuration:'
    git config --global --get user.name
    git config --global --get user.email
    git config --get user.name
    git config --get user.email
    git config --get credential.username
}

Main
```

Copy of this script (including documentation) can be found at:
https://github.com/atiq-cs/old-scripts/blob/dev/pwsh/Switch-GitConfig.ps1

More scripts like this can be found at:
https://github.com/atiq-cs/old-scripts/blob/dev/pwsh
