using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
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
        protected bool showBorder;
        protected string description;

        private bool localChangeMin;
        private bool localChangeMax;

        protected TextBox minTextBox;
        protected TextBox maxTextBox;
        protected XamNumericRangeSlider rangeSlider;
        XamSliderNumericThumb minThumb;
        XamSliderNumericThumb maxThumb;

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
                    throw new Exception("Value should be of type Dictionary<string, float/int>");
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
            minThumb.Value = value;

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
            maxThumb.Value = value;

            localChangeMax = false;
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
        /// slider with two thumbs and text boxes corresponding to the min and max values.
        /// </summary>
        public override void CreateUIElement()
        {
            var panel = allowText ? new LayoutPanel(1, 3) : new LayoutPanel(1, 1);
            panel.ChangeDimension(80, 800);
            panel.Margin = new Thickness(10);

            if (showBorder)
            {
                panel.AddBorder(description);
            }

            if (allowText)
            {
                AddMinText(panel);
                AddSlider(panel);
                AddMaxText(panel);
            }
            else
                AddSlider(panel);

            UIElement = panel;
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
            minTextBox.RenderSize = UIConstants.TEXT_PREFERRED_SIZE;
            minTextBox.LostFocus += MinTextBox_LostFocus;
            panel.Add(minTextBox, 1);
        }

        private void MinTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            float cur;
            bool isAllowed = float.TryParse(minTextBox.Text, out cur);
            if (!isAllowed)
            {
                MessageBox.Show("Please enter a number!");
                minTextBox.Text = minValue.ToString();
            }
            else
            {
                Value = new Dictionary<string, float>() { { "min", cur }, { "max", maxValue } };
            }
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
            maxTextBox.RenderSize = UIConstants.TEXT_PREFERRED_SIZE;
            maxTextBox.LostFocus += MaxTextBox_LostFocus; 
            panel.Add(maxTextBox, 1);
        }

        private void MaxTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            float cur;
            bool isAllowed = float.TryParse(maxTextBox.Text, out cur);
            if (!isAllowed)
            {
                MessageBox.Show("Please enter a number!");
                maxTextBox.Text = maxValue.ToString();
            }
            else
            {
                Value = new Dictionary<string, float>() { { "min", minValue }, { "max", cur } };
            }
        }

        /// <summary>
        /// Adds a range slider with two thumbs. Called by the CreateUIElement method.
        /// </summary>
        /// <param name="panel"></param>
        private void AddSlider(LayoutPanel panel)
        {
            rangeSlider = new XamNumericRangeSlider()
            {
                MinValue = min,
                MaxValue = max
            };

            minThumb = new XamSliderNumericThumb() { Value = min };
            minThumb.ToolTipTemplate = CreateThumbTemplate(typeof(TextBlock), minLabel);
            minThumb.ToolTipVisibility = Visibility.Visible;
            minThumb.ValueChanged += MinThumb_ValueChanged;
            rangeSlider.Thumbs.Add(minThumb);

            maxThumb = new XamSliderNumericThumb() { Value = max };
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

            rangeSlider.TickMarks.Add(tickMarks);
            rangeSlider.TickMarks.Add(tickMarksLabels);

            panel.Add(rangeSlider, width);
        }

        private void MaxThumb_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (maxThumb.Value < minThumb.Value)
            {
                SetMinValue(Convert.ToSingle(maxThumb.Value));
            }

            if (localChangeMax)
                return;

            SetMaxValue(Convert.ToSingle(maxThumb.Value));

        }

        private void MinThumb_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (minThumb.Value > maxThumb.Value)
            {
                SetMaxValue(Convert.ToSingle(minThumb.Value));
            }

            if (localChangeMin)
                return;

            SetMinValue(Convert.ToSingle(minThumb.Value));

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
            Input = input;

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

            showBorder = (bool)Input.GetInput("showBorder", true);

            description = (string)Input.GetInput("Description", "Range Slider");
        }
    }
}
