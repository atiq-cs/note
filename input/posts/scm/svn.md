Title: Subversion Basics
Published: 11/14/2014
Tags:
  - version Control
---
## SVN - Version Control
Checking out a fresh copy from the remote repo,

    svn checkout URL local_target_dir_path --username name@company.com --password password_for_repo

Add files to svn repo,

    svn add file_path
    svn add directory_path/pattern*

Exporting a file from SVN repo,

    svn export -r 5 "file:///D:/svnrepo/repo1/CoolApp/cpp/app.cpp" D:\Projects\CoolApp\app.cpp `
        --username YOUR_USER_NAME --password YOUR_PASS

Removing files,

    svn remove file_path

Finally, commit, unlike git/hg in svn a commit pushes files to server as well,

    svn commit D:\Projects\CoolApp --username YOUR_USER_NAME --password YOUR_PASS -m "my change summary"

Also we can do,

    svn update
    svn info
    svn status

## Running SVN Server Locally
We complete these parts with the help of `svnadmin` tool.

I used to use "CollabNet Subversion" to accomplish this. The software placed svnadmin binary in `C:\Program Files\CollabNet\Subversion Client`.

Using `svnadmin` we are able to create repositories locally.

    svnadmin create "D:\svnrepo\repo1"

Initial import of a project into this repo,

    svn import D:\Projects\CoolApp "file:///D:\svnrepo\repo1\CoolApp" -m "Initial import"

Later, to checkout from this local repo,

    svn checkout "file:///D:\svnrepo\repo1\CoolApp" D:\Projects\CoolApp `
        --username YOUR_USER_NAME --password YOUR_PASS

If `svn info` shows an outdated local repo, we should update the checkout,

    svn update
