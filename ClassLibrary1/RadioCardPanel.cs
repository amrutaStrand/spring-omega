using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Agilent.OpenLab.Spring.Omega
{
    class RadioCardPanel : AbstractUIControl
    {
        public override object Value { get; set; }

        public Dictionary<string, UIElement> Options { get ; set ; }

        Dictionary<string, UIElement> Components { get; set; }

        public UIElement Cards { get; set; }

        public List<RadioButton> RadioButtons { get; set; }

        int cardCount;

        public override void CreateUIElement()
        {
            RadioButtons = new List<RadioButton>();
            var labels = Options.Keys;
            Components = new Dictionary<string, UIElement>();
            List<string> componentName = new List<string>();
            var panel = new StackPanel();
            cardCount = 0;
            foreach (string label in labels)
            {
                if (Options[label] != null)
                {
                    if (Options[label] is UIElement)
                    {
                        Components.Add(label, (UIElement)Options[label]);
                    }
                    else if (Options[label] is IUIControl)
                    {
                        IUIControl control = (IUIControl)Options[label];
                        Components.Add(label, control.GetUIElement());
                    }
                    cardCount++;
                    componentName.Add(label);
                    RadioButton radioButton = new RadioButton();
                    radioButton.Content = label;
                    panel.Children.Add(radioButton);
                    radioButton.Checked += RadioButton_Checked;
                    radioButton.Unchecked += RadioButton_Unchecked;
                    RadioButtons.Add(radioButton);
                }
            }

            Value = componentName[0];

            var completePanel = new StackPanel();
            completePanel.Children.Add(panel);
            UIElement = completePanel;
        }

        private void RadioButton_Unchecked(object sender, RoutedEventArgs e)
        {
            if (((StackPanel)UIElement).Children.Count > 1)
            {
                ((StackPanel)UIElement).Children.RemoveAt(((StackPanel)UIElement).Children.Count - 1);
            }
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            Value = (string)rb.Content;
            ((StackPanel)UIElement).Children.Add(Options[(string)rb.Content]);
        }

        public override UIElement GetUIElement()
        {
            if (UIElement == null)
                CreateUIElement();
            return UIElement;
        }

        public override void SetInput(IUIInput input) // IUIInput
        {
            Options = (Dictionary<string, UIElement>)input.GetInput("Options");
        }

        public override bool Validate(object value)
        {
            return true;
        }
    }
}
