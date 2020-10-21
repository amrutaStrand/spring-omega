using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Markup;
using System.Xml;

namespace Agilent.OpenLab.Spring.Omega
{
    /// <summary>
    /// Abstract implementation of <see cref="IUIControl"/> interface.
    /// </summary>
    public abstract class AbstractUIControl : IUIControl
    {
        /// <summary>
        /// Constructor to set parameters to default values.
        /// </summary>
        public AbstractUIControl() => SetInput(new UIInput());

        public virtual object Value { get; set; }

        public IUIInput Input { get; set; }

        public string Id { get; set; }

        /// <summary>
        /// The Property for <see cref="UIElement"/> stored in the control.
        /// </summary>
        protected UIElement UIElement { get; set; }

        public void SetEnabled(bool isEnabled)
        {
            UIElement.IsEnabled = isEnabled;
        }

        public virtual void SetInput(IUIInput input)
        {
            Input = input;
            Id = (string)Input.GetInput("id", null);
        } 

        public abstract void CreateUIElement();

        /// <summary>
        /// Gets the UIElement if it is already created otherwise create one
        /// </summary>
        /// <returns></returns>
        public virtual UIElement GetUIElement()
        {
            if (UIElement == null)
                CreateUIElement();
            return UIElement;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual bool IsValid(object value) // validation messages
        {
            return true;
        }

        public virtual void ShowValidationError()
        {

        }

        /// <summary>
        /// Sets the resources of <paramref name="frameworkElement"/> using resource disctinary.
        /// To be called by child classes to set the resources of encapsulating <see cref="UIElement"/>.
        /// </summary>
        /// <param name="frameworkElement"></param>
        protected void SetResources(FrameworkElement frameworkElement)
        {
            frameworkElement.HorizontalAlignment = HorizontalAlignment.Left;

            string path = string.Format("{0}.{1}.{2}", "OmegaUIControls", "OmegaUIUtils", "lucid.xaml");

            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(path))
            {
                XmlReader xmlReader = XmlReader.Create(stream);
                frameworkElement.Resources = XamlReader.Load(xmlReader) as ResourceDictionary;
            }
        }

    }
}
