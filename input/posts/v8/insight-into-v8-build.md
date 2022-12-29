Title: Insight into V8 Build System
Published: 05/17/2016
Tags:
  - Javascript V8
  - V8 Build
---
*Please be aware of notation below in command outlines. `$` represents a command and rests of the lines following that line are output.*

Just like [Chromium](https://www.chromium.org/) v8 is cross platform. Its build system works on Windows, Linux, MAC and Unix. It is possible to bring a new OS or hardware platform by looking at examples provided by the source code. However, when you start working you feel the lack of documentation about how things get done. So I decided to document what I learnt.

### What is in build/gyp ?

V8 build uses [build/gyp](https://chromium.googlesource.com/external/gyp.git/+/master) project to generate build configuration files. `make` reads those configuration files (includes .mk etc) during the build. gyp retains provided command line gyp flags, sets appropriate compiler flags (takes care of reading from environment variables as well). Some of the make arguments are mapped properly by the [Makefile](https://github.com/v8/v8/blob/master/Makefile).

    # disassembler=on
    ifeq ($(disassembler), on)
      GYPFLAGS += -Dv8_enable_disassembler=1
    endif
    # objectprint=on
    ifeq ($(objectprint), on)
      GYPFLAGS += -Dv8_object_print=1
    endif
    

Code snippet above in the `Makefile` ensures that make argument `disassembler=on` would work as well as `objectprint=on`. It sets the appropriate GYP Flags for those options.

### An example of build

To be acquainted with the v8 build system it is handy to do a build yourself. The build is pretty simple on Linux and is worth trying. I have compiled an [example of Linux build](https://atiqcs.wordpress.com/2016/05/18/v8-build-fedora-23) as reference. If you are a beginner you should have a look at that before proceeding to next part of the article.

### The Fetch Script

We talked little bit about `fetch` on the [v8 build on Linux](https://atiqcs.wordpress.com/2016/05/18/v8-build-fedora-23) article. As long as fetch can determine the target platform it's all good. It works fine on Linux, FreeBSD, Windows and MACOSX. Based on target platform it determines what build-tools to download. `fetch` throws an exception on a new platform that it does not know. It throws an exception when it cannot determine target platform. To explain this better we need to look at next two sub-sections.

### build/landmine_utils.py

`fetch` uses `build/landmines.py` script to detect builder setting and gyp environment setting. The script, `build/landmine_utils.py` is used by `build/landmines.py`. I do not know if any other file uses this script. There are a number of `@memoize()` blocks in this file that determines the target platform,

    @memoize()
    def IsWindows():
      return sys.platform in ['win32', 'cygwin']
    
    @memoize()
    def IsLinux():
      return sys.platform.startswith(('linux', 'freebsd'))
    
    @memoize()
    def IsMac():
      return sys.platform == 'darwin'
    

It is easier to understand looking at the `builder()` function used by landmines.py,

    if platform() == 'android':
      # Good enough for now? Do any android bots use make?
      return 'make'
    elif platform() == 'ios':
      return 'xcode'
    elif IsWindows():
      return 'msvs'
    elif IsLinux():
      return 'make'
    elif IsMac():
      return 'xcode'
    else:
      assert False, 'Don\'t know what builder we\'re using!'
    

It tries to get what builder the target platform uses. When platform does not match with either one from the conditional statements it does an `assert`. This is where you need to look at if fetch is not working on your new platform. If your build should use `gmake` like Linux you can return `IsLinux()` true,

    def IsLinux():
        return sys.platform.startswith(('linux', 'freebsd', 'new_os'))
    

### build/landmines.py

If your platform uses an unconventional builder instead of `make` you need to add that to the `get_build_dir` function. Looking at following code block in the file you will get the idea,

    ret = None
    if build_tool == 'xcode':
        ret = os.path.join(SRC_DIR, 'xcodebuild')
    elif build_tool in ['make', 'ninja', 'ninja-ios']:  # TODO: Remove ninja-ios.
        if 'CHROMIUM_OUT_DIR' in os.environ:
          output_dir = os.environ.get('CHROMIUM_OUT_DIR').strip()
          if not output_dir:
            raise Error('CHROMIUM_OUT_DIR environment variable is set but blank!')
        else:
          output_dir = landmine_utils.gyp_generator_flags().get('output_dir', 'out')
        ret = os.path.join(SRC_DIR, output_dir)
    elif build_tool in ['msvs', 'vs', 'ib']:
        ret = os.path.join(SRC_DIR, 'build')
    else:
        raise NotImplementedError('Unexpected GYP_GENERATORS (%s)' % build_tool)
    return os.path.abspath(ret)
    

In this file, there are two other code blocks where functions from landmine\_util.py are called. In conclusion, build system maps detected platform to a builder like gmake, msvs, xcode etc. When it cannot map it throws an exception.

### v8 Project Makefile

[Current Makefile](https://github.com/v8/v8/blob/master/Makefile) in the v8 repository is first class one. It defines build modes and build targets for a number of platforms and build modes. It works with gmake for sure. Other Make tools might give error except for Windows where Visual Studio/Windows SDK stuffs are used to build. There are plenty of things that can be discussed. I would like to start with following command,

    make native
    

If you have done a build on Linux you probably already have an idea what it does. 'native' does a native release build for target platform: x64 on 64 bit architecture and ia32 on 32 bit architecture. We can do specific builds,

    make x64.release
    

Besides, it supports other architecture/hardware platforms. However, be mindful that those commands will default to output of the following cmmand,

    g++ -v
    Target: x86_64-pc-solaris2.11
    

Detailed output of the command is *link expired: http://collabedit.com/sk78q*. and takes out the target string to determine the architecture. To say specifically `g++` is wrong! It uses whatever `CXX` is set for the platform. CXX can be clang++, g++ etc. Here is the code block in the Makefile that shows of use of `CXX` to get target architecture string,

```bash
$(eval CXX_TARGET_ARCH:=$(shell $(CXX) -v 2>&1 | grep ^Target: | \
     cut -f 2 -d " " | cut -f 1 -d "-" ))
$(eval CXX_TARGET_ARCH:=$(subst aarch64,arm64,$(CXX_TARGET_ARCH)))
$(eval CXX_TARGET_ARCH:=$(subst x86_64,x64,$(CXX_TARGET_ARCH)))
```

Similar code block can be found under Makefile target of `$(ENVFILE).new`. In some systems, `g++ -v` does not give actual target information. For example, gcc-4.9 or older on Solaris. In x86 (64 bit system) those gcc give,

    gcc -v
    Target: i386-solaris2.11
    

As a result, with those gcc build target becomes ia32 on 64 bit system. Funny? It is possible to do multiple target builds on biarch systems. By that I mean you can do 32 bit build on some 64 bit systems for some platforms. More on this on toolchain.gypi section. For a new platform, if the hardware is not listed in ARCHES in Makefile it should be added. Following lines show what target platforms are supported for build,

    ARCHES = ia32 x64 x32 arm arm64 mips mipsel mips64 mips64el x87 ppc ppc64 \
        s390 s390x
    

A NEW PLATFORM can be added to the list,

    ARCHES = ia32 x64 x32 arm arm64 mips mipsel mips64 mips64el x87 ppc ppc64 \
        s390 s390x NEW_PLATFORM
    

Makefile has target definitions for following type of build commands,

    gmake native
    gmake platform.release
    gmake platform.debug
    gmake platform.optdebug
    

Here, platform can be anything that is defined in ARCHES (discussed in previous paragraph). How do I know the modes? It's here,

    MODES = release debug optdebug
    

A bare `gmake` command without any arguments means building all combination of default modes and default arches. Total 2\*3=6 of following combinations are attempted to be built when the command is applied,

    DEFAULT_ARCHES = ia32 x64 arm
    DEFAULT_MODES = release debug
    

#### Supported clean commands

The Makefile has target definitions for following type of clean commands,

    gmake clean
    gmake native.clean
    gmake platform.clean
    

`gmake clean` attempts clean for all combination of Modes and Arch. Therefore, it can be slow. On the contrary, following are examples of faster clean commands,

    gmake native.clean
    gmake ia32.clean
    gmake x64.clean
    gmake sparc64.clean
    

#### Modifying clean target

Why do we need to do that? You will need that if you want to apply a different command then find or you want to apply additional commands that only applies to your new platform. For example, instead of,

    find $(OUTDIR) -regex '.*\(host\|target\)\.$(basename $@).*\.mk' -delete
    

you can use a custom find command on your new OS,

```bash
$(eval TARGET_ARCH_DESC:=$(shell $(CXX) -v 2>&1 | grep ^Target: |  cut -f 2 -d " " ))
$(if $(findstring NEW_OS, $(TARGET_ARCH_DESC)), /usr/custom/bin/find, find) $(OUTDIR) -regex \
'.*\(host\|target\)\.$(basename $@).*\.mk' -delete
```

### build/detect_v8_host_arch.py
*ref, this source file on [upstream v8/v8](https://github.com/v8/v8/blob/8ffa1f10cb9c670fd6416bc214e2c22ebec8af12/third_party/binutils/detect_v8_host_arch.py)*  
  
As part of the fixing the build system for a new platform this file should be taken care of as well,

```c
def DoMain(_):
  host_arch = platform.machine()
  host_system = platform.system();

  # Convert machine type to format recognized by gyp.
  if re.match(r'i.86', host_arch) or host_arch == 'i86pc':
    host_arch = 'ia32'
  elif host_arch in ['x86_64', 'amd64']:
    host_arch = 'x64'
  elif host_arch.startswith('arm'):
    host_arch = 'arm'
  # code trimmed    

  if host_arch == 'x64' and platform.architecture()[0] == '32bit':
    host_arch = 'ia32'

  return host_arch
```    

If `gcc -v` is giving you "i386" then when you apply following command,

    $ python build/detect_v8_host_arch.py
    

you get ia32. Even though your platform is 64 bit even `platform.architecture()[0]` gives 32 bit tragically. To fix this problem following modification on the first conditional can be considered,

    if re.match(r'i.86', host_arch) or host_arch == 'i86pc':
        if host_system == 'NEW_OS':
            host_arch = 'x64'
        return host_arch
    

### build/standalone.gypi
*ref, this source file on [upstream v8/v8](https://github.com/v8/v8/blob/8ffa1f10cb9c670fd6416bc214e2c22ebec8af12/gypfiles/standalone.gypi)*  
  
We have talked about `detect_v8_host_arch` function on section `build/detect_v8_host_arch.py`. If you want your new OS to support this it can be added as showed in following block,

```python
['OS=="linux" or OS=="freebsd" or OS=="openbsd" or \
    OS=="netbsd" or OS=="mac" or OS=="qnx" or OS=="aix" or \
    OS=="new_OS"', {
        'host_arch%': '<!pymod_do_main(detect_v8_host_arch)',
    }, {
```

If the new OS does not have clang yet it can choose to disable use of clang/clang++ during build,

```python
['host_arch!="ppc" and host_arch!="ppc64" and host_arch!="ppc64le" and \
  host_arch!="s390" and host_arch!="s390x" and OS!="NEW_OS"', {
    'host_clang%': 1,
}, {
    'host_clang%': 0,
}],
```
    

This is way to disable `Werror` temporarily,

```python
['OS=="NEW_OS"', {
  'target_defaults': {
    'conditions': [
      ['target_arch=="NEW_PLATFORM"', {
        'cflags!': [ '<(werror)', ],
      }],
    ],
  },
}],  # OS=="NEW_OS"
```
    

If you want to do shared build instead of static it is a good idea to put the condition right after `conditions` statement,

    'conditions': [
      ['OS=="NEW_OS"', {
        'component%': 'shared_library',
      }],
    

##### How to specify clang dir during compile?

Default location of clangdir is `<(base_dir)/third_party/llvm-build/Release+Asserts` which uses clang compiler binaries downloaded from google storage. For platforms, windows, linux, mac they have separate binaries. For a new platform if clang installed in the system, `/usr/bin/clang`.

    $ diff standalone.gypi standalone.gypi.modified
    94c94
    <       'clang_dir%': '       'clang_dir%': '/usr',
    

Change on line 94 specifies the new clang location (which contains `/bin/clang`).

### build/toolchain.gypi
*ref, this source file on [upstream v8/v8](https://github.com/v8/v8/blob/8ffa1f10cb9c670fd6416bc214e2c22ebec8af12/gypfiles/toolchain.gypi)*  
  
Changes in this file for a new platform helps us to build properly for different architectures i.e., 32 bit and 64 bit. This is why we enable host and target biarch variables for a new platform (I can be wrong) so that we can do 32 bit build on 64 bit if supported by CXX. Use only if CXX in your platform supports this, First, we set _biarch_ variables: this is for `host_cxx_is_biarch`,

```python
['host_arch=="ia32" or host_arch=="x64" or \
  host_arch=="ppc" or host_arch=="ppc64" or \
  host_arch=="s390" or host_arch=="s390x" or \
  host_arch=="NEW_PLATFORM" or clang==1', {
  'variables': {
    'host_cxx_is_biarch%': 1,
  },
```
    

And, this is for target\_cxx\_is\_biarch,

    ['target_arch=="ia32" or target_arch=="x64" or target_arch=="x87" or \
      target_arch=="ppc" or target_arch=="ppc64" or target_arch=="s390" or \
      target_arch=="s390x" or host_arch=="NEW_PLATFORM" or clang==1', {
      'variables': {
        'target_cxx_is_biarch%': 1,
      },
    

This is the code block to look into to set compile flags for 64 bit builds,

    ['(OS=="linux" or OS=="android" or OS=="NEW_OS") and \
    (v8_target_arch=="x64" or v8_target_arch=="arm64" or \
     v8_target_arch=="ppc64" or v8_target_arch=="s390x" or v8_target_arch=="NEW_PLATFORM")', {
    'target_conditions': [
      ['_toolset=="host"', {
        'conditions': [
          ['host_cxx_is_biarch==1', {
            'cflags': [ '-m64' ],
            'ldflags': [ '-m64' ]
          }],
         ],
       }],
    

The next code block is the one to look into to set compile flags for 32 bit builds,

```python
['(OS=="linux" or OS=="freebsd" or OS=="openbsd" or OS=="NEW_OS" \
  or OS=="netbsd" or OS=="mac" or OS=="android" or OS=="qnx") and \
(v8_target_arch=="arm" or v8_target_arch=="ia32" or \
  v8_target_arch=="x87" or v8_target_arch=="mips" or \
  v8_target_arch=="mipsel" or v8_target_arch=="ppc" or \
  v8_target_arch=="s390" or v8_target_arch=="NEW_PLATFORM")', {
'target_conditions': [
  ['_toolset=="host"', {
    'conditions': [
      ['host_cxx_is_biarch==1', {
        'conditions': [
          ['host_arch=="s390" or host_arch=="s390x"', {
            'cflags': [ '-m31' ],
            'ldflags': [ '-m31' ]
          },{
            'cflags': [ '-m32' ],
            'ldflags': [ '-m32' ]
          }],
        ],
      }],
    ],
```

### build/features.gypi
*ref, this source file on [upstream v8/v8](https://github.com/v8/v8/blob/8ffa1f10cb9c670fd6416bc214e2c22ebec8af12/gypfiles/features.gypi)*  
  
On a new platform we can disable use of snapshot adding a condition block,

    'v8_use_snapshot%': 'true',
    'conditions': [
      ['target_arch == "NEW_PLATFORM"', {
        'v8_use_snapshot%': 'false',
      }],
    ],
    

### FAQ

Q1. How to override compiler, assembler and linker for the build?

You can always override those like this,

    export CC=/usr/custom/gcc
    export AR=/usr/custom/ar
    export LD=/usr/custom/ld
    

By default, CC is set to cc instead of gcc in some platforms. If there is no cc found in current path it will give an error saying "cc: command not found". This is when mentioned export statements are very useful. An example to override this use clang would be \[[ref](http://stackoverflow.com/questions/7031126/switching-between-gcc-and-clang-llvm-using-cmake)\],

    $ export CC=/usr/bin/clang
    $ export CXX=/usr/bin/clang++
    

Q2. Why is `-m64` flag duplicate?

Probably, biarch conditionals are hitting match multiple times. Since, the behavior is identical on other platforms I will check this later.

Q3. How to disable use of clang during build?

There can be more than one ways to do this. Pass a GYP flag with `gmake` command like this, `GYPFLAGS="-Dclang=0"`

Q4. Why GYPFlags are messed up when I mix -DVAR and a shortcut?

GYPFlags get messed up if you mix something like `snapshot=on` and `GYPFLAGS="-Dclang=0"` on the command line. `GYPFLAGS="-Dclang=0"` will probably override the previous flag added by `snapshot=on`. So, be careful on mixing those arguments together.

**Acknowledgement**  
V8 Team,
- [Peter Schow](https://www.linkedin.com/in/peterschow)
- [Alfred Huang](https://www.linkedin.com/in/alfred-huang-61996a10)
- [Miguel Ulloa](https://www.linkedin.com/in/miguel-ulloa)

*Disclaimer: Not expert advise! Opinions are personal, my employer is not resonsible for opinions/information on this article.*


**References**
1. [chromium - buildtools](https://chromium.googlesource.com/chromium/buildtools/+/master)
2. [host arch related commit history on upstream](https://github.com/v8/v8/search?q=detect_v8_host_arch.py&type=commits)
