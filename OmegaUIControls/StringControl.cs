using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Agilent.MHDA.Omega;

namespace Agilent.MHDA.Omega
{
    /// <summary>
    /// This class is responsible for creating the string control which is a panel of having 
    /// a label and textbox aligned horizontally
    /// </summary>
    public class StringControl : AbstractUIControl
    {
        //Error message to be shown when input is invaid or out of range
        protected string errorMsg;

        //These fields are for int and float control
        protected float? min;
        protected float? max;

        //Sub controls of String control
        protected Label Label;
        protected TextBox TextBox;

        //Tool tip to show errorMsg
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

            //var panel = new StackPanel() { Orientation = Orientation.Horizontal };
            //panel.Children.Add(Label);
            //panel.Children.Add(TextBox);

            var panel = new LayoutPanel(1, 2);
            panel.Add(Label, 1);
            panel.Add(TextBox, 1);
            panel.ChangeDimension(40, 300);

            ErrorToolTip = new ToolTip();
            ErrorToolTip.Style = UIConstants.GetErrorToolTipStyle();

            UtilityMethods.SetPanelResources(panel);

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
                TextBox.Style = UtilityMethods.GetStyle("validTextInput");
                TextBox.ToolTip = null;
                Value = val;
            }
            else
            {
                TextBox.Style = UtilityMethods.GetStyle("invalidTextInput");
                if (!IsValid(val))
                    ShowValidationError();
                else if(!IsWithinRange(val))
                    ShowOutOfRangeError();
                TextBox.ToolTip = ErrorToolTip;
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
                errorMsg = string.Format(MessageInfo.OUT_OF_RANGE_ERROR_MESSAGE, min, max);
            else if (min == null)
                errorMsg = string.Format(MessageInfo.LESS_THAN_ERROR_MESSAGE, max);
            else if (max == null)
                errorMsg = string.Format(MessageInfo.GREATER_THAN_ERROR_MESSAGE, min);

            ErrorToolTip.Content = errorMsg;
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
