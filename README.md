<meta name="google-site-verification" content="-bIC5ENYcQn5ByB0RNNkYhpV3ctuqX1kRcf-oulF8EU" />

# OpenResxLocalization
The code implements a localization in ASP.NET Core 3+ by non-compiled RESX file. 

## How to integrate the OpenResxLocalization in your project
1. Download a content of folder src and include the files into your project (change namespace ...)

2. Initialization in `Startup.cs` in methode `ConfigureServices`
   ```
   services.AddOpenResxLocalization(options => options.ResourcesPath = "Resources");
   ```
   
3. Create a folder with name 'Resources' in base project folder and in it create folders by typical localization steps (You have to use a path naming - [Globalization and localization in ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/localization?view=aspnetcore-3.1))<br />
For example:<br />
![resource folder structure](https://github.com/ladinek82/OpenResxLocalization/raw/master/images/structure.png)

4. Set properties of resx file
   - Build Action on Content
   - Copy to Output Directory on Copy always of on Copy if newer<br />
   ![resx properties](https://github.com/ladinek82/OpenResxLocalization/raw/master/images/resxProperties.png)
   
5. It is all. 
   
## My expirience with the OpenResxLocalization
- It works with all type of localizations: in Controllers, in Views and in DataAnnotations.
- It is possible to use it in desktop applications, web applications (ASP.NET) and libraries.
- I don't have an expirience with hosting the code on Linux. 

<br/><br/>
*I have look for on the Internet libraries, nugets and articles on blogs where somebody solves this issue but I can not find anything. I have to create my own solution and I hope it helps you. If you have a tip for improvment, you will write it. I will pleasere.*
<br />

## Tips for you
- OpenResxResourceManager uses XDocument for finding a localized value, because I have found an article where somebody write. The XDocument is faster than XmlDocument. There is both impelementation (XDocument is used and XmlDocument is commented)
- OpenResxResourceManager has only one constructor but other constructors are commented.



