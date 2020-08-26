using System;
using System.Windows.Controls;
using System.Windows;
using Agilent.OpenLab.Spring.Omega;

namespace OmegaUIControls
{
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
            UIElement = panel;
        }

        public override UIElement GetUIElement()
        {
            if (UIElement == null)
                CreateUIElement();
            return UIElement;
        }

        public override void SetInput(IUIInput input)
        {
            Input = input;
        }

        public override bool Validate(object value)
        {
            return true;
        }
    }
}
