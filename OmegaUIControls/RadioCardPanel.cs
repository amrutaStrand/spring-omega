using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Agilent.OpenLab.Spring.Omega
{
    class RadioCardPanel : AbstractUIControl
    {
        public override object Value { get; set; }

        public Dictionary<string, object> Parameters { get ; set ; }

        //public List<RadioButton> RadioButtons { get; set; }

        public override void CreateUIElement()
        {
            //RadioButtons = new List<RadioButton>();
            var panel = new StackPanel();
            var labels = (List<string>)Parameters["RadioLabels"];

            foreach (string label in labels)
            {
                RadioButton radioButton = new RadioButton();
                radioButton.Content = label;
                //RadioButtons.Add(radioButton);
                panel.Children.Add(radioButton);
            }
            UIElement = panel;
        }

        public override UIElement GetUIElement()
        {
            if (UIElement == null)
                CreateUIElement();
            return UIElement;
        }

        public override void SetInput(IUIInput input) // IUIInput
        {
            Parameters = new Dictionary<string, object>();
            List<string> myList = new List<string>();
            var labels = (List<string>)input.GetInput("RadioLabels");

            foreach (string label in labels)
            {
                myList.Add(label);
            }
            Parameters.Add("RadioLabels", myList);
        }

        public override bool Validate(object value)
        {
            return true;
        }
    }
}
