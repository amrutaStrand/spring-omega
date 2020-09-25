using System;
using System.Windows.Controls;
using System.Windows;

namespace Agilent.OpenLab.Spring.Omega
{
    /// <summary>
    /// <see cref="CheckBox"/> control.
    /// </summary>
    class BooleanControl : AbstractUIControl
    {
        private CheckBox checkBox;
        public override object Value 
        { 
            get => checkBox.IsChecked;
            set => checkBox.IsChecked = (bool)value;
        }

        public override void CreateUIElement()
        {
            checkBox = new CheckBox();
            checkBox.Content = (string)Input.GetInput("Description");
            var panel = new Grid();
            panel.Children.Add(checkBox);
            panel.Margin = new Thickness(10);
            UIElement = panel;
        }

        public override void SetInput(IUIInput input)
        {
            Input = input;
        }
    }
}
