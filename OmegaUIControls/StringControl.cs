using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using OmegaUIControls.OmegaUIUtils;

namespace Agilent.OpenLab.Spring.Omega
{
    /// <summary>
    /// This class is responsible for creating the string control which is a panel of having 
    /// a label and textbox aligned horizontally
    /// </summary>
    public class StringControl : AbstractUIControl
    {
        protected Brush borderBrush;
        protected Thickness borderThickness;
        protected Brush textBackground;
        protected double textBackgroundOpacity;
        protected string errorMsg;

        private SolidColorBrush errorBrush = new SolidColorBrush();

        //These fields are for int and float control
        protected float? min;
        protected float? max;

        //Sub controls of String control
        protected Label Label;
        protected TextBox TextBox;
        protected ToolTip ErrorToolTip;

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
            Label.Content = Input.GetInput("Label", "Input").ToString();

            TextBox = new TextBox();
            TextBox.LostFocus += this.HandleTextChanged;

            var panel = new StackPanel() { Orientation = Orientation.Horizontal };
            panel.Children.Add(Label);
            panel.Children.Add(TextBox);

            //Tool tip styles
            ErrorToolTip = new ToolTip();
            ErrorToolTip.Background = new SolidColorBrush(UIConstants.ColorError);
            ErrorToolTip.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ffffff"));
            ErrorToolTip.FontFamily = new FontFamily("Calibri");
            ErrorToolTip.FontWeight = FontWeights.Regular;
            ErrorToolTip.FontSize = 14;
            //ErrorToolTip.Height = 22;

            SetResources(panel);
            borderBrush = TextBox.BorderBrush;
            borderThickness = TextBox.BorderThickness;
            textBackground = TextBox.Background;
            errorBrush.Color = UIConstants.ColorError;
            errorBrush.Opacity = UIConstants.OpacityError;

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
            if(Input.HasParameter("min"))
                min = float.Parse(input.GetInput("min").ToString());
            if (Input.HasParameter("max"))
                max = float.Parse(input.GetInput("max").ToString());
        }

        public void Validate(object val)
        {
            if (IsValid(val) && IsWithinRange(val))
            {
                TextBox.BorderBrush = borderBrush;
                TextBox.BorderThickness = borderThickness;
                TextBox.Background = textBackground;
                TextBox.ToolTip = null;
                Value = val;
            }
            else
            {
                TextBox.BorderBrush = new SolidColorBrush(UIConstants.ColorError);
                TextBox.BorderThickness = UIConstants.BorderThicknessError;
                TextBox.Background = errorBrush;
                if (!IsValid(val))
                    ShowValidationError();
                else if(!IsWithinRange(val))
                    ShowOutOfRangeError();
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
            errorMsg = MessageInfo.STRING_ERROR_MESSAGE;
            ErrorToolTip.Content = errorMsg;
            TextBox.ToolTip = ErrorToolTip;
        }
        protected virtual bool IsWithinRange(object val)
        {
            if (min == null && max == null)
                return true;

            float tmp = Convert.ToSingle(val);

            if (min == null && tmp <= max)
                return true;
            else if (max == null && tmp >= min)
                return true;
            else if (tmp >= min && tmp <= max)
                return true;
            else
                return false;
        }

        protected virtual void ShowOutOfRangeError()
        {
            if (min != null && max != null)
                errorMsg = string.Format("Input value must be between {0} and {1}", min, max);
            else if (min == null)
                errorMsg = string.Format("Input value must be less than {0}", max);
            else if (max == null)
                errorMsg = string.Format("Input value must be greater than {0}", min);

            ErrorToolTip.Content = errorMsg;
            TextBox.ToolTip = ErrorToolTip;
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
