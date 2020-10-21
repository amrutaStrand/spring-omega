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

            checkBox.IsChecked = (bool)Input.GetInput("Value", false);
            checkBox.Content = Input.GetInput("Description", "Description about the check box");

            checkBox.Margin = new Thickness(10);
            SetResources(checkBox);
            UIElement = checkBox;
        }
    }
}
