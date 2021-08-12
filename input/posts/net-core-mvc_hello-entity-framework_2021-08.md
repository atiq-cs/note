Title: dotnet core Hello World with Entity Framework and Database
Published: 8/12/2021
Tags:
  - dotnet core
  - entity framework
---

## Razor Pages App
We follow this primary reference documentation, [MS Docs - aspnet/core/data/ef-rp/intro](https://docs.microsoft.com/en-us/aspnet/core/data/ef-rp/intro)

    dotnet new webapp -n P03_ContosoUniversity_RP
    cd P03_ContosoUniversity_RP

    dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design
    dotnet add package Microsoft.EntityFrameworkCore.SQLite
    dotnet add package Microsoft.EntityFrameworkCore.SqlServer
    dotnet add package Microsoft.EntityFrameworkCore.Design
    dotnet add package Microsoft.EntityFrameworkCore.Tools

    dotnet add package Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore

    dotnet aspnet-codegenerator razorpage -m Student -dc ContosoUniversity.Data.SchoolContext `
    -udl -outDir Pages\Students --referenceScriptLibraries

If we are using SQLite database,

    dotnet aspnet-codegenerator razorpage -m Student -dc ContosoUniversity.Data.SchoolContext `
    -udl -outDir Pages\Students --referenceScriptLibraries -sqlite


We refactor all `P03_ContosoUniversity_RP` with `ContosoUniversity`.

Scaffolding source files have `context.Student` which is not correct. We update those with `context.Students`.

    dotnet tool update --global dotnet-ef
    dotnet ef database update

EF supports dev environment as well,

    dotnet ef database update -- --environment Development

Finally, we run it,

    $Env:ASPNETCORE_ENVIRONMENT = "Development"
    dotnet run

There's some differences with the cu50 sample.
- `Pages\Students\Index.cshtml.cs` is much larger not just one line,

    Student = await _context.Students.ToListAsync();

- Indentations under `Data` folder
- Code for Error messages, logger etc in a number of files
- they have some region definitions with snippet names

In summary, the actual cu50 sample is much more complete than the one we create by following the tutorial.

Some observations,
- localdb mdf files are usually stored at `$HOME` (for example, `C:\Users\YOUR_NAME`)

### References
**Refs on Razor Pages**
- [Razor Pages](https://docs.microsoft.com/en-us/aspnet/core/razor-pages)
- [learnrazorpages - Handler Methods in Razor Pages](https://www.learnrazorpages.com/razor-pages/handler-methods)

**Refs on Entity Framework Performance**
- [Common performance mistakes](https://medium.com/swlh/entity-framework-common-performance-mistakes-cdb8861cf0e7)


**Example to connect from Visual Studio Server Explorer**

    Server Name: (LocalDb)\MSSQLLocalDB
    DB: CU-1

ref: 1. [connection strings example](https://docs.microsoft.com/en-us/aspnet/mvc/overview/getting-started/introduction/creating-a-connection-string), 2. [how to connect database in visual studio data explorer](https://stackoverflow.com/questions/21563940/how-to-connect-to-localdb-in-visual-studio-server-explorer)


## MVC App
Steps

    dotnet new mvc -n P03_ContosoUniversity

Replace `P03_ContosoUniversity` with 'Contoso University'.

    dotnet add package Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore

Primary reference documentation, [MS Docs - aspnet/core/data/ef-mvc/intro](https://docs.microsoft.com/en-us/aspnet/core/data/ef-mvc/intro)

We add default connection in `appsettings.Development.json`. We pretty much follow the steps in tutorial and able to produce working version. Only, using VS Code instead of Visual Studio Pro is bit of work since all CLI commands are not provided and we need to repro them, especially scaffolding ones.

## Razor Pages App (net core)
This is an experiment to run the EF Core Razor App with .net core,

We update net framework target in `.csproj` file.

We add following nuget packages,

    dotnet new webapp -n P03_ContosoUniversity_netcore
    dotnet add package Microsoft.EntityFrameworkCore.Design
    dotnet add package Microsoft.EntityFrameworkCore
    dotnet add package Microsoft.EntityFrameworkCore.SQLite


We add all the scaffolding files from previous project (above) that targets .net6

We end up finding that the following method calls:

    // in ConfigureServices
    // services.AddDatabaseDeveloperPageExceptionFilter();
    // in Configure
    // app.UseMigrationsEndPoint();

require `Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore` package,

    dotnet add package Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore
    Determining projects to restore...
    Writing C:\Users\atiq\AppData\Local\Temp\tmp6CF5.tmp
    info : Adding PackageReference for package 'Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore' into project 'D:\Code\CS\P03_ContosoUniversity_netcore\P03_ContosoUniversity_netcore.csproj'.
    info :   GET https://api.nuget.org/v3/registration5-gz-semver2/microsoft.aspnetcore.diagnostics.entityframeworkcore/index.json
    info :   OK https://api.nuget.org/v3/registration5-gz-semver2/microsoft.aspnetcore.diagnostics.entityframeworkcore/index.json 419ms
    info : Restoring packages for D:\Code\CS\P03_ContosoUniversity_netcore\P03_ContosoUniversity_netcore.csproj...
    error: NU1202: Package Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore 5.0.9 is not compatible with netcoreapp3.1 (.NETCoreApp,Version=v3.1). Package Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore 5.0.9 supports: net5.0 (.NETCoreApp,Version=v5.0)
    error: Package 'Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore' is incompatible with 'all' frameworks in project 'D:\Code\CS\P03_ContosoUniversity_netcore\P03_ContosoUniversity_netcore.csproj'

Also, the old version (3.1.18 supports .net core) of `Diagnostics.EntityFrameworkCore` doesn't have the definitions mentioned above.

Conclusion: since some of the packages i.e., `Diagnostics.EntityFrameworkCore` don't target .net core and only targets newer versions of net framework, for example, .net 6. We cannot run the examples from official tutorial that uses EF Core or Diagnostics (may be Logging package as well).