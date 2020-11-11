using System;
using System.Windows.Controls;
using System.Windows;
using OmegaUIControls.OmegaUIUtils;

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
            var panel = new LayoutPanel(1, 1);
            panel.Add(checkBox, 1);
            panel.ChangeDimension(30, 200);

            checkBox.IsChecked = (bool)Input.GetInput("Value", false);
            checkBox.Content = Input.GetInput("Description", "Check box description");

            UtilityMethods.SetPanelResources(panel);
            UIElement = panel;
        }
    }
}
