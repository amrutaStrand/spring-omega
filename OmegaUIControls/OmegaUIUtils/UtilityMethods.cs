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

        /// <summary>
        /// Sets the resources of <paramref name="frameworkElement"/> using resource disctinary.
        /// To be called by child classes to set the resources of encapsulating <see cref="UIElement"/>.
        /// </summary>
        /// <param name="frameworkElement"></param>
        public static void SetPanelResources(FrameworkElement frameworkElement)
        {
            //Set common properties of the controls
            frameworkElement.Margin = UIConstants.ControlMargin;
            frameworkElement.HorizontalAlignment = UIConstants.ControlHorizontalAlignment;
            frameworkElement.MaxWidth = UIConstants.ControlMaxWidth;
            frameworkElement.MaxHeight = UIConstants.ControlMaxHeight;
            //frameworkElement.MinWidth = UIConstants.ControlMinWidth;
            //frameworkElement.MinHeight = UIConstants.ControlMinHeight;

            string path = string.Format("{0}.{1}.{2}", "OmegaUIControls", "OmegaUIUtils", "lucid.xaml");

            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(path))
            {
                XmlReader xmlReader = XmlReader.Create(stream);
                frameworkElement.Resources = XamlReader.Load(xmlReader) as ResourceDictionary;
            }
        }
    }
}
