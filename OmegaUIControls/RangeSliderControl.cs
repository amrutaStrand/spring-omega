using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using Agilent.OpenLab.Spring.Omega;
using Infragistics.Controls.Editors;
using OmegaUIControls.OmegaUIUtils;

namespace OmegaUIControls
{
    //Parameters of this class are same as that of XamSliderControl class.
    class RangeSliderControl : AbstractUIControl
    {
        protected string sliderType;
        protected float min;
        protected float max;
        protected Hashtable labels;
        protected bool allowText;
        protected double width;
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

        //This method is called when Value prop is set or minThumb is changed
        private void SetMinValue(float value)
        {
            localChangeMin = true;

            if (sliderType.Equals("int"))
                minValue = (int)value;
            else if (sliderType.Equals("float"))
                minValue = value;

            minTextBox.Text = minValue.ToString();
            minThumb.Value = value;

            localChangeMin = false;
        }

        //This method is called when Value prop is set or maxThumb is changed
        private void SetMaxValue(float value)
        {
            localChangeMax = true;

            if (sliderType.Equals("int"))
                maxValue = (int)value;
            else if (sliderType.Equals("float"))
                maxValue = value;

            maxTextBox.Text = maxValue.ToString();
            maxThumb.Value = value;

            localChangeMax = false;
        }

        //Updates the range of the slider i.e. min and max values of the slider
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

        public override void CreateUIElement()
        {
            var panel = allowText ? new LayoutPanel(1, 3) : new LayoutPanel(1, 1);
            panel.ChangeDimension(80, 800);
            panel.Margin = new Thickness(10);
            if (showBorder)
            {
                panel.AddBorder(description);
            }

            AddMinText(panel);
            AddSlider(panel);
            AddMaxText(panel);

            UIElement = panel;
        }

        //Adds a text box to show value corresponding to min thumb. Called by the CreateUIElement method.
        private void AddMinText(LayoutPanel panel)
        {
            if (!allowText)
            {
                return;
            }
            minTextBox = new TextBox();
            minTextBox.RenderSize = UIConstants.TEXT_PREFERRED_SIZE;
            minTextBox.LostFocus += MinTextBox_LostFocus;
            panel.Add(minTextBox, 1);
            Grid.SetColumn(minTextBox, 0);
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

        //Adds a text box to show value corresponding to max thumb. Called by the CreateUIElement method.
        private void AddMaxText(LayoutPanel panel)
        {
            if (!allowText)
            {
                return;
            }
            maxTextBox = new TextBox();
            maxTextBox.RenderSize = UIConstants.TEXT_PREFERRED_SIZE;
            maxTextBox.LostFocus += MaxTextBox_LostFocus; 
            panel.Add(maxTextBox, 1);
            Grid.SetColumn(maxTextBox, 2);
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

        //Adds a range slider with two thumbs. Called by the CreateUIElement method.
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

        //Called by the AddSlider method to set the DataTemplate of the thumbs.
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

        //Called by the AddSlider method to set the DataTemplate of the slider labels.
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

            sliderType = Input.HasParameter("sliderType") ? (string)Input.GetInput("sliderType") : "float";

            min = Input.HasParameter("min") ? Convert.ToSingle(Input.GetInput("min")) : 0f;

            max = Input.HasParameter("max") ? Convert.ToSingle(Input.GetInput("max")) : 100f;

            labels = Input.HasParameter("labelTable") ? (Hashtable)Input.GetInput("labelTable") : new Hashtable();

            if (Input.HasParameter("leftLabel") && Input.HasParameter("rightLabel"))
            {
                labels.Add(0, (string)Input.GetInput("leftLabel"));
                labels.Add(100, (string)Input.GetInput("rightLabel"));
            }

            allowText = Input.HasParameter("allowTextBox") ? (bool)Input.GetInput("allowTextBox") : false;

            width = Input.HasParameter("width") ? (double)Input.GetInput("width") : 2;

            tickSpace = Input.HasParameter("tickSpace") ? (int)Input.GetInput("tickSpace") : 25;

            adjustMinMax = Input.HasParameter("adjustMinMax") ? (bool)Input.GetInput("adjustMinMax") : false;

            minLabel = Input.HasParameter("minLabel") ? (string)Input.GetInput("minLabel") : "Minimum";

            maxLabel = Input.HasParameter("maxLabel") ? (string)Input.GetInput("maxLabel") : "Maximum";

            showBorder = Input.HasParameter("showBorder") ? (bool)Input.GetInput("showBorder") : true;

            description = Input.HasParameter("Description") ? (string)Input.GetInput("Description") : "Range Slider";
        }
    }
}
