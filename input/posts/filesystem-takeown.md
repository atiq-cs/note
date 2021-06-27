Title: Fix File Ownership / Readonly Attribute
Published: 07/25/2019
Tags:
  - windows command prompt
---

[to do: import [from wordpress](https://atiqcs.wordpress.com/2014/03/17/take-ownership-remove-read-only-attributes-of-directory-or-file), requires linking 7 images]

# Introduction
some programs cannot access certain file/folder, we need to reset permissions,

for transferring ownership to current user we will still need an elevated prompt,

	$ takeown /f D:\PFiles_x64 /r

for removing readonly, we can also use Set-ItemProperty,

	$ Set-ItemProperty file.txt -name IsReadOnly -value $false


# References
 1. [SO - How to remove readonly attribute using powershell][1]


  [1]: https://stackoverflow.com/questions/893167/how-to-remove-readonly-attribute-on-file-using-powershell
 