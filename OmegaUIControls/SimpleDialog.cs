using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Xml;
using System.Windows.Media;

namespace Agilent.OpenLab.Spring.Omega
{
    /// <summary>
    /// SimpleDialog is a dialog box encapsulating the controls in the <see cref="DialogContent"/> property 
    /// and the "OK", "Cancel" and "Help" buttons depending on the input properties which can be:
    /// <list type="bullet" >
    /// <item>hasOK (bool) : if this is true, then "OK" button is added to the dialog box</item>
    /// <item>okLabel (String) : content of the "OK" button</item>
    /// <item>onOk (Action event) : event to be invoked on clicking "OK" button</item>
    /// <item>hasCancel (bool) : if this is true, then "Cancel" button is added to the dialog box</item>
    /// <item>cancelLabel (String) : content of the "Cancel" button</item>
    /// <item>onCancel (Action event) : event to be invoked on clicking "Cancel" button</item>
    /// <item>hasHelp (bool) : if this is true, then "Help" button is added to the dialog box</item>
    /// <item>helpLabel (String) : content of the "Help" button</item>
    ///</list>
    /// </summary>
    public class SimpleDialog : Window
    {
        protected bool hasOK;
        protected string okLabel;
        protected bool statusOk = false;
        private Button okButton;

        //This event is invoked when the user clicks the "OK" button.
        public event Action onOk;

        protected bool hasCancel;
        protected string cancelLabel;
        private Button cancelButton;

        //This event is invoked when the user clicks the "Cancel" button.
        public event Action onCancel;

        protected bool hasHelp;
        protected string helpLabel;
        protected string helpId;
        protected Button helpButton;

        protected Panel dialogContent;

        /// <summary>
        /// Content of the dialog box.
        /// </summary>
        public Panel DialogContent
        {
            get
            {
                return dialogContent;
            }
            set
            {
                DockPanel panel = this.Content as DockPanel;
                panel.Children.Remove(dialogContent);
                panel.Children.Add(value);
                DockPanel.SetDock(value, Dock.Top);
            }
        }

        /// <summary>
        /// Creates dialog with <paramref name="parent"/> as its owner.
        /// </summary>
        /// <param name="parent"></param>
        public SimpleDialog(Window parent) : this(parent, new Dictionary<string, object>())
        {

        }

        /// <summary>
        /// Creates dialog with <paramref name="parent"/> as its owner and <paramref name="title"/> as its title.
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="title"></param>
        public SimpleDialog(Window parent, string title) : this(parent, new Dictionary<string, object> { { "title", title } })  
        {

        }

        /// <summary>
        /// Creates dialog using the <paramref name="properties"/> dictionary with <paramref name="parent"/> as its owner.
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="properties"></param>
        public SimpleDialog(Window parent, IDictionary<string, object> properties) : base()
        {
            this.Owner = parent;
            this.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            this.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#e2eef5"));
            
            if (properties.ContainsKey("title"))
                this.Title = (string)properties["title"];

            hasOK = properties.ContainsKey("hasOK") ? (bool)properties["hasOK"] : true;
            okLabel = hasOK && properties.ContainsKey("okLabel") ? (string)properties["okLabel"] : "OK";

            hasCancel = properties.ContainsKey("hasCancel") ? (bool)properties["hasCancel"] : true;
            cancelLabel = hasCancel && properties.ContainsKey("cancelLabel") ? (string)properties["cancelLabel"] : "Cancel";

            hasHelp = properties.ContainsKey("hasHelp") ? (bool)properties["hasHelp"] : false;
            helpLabel = hasHelp && properties.ContainsKey("helpLabel") ? (string)properties["helpLabel"] : "Help";
            helpId = hasHelp && properties.ContainsKey("helpId") ? (string)properties["helpId"] : null;
            
            //StackPanel panel = new StackPanel();

            DockPanel panel = new DockPanel();
            panel.LastChildFill = false;

            dialogContent = new StackPanel();
            panel.Children.Add(dialogContent);
            DockPanel.SetDock(dialogContent, Dock.Top);

            if (hasOK || hasCancel || hasHelp)
            {
                Panel controlPanel = CreateControlPanel();
                panel.Children.Add(controlPanel);
                DockPanel.SetDock(controlPanel, Dock.Bottom);
            }

            this.Content = panel;
        }

        private void SetResources(FrameworkElement frameworkElement)
        {
            string path = string.Format("{0}.{1}.{2}", "OmegaUIControls", "OmegaUIUtils", "lucid.xaml");

            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(path))
            {
                XmlReader xmlReader = XmlReader.Create(stream);
                frameworkElement.Resources = XamlReader.Load(xmlReader) as ResourceDictionary;
            }
        }

        /// <summary>
        /// Creates the "OK", "Cancel" and "Help" buttons depending on the input properties.
        /// </summary>
        /// <returns>Panel containing these buttons.</returns>
        private Panel CreateControlPanel()
        {
            DockPanel panel = new DockPanel();
            SetResources(panel);
            panel.LastChildFill = false;

            //panel for ok and cancel button
            StackPanel rightPanel = new StackPanel() { Orientation = Orientation.Horizontal };
            //panel for help button
            StackPanel leftPanel = new StackPanel() { Orientation = Orientation.Horizontal };
            
            if (hasOK)
            {
                okButton = new Button();
                okButton.Width = 60;
                okButton.Margin = new Thickness(10);
                okButton.Content = okLabel;
                okButton.IsDefault = true;
                okButton.Click += OkButton_Click;

                rightPanel.Children.Add(okButton);
                panel.Children.Add(rightPanel);
                DockPanel.SetDock(rightPanel, Dock.Right);
            }

            if (hasCancel)
            {
                cancelButton = new Button();
                cancelButton.Width = 60;
                cancelButton.Margin = new Thickness(10);
                cancelButton.Content = cancelLabel;
                cancelButton.IsCancel = true;
                cancelButton.Click += CancelButton_Click;
                rightPanel.Children.Add(cancelButton);
            }

            if (hasHelp)
            {
                helpButton = new Button();
                helpButton.Width = 60;
                helpButton.Margin = new Thickness(10);
                helpButton.Content = helpLabel;
                helpButton.Click += HelpButton_Click;

                leftPanel.Children.Add(helpButton);
                panel.Children.Add(leftPanel);
                DockPanel.SetDock(leftPanel, Dock.Left);
            }

            this.Closing += SimpleDialog_Closing;

            //Set panel border??

            return panel;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            statusOk = true;

            if(onOk!=null)
                onOk.Invoke();

            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            statusOk = false;

            if (onCancel != null)
                onCancel.Invoke();

            this.Close();
        }

        private void HelpButton_Click(object sender, RoutedEventArgs e)
        {
            //TO-DO: uses com.strandgenomics.cube.framework.HelpManager
        }

        private void SimpleDialog_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (onCancel != null)
                onCancel.Invoke();
        }

        protected void ShowError(string title, string message)
        {
            MessageBox.Show(message, title, MessageBoxButton.OKCancel, MessageBoxImage.Error);
        }

        protected void ShowError(string message) => ShowError("Error", message);

        /// <summary>
        /// Enables or disables the "OK" button.
        /// </summary>
        /// <param name="enable"></param>
        /// <returns>true on success</returns>
        public bool OkButtonIsEnabled(bool enable)
        {
            if(hasOK && okButton != null && okButton.IsEnabled!=enable)
            {
                okButton.IsEnabled = enable;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Enables or disables the "Cancel" button.
        /// </summary>
        /// <param name="enable"></param>
        /// <returns>true on success</returns>
        public bool CancelButtonIsEnabled(bool enable)
        {
            if (hasCancel && cancelButton != null && cancelButton.IsEnabled != enable)
            {
                cancelButton.IsEnabled = enable;
                return true;
            }
            return false;
        }

        public bool IsOk() => statusOk;
    }
}
