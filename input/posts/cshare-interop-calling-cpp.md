Title: InterOp Calling C++ from C#
Published: 4/12/2018
Tags:
  - cpp
  - csharp
  - dotnet
---
For this demonstration, let's consider a Visual Studio 2017 Solution containing two projects,

 - One project containing C++ Code
 - Other project contain C# driving code doing the demonstrating interop

C++ code looks like,

    extern "C"             //No name mangling
    __declspec(dllexport)  //Tells the compiler to export the function
    int                    //Function return type     
    __cdecl                //Specifies calling convention, cdelc is default, 
                           //so this can be omitted 
      test(int number) {
      return number + 1;
    }

Please note the way return type specified compare to traditional function declaration.

C# driver code looks like following,

    using System;
    using System.Runtime.InteropServices;

    namespace p1
    {
      public static class NativeTest
      {
        static void Main(string[] args)
        {
          Console.WriteLine("Interop test: " + test(100));
        }

        private const string DllFilePath = @"D:\Code\CSharp\InterOp\Debug\Cpp.dll";

        [DllImport(DllFilePath , CallingConvention = CallingConvention.Cdecl)]
        private extern static int test(int number);

        public static int Test(int number) {
            return test(number);
        }
      }
    }


My projects dir structure looks like,

    D:\CODE\CSHARP\INTEROP
    │   p1.sln
    │
    ├───Cpp
    │   │   Cpp.vcxproj
    │   │   Cpp.vcxproj.filters
    │   │   Cpp.vcxproj.user
    │   │   Main.cpp
    │   │
    │   └───Debug
    │       │   Cpp.dll
    │       │   Cpp.exp
    │       │   Cpp.ilk
    │       │   Cpp.lib
    │       │   Cpp.log
    │       │   Cpp.pdb
    │       │   Main.obj
    │       │   vc141.idb
    │       │   vc141.pdb
    │ 
    └───p1
        │   App.config
        │   p1.csproj
        │   Program.cs
        │
        ├───bin
        │   └───Debug
        │           p1.exe
        │
        ├───obj
        │   └───Debug
        │       │   p1.exe
        │       │   p1.pdb
        └───Properties
                AssemblyInfo.cs

For the project structure above, I can use a relative location of the file,

    private const string DllFilePath = @"..\..\..\Cpp\Debug\Cpp.dll";

Additionally, I have changed following for C++ project,

*Output Directory:* $(SolutionDir)$(ProjectName)\$(Configuration)\
*Configuration Type:* Dynamic Library (.dll)


When we are passing arrays and pointers we need data marshalling. ref, [Using arrays and pointers in C# with C DLL](https://stackoverflow.com/questions/741206/using-arrays-and-pointers-in-c-sharp-with-c-dll)

msdn documentation provides instructions for this for managed C++ to unmanaged C++.
[How to: Call Native DLLs from Managed Code Using PInvoke](https://docs.microsoft.com/en-us/cpp/dotnet/how-to-call-native-dlls-from-managed-code-using-pinvoke)

So, for a new project it makes sense. Or if it is feasible to create managed C++ wrapper around the unmanaged code. It gives us an assembly that can be directly referenced from C#. Ref, [Possible to call C++ code from C#?](https://stackoverflow.com/questions/935664/possible-to-call-c-code-from-c)

## References

 - [How to call C++ code from C#](https://stackoverflow.com/questions/9407616/how-to-call-c-code-from-c-sharp)
 - [Calling Native Functions from Managed Code - PInvoke and the DllImport, Marshaling Arguments](https://docs.microsoft.com/en-us/cpp/dotnet/calling-native-functions-from-managed-code)
- Interop msdn blog posts in 2006 [part 1](https://blogs.msdn.microsoft.com/borisj/2006/07/30/interop-101-part-1/) and [part 5](https://blogs.msdn.microsoft.com/borisj/2007/02/09/interop-101-part-5/)