using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Agilent.OpenLab.Spring.Omega;

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
    //7. width
    //8. tickPlacement : "top" or "bottom"
    //9. tickSpace
    class SliderControl : AbstractUIControl
    {
        protected string sliderType;
        protected float min;
        protected float max;
        protected Hashtable labels;
        protected bool allowText;
        protected double width;
        protected string tickPlacement;
        protected int tickSpace;

        protected Label label;
        protected Slider slider;
        protected TextBox textBox;

        public override object Value { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void CreateUIElement()
        {
            var panel = new Grid();
            ColumnDefinition col1 = new ColumnDefinition();
            ColumnDefinition col2 = new ColumnDefinition();
            ColumnDefinition col3 = new ColumnDefinition();
            ColumnDefinition col4 = new ColumnDefinition();
            panel.ColumnDefinitions.Add(col1);
            panel.ColumnDefinitions.Add(col2);
            panel.ColumnDefinitions.Add(col3);
            panel.ColumnDefinitions.Add(col4);

            addLabel(panel);
            addSlider(panel);
            addText(panel);

            var panel1 = new Slider();
            panel1.TickPlacement = System.Windows.Controls.Primitives.TickPlacement.TopLeft;
            panel1.Maximum = 200;
            panel1.Width = 100;
            panel1.TickFrequency = 10;

            UIElement = panel;
        }

        private void addText(Grid panel)
        {
            if (!allowText)
            {
                return;
            }
            textBox = new TextBox();
            textBox.Text = slider.Value.ToString();
            textBox.TextChanged += TextBox_TextChanged;
            textBox.Width = 50;
            panel.Children.Add(textBox);
            Grid.SetColumn(textBox, 2);
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            double value;
            bool s = Double.TryParse(textBox.Text, out value);
            slider.Value = s ? value : 0;
        }

        private void addSlider(Grid panel)
        {
            slider = new Slider();
            slider.Minimum = 0;
            slider.Maximum = 100;
            slider.IsSnapToTickEnabled = true;

            if(labels != null)
            {
                //set custom labels to the slider
            }

            if (tickPlacement != null)
            {
                slider.TickFrequency = tickSpace;

                if (tickPlacement == "top")
                {
                    slider.TickPlacement = System.Windows.Controls.Primitives.TickPlacement.TopLeft;
                }
                else if (tickPlacement == "bottom")
                {
                    slider.TickPlacement = System.Windows.Controls.Primitives.TickPlacement.BottomRight;
                }
                else if (tickPlacement == "both")
                {
                    slider.TickPlacement = System.Windows.Controls.Primitives.TickPlacement.Both;
                }
            }

            slider.ValueChanged += Slider_ValueChanged;

            panel.Children.Add(slider);
            Grid.SetColumn(slider, 1);
            panel.ColumnDefinitions[1].Width = new GridLength(width);
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            textBox.Text = slider.Value.ToString();
        }

        private void addLabel(Grid panel)
        {
            label = new Label();
            label.Content = Input.GetInput("Description");
            panel.Children.Add(label);
            Grid.SetColumn(label, 0);
        }

        public override void SetInput(IUIInput input)
        {
            Input = input;

            sliderType = Input.HasParameter("sliderType") ? (string)Input.GetInput("sliderType") : "float";

            min = Input.HasParameter("min") ? (float)Input.GetInput("min") : 0;

            max = Input.HasParameter("max") ? (float)Input.GetInput("max") : 100;

            labels = Input.HasParameter("labelTable") ? (Hashtable)Input.GetInput("labelTable") : new Hashtable();

            if (Input.HasParameter("leftLabel") && Input.HasParameter("rightLabel"))
            {
                labels.Add(0, (string)Input.GetInput("leftLabel"));
                labels.Add(100, (string)Input.GetInput("rightLabel"));
            }

            allowText = Input.HasParameter("allowTextBox") ? (bool)Input.GetInput("allowTextBox") : false;

            //width = Input.HasParameter("width") ? (double)Input.GetInput("width") : (allowText ? 0.3 : 0.6);
            width = 200;

            tickPlacement = Input.HasParameter("tickPlacement") ? (string)Input.GetInput("tickPlacement") : null;

            tickSpace = Input.HasParameter("tickSpace") ? (int)Input.GetInput("tickSpace") : 25;
        }
    }
}
