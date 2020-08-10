using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Agilent.OpenLab.Spring.Omega
{
    public class StringControl : AbstractUIControl
    {

        public StringControl()
        {
            CreateUIElement();
        }

        public override void CreateUIElement()
        {
            Label = new Label() { Height=50, Width=100};
            TextBox = new TextBox() {Height=50, Width=100 };
            var panel = new StackPanel() { Orientation = Orientation.Horizontal };
            panel.Children.Add(Label);
            panel.Children.Add(TextBox);
            UIElement = panel;
        }

        public override UIElement GetUIElement()
        {
            if (UIElement == null)
                CreateUIElement();
            return UIElement;
        }

        public override void SetParameters(Dictionary<string, object> valuePairs)
        {
            Label.Content = valuePairs["Label"].ToString();
            Value = valuePairs["Value"].ToString();
        }

        public Label Label { get; set; }
        public TextBox TextBox { get; set; }

        public override object Value 
        {
            get => this.TextBox.Text;
            set
            {
                if(Validate(value))
                    TextBox.Text = value.ToString();
            }
        }

        public override Dictionary<string, object> Parameters { get; set; }

        public override bool Validate(object val)
        {
            return val!=null;
        }
    }
}
