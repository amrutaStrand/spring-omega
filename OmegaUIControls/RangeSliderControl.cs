using System;
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
    class RangeSliderControl : AbstractUIControl
    {

        protected string sliderType;
        protected double width;
        protected int tickSpace;
        protected string minLabel;
        protected string maxLabel;
        protected Label label;
        protected XamNumericRangeSlider rangeSlider;
        XamSliderNumericThumb minThumb;
        XamSliderNumericThumb maxThumb;
        private object value;
        public override object Value
        {
            get
            {
                Dictionary<string, double> map = new Dictionary<string, double>();
                map.Add("min", minThumb.Value);
                map.Add("max", maxThumb.Value);
                value = map;
                return value;
            }
            set
            {
                if(value is Dictionary<string, double>)
                {
                    Dictionary<string, double> map = value as Dictionary<string, double>;
                    minThumb.Value = map["min"];
                    maxThumb.Value = map["max"];
                }
                else
                {
                    throw new Exception("Value should be of type Dictionary<string, double>");
                }
            }
        }

        public override void CreateUIElement()
        {
            var panel = new LayoutPanel(1, 2);
            panel.ChangeDimension(60, 800);

            AddLabel(panel);
            AddSlider(panel);

            UIElement = panel;
        }

        private void AddSlider(LayoutPanel panel)
        {
            rangeSlider = new XamNumericRangeSlider()
            {
                MinValue = 100,
                MaxValue = 200
            };

            minThumb = new XamSliderNumericThumb() { Value = 120 };
            minThumb.ToolTipTemplate = CreateThumbTemplate(typeof(TextBlock), minLabel);
            minThumb.ToolTipVisibility = Visibility.Visible;
            minThumb.ValueChanged += MinThumb_ValueChanged;
            rangeSlider.Thumbs.Add(minThumb);

            maxThumb = new XamSliderNumericThumb() { Value = 180 };
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
                minThumb.Value = maxThumb.Value;
            }
        }

        private void MinThumb_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (minThumb.Value > maxThumb.Value)
            {
                maxThumb.Value = minThumb.Value;
            }
        }

        private DataTemplate CreateThumbTemplate(Type viewType, string name)
        {
            const string xamlTemplate = "<DataTemplate><StackPanel><{0} Text={1}/><{2} {3}/></StackPanel></DataTemplate>";
            var xaml = String.Format(xamlTemplate, viewType.Name, "\"" + name + "\"", viewType.Name, "Text =\"{Binding}\"");
            var context = new ParserContext();
            context.XmlnsDictionary.Add("", "http://schemas.microsoft.com/winfx/2006/xaml/presentation");
            context.XmlnsDictionary.Add("x", "http://schemas.microsoft.com/winfx/2006/xaml");

            var template = (DataTemplate)XamlReader.Parse(xaml, context);
            return template;
        }

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

        private void AddLabel(LayoutPanel panel)
        {
            label = new Label();
            label.Content = Input.GetInput("Description");

            panel.Add(label, 1);
            Grid.SetColumn(label, 0);
        }

        public override void SetInput(IUIInput input)
        {
            Input = input;

            sliderType = Input.HasParameter("sliderType") ? (string)Input.GetInput("sliderType") : "float";

            width = Input.HasParameter("width") ? (double)Input.GetInput("width") : 2;

            tickSpace = Input.HasParameter("tickSpace") ? (int)Input.GetInput("tickSpace") : 25;

            minLabel = Input.HasParameter("minLabel") ? (string)Input.GetInput("minLabel") : "Minimum";

            maxLabel = Input.HasParameter("maxLabel") ? (string)Input.GetInput("maxLabel") : "Maximum";
        }
    }
}
