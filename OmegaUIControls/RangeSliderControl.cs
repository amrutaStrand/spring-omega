using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using Infragistics.Controls.Editors;
using OmegaUIControls.OmegaUIUtils;

namespace Agilent.OpenLab.Spring.Omega
{
    /// <summary>
    /// Contains a slider with two thumbs corresponding to the min and max values. Parameters 
    /// of this class are same as that of XamSliderControl class.
    /// </summary>
    class RangeSliderControl : AbstractUIControl
    {
        protected string sliderType;
        protected float min;
        protected float max;
        protected Hashtable labels;
        protected bool allowText;
        protected float width;
        protected int tickSpace;
        protected bool adjustMinMax;
        protected string minLabel;
        protected string maxLabel;
        protected bool showAbsolute;

        private bool localChangeMin;
        private bool localChangeMax;

        protected Label label;
        protected TextBox minTextBox;
        protected TextBox maxTextBox;
        protected XamNumericRangeSlider rangeSlider;
        XamSliderNumericThumb minThumb;
        XamSliderNumericThumb maxThumb;

        #region fields for error handling
        //Tool tip to show errorMsg
        protected ToolTip minErrorToolTip;
        protected ToolTip maxErrorToolTip;

        //These fields are to store initial styles of the TextBox
        protected Brush minBorderBrush;
        protected Thickness minBorderThickness;
        protected Brush minTextBackground;
        protected Brush maxBorderBrush;
        protected Thickness maxBorderThickness;
        protected Brush maxTextBackground;

        protected bool minLastValid = true;
        protected bool maxLastValid = true;

        #endregion

        private float minValue;
        private float maxValue;

        /// <summary>
        /// Value of RangeSliderControl is a <see cref="Dictionary{TKey, TValue}"/> with "min" and "max" keys.
        /// </summary>
        public override object Value
        {
            get
            {
                Dictionary<string, float> map = new Dictionary<string, float>();
                map.Add("min", minValue);
                map.Add("max", maxValue);
                return map;
            }
            set
            {
                if(value is Dictionary<string, float> || value is Dictionary<string, int>)
                {
                    Dictionary<string, float> map = value as Dictionary<string, float>;

                    float setMin = map["min"];
                    float setMax = map["max"];

                    if (adjustMinMax)
                    {
                        UpdateSliderRange(map["min"]);
                        UpdateSliderRange(map["max"]);
                    }
                    else
                    {
                        if (setMin < min)
                            setMin = min;
                        else if (setMax < min)
                            setMax = min;
                        else if (setMin > max)
                            setMin = max;
                        else if (setMax > max)
                            setMax = max;
                    }

                    SetMinValue(setMin);
                    SetMaxValue(setMax);
                }
                else
                {
                    //throw new Exception("Value should be of type Dictionary<string, float/int>");
                }
            }
        }

        /// <summary>
        ///  While setting MinThumbValue, absolute value is converted into % and 
        ///  in the get method % is converted back into absolute value.
        /// </summary>
        protected float MinThumbValue {
            get
            {
                if (showAbsolute)
                {
                    return Convert.ToSingle(minThumb.Value);
                }
                else
                {
                    float fraction = Convert.ToSingle(minThumb.Value / 100);
                    return min + fraction * (max - min);
                }
            }
            set
            {
                if (showAbsolute)
                {
                    minThumb.Value = value;
                }
                else
                {
                    float fraction = (value - min) / (max - min);
                    minThumb.Value = fraction * (100);
                }
            }
        }

        /// <summary>
        ///  While setting MaxThumbValue, absolute value is converted into % and 
        ///  in the get method % is converted back into absolute value.
        /// </summary>
        protected float MaxThumbValue
        {
            get
            {
                if (showAbsolute)
                {
                    return Convert.ToSingle(maxThumb.Value);
                }
                else
                {
                    float fraction = Convert.ToSingle(maxThumb.Value / 100);
                    return min + fraction * (max - min);
                }
            }
            set
            {
                if (showAbsolute)
                {
                    maxThumb.Value = value;
                }
                else
                {
                    float fraction = (value - min) / (max - min);
                    maxThumb.Value = fraction * (100);
                }
            }
        }

        /// <summary>
        /// Sets the value of minTextBox and minThumb. This method is called when Value prop is set 
        /// or minThumb is changed.
        /// </summary>
        /// <param name="value"></param>
        private void SetMinValue(float value)
        {
            localChangeMin = true;

            if (sliderType.Equals("int"))
                minValue = (int)value;
            else if (sliderType.Equals("float"))
                minValue = value;

            if(allowText)
                minTextBox.Text = minValue.ToString();

            MinThumbValue = value;

            localChangeMin = false;
        }

        /// <summary>
        /// Sets the value of maxTextBox and maxThumb. This method is called when Value prop is set 
        /// or maxThumb is changed.
        /// </summary>
        /// <param name="value"></param>
        private void SetMaxValue(float value)
        {
            localChangeMax = true;

            if (sliderType.Equals("int"))
                maxValue = (int)value;
            else if (sliderType.Equals("float"))
                maxValue = value;

            if (allowText)
                maxTextBox.Text = maxValue.ToString();

            MaxThumbValue = value;

            localChangeMax = false;
        }

        /// <summary>
        /// If adjustMinMax is true, updates the range of the slider i.e. min or max values of the 
        /// slider depending on the value to be set.
        /// </summary>
        /// <param name="val"></param>
        public void UpdateSliderRange(float val)
        {
            if (adjustMinMax)
            {
                if (val < min)
                {
                    min = val;
                    rangeSlider.MinValue = min;
                    //Input.AddInput("min", min);
                }
                else if (val > max)
                {
                    max = val;
                    rangeSlider.MaxValue = max;
                    //Input.AddInput("max", max);
                }
            }
        }

        /// <summary>
        /// Creates UIElement of RangeSliderControl which is a <see cref="LayoutPanel"/> containing a 
        /// slider with two thumbs, a label and text boxes corresponding to the min and max values.
        /// </summary>
        public override void CreateUIElement()
        {
            var panel = allowText ? new LayoutPanel(1, 4) : new LayoutPanel(1, 2);

            AddLabel(panel);
            if (allowText)
            {
                AddMinText(panel);
                AddSlider(panel);
                AddMaxText(panel);
            }
            else
                AddSlider(panel);

            Value = Input.GetInput("Value", new Dictionary<string, float> { { "min", min }, { "max", max } });

            panel.ChangeDimension(80, 800);
            UtilityMethods.SetPanelResources(panel);
            UIElement = panel;

            minErrorToolTip = new ToolTip();
            minErrorToolTip.Style = UIConstants.GetErrorToolTipStyle();

            minBorderBrush = minTextBox.BorderBrush;
            minBorderThickness = minTextBox.BorderThickness;
            minTextBackground = minTextBox.Background;

            maxErrorToolTip = new ToolTip();
            maxErrorToolTip.Style = UIConstants.GetErrorToolTipStyle();

            maxBorderBrush = maxTextBox.BorderBrush;
            maxBorderThickness = maxTextBox.BorderThickness;
            maxTextBackground = maxTextBox.Background;
        }

        /// <summary>
        /// Adds a label to show the Description of the slider.
        /// </summary>
        /// <param name="panel"></param>
        private void AddLabel(LayoutPanel panel)
        {
            label = new Label();
            label.Content = Input.GetInput("Description", "Range Slider Description");

            panel.Add(label, 1);
        }

        /// <summary>
        /// Adds a text box to show value corresponding to min thumb. Called by the CreateUIElement method.
        /// </summary>
        /// <param name="panel"></param>
        private void AddMinText(LayoutPanel panel)
        {
            if (!allowText)
            {
                return;
            }
            minTextBox = new TextBox();
            minTextBox.Text = min.ToString();
            minTextBox.LostFocus += MinTextBox_LostFocus;
            panel.Add(minTextBox, 1);
        }

        private void MinTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            float cur;
            bool isAllowed = float.TryParse(minTextBox.Text, out cur);

            if (!Validate(minTextBox.Text, minLastValid, minTextBox, minErrorToolTip, "min"))
                return;

            Value = new Dictionary<string, float>() { { "min", cur }, { "max", maxValue } };
        }

        /// <summary>
        /// Adds a text box to show value corresponding to max thumb. Called by the CreateUIElement method.
        /// </summary>
        /// <param name="panel"></param>
        private void AddMaxText(LayoutPanel panel)
        {
            if (!allowText)
            {
                return;
            }
            maxTextBox = new TextBox();
            maxTextBox.Text = max.ToString();
            maxTextBox.LostFocus += MaxTextBox_LostFocus; 
            panel.Add(maxTextBox, 1);
        }

        private void MaxTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            float cur;
            bool isAllowed = float.TryParse(maxTextBox.Text, out cur);

            if (!Validate(maxTextBox.Text, maxLastValid, maxTextBox, maxErrorToolTip, "max"))
                return;

            Value = new Dictionary<string, float>() { { "min", minValue }, { "max", cur } };
        }

        public bool Validate(object val, bool lastValid, TextBox textBox, ToolTip ErrorToolTip, string name)
        {
            if (IsValid(val) && IsWithinRange(val))
            {
                if (!lastValid)
                {
                    textBox.BorderBrush = minBorderBrush;
                    textBox.BorderThickness = minBorderThickness;
                    textBox.Background = minTextBackground;
                    textBox.ToolTip = null;
                    //UtilityMethods.SetResources(textBox);
                }

                if (name == "min")
                    minLastValid = true;
                else if (name == "max")
                    maxLastValid = true;

                return true;
            }
            else
            {
                textBox.BorderBrush = new SolidColorBrush(UIConstants.ColorError);
                textBox.BorderThickness = UIConstants.BorderThicknessError;
                textBox.Background = UIConstants.GetTextBackgroundError();
                if (!IsValid(val))
                    ShowValidationError(ErrorToolTip);
                else if (!IsWithinRange(val))
                    ShowOutOfRangeError(ErrorToolTip);
                textBox.ToolTip = ErrorToolTip;

                if (name == "min")
                    minLastValid = false;
                else if (name == "max")
                    maxLastValid = false;

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

        protected virtual void ShowValidationError(ToolTip ErrorToolTip)
        {
            string errorMsg = "";
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

        private void ShowOutOfRangeError(ToolTip ErrorToolTip)
        {
            ErrorToolTip.Content = string.Format(MessageInfo.OUT_OF_RANGE_ERROR_MESSAGE, min, max);
        }

        /// <summary>
        /// Adds a range slider with two thumbs. Called by the CreateUIElement method.
        /// </summary>
        /// <param name="panel"></param>
        private void AddSlider(LayoutPanel panel)
        {
            rangeSlider = new XamNumericRangeSlider()
            {
                MinValue = showAbsolute ? min : 0,
                MaxValue = showAbsolute ? max : 100
            };

            minThumb = new XamSliderNumericThumb();
            minThumb.ToolTipTemplate = CreateThumbTemplate(typeof(TextBlock), minLabel);
            minThumb.ToolTipVisibility = Visibility.Visible;
            minThumb.ValueChanged += MinThumb_ValueChanged;
            rangeSlider.Thumbs.Add(minThumb);

            maxThumb = new XamSliderNumericThumb();
            maxThumb.ToolTipTemplate = CreateThumbTemplate(typeof(TextBlock), maxLabel);
            maxThumb.ToolTipVisibility = Visibility.Visible;
            maxThumb.ValueChanged += MaxThumb_ValueChanged;
            rangeSlider.Thumbs.Add(maxThumb);

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

            if ((bool)Input.GetInput("showTickMarks", true))
            {
                rangeSlider.TickMarks.Add(tickMarks);
                rangeSlider.TickMarks.Add(tickMarksLabels);
            }

            panel.Add(rangeSlider, width);
        }

        private void MaxThumb_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (maxThumb.Value < minThumb.Value)
            {
                SetMinValue(MaxThumbValue);
            }

            if (localChangeMax)
                return;

            SetMaxValue(MaxThumbValue);

            if (!maxLastValid)
            {
                maxTextBox.BorderBrush = maxBorderBrush;
                maxTextBox.BorderThickness = maxBorderThickness;
                maxTextBox.Background = maxTextBackground;
                maxTextBox.ToolTip = null;
                maxLastValid = true;
                //UtilityMethods.SetResources(textBox);
            }

        }

        private void MinThumb_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (minThumb.Value > maxThumb.Value)
            {
                SetMaxValue(MinThumbValue);
            }

            if (localChangeMin)
                return;

            SetMinValue(MinThumbValue);

            if (!minLastValid)
            {
                minTextBox.BorderBrush = minBorderBrush;
                minTextBox.BorderThickness = minBorderThickness;
                minTextBox.Background = minTextBackground;
                minTextBox.ToolTip = null;
                minLastValid = true;
                //UtilityMethods.SetResources(textBox);
            }
        }

        /// <summary>
        /// Called by the AddSlider method to set the DataTemplate of the thumbs.
        /// </summary>
        /// <param name="viewType"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        private DataTemplate CreateThumbTemplate(Type viewType, string name)
        {
            const string xamlTemplate = "<DataTemplate><StackPanel><{0} Text={1}/><{2} Text={3:0.00}/></StackPanel></DataTemplate>";
            var xaml = String.Format(xamlTemplate, viewType.Name, "\"" + name + "\"", viewType.Name, "\"{Binding}\"");
            var context = new ParserContext();
            context.XmlnsDictionary.Add("", "http://schemas.microsoft.com/winfx/2006/xaml/presentation");
            context.XmlnsDictionary.Add("x", "http://schemas.microsoft.com/winfx/2006/xaml");

            var template = (DataTemplate)XamlReader.Parse(xaml, context);
            return template;
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

        public override void SetInput(IUIInput input)
        {
            base.SetInput(input);

            localChangeMin = false;

            localChangeMax = false;

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

            width = Input.HasParameter("width") ? Convert.ToSingle(Input.GetInput("width")) : 2f;

            tickSpace = (int)Input.GetInput("tickSpace", 25);

            adjustMinMax = (bool)Input.GetInput("adjustMinMax", false);

            minLabel = (string)Input.GetInput("minLabel", "Minimum");

            maxLabel = (string)Input.GetInput("maxLabel", "Maximum");

            showAbsolute = (bool)Input.GetInput("showAbsolute", true);
        }
    }
}
