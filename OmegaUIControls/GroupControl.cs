using Agilent.MHDA.Omega;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Agilent.MHDA.Omega
{
    /// <summary>
    /// This class groups multiple <see cref="UIElement"/> or <see cref="IUIControl"/>.
    /// </summary>
    class GroupControl : AbstractUIContainer
    {
        /// <summary>
        /// Creates UIElement of <see cref="GroupControl"/>. The UIElement is a <see cref="StackPanel"/> inside
        /// a <see cref="ScrollViewer"/> which is contained in a <see cref="GroupBox"/>.
        /// </summary>
        public override void CreateUIElement()
        {
            GroupBox box = new GroupBox();
            box.BorderThickness = new System.Windows.Thickness(0);

            bool showBorder = (bool)Input.GetInput("showBorder", true);

            string description = (string)Input.GetInput("Description", "Omega Container");

            if (showBorder)
            {
                box.BorderThickness = new System.Windows.Thickness(2);
                box.Header = description;
            }

            UIElement panel = CreateComponentsPanel();

            box.Content = panel;

            //box.Height = 300;
            //box.Width = 1000;
            UtilityMethods.SetPanelResources(panel as FrameworkElement);
            UIElement = box;
        }

        /// <summary>
        /// Adds the controls to a <see cref="StackPanel"/> inside a <see cref="ScrollViewer"/>.
        /// </summary>
        /// <returns></returns>
        private UIElement CreateComponentsPanel()
        {
            //the stack panel hold the controls
            StackPanel panel = new StackPanel();

            //the stack panel is placed inside a scroll viewer to enable scrolling
            ScrollViewer scrollViewer = new ScrollViewer();
            scrollViewer.Content = panel;
            scrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            scrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;

            scrollViewer.MaxHeight = Convert.ToDouble(Input.GetInput("MaxHeight", UIConstants.ControlMaxHeight));
            scrollViewer.MaxWidth = Convert.ToDouble(Input.GetInput("MaxWidth", UIConstants.ControlMaxWidth));
            scrollViewer.MinHeight = Convert.ToDouble(Input.GetInput("MinHeight", UIConstants.ControlMinHeight));
            scrollViewer.MinWidth = Convert.ToDouble(Input.GetInput("MinWidth", UIConstants.ControlMinWidth));

            //set orientation of the stack panel depending on the input parameter
            string orientation = (string)Input.GetInput("orientation", "vertical");
            panel.Orientation = orientation.Equals("horizontal") ? Orientation.Horizontal : Orientation.Vertical;

            //add controls in the stack panel
            int n = GetControlCount();
            for (int i = 0; i < n; i++)
            {
                IUIControl control = GetControl(i);
                UIElement u = control.GetUIElement();
                (u as FrameworkElement).VerticalAlignment = VerticalAlignment.Center;
                panel.Children.Add(u);
            }

            return scrollViewer;
        }
    }
}
