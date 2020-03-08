using System;
using System.Linq;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Collections.Concurrent;

namespace OpenResxLocalization
{
    public class OpenResxResourceManager : System.Resources.ResourceManager
    {
        private readonly string _resourcesRelativePath;
        private readonly ConcurrentDictionary<string, XDocument> _cacheResx;

        //public OpenResxResourceManager(Type resourceSource)
        //    : base(resourceSource)
        //{
        //}

        //public OpenResxResourceManager(string baseName, Assembly assembly)
        //    : base(baseName, assembly)
        //{
        //}

        public OpenResxResourceManager(string baseName, Assembly assembly, string relativeResourcePath)
            : base(baseName, assembly)
        {
            _resourcesRelativePath = relativeResourcePath;
            _cacheResx = new ConcurrentDictionary<string, XDocument>();
        }

        //public OpenResxResourceManager(string baseName, Assembly assembly, Type usingResourceSet)
        //    : base(baseName, assembly, usingResourceSet)
        //{
        //}

        public override string GetString(string name)
        {
            return GetString(name, System.Threading.Thread.CurrentThread.CurrentCulture);
        }

        public override string GetString(string name, CultureInfo culture)
        {
            if (name == null) throw new ArgumentNullException();

            try
            {
                var resxFile = GetResxFile(culture.Name);
                if (!File.Exists(resxFile))
                    return null;

                //return FindValueByXmlDocument(resxFile, name);
                return FindValueByXDocument(resxFile, name);

            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Processing of resx file failed", ex);
            }
        }

        private string GetResxFile(string cultureCode)
        {
            var folder = _resourcesRelativePath ?? "";
            var objPath = this.BaseName.Split('.').ToList();
            var resIndex = objPath.IndexOf(_resourcesRelativePath);

            for(int i = resIndex+1; i< objPath.Count; i++)
            {
                folder += Path.DirectorySeparatorChar+objPath[i];
            }

            return  $"{folder}.{cultureCode}.resx";
        }

        //private XmlDocument xmlResx;
        //private string FindValueByXmlDocument(string fileName, string name)
        //{
        //    if (xmlResx == null)
        //    {
        //        var xmlResx = new XmlDocument();
        //        xmlResx.Load(fileName);
        //    }

        //    var nodes = xmlResx.SelectNodes($"/*[local-name()='root']/*[local-name()='data']");

        //    foreach (XmlNode n in nodes)
        //    {
        //        var attrName = n.Attributes["name"]?.Value;
        //        if (attrName == name)
        //        {
        //            return n.SelectSingleNode("./*[local-name() = 'value']")?.InnerText;
        //        }
        //    }

        //    return null;
        //}

        private string FindValueByXDocument(string fileName, string name)
        {
            XDocument xResx = null;
            if (_cacheResx.ContainsKey(fileName))
            {
                _cacheResx.TryGetValue(fileName, out xResx);
            }
            else
            {
                xResx = XDocument.Load(fileName);
                _cacheResx.TryAdd(fileName, xResx);
            }            

            var xElement = xResx.Root.Descendants("data").Where(c => (string)c.Attribute("name") == name).FirstOrDefault();

            return xElement?.Descendants("value")?.FirstOrDefault()?.Value;
        }

    }
}
