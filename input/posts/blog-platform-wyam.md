Title: Wyam with Cake Build, Github pages and Azure Pipeline (Windows Server 2019)
Published: 11/27/2019
Tags:
  - markdown
  - statiq
---
*Wyam is deprecated by [statiq](https://github.com/statiqdev).*

## Wyam
First, we install wyam install locally using pwsh admin,

    choco install wyam -y

Then, if we don't have dotnet core in path, we do in current shell,

    $Env:Path += ';C:\Program Files\dotnet'

We browse to our source dir and do a build,

    dotnet D:\PFiles_x64\Chocolatey\lib\wyam\Wyam.dll help
    dotnet D:\PFiles_x64\Chocolatey\lib\wyam\Wyam.dll help preview
    dotnet D:\PFiles_x64\Chocolatey\lib\wyam\Wyam.dll build --help


## github setup and local build
Create a new repo. do a clone or (init and add files).

However, _we don't create master branch_. We only create a branch called `source`.

    git clone https://github.com/atiq-cs/fftsys-site
    Rename-Item fftsys-site wyamBlog
    pushd wyamBlog
    git checkout -b source
    git add .gitignore
    git commit -m 'add git ignore file'
    git push -u origin source

Let's create the blog,

    dotnet D:\PFiles_x64\Chocolatey\lib\wyam\Wyam.dll new -r Blog
    dotnet D:\PFiles_x64\Chocolatey\lib\wyam\Wyam.dll build
    dotnet D:\PFiles_x64\Chocolatey\lib\wyam\Wyam.dll preview

After we add required config file, yml file etc we build on next step. After we are satisfied with local build and previous we push them to the repo.

## Cake Build
I update package versions in `build.cake` file,

    #tool nuget:?package=Wyam&version=2.2.9
    #tool nuget:?package=GitVersion.CommandLine&version=5.1.3
    #addin nuget:?package=Cake.Wyam&version=2.2.9
    #addin nuget:?package=Cake.Git&version=0.21.0

For successful build, we need to set the variables properly,

    $Env:GITHUB_USERNAME = 'atique'
    $Env:GITHUB_USEREMAIL = 'atiq@github.com'
    $Env:GITHUB_ACCESS_TOKEN = '123abcdef456'
    $Env:CNAME_CONTENT = 'blog.domain.com'

To generate new token visit [tokens page](https://github.com/settings/tokens/new)

Get build script for cake,

    Invoke-WebRequest https://cakebuild.net/download/bootstrapper/windows -OutFile build.ps1

We update in `config.wyam`,

    Settings[Keys.Host] = "blog.domain.com";

Otherwise we get,

    Compiling build script...
    Error: One or more errors occurred.
            Expecting state 'Element'.. Encountered 'Text'  with name '', namespace ''.

Here's example of local build,

    $ .\build -Target Build -Verbosity=verbose
    Preparing to run build script...
    Running build script...
    Analyzing build script...
    Processing build script...
    Installing tools...
    Installing addins...
    Compiling build script...

    ========================================
    Build
    ========================================
    Executing task: Build
    Wyam version 2.2.9

                    ,@@@@@@p
                  ,@@@@@@@@@@g
                z@@@@@@@@@@@@@@@
              g@@@@@@@@@@@@@@@@@@@,
            g@@@@@@@@@@@@@@@@@@@@@@@,
          ,@@@@@@@@@@@@@@@@@@@@@@@@@@@
         ,@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
         $@@@@@@@@@@@@@@@@@@@@@@@@@@@@@c
         @@@@@@@@@@@@@@@@@@@@@@@@B@@@@@@
         @@@@@@@@@@@@@@@@@@@@@@@  j@@@@@
         $@@@@@@@@@@@@@@@@@@@@@F  #@@@@`
          $@@@@@@@@@@@@@@@@@@P   g@@@@P
           %@@@@@@@@@@@@@     ,g@@@@@P
            3@@@@@@@@@@@@@@@@@@@@@@B`
              `%@@@@@@@@@@@@@@@@@P
                 `*%RB@@@@@RRP`
    **BUILD**
    Loading configuration from file:///D:/Code/wyamBlog/fftsys/config.wyam
    Installing NuGet packages
        Installing packages to C:/Users/atiq/.nuget/packages (using global packages folder)
        NuGet packages installed in 250 ms
    Loading assemblies
        Assemblies loaded in 194 ms
    Cataloging classes
        Classes cataloged in 116 ms
    Evaluating configuration script
        Evaluated configuration script in 26 ms
    Root path:
        file:///D:/Code/wyamBlog/fftsys
    Input path(s):
        file:///C:/Users/atiq/.nuget/packages/Wyam.Blog.CleanBlog.2.2.9/content
        theme
        input
    Output path:
        output
    Temp path:
        temp
    Using  as the JavaScript engine
    Cleaning temp path: temp
    Cleaned temp directory
    Cleaning output path: output
    Cleaned output directory
    Executing 15 pipelines
        Executing pipeline "Pages" (1/15) with 4 child module(s)
            Executed pipeline "Pages" (1/15) in 502 ms resulting in 1 output document(s)
        Executing pipeline "BlogPosts" (2/15) with 6 child module(s)
            Executed pipeline "BlogPosts" (2/15) in 120 ms resulting in 8 output document(s)
        Executing pipeline "Tags" (3/15) with 4 child module(s)
            Executed pipeline "Tags" (3/15) in 3737 ms resulting in 15 output document(s)
        Executing pipeline "TagIndex" (4/15) with 1 child module(s)
            Executed pipeline "TagIndex" (4/15) in 87 ms resulting in 1 output document(s)
        Executing pipeline "BlogArchive" (5/15) with 1 child module(s)
            Executed pipeline "BlogArchive" (5/15) in 93 ms resulting in 1 output document(s)
        Executing pipeline "Index" (6/15) with 4 child module(s)
            Executed pipeline "Index" (6/15) in 236 ms resulting in 1 output document(s)
        Executing pipeline "Feed" (7/15) with 3 child module(s)
            Executed pipeline "Feed" (7/15) in 151 ms resulting in 2 output document(s)
        Executing pipeline "RenderBlogPosts" (8/15) with 6 child module(s)
            Executed pipeline "RenderBlogPosts" (8/15) in 497 ms resulting in 8 output document(s)
        Executing pipeline "RenderPages" (9/15) with 6 child module(s)
            Executed pipeline "RenderPages" (9/15) in 141 ms resulting in 1 output document(s)
        Executing pipeline "Redirects" (10/15) with 3 child module(s)
            Executed pipeline "Redirects" (10/15) in 15 ms resulting in 0 output document(s)
        Executing pipeline "Less" (11/15) with 3 child module(s)
            Executed pipeline "Less" (11/15) in 8 ms resulting in 0 output document(s)
        Executing pipeline "Sass" (12/15) with 3 child module(s)
            Executed pipeline "Sass" (12/15) in 6 ms resulting in 0 output document(s)
        Executing pipeline "Resources" (13/15) with 1 child module(s)
            Executed pipeline "Resources" (13/15) in 30 ms resulting in 28 output document(s)
        Executing pipeline "ValidateLinks" (14/15) with 1 child module(s)
            Executed pipeline "ValidateLinks" (14/15) in 1 ms resulting in 0 output document(s)
        Executing pipeline "Sitemap" (15/15) with 3 child module(s)
            Executed pipeline "Sitemap" (15/15) in 10 ms resulting in 1 output document(s)
        Executed 15/15 pipelines in 5676 ms
    Finished executing task: Build

    Task                          Duration
    --------------------------------------------------
    Build                         00:00:06.8752279
    --------------------------------------------------
    Total:                        00:00:06.8752279


Here's example of deployment from local pwsh,

    $ .\build.ps1 -Target Deploy
    Preparing to run build script...
    Running build script...

    ========================================
    Build
    ========================================
    Wyam version 2.2.9

                    ,@@@@@@p
                  ,@@@@@@@@@@g
                z@@@@@@@@@@@@@@@
              g@@@@@@@@@@@@@@@@@@@,
            g@@@@@@@@@@@@@@@@@@@@@@@,
          ,@@@@@@@@@@@@@@@@@@@@@@@@@@@
         ,@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
         $@@@@@@@@@@@@@@@@@@@@@@@@@@@@@c
         @@@@@@@@@@@@@@@@@@@@@@@@B@@@@@@
         @@@@@@@@@@@@@@@@@@@@@@@  j@@@@@
         $@@@@@@@@@@@@@@@@@@@@@F  #@@@@`
          $@@@@@@@@@@@@@@@@@@P   g@@@@P
           %@@@@@@@@@@@@@     ,g@@@@@P
            3@@@@@@@@@@@@@@@@@@@@@@B`
              `%@@@@@@@@@@@@@@@@@P
                 `*%RB@@@@@RRP`
    **BUILD**
    Loading configuration from file:///D:/Code/wyamBlog/fftsys/config.wyam
    Installing NuGet packages
        Installing packages to C:/Users/atiq/.nuget/packages (using global packages folder)
        NuGet packages installed in 258 ms
    Loading assemblies
        Assemblies loaded in 248 ms
    Cataloging classes
        Classes cataloged in 117 ms
    Evaluating configuration script
        Evaluated configuration script in 5 ms
    Root path:
        file:///D:/Code/wyamBlog/fftsys
    Input path(s):
        file:///C:/Users/atiq/.nuget/packages/Wyam.Blog.CleanBlog.2.2.9/content
        theme
        input
    Output path:
        output
    Temp path:
        temp
    Using  as the JavaScript engine
    Cleaning temp path: temp
    Cleaned temp directory
    Cleaning output path: output
    Cleaned output directory
    Executing 15 pipelines
        Executing pipeline "Pages" (1/15) with 4 child module(s)
            Executed pipeline "Pages" (1/15) in 486 ms resulting in 1 output document(s)
        Executing pipeline "BlogPosts" (2/15) with 6 child module(s)
            Executed pipeline "BlogPosts" (2/15) in 67 ms resulting in 8 output document(s)
        Executing pipeline "Tags" (3/15) with 4 child module(s)
            Executed pipeline "Tags" (3/15) in 3617 ms resulting in 15 output document(s)
        Executing pipeline "TagIndex" (4/15) with 1 child module(s)
            Executed pipeline "TagIndex" (4/15) in 74 ms resulting in 1 output document(s)
        Executing pipeline "BlogArchive" (5/15) with 1 child module(s)
            Executed pipeline "BlogArchive" (5/15) in 124 ms resulting in 1 output document(s)
        Executing pipeline "Index" (6/15) with 4 child module(s)
            Executed pipeline "Index" (6/15) in 221 ms resulting in 1 output document(s)
        Executing pipeline "Feed" (7/15) with 3 child module(s)
            Executed pipeline "Feed" (7/15) in 135 ms resulting in 2 output document(s)
        Executing pipeline "RenderBlogPosts" (8/15) with 6 child module(s)
            Executed pipeline "RenderBlogPosts" (8/15) in 411 ms resulting in 8 output document(s)
        Executing pipeline "RenderPages" (9/15) with 6 child module(s)
            Executed pipeline "RenderPages" (9/15) in 48 ms resulting in 1 output document(s)
        Executing pipeline "Redirects" (10/15) with 3 child module(s)
            Executed pipeline "Redirects" (10/15) in 10 ms resulting in 0 output document(s)
        Executing pipeline "Less" (11/15) with 3 child module(s)
            Executed pipeline "Less" (11/15) in 6 ms resulting in 0 output document(s)
        Executing pipeline "Sass" (12/15) with 3 child module(s)
            Executed pipeline "Sass" (12/15) in 8 ms resulting in 0 output document(s)
        Executing pipeline "Resources" (13/15) with 1 child module(s)
            Executed pipeline "Resources" (13/15) in 67 ms resulting in 28 output document(s)
        Executing pipeline "ValidateLinks" (14/15) with 1 child module(s)
            Executed pipeline "ValidateLinks" (14/15) in 3 ms resulting in 0 output document(s)
        Executing pipeline "Sitemap" (15/15) with 3 child module(s)
            Executed pipeline "Sitemap" (15/15) in 23 ms resulting in 1 output document(s)
        Executed 15/15 pipelines in 5343 ms

    ========================================
    CloneMasterBranch
    ========================================
    Cloning master branch into temp directory

    ========================================
    EmptyMasterBranch
    ========================================
    Emptying master branch

    ========================================
    CopyToMasterBranch
    ========================================
    Copying files to master branch

    ========================================
    CommitMasterBranch
    ========================================
    Performing Git commit on master branch

    ========================================
    PushMasterBranch
    ========================================
    Pushing master branch to origin

    ========================================
    Deploy
    ========================================

    Task                          Duration
    --------------------------------------------------
    Build                         00:00:06.4142491
    CloneMasterBranch             00:00:04.9115101
    EmptyMasterBranch             00:00:00.0460689
    CopyToMasterBranch            00:00:00.7293069
    CommitMasterBranch            00:00:02.1444625
    PushMasterBranch              00:00:04.0557769
    --------------------------------------------------
    Total:                        00:00:18.3127486


## Azure Pipelines
Surf [azure-pipelines](https://github.com/marketplace/azure-pipelines) and choose free plan.
`azure-pipelines.yml` is the pipeline file that triggers the deployment. Additionally, I upgrade it to use the latest VM,

    pool:
      vmImage: 'windows-2019'

However, this one can be tricky to find the right `cake-build` package in azure. For convenience, here's the one that works: [cake build pacakage in marketplacevisualstudio ](https://marketplace.visualstudio.com/acquisition?itemName=cake-build.cake)

Without this package, pipeline build doesn't succeed and encounters following error,

    A task is missing. The pipeline references a task called 'cake-build.cake.cake-build-task.Cake'. This usually indicates the task isn't installed, and you may be able to install it from the Marketplace: https://marketplace.visualstudio.com. (Task version 0, job 'Job', step ''.)

The call flow looks like following,

    Deploy
    -> PushMasterBranch
    -> CommitMasterBranch
      -> CopyToMasterBranch
      -> Build, EmptyMasterBranch

To make wyam work, the first one in references section helped a lot.

## Setting up a custom domain
References for setting up custom domain:
- [Parker Moore - Custom domains on GitHub Pages gain support for HTTPS](https://github.blog/2018-05-01-github-pages-custom-domains-https)
- [Host on GitHub - Use a Custom Domain](https://gohugo.io/hosting-and-deployment/hosting-on-github/#use-a-custom-domain)
- [github - Managing a custom domain for your GitHub Pages site](https://help.github.com/en/github/working-with-github-pages/managing-a-custom-domain-for-your-github-pages-site)

I my case I set it up on subdomain, `blog.fftsysx.com`

Additionally, I enable DNSSEC for my primary domain which is not required in your case:

- [godaddy - dnssec add record](https://ph.godaddy.com/help/add-a-ds-record-23865)
- [numeric representation for digest type](https://www.iana.org/assignments/ds-rr-types/ds-rr-types.xhtml)
- [trick to enabling https](https://blog.cloudflare.com/secure-and-fast-github-pages-with-cloudflare/) (even though github not allowing)

### Generating CNAME file for the blog
I find the section in deployment where files are being generated,

    Task("CopyToMasterBranch")
        .IsDependentOn("Build")
        .IsDependentOn("EmptyMasterBranch")
        .Does(() => {
            var sourcePath = "./output";

            Information("Copying files to master branch");

            // Now Create all of the directories
            foreach (string dirPath in System.IO.Directory.GetDirectories(sourcePath, "*", SearchOption.AllDirectories))
            {
                System.IO.Directory.CreateDirectory(dirPath.Replace(sourcePath, tempDir));
            }

            // Copy all the files & Replaces any files with the same name
            foreach (string newPath in System.IO.Directory.GetFiles(sourcePath, "*.*", SearchOption.AllDirectories))
                System.IO.File.Copy(newPath, newPath.Replace(sourcePath, tempDir), true);
        });

To explain the code segment above,
 - sourcePath = the current output dir from wyam
 - tempDir = windows temp dir
 - `newPath.Replace(sourcePath, tempDir)` = tempDir + relative Path of the file

File.Copy ref,

    public static void Copy (string sourceFileName, string destFileName);

After last copy in the code semgent above we add this to add CNAME,

We can write the CName in following way,

    string CNameContent = "blog.domain.com";
    System.IO.File.WriteAllText(System.IO.Path.Combine(tempDir, CNameFileName), CNameContent);

## Pipeline Variables
Probably these variables reset after we do deploy as below,

    .\build -Target Deploy

[MS Docs - Pipeline Variables](https://docs.microsoft.com/en-us/azure/devops/pipelines/process/variables)

To edit pipeline variables on azure cloud.
1. Naviagate to your pipeline URL.
2. Click Edit and noow you can access variables

#### Conclusion
wyam uses markdig flavor of Markdown of by default: [markdig math](https://github.com/lunet-io/markdig/blob/master/src/Markdig.Tests/Specs/MathSpecs.md)

Readily, I can think of following cons,
- mathematics/latex notation is not working by default


### References
Usueful refs are below,

- primary ref: [Johan Vergeer - Wyam, Azure DevOps and GitHub Pages](https://johanvergeer.github.io/posts/wyam-azure-devops)
- [Jonathan Higgs - Create GitHub Pages Blog with Wyam](https://jonathanhiggs.github.io/posts/create-github-pages-with-wyam)
- [wyam official documentation](https://wyam.io/docs)
- [daveaglick - Converting My Blog to Wyam](https://daveaglick.com/posts/converting-my-blog-to-wyam)
- [MS Azure Cloud - availabe VMs](https://docs.microsoft.com/en-us/azure/devops/pipelines/agents/hosted?view=azure-devops)
- [github daveaglick](https://github.com/daveaglick/daveaglick)
