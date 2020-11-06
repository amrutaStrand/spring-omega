using OmegaUIControls.OmegaUIUtils;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Agilent.OpenLab.Spring.Omega
{
    /// <summary>
    /// RadioCardUIControl displays a set of radio buttons and the control corrresponding 
    /// to the selected radio button.
    /// </summary>
    class RadioCardUIControl : AbstractUIControl
    {
        private StackPanel completePanel = new StackPanel();

        private string value;
        public override object Value
        {
            get
            {
                return value;
            }
            set
            {
                foreach (RadioButton rb in RadioButtons)
                {
                    if ((string)rb.Content == (string)value)
                    {
                        rb.IsChecked = true;
                        value = (string)rb.Content;
                    } 
                }
            }
        }

        public Dictionary<string, object> Options { get ; set ; }

        Dictionary<string, UIElement> Components { get; set; }

        public List<RadioButton> RadioButtons { get; set; }

        int cardCount;

        /// <summary>
        /// Creates a stack panel which contains another stack panel to which the radio buttons 
        /// (for which the 'card' is not null) are added.
        /// </summary>
        public override void CreateUIElement()
        {
            RadioButtons = new List<RadioButton>();
            var labels = Options.Keys;
            Components = new Dictionary<string, UIElement>();
            List<string> componentName = new List<string>();
            StackPanel panel = new StackPanel();
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

            completePanel = new StackPanel();
            completePanel.Children.Add(panel);

            var scrollViewer = new ScrollViewer();
            scrollViewer.Content = completePanel;
            scrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            scrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;

            UtilityMethods.SetPanelResources(scrollViewer);
            scrollViewer.MaxHeight = Convert.ToDouble(Input.GetInput("MaxHeight", UIConstants.ControlMaxHeight));
            scrollViewer.MaxWidth = Convert.ToDouble(Input.GetInput("MaxWidth", UIConstants.ControlMaxWidth));
            scrollViewer.MinHeight = Convert.ToDouble(Input.GetInput("MinWidth", UIConstants.ControlMinHeight));
            scrollViewer.MinWidth = Convert.ToDouble(Input.GetInput("MinWidth", UIConstants.ControlMinWidth));
            UIElement = scrollViewer;

            if (Input.HasParameter("Value"))
            {
                if (Options[Input.GetInput("Value").ToString()] != null)
                    Value = Input.GetInput("Value").ToString();
            }
            else
                Value = componentName[0];
        }

        //Whenever another RadioButton is checked, Unchecked event of the previous RadioButton
        //is fired to which this method is subscribed. This method removes the last children in 
        //the completePanel which corresponds to the previously checked RadioButton.
        private void RadioButton_Unchecked(object sender, RoutedEventArgs e)
        {
            completePanel.Children.RemoveAt(completePanel.Children.Count - 1);
        }

        //Whenever a RadioButton is checked, Checked event of the RadioButton is fired to which this method
        //is subscribed. This method adds a card (corresponding to the checked RadioButton) to the completePanel.
        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            value = (string)rb.Content;
            completePanel.Children.Add(Components[(string)rb.Content]);
        }

        public override void SetInput(IUIInput input)
        {
            base.SetInput(input);

            Options = (Dictionary<string, object>)input.GetInput("Options", new Dictionary<string, object>());
        }
    }
}
