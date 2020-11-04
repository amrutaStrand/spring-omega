using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Markup;
using System.Xml;

namespace OmegaUIControls.OmegaUIUtils
{
    public class UtilityMethods
    {
        public static void SetResources(FrameworkElement frameworkElement)
        {
            string path = string.Format("{0}.{1}.{2}", "OmegaUIControls", "OmegaUIUtils", "lucid.xaml");

            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(path))
            {
                XmlReader xmlReader = XmlReader.Create(stream);
                frameworkElement.Resources = XamlReader.Load(xmlReader) as ResourceDictionary;
            }
        }
    }
}
