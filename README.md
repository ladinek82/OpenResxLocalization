# OpenResxLocalization
The code implements a localization in ASP.NET Core 3+ by non-compiled RESX file. 

## How to integrate the OpenResxLocalizer in your project
1. Download a content of folder src and include the files into your project (change namespace ...)
2. Initialization in `Startup.cs` in methode `ConfigureServices`
   ```
   services.AddOpenResxLocalization(options => options.ResourcesPath = "Resources");
   ```
3. Create a folder with name 'Resources' in base project folder and in it create folders by typical localization steps (You have to use a path naming - [Globalization and localization in ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/localization?view=aspnetcore-3.1))<br />
For example:<br />
![resource folder structure](https://github.com/ladinek82/OpenResxLocalization/raw/master/images/structure.png)

