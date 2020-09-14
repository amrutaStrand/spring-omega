using System;
using System.Windows;
using System.Windows.Controls;
using Agilent.OpenLab.Spring.Omega;
using OmegaUIControls.OmegaUIUtils;
using Infragistics.Controls.Editors;
using System.Collections;
using System.Windows.Markup;

namespace OmegaUIControls
{
    //Parameters of this class are
    //1. sliderType (string) : sliderType should be any one "float" or "int"
    //2. min (number) : Minimum value of this slider
    //3. max (number) : Maximum value of this slider
    //4. leftLabel (string) : text that should display on the left side of the slider
    //5. rightLabel (string) : text that should display on the right side of the slider
    //6. allowTextBox (bool) : if this value is true then it will add a textfield next to slider and
    //                      the value of the textField is the slider current value
    //7. width (double) : relative to textBox and label (1 means equal, 2 means double, etc.)
    //8. tickSpace
    //9. adjustMinMax (bool) : if set to true, the min/max values are upadated when the value inside the
    //                          TextBox is out of the current range
    class XamSliderControl : AbstractUIControl
    {
        protected string sliderType;
        protected float min;
        protected float max;
        protected Hashtable labels;
        protected bool allowText;
        protected double width;
        protected int tickSpace;
        protected bool adjustMinMax;
        protected bool localChange;

        protected Label label;
        protected XamNumericSlider slider;
        protected TextBox textBox;

        private object value;
        public override object Value
        {
            get
            {
                float f = SliderValue;
                if (sliderType.Equals("int"))
                    value = (int)f;
                else if (sliderType.Equals("float"))
                    value = f;
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

        //Sets the slider position, textBox value and the value field. Unlike the set method of the Value prop,
        //it calls the SetValue method without checking if the value is between min and max.
        public void SetValueExplicitly(object explicitValue)
        {
            float val = Convert.ToSingle(explicitValue);
            if (adjustMinMax)
                UpdateSliderRange(val);
            SetValue(val);
        }

        //This method is called when Value prop is set or slider is changed
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

        //While setting this prop, absolute value is converted into % and in the get method,
        //% is converted back into absolute value
        protected float SliderValue
        {
            get
            {
                float fraction = (float)(slider.Value / 100);
                return min + fraction * (max - min);
            }
            set
            {
                float fraction = (value - min) / (max - min);
                slider.Value = fraction * (100);
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
            textBox.Text = Value.ToString();

            UIElement = panel;
        }

        //Adds a text box to show value corresponding to the slider. Called by the CreateUIElement method.
        private void AddText(LayoutPanel panel)
        {
            if (!allowText)
            {
                return;
            }
            textBox = new TextBox();
            textBox.LostFocus += TextBox_LostFocus;
            textBox.RenderSize = UIConstants.TEXT_PREFERRED_SIZE;
            panel.Add(textBox, 1);
            Grid.SetColumn(textBox, 2);
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            float cur;
            float last = Convert.ToSingle(value);
            bool isAllowed = float.TryParse(textBox.Text, out cur);
            if (!isAllowed)
            {
                textBox.Text = Value.ToString();
                SliderValue = last;
                MessageBox.Show("Please enter a number!");
            }
            else if (cur != last)
            {
                Value = cur;
            }
        }

        //Adds a xam numeric slider. Called by the CreateUIElement method.
        private void AddSlider(LayoutPanel panel)
        {
            slider = new XamNumericSlider()
            {
                MinValue = 0,
                MaxValue = 100
            };

            slider.Value = 0;

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

            slider.TickMarks.Add(tickMarks);
            slider.TickMarks.Add(tickMarksLabels);

            if (allowText)
                slider.ThumbValueChanged += Slider_ThumbValueChanged;

            panel.Add(slider, width);
        }

        private void Slider_ThumbValueChanged(object sender, ThumbValueChangedEventArgs<double> e)
        {
            if (localChange)
                return;
            SetValue(SliderValue);
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

        //Adds a label to show the Description of the slider.
        private void AddLabel(LayoutPanel panel)
        {
            label = new Label();
            label.Content = Input.GetInput("Description");
            label.RenderSize = UIConstants.LABEL_PREFERRED_SIZE;

            panel.Add(label, 1);
            Grid.SetColumn(label, 0);
        }

        public override void SetInput(IUIInput input)
        {
            Input = input;

            localChange = false;

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

            width = Input.HasParameter("width") ? (double)Input.GetInput("width") : (allowText ? 1 : 2);

            tickSpace = Input.HasParameter("tickSpace") ? (int)Input.GetInput("tickSpace") : 25;

            adjustMinMax = Input.HasParameter("adjustMinMax") ? (bool)Input.GetInput("adjustMinMax") : false;
        }

        public void UpdateRange(float min, float max)
        {
            this.min = min;
            this.max = max;
            //Input.AddInput("min", min);
            //Input.AddInput("max", max);
        }

        //Updates the range of the slider i.e. min and max values of the slider
        public void UpdateSliderRange(float val)
        {
            if (adjustMinMax)
            {
                if (val < min)
                {
                    min = val;
                    //Input.AddInput("min", min);
                }
                else if (val > max)
                {
                    max = val;
                    //Input.AddInput("max", max);
                }
            }
            else
            {
                textBox.Text = Value.ToString();
            }
        }

        public void UpdateDescription(string desc)
        {
            label.Content = desc;
        }
    }
}
