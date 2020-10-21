using System;
using System.Windows;
using System.Windows.Controls;
using Infragistics.Windows.Controls;

namespace Agilent.OpenLab.Spring.Omega
{
    /// <summary>
    /// TabOmegaControl is a <see cref="TabControl"/> with each control in one tab.
    /// Title of each tab is set to the description of the contained control.
    /// </summary>
    class TabOmegaControl : AbstractUIContainer
    {
        public override void CreateUIElement()
        {
            XamTabControl tab = new XamTabControl();
            
            int n = GetControlCount();
            for (int i = 0; i < n; i++)
            {
                //Get the control
                IUIControl control = GetControl(i);
                IUIInput param = control.Input;

                //Create the tab item
                TabItemEx item = new TabItemEx();
                string defaultTitle = (string)param.GetInput("Description", "Item-" + (i + 1).ToString());
                item.Header = (string)param.GetInput("title", defaultTitle);
                item.Content = control.GetUIElement();

                //Add the tab item to the tab control
                tab.Items.Add(item);
            }

            tab.Margin = new Thickness(10);
            SetResources(tab);
            UIElement = tab;
        }
    }
}
