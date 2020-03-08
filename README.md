# OpenResxLocalization
The code implements a localization in ASP.NET Core 3+ by non-compiled RESX file. 

## How to integrate the OpenResxLocalizer in your project
1. Download a content of folder src and include the files into your project (change namespace ...)
2. Initialization in `Startup.cs` in methode `ConfigureServices`
   ```
   services.AddOpenResxLocalization(options => options.ResourcesPath = "Resources");
   ```
3. 
