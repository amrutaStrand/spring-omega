using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Agilent.OpenLab.Spring.Omega
{
    /// <summary>
    /// This class is responsible for creating the string control which is a panel of having a label and textbox aligned horizontally
    /// </summary>
    public class StringControl : AbstractUIControl
    {
        /// <summary>
        /// Public constructor of StringControl
        /// </summary>
        public StringControl()
        {
            CreateUIElement();
        }

        /// <summary>
        /// Creates the string UI Element
        /// Initializes the layout and updates the UIElement
        /// </summary>
        public override void CreateUIElement()
        {
            Label = new Label() { Height=50, Width=100};
            TextBox = new TextBox() {Height=50, Width=100 };
            var panel = new StackPanel() { Orientation = Orientation.Horizontal };
            panel.Children.Add(Label);
            panel.Children.Add(TextBox);
            UIElement = panel;
        }

        /// <summary>
        /// Get's the UIElement if it is already created otherwise create one
        /// </summary>
        /// <returns></returns>
        public override UIElement GetUIElement()
        {
            if (UIElement == null)
                CreateUIElement();
            return UIElement;
        }

        /// <summary>
        /// Set the parameters of the UIElement
        /// </summary>
        /// <param name="valuePairs"></param>
        public override void SetParameters(Dictionary<string, object> valuePairs) // IUIInput
        {
            Label.Content = valuePairs["Label"].ToString();
            Value = valuePairs["Value"].ToString();
        }

        /// <summary>
        /// One of the sub control of String control
        /// </summary>
        public Label Label { get; set; }

        /// <summary>
        /// One of the sub control of String control
        /// </summary>
        public TextBox TextBox { get; set; }

        /// <summary>
        /// A property to get the text in the textbox
        /// </summary>
        public override object Value 
        {
            get => this.TextBox.Text;
            set
            {
                if(Validate(value))
                    TextBox.Text = value.ToString(); 
                else
                    MessageBox.Show(MessageInfo.STRING_ERROR_MESSAGE); // Red star
            }
        }
        /// <summary>
        /// Property to set and get the parameters of the string control
        /// </summary>
        public override Dictionary<string, object> Parameters { get; set; }

        /// <summary>
        /// The entered text in the textbox will be validated against the 
        /// null condition checks
        /// Returns true if the validation is true else false
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public override bool Validate(object val)
        {
            return val!=null;
        }
    }
}
