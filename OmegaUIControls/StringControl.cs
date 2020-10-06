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
        protected Color BorderColor = Color.FromRgb(0, 0, 10);
        private string LabelContent;

        public delegate bool ValidateMessageDelegate(String message);


        private Style GetLabelStyle()
        {
            Style style = new Style(typeof(Label));
            style.Setters.Add(new Setter(FrameworkElement.HeightProperty, 30d));
            style.Setters.Add(new Setter(FrameworkElement.WidthProperty, 100d));
            return style;
        }

        private Style GetTextBoxStyle()
        {
            Style style = new Style(typeof(TextBox));
            style.Setters.Add(new Setter(FrameworkElement.HeightProperty, 30d));
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
            TextBox.LostFocus += this.HandleTextChanged;

            var panel = new StackPanel() { Orientation = Orientation.Horizontal };
            panel.Margin = new Thickness(10);
            panel.Children.Add(Label);
            panel.Children.Add(TextBox);
            UIElement = panel;
        }

        private void HandleTextChanged(object sender, RoutedEventArgs e)
        {
            Validate(TextBox.Text);
        }

        /// <summary>
        /// Set the parameters of the UIElement
        /// </summary>
        /// <param name="valuePairs"></param>
        public override void SetInput(IUIInput input) // IUIInput
        {
            Input = input;
            LabelContent = input.GetInput("Label", "String").ToString();
            if (input.HasParameter("Value"))
               Validate(input.GetInput("Value"));
        }

        /// <summary>
        /// One of the sub control of String control
        /// </summary>
        public Label Label { get; set; }

        /// <summary>
        /// One of the sub control of String control
        /// </summary>
        public TextBox TextBox { get; set; }


        public virtual void SetValue(object val)
        {
            Value = val.ToString();
        }

        public void Validate(object val)
        {
            if (IsValid(val))
            {
                BorderColor = Color.FromRgb(255, 255, 0);
                SetValue(val);
            }
            else
            {
                ShowValidationError();
                BorderColor = Color.FromRgb(255, 0, 0);
                CreateUIElement();
            }

        }

        /// <summary>
        /// The entered text in the textbox will be validated against the 
        /// null condition checks
        /// Returns true if the validation is true else false
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public override bool IsValid(object val)
        {
            return val!=null;
        }

        public override void ShowValidationError()
        {
            MessageBox.Show(MessageInfo.STRING_ERROR_MESSAGE);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="validateMessage"></param>
        public bool ValidateMessage(ValidateMessageDelegate validateMessage)
        {
            return validateMessage(this.Value.ToString());
        }
    }
}
