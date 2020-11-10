using System;
using System.Windows;
using System.Windows.Controls;
using OmegaUIControls.OmegaUIUtils;
using Infragistics.Controls.Editors;
using System.Collections;
using System.Windows.Markup;
using System.Windows.Media;

namespace Agilent.OpenLab.Spring.Omega
{
    /// <summary>
    /// Parameters of this class are<list type="bullet" >
    /// <item>Description (String) : string shown in the label</item>
    /// <item>sliderType (string) : sliderType should be any one "float" or "int"</item>
    /// <item>min (number) : Minimum value of this slider</item>
    /// <item>max (number) : Maximum value of this slider</item>
    /// <item>leftLabel (string) : text that should display on the left side of the slider</item>
    /// <item>rightLabel (string) : text that should display on the right side of the slider</item>
    /// <item>allowTextBox (bool) : if this value is true then it will add a textfield next to slider 
    /// and the value of the textField is the slider current value</item>
    /// <item>width (float) : width of slider relative to textBox and label (1 means equal, 2 means double, etc.)</item>
    /// <item>tickSpace (number) : spacing between the tick marks of the slider</item>
    /// <item>adjustMinMax (bool) : if set to true, the min/max values are upadated when the value inside 
    /// the text box is out of the current range</item>>
    /// <item>showAbsolute (bool) : if false, the slider shows the percentage value</item>
    /// <item>showTickMarks (bool) : parameter to display slider tick marks</item>
    /// </list>
    /// </summary>

    class XamSliderControl : AbstractUIControl
    {
        protected string sliderType;
        protected float min;
        protected float max;
        protected Hashtable labels;
        protected bool allowText;
        protected float width;
        protected int tickSpace;
        protected bool adjustMinMax;
        protected bool localChange;
        protected bool showAbsolute;

        protected Label label;
        protected XamNumericSlider slider;
        protected TextBox textBox;

        #region fields for error handling
        //Tool tip to show errorMsg
        protected ToolTip ErrorToolTip;

        //Error message to be shown when input is invaid or out of range
        protected string errorMsg;

        protected bool lastValid = true;
        #endregion

        private object value;
        public override object Value
        {
            get
            {
                return value;
            }
            set
            {
                float val;
                bool isAllowed = float.TryParse(value.ToString(), out val);
                if (!isAllowed)
                {
                    //Throw some error like "Please enter a number!"
                }
                if (adjustMinMax)
                    UpdateSliderRange(val);
                else
                {
                    if (val < min)
                        val = min;
                    else if (val > max)
                        val = max;
                }
                SetValue(val);
            }
        }

        /// <summary>
        /// Sets the slider position, textBox value and the value field. Unlike the set method of the Value prop,
        /// it calls the SetValue method without checking if the value is between min and max.
        /// </summary>
        /// <param name="explicitValue"></param>
        public void SetValueExplicitly(object explicitValue)
        {
            float val = Convert.ToSingle(explicitValue);
            if (adjustMinMax)
                UpdateSliderRange(val);
            SetValue(val);
        }

        /// <summary>
        /// Sets the value of slider and textBox. This method is called when Value prop is set or slider is changed.
        /// </summary>
        /// <param name="value"></param>
        private void SetValue(float value)
        {
            localChange = true;

            if (Convert.ToSingle(this.value) == value)
            {
                localChange = false;
                return;
            }

            if (sliderType.Equals("int"))
                this.value = (int)value;
            else if (sliderType.Equals("float"))
                this.value = value;

            SliderValue = value;

            if (allowText)
                textBox.Text = (this.value).ToString();

            localChange = false;
        }

        /// <summary>
        /// While setting SliderValue, absolute value is converted into % and in the get method,
        /// % is converted back into absolute value.
        /// </summary>
        protected float SliderValue
        {
            get
            {
                if (showAbsolute)
                {
                    return Convert.ToSingle(slider.Value);
                }
                else
                {
                    float fraction = Convert.ToSingle(slider.Value / 100);
                    return min + fraction * (max - min);
                }
            }
            set
            {
                if (showAbsolute)
                {
                    slider.Value = value;
                }
                else
                {
                    float fraction = (value - min) / (max - min);
                    slider.Value = fraction * (100);
                }
            }
        }

        public override void CreateUIElement()
        {
            var panel = allowText ? new LayoutPanel(1, 3) : new LayoutPanel(1, 2);
            panel.ChangeDimension(60, 800);

            AddLabel(panel);
            AddSlider(panel);
            AddText(panel);

            this.value = min;
            Value = Input.GetInput("Value", Value);

            UtilityMethods.SetPanelResources(panel);
            UIElement = panel;

            ErrorToolTip = new ToolTip();
            ErrorToolTip.Style = UIConstants.GetErrorToolTipStyle();
        }

        /// <summary>
        /// Adds a text box to show value corresponding to the slider. Called by the CreateUIElement method.
        /// </summary>
        /// <param name="panel"></param>
        private void AddText(LayoutPanel panel)
        {
            if (!allowText)
            {
                return;
            }
            textBox = new TextBox();
            textBox.LostFocus += TextBox_LostFocus;
            panel.Add(textBox, 1);
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            float cur;
            float.TryParse(textBox.Text, out cur);
            float last = Convert.ToSingle(value);

            if (!Validate(textBox.Text))
                return;

            if (cur != last)
                Value = cur;
        }

        public bool Validate(object val)
        {
            if (IsValid(val) && IsWithinRange(val))
            {
                if (!lastValid)
                {
                    textBox.Style = UtilityMethods.GetStyle("validTextInput");
                    textBox.ToolTip = null;
                }
                lastValid = true;
                return true;
            }
            else
            {
                textBox.Style = UtilityMethods.GetStyle("invalidTextInput");
                if (!IsValid(val))
                    ShowValidationError();
                else if (!IsWithinRange(val))
                    ShowOutOfRangeError();
                textBox.ToolTip = ErrorToolTip;
                lastValid = false;
                return false;
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
            string num = (string)val;
            bool isFloat = float.TryParse(num, out float floatResult);
            bool isInt = Int32.TryParse(num, out int intResult);

            if (sliderType.Equals("float"))
                return isFloat;
            else if (sliderType.Equals("int"))
                return isInt;
            else
                return false;
        }

        public override void ShowValidationError()
        {
            if (sliderType.Equals("float"))
                errorMsg = MessageInfo.FLOAT_ERROR_MESSAGE;
            else if (sliderType.Equals("int"))
                errorMsg = MessageInfo.INT_ERROR_MESSAGE;
            ErrorToolTip.Content = errorMsg;

        }
        protected virtual bool IsWithinRange(object val)
        {
            if (adjustMinMax)
                return true;

            float tmp = Convert.ToSingle(val);

            if (tmp >= min && tmp <= max)
                return true;
            else
                return false;
        }

        protected virtual void ShowOutOfRangeError()
        {
            ErrorToolTip.Content = string.Format(MessageInfo.OUT_OF_RANGE_ERROR_MESSAGE, min, max);
        }

        /// <summary>
        /// Adds a xam numeric slider. Called by the CreateUIElement method.
        /// </summary>
        /// <param name="panel"></param>
        private void AddSlider(LayoutPanel panel)
        {
            slider = new XamNumericSlider()
            {
                MinValue = showAbsolute? min : 0,
                MaxValue = showAbsolute ? max : 100
            };

            //slider.Width = 200;

            if (labels != null)
            {
                //set custom labels to the slider
            }

            SliderTickMarks tickMarks = new SliderTickMarks
            {
                NumberOfTickMarks = 100 / tickSpace - 1,
                UseFrequency = false,
            };

            SliderTickMarks tickMarksLabels = new SliderTickMarks
            {
                NumberOfTickMarks = 100 / tickSpace - 1,
                UseFrequency = false,
                HorizontalTickMarksTemplate = CreateTemplate(typeof(TextBlock))
            };

            if((bool)Input.GetInput("showTickMarks", true))
            {
                slider.TickMarks.Add(tickMarks);
                slider.TickMarks.Add(tickMarksLabels);
            }

            if (allowText)
                slider.ThumbValueChanged += Slider_ThumbValueChanged;

            panel.Add(slider, width);
        }

        private void Slider_ThumbValueChanged(object sender, ThumbValueChangedEventArgs<double> e)
        {
            if (localChange)
                return;
            
            SetValue(SliderValue);

            if (!lastValid)
            {
                textBox.Style = UtilityMethods.GetStyle("validTextInput");
                textBox.ToolTip = null;
                lastValid = true;
            }
        }

        /// <summary>
        /// Called by the AddSlider method to set the DataTemplate of the slider labels.
        /// </summary>
        /// <param name="viewType"></param>
        /// <returns></returns>
        private DataTemplate CreateTemplate(Type viewType)
        {
            const string xamlTemplate = "<DataTemplate><Border><{0} {1} {2}/></Border></DataTemplate>";
            var xaml = String.Format(xamlTemplate, viewType.Name, "Text =\"{Binding}\"", "Margin = \"0,30,0,0\"");
            var context = new ParserContext();
            context.XmlnsDictionary.Add("", "http://schemas.microsoft.com/winfx/2006/xaml/presentation");
            context.XmlnsDictionary.Add("x", "http://schemas.microsoft.com/winfx/2006/xaml");

            var template = (DataTemplate)XamlReader.Parse(xaml, context);
            return template;
        }

        /// <summary>
        /// Adds a label to show the Description of the slider.
        /// </summary>
        /// <param name="panel"></param>
        private void AddLabel(LayoutPanel panel)
        {
            label = new Label();
            label.Content = Input.GetInput("Description", "Slider Description");

            panel.Add(label, 1);
        }

        public override void SetInput(IUIInput input)
        {
            base.SetInput(input);

            localChange = false;

            sliderType = (string)Input.GetInput("sliderType", "float");

            min = Input.HasParameter("min") ? Convert.ToSingle(Input.GetInput("min")) : 0f;

            max = Input.HasParameter("max") ? Convert.ToSingle(Input.GetInput("max")) : 100f;

            labels = (Hashtable)Input.GetInput("labelTable", new Hashtable());

            if (Input.HasParameter("leftLabel") && Input.HasParameter("rightLabel"))
            {
                labels.Add(0, (string)Input.GetInput("leftLabel"));
                labels.Add(100, (string)Input.GetInput("rightLabel"));
            }

            allowText = (bool)Input.GetInput("allowTextBox", false);

            float defaultWidth = allowText ? 2 : 3;

            width = Input.HasParameter("width") ? Convert.ToSingle(Input.GetInput("width")) : defaultWidth;

            tickSpace = (int)Input.GetInput("tickSpace", 25);

            adjustMinMax = (bool)Input.GetInput("adjustMinMax", false);

            showAbsolute = (bool)Input.GetInput("showAbsolute", true);
        }

        public void UpdateRange(float min, float max)
        {
            this.min = min;
            this.max = max;
            //Input.AddInput("min", min);
            //Input.AddInput("max", max);
        }

        /// <summary>
        /// If adjustMinMax is true, updates the range of the slider i.e. min or max values of the slider
        /// depending on the value to be set.
        /// </summary>
        /// <param name="val"></param>
        public void UpdateSliderRange(float val)
        {
            if (adjustMinMax)
            {
                if (val < min)
                {
                    min = val;
                    if (showAbsolute)
                        slider.MinValue = val;
                    //Input.AddInput("min", min);
                }
                else if (val > max)
                {
                    max = val;
                    if (showAbsolute)
                        slider.MaxValue = val;
                    //Input.AddInput("max", max);
                }
            }
            else
            {
                textBox.Text = Value.ToString();
            }
        }

        /// <summary>
        /// Updates description of the slider shown in  the label.
        /// </summary>
        /// <param name="desc"></param>
        public void UpdateDescription(string desc)
        {
            label.Content = desc;
        }
    }
}
