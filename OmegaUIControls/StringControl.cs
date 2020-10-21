using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Agilent.OpenLab.Spring.Omega
{
    /// <summary>
    /// This class is responsible for creating the string control which is a panel of having 
    /// a label and textbox aligned horizontally
    /// </summary>
    public class StringControl : AbstractUIControl
    {
        protected Brush BorderBrush;
        protected string LabelContent;

        //Sub controls of String control
        protected Label Label;
        protected TextBox TextBox;

        public delegate bool ValidateMessageDelegate(String message);

        public override object Value {
            get
            {
                return TextBox.Text;
            }
            set
            {
                TextBox.Text = value.ToString();
            }
        }

        /// <summary>
        /// Creates the string UI Element
        /// Initializes the layout and updates the UIElement
        /// </summary>
        public override void CreateUIElement()
        {
            Label = new Label();
            Label.Content = LabelContent;

            TextBox = new TextBox();
            TextBox.LostFocus += this.HandleTextChanged;

            var panel = new StackPanel() { Orientation = Orientation.Horizontal };
            panel.Children.Add(Label);
            panel.Children.Add(TextBox);

            SetResources(panel);
            BorderBrush = TextBox.BorderBrush;
            Validate(Input.GetInput("Value"));
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
            base.SetInput(input);
            LabelContent = input.GetInput("Label", "Input").ToString();
        }

        public void Validate(object val)
        {
            if (IsValid(val))
            {
                TextBox.BorderBrush = BorderBrush;
                Value = val;
            }
            else
            {
                TextBox.BorderBrush = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                ShowValidationError();
            }
        }

        /// <summary>
        /// The entered text in the textbox will be validated against the null condition checks
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
