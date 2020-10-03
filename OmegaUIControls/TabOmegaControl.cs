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
            //TabControl tab = new TabControl();
            XamTabControl tab = new XamTabControl();
            //tab.BorderThickness = new Thickness(2);
            tab.Margin = new Thickness(10);

            int n = GetControlCount();
            for (int i = 0; i < n; i++)
            {
                //Get the control
                IUIControl control = GetControl(i);
                IUIInput param = control.Input;

                //Create the tab item
                //TabItem item = new TabItem();
                TabItemEx item = new TabItemEx();
                item.Header = param.HasParameter("title") ? (string)param.GetInput("title") : ( param.HasParameter("description") ? (string)param.GetInput("description") : "Item-" + (i+1).ToString() );
                item.Content = control.GetUIElement();

                //Add the tab item to the tab control
                tab.Items.Add(item);
            }

            UIElement = tab;
        }
    }
}
