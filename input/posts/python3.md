Title: Python Language Reference
Published: 11/14/2019
Tags:
  - python
  - programming languages
---
## Install
After install, to verify,

    "import this" | python

This works on Powershell / pwsh as well.

_How do I return multiple values from a function?_
ref: [so](https://stackoverflow.com/q/354883)

_Typing “python” on Windows 10 (version 1903) command prompt opens Microsoft store_
ref [so](https://superuser.com/q/1437590)


### Program to find python version
Example below,

    import sys
    print (sys.version) #parentheses necessary in python 3. 
    print (sys.version_info)
    print (sys.hexversion)

### IDLE Editor
How to quit,

    quit()
    exit()

How to print something

    print(“string”)

run a script

    execfile()
