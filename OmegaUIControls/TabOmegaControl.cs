using System;
using System.Windows;
using System.Windows.Controls;
using Infragistics.Windows.Controls;
using Agilent.MHDA.Omega;

namespace Agilent.MHDA.Omega
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
            tab.MinHeight = Convert.ToDouble(Input.GetInput("MinHeight", 100));
            tab.MinWidth = Convert.ToDouble(Input.GetInput("MinWidth", 400));
            tab.MaxHeight = Convert.ToDouble(Input.GetInput("MaxHeight", 500));
            tab.MaxWidth = Convert.ToDouble(Input.GetInput("MaxWidth", 1000));

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

            UtilityMethods.SetPanelResources(tab);
            UIElement = tab;
        }
    }
}
