using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

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

        protected Color BorderColor = Color.FromRgb(0, 0, 10);
        private string LabelContent = "";

        public delegate bool ValidateMessageDelegate(String message);

        private ICommand textChanged;
        /// <summary>
        /// adds new files to the table.
        /// </summary>
        public ICommand TextChanged
        {
            get
            {
                return new CommandHandler(() => TextChangedEventHandler(), ()=>true);
            }
        }

        private void TextChangedEventHandler()
        {
            this.Value = TextBox.Text;
        }

        private Style GetLabelStyle()
        {
            Style style = new Style(typeof(Label));
            style.Setters.Add(new Setter(FrameworkElement.HeightProperty, 50d));
            style.Setters.Add(new Setter(FrameworkElement.WidthProperty, 100d));
            return style;
        }

        private Style GetTextBoxStyle()
        {
            Style style = new Style(typeof(TextBox));
            style.Setters.Add(new Setter(FrameworkElement.HeightProperty, 50d));
            style.Setters.Add(new Setter(FrameworkElement.WidthProperty, 100d));
            var brush = new SolidColorBrush(BorderColor);
            style.Setters.Add(new Setter(Control.BorderBrushProperty, brush));
            return style;
        }

        /// <summary>
        /// Creates the string UI Element
        /// Initializes the layout and updates the UIElement
        /// </summary>
        public override void CreateUIElement()
        {
            Label = new Label();
            Label.Content = LabelContent;
            Label.Style = GetLabelStyle();

            TextBox = new TextBox() ;
            TextBox.Style = GetTextBoxStyle();

            var panel = new StackPanel() { Orientation = Orientation.Horizontal };
            panel.Children.Add(Label);
            panel.Children.Add(TextBox);
            UIElement = panel;
        }

        /// <summary>
        /// Set the parameters of the UIElement
        /// </summary>
        /// <param name="valuePairs"></param>
        public override void SetInput(IUIInput input) // IUIInput
        {
            LabelContent = input.GetInput("Label").ToString();
            Label.Content = LabelContent;
            Value = input.GetInput("Value").ToString();
        }

        /// <summary>
        /// One of the sub control of String control
        /// </summary>
        public Label Label { get; set; }

        /// <summary>
        /// One of the sub control of String control
        /// </summary>
        public TextBox TextBox { get; set; }

        static bool PrintTitle(String val)
        {
            return val.Length < 5;
        }

        /// <summary>
        /// A property to get the text in the textbox
        /// </summary>
        public override object Value 
        {
            get => this.TextBox.Text;
            set
            {
                //TextBox.Text = value.ToString();
                //if (ValidateMessage(PrintTitle))
                if (Validate(value))
                    TextBox.Text = value.ToString();
                else
                {
                    MessageBox.Show(MessageInfo.STRING_ERROR_MESSAGE); // Red star
                    BorderColor = Color.FromRgb(255, 0, 0);
                }
            }
        }

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="processBook"></param>
        public bool ValidateMessage(ValidateMessageDelegate validateMessage)
        {
            return validateMessage(this.Value.ToString());
        }
    }
}
