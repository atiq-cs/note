Title: System Programming
Published: 02/21/2018
Tags:
  - System Programming
---
**System call related questions**

**Which two System calls are used to create new process?**
 fork() and exec()

In Windows, it is called `CreateProcess`.

values return by `fork()`,

 - 0 in the child process
 - The PID of the child in the parent process
 - -1 in the parent if there was a failure (there is no child, naturally).
 
**How are System Calls provided?**
Generally, systems provide a library or API that sits between normal programs and the operating system. On Unix-like systems, that API is usually part of an implementation of the C library (libc), such as glibc, that provides wrapper functions for the system calls, often named the same as the system calls they invoke. On Windows NT, that API is part of the Native API, in the ntdll.dll library; this is an undocumented API used by implementations of the regular Windows API a

Number of fd allowed in the system?

This limit is defined by,

    /proc/<pid>/limits.
    /proc/<pid>/fd/

Or can be found using command,

    ulimit -Hn

[OS - File System Calls - .cs.uregina.ca/~hamilton](http://www2.cs.uregina.ca/~hamilton/courses/330/notes/unix/filesyscalls.html)

**How to kill/terminate process?**
We can utilize `pgrep` to find the process, `pkill` to kill it.

On Windows, we have `tasklist`, `taskkill` to kill.

**What is `kill -9`?**
Variants: `-9`, `-KILL`, `-SIGKILL`

if one of these are not specified usually kill command sends a TERM signal.
The difference between SIGTERM and SIGKILL is the way an application may act on the signal:

 - TERM: an application will be able to terminate, i.e. properly run a shutdown routine.
 - KILL: the applications is stopped and killed immediately (which could lead to data loss or raising apport to report a presumed crash in some cases).
 

### References
- [askubuntu](https://askubuntu.com/questions/184071/what-is-the-purpose-of-the-9-option-in-the-kill-command)
- [After fork(), where does the child begin its execution?](https://unix.stackexchange.com/questions/4377/after-fork-where-does-the-child-begin-its-execution?rq=1)
- [wiki System Call](https://en.wikipedia.org/wiki/System_call)
