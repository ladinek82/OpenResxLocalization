using System;
using System.Collections.Concurrent;
using System.IO;
using System.Reflection;
using System.Resources;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace OpenResxLocalization
{

    public class OpenResxResourceManagerStringLocalizerFactory : ResourceManagerStringLocalizerFactory
    {
        private readonly IResourceNamesCache _resourceNamesCache = new ResourceNamesCache();
        private readonly ILoggerFactory _loggerFactory;
        private readonly string _resourcesRelativePath;



        public OpenResxResourceManagerStringLocalizerFactory(IOptions<LocalizationOptions> localizationOptions, ILoggerFactory loggerFactory) 
            : base(localizationOptions, loggerFactory)
        {
            _loggerFactory = loggerFactory;
            _resourcesRelativePath = localizationOptions.Value.ResourcesPath ?? string.Empty;
        }

        /// <summary>Creates a <see cref="ResourceManagerStringLocalizer"/> for the given input.</summary>
        /// <param name="assembly">The assembly to create a <see cref="ResourceManagerStringLocalizer"/> for.</param>
        /// <param name="baseName">The base name of the resource to search for.</param>
        /// <returns>A <see cref="ResourceManagerStringLocalizer"/> for the given <paramref name="assembly"/> and <paramref name="baseName"/>.</returns>
        /// <remarks>This method is virtual for testing purposes only.</remarks>
        protected override ResourceManagerStringLocalizer CreateResourceManagerStringLocalizer(
            Assembly assembly,
            string baseName)
        {
            

            return new ResourceManagerStringLocalizer(
                new OpenResxResourceManager(baseName, assembly, _resourcesRelativePath),
                assembly,
                baseName,
                _resourceNamesCache,
                _loggerFactory.CreateLogger<ResourceManagerStringLocalizer>());
        }
    }
}
