using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ClassLibrary1
{
    public class FloatControl : AbstractUIControl
    {

        public FloatControl()
        {
            InitializeLayout();
        }

        private void InitializeLayout()
        {
            Label = new Label() { Height = 50, Width = 100 };
            TextBox = new TextBox() { Height = 50, Width = 100 };
            var panel = new StackPanel() { Orientation = Orientation.Horizontal };
            panel.Children.Add(Label);
            panel.Children.Add(TextBox);
            this.AddChild(panel);
        }

        public new void SetParameters(Dictionary<string, object> valuePairs)
        {
            Label.Content = valuePairs["Label"].ToString();
            TextBox.Text = valuePairs["Value"].ToString();
        }

        public Label Label { get; set; }
        public TextBox TextBox { get; set; }

        public override object Value
        {
            get => this.TextBox.Text;
            set
            {
                if (Validate(value))
                    TextBox.Text = value.ToString();
            }
        }

        public override Dictionary<string, object> Parameters { get; set; }

        public bool Validate(object val)
        {
            return (val != null && val is float) ? true : false;
        }
    }
}
