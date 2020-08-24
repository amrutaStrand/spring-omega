using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Agilent.OpenLab.Spring.Omega
{
    class RadioCardUIControl : AbstractUIControl
    {
        public override object Value { get; set; }

        public Dictionary<string, UIElement> Options { get ; set ; }

        Dictionary<string, UIElement> Components { get; set; }

        public List<RadioButton> RadioButtons { get; set; }

        int cardCount;

        //All the radio buttons (for which the 'card' is not null) are added to a stack panel (vertical orientation) 
        //which in turn is added to another stack panel called completePanel. In the java code,
        //completePanel is a JPanel with BorderLayout.
        public override void CreateUIElement()
        {
            RadioButtons = new List<RadioButton>();
            var labels = Options.Keys;
            Components = new Dictionary<string, UIElement>();
            List<string> componentName = new List<string>();
            //In the Java code, 'panel' (to which the RadioButtons are added) is a JPanel with GridLayout.
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

        //Whenever another RadioButton is checked, Unchecked event of the previous RadioButton
        //is fired to which this method is subscribed. This method removes the last children in 
        //the completePanel which corresponds to the previously checked RadioButton.
        private void RadioButton_Unchecked(object sender, RoutedEventArgs e)
        {
            //Does not remove the last child when there is no card in the completePanel
            //i.e. no RadioButton is checked
            if (((StackPanel)UIElement).Children.Count > 1)
            {
                ((StackPanel)UIElement).Children.RemoveAt(((StackPanel)UIElement).Children.Count - 1);
            }
        }

        //Whenever a RadioButton is checked, Checked event of the RadioButton is fired to which this method
        //is subscribed. This method adds a card (corresponding to the checked RadioButton) to the completePanel.
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
