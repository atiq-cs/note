Title: dotnet core Hello World with Entity Framework and Database in 2018
Published: 7/1/2018
Tags:
  - dotnet
  - entity framework
---
Before we show how to create MVC app with entity framework we show how to create a simple console app.

## Creating a hello world console application
With .net core things are pretty simple. We run two commands and it does everything we need for a hello world program,

    $ dotnet new console -n P1_ConsoleApp

It creates the directory and puts required hello world source file and project file inside the dir named 'P1_Console'. What type of applications does it support can be found at [microsoft docs - dotnet-new][4]

    $ dotnet run
    Hello World!

detailed reference at [microsoft docs - dotnet-run][2]. It is minimalist and does what is supposed to do.

This builds the application and runs it. If you only want to build and don't wanna run.

    $ dotnet build
    Microsoft (R) Build Engine version 15.7.179.6572 for .NET Core
    Copyright (C) Microsoft Corporation. All rights reserved.

      Restore completed in 33.49 ms for D:\Code\netcore\P01_Console\P01_Console.csproj.
      P01_Console -> D:\Code\netcore\P01_Console\bin\Debug\netcoreapp2.1\P01_Console.dll

    Build succeeded.
        0 Warning(s)
        0 Error(s)

    Time Elapsed 00:00:00.80

Build ref is at [microsoft - dotnet-build][1]

Default source code of `Program.cs` looks like,

    using System;

    namespace P01_Console
    {
        class Program
        {
            static void Main(string[] args)
            {
                Console.WriteLine("Hello World!");
            }
        }
    }

I change it slightly to create a generic list of cities and to sort them which is as below,

    using System;
    using System.Collections.Generic;

    namespace P01_Console {
      class Program {
        static void Main(string[] args) {
          var cityList = new List { "San Jose", "Santa Clara", "Redwood" };
          cityList.Sort();
          foreach (var city in cityList)
            Console.WriteLine(city);
        }
      }
    }

And then I build/run it,

    $ dotnet run
    Redwood
    San Jose
    Santa Clara

`dotnet run` kinda quietly gets it done; it does not talk about changes it found, recompiling info etc. Command looks pretty simple. How about providing some arguments for the program? [ms docs - Getting started with .NET Core on Windows/Linux/macOS using the command line][3] demonstrates it.


## Creating hello world MVC application with Entity Framework and SQL Database Support
### Creating hello world MVC
First, we create a new MVC application,

    $ dotnet new mvc -n P2_MVC_SQLDB
    The template "ASP.NET Core Web App (Model-View-Controller)" was created successfully.
    This template contains technologies from parties other than Microsoft, see https://aka.ms/aspnetcore-template-3pn-210 for details.

    Processing post-creation actions...
    Running 'dotnet restore' on P2_MVC_SQLDB\P2_MVC_SQLDB.csproj...
      Restoring packages for D:\git_ws\net-core-demos\P2_MVC_SQLDB\P2_MVC_SQLDB.csproj...
      Generating MSBuild file D:\git_ws\net-core-demos\P2_MVC_SQLDB\obj\P2_MVC_SQLDB.csproj.nuget.g.props.
      Generating MSBuild file D:\git_ws\net-core-demos\P2_MVC_SQLDB\obj\P2_MVC_SQLDB.csproj.nuget.g.targets.
      Restore completed in 1 sec for D:\git_ws\net-core-demos\P2_MVC_SQLDB\P2_MVC_SQLDB.csproj.

    Restore succeeded.


This immediately creates a hello world mvc application and we can build/run it.

    $ dotnet run
    Using launch settings from D:\git_ws\net-core-demos\mvc_test\Properties\launchSettings.json...
    info: Microsoft.AspNetCore.DataProtection.KeyManagement.XmlKeyManager[0]
          User profile is available. Using 'C:\Users\atiqc\AppData\Local\ASP.NET\DataProtection-Keys' as key repository and Windows DPAPI to encrypt keys at rest.
    Hosting environment: Development
    Content root path: D:\git_ws\net-core-demos\mvc_test
    Now listening on: https://localhost:5001
    Now listening on: http://localhost:5000
    Application started. Press Ctrl+C to shut down.

When we navigate using the browser to https://localhost:5001 it looks like this.

![.net Core MVC Demo][8]

### Changing hello world mvc to EF, DB supported App

We add following two packages,

    $ dotnet add package Microsoft.EntityFrameworkCore.SqlServer -v 2.1.0
    $ dotnet add package Microsoft.EntityFrameworkCore.Tools -v 2.1.0

We add specific package version 2.1.0 because latest version at this time is 2.1.1 which has dependency conflict.

When we add/remove/change packages basically it changes the project file `P2_MVC_SQLDB.csproj`. Also note that,

    $ dotnet add package package_name

adds the latest version of the package.

Regarding package management, we also have remove package if we want,

    $ dotnet remove package Microsoft.EntityFrameworkCore.SqlServer

Now, we change source file `Startup.cs` to register our context with dependency injection. To know more about Dependency injection in ASP.NET Core [MS Docs article  - dependency-injection][5]

We add following namespaces,

    using EFGetStarted.AspNetCore.NewDb.Models;
    using Microsoft.EntityFrameworkCore;

And change the method `ConfigureServices` to,

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddMvc();

        var connection = @"Server=(localdb)\mssqllocaldb;Database=EFGetStarted.AspNetCore.NewDb;Trusted_Connection=True;ConnectRetryCount=0";
        services.AddDbContext(options => options.UseSqlServer(connection));
    }

This is not the best way to add connection strings but this serves the hello world purpose for simplification.

We add `Models\Model.cs`,

    New-Item Models\Model.cs

and paste following contents,

    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;

    namespace EFGetStarted.AspNetCore.NewDb.Models
    {
        public class BloggingContext : DbContext
        {
            public BloggingContext(DbContextOptions options)
                : base(options)
            { }

            public DbSet Blogs { get; set; }
            public DbSet Posts { get; set; }
        }

        public class Blog
        {
            public int BlogId { get; set; }
            public string Url { get; set; }

            public List Posts { get; set; }
        }

        public class Post
        {
            public int PostId { get; set; }
            public string Title { get; set; }
            public string Content { get; set; }

            public int BlogId { get; set; }
            public Blog Blog { get; set; }
        }
    }

Similarly we add `Controllers/BlogsController.cs`,

    New-Item Controllers/BlogsController.cs

with following contents,

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using EFGetStarted.AspNetCore.NewDb.Models;

    namespace P02_MVC.Controllers
    {
        public class BlogsController : Controller
        {
            private readonly BloggingContext _context;

            public BlogsController(BloggingContext context)
            {
                _context = context;
            }

            // GET: Blogs
            public async Task Index()
            {
                return View(await _context.Blogs.ToListAsync());
            }

            // GET: Blogs/Details/5
            public async Task Details(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var blog = await _context.Blogs
                    .FirstOrDefaultAsync(m => m.BlogId == id);
                if (blog == null)
                {
                    return NotFound();
                }

                return View(blog);
            }

            // GET: Blogs/Create
            public IActionResult Create()
            {
                return View();
            }

            // POST: Blogs/Create
            // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
            // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task Create([Bind("BlogId,Url")] Blog blog)
            {
                if (ModelState.IsValid)
                {
                    _context.Add(blog);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(blog);
            }

            // GET: Blogs/Edit/5
            public async Task Edit(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var blog = await _context.Blogs.FindAsync(id);
                if (blog == null)
                {
                    return NotFound();
                }
                return View(blog);
            }

            // POST: Blogs/Edit/5
            // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
            // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task Edit(int id, [Bind("BlogId,Url")] Blog blog)
            {
                if (id != blog.BlogId)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(blog);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!BlogExists(blog.BlogId))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return RedirectToAction(nameof(Index));
                }
                return View(blog);
            }

            // GET: Blogs/Delete/5
            public async Task Delete(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var blog = await _context.Blogs
                    .FirstOrDefaultAsync(m => m.BlogId == id);
                if (blog == null)
                {
                    return NotFound();
                }

                return View(blog);
            }

            // POST: Blogs/Delete/5
            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public async Task DeleteConfirmed(int id)
            {
                var blog = await _context.Blogs.FindAsync(id);
                _context.Blogs.Remove(blog);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            private bool BlogExists(int id)
            {
                return _context.Blogs.Any(e => e.BlogId == id);
            }
        }
    }

And Copy Views folder into View `Views/Blogs`.

 - Create.cshtml
 - Delete.cshtml
 - Details.cshtml
 - Edit.cshtml
 - Index.cshtml

In this demo, we have manually added all components required for enabling a blog controller, providing it view, providing basic operations and bind it with the Model using EF.

Visual Studio allows to do this automatically using scaffolding. When we do that we usually need following package to be installed,

    $ dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design -v 2.1.0

and then, we could use scaffolding template to create this as showed in screenshot below,

![.net Core MVC Demo Scaffolding][9]

Then, we create the database and update it,

    $ dotnet ef -v migrations add InitialCreate
    $ dotnet ef -v database update
    output here: https://gist.github.com/atiq-cs/2cfe23a43a296685aaea1fc379ce3ac0

I add `-v` to see verbose output which is not required. It's time to run the app,

    $ dotnet run
    Using launch settings from D:\git_ws\net-core-demos\P2_MVC_SQLDB\Properties\launchSettings.json...
    info: Microsoft.AspNetCore.DataProtection.KeyManagement.XmlKeyManager[0]
          User profile is available. Using 'C:\Users\atiqc\AppData\Local\ASP.NET\DataProtection-Keys' as key repository and Windows DPAPI to encrypt keys at rest.
    Hosting environment: Development
    Content root path: D:\git_ws\net-core-demos\P2_MVC_SQLDB
    Now listening on: https://localhost:5001
    Now listening on: http://localhost:5000
    Application started. Press Ctrl+C to shut down.

Now we navigate to https://localhost:5001/Blogs/ and view the site.

I follow following two reference while creating this.
- [MS Docs - Getting Started with EF Core on ASP.NET Core with a New database][6]
- [MS Docs - Getting Started with EF Core on .NET Core Console App with a New database][7]


  [1]: https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-build
  [2]: https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-run
  [3]: https://docs.microsoft.com/en-us/dotnet/core/tutorials/using-with-xplat-cli
  [4]: https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-new
  [5]: https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection
  [6]: https://docs.microsoft.com/en-us/ef/core/get-started/aspnetcore/new-db
  [7]: https://docs.microsoft.com/en-us/ef/core/get-started/netcore/new-db-sqlite
  [8]: https://github.com/atiq-cs/atiqcs-wp-com/raw/master/images/2018/07/03_netcore-demo1.png
  [9]: https://github.com/atiq-cs/atiqcs-wp-com/raw/master/images/2018/07/05_netcore-hello-mvc-scaffolding.png
