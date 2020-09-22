using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using OmegaUIControls.OmegaUIUtils;

namespace Agilent.OpenLab.Spring.Omega
{
    /// <summary>
    /// This class groups multiple <see cref="UIElement"/> or <see cref="IUIControl"/>.
    /// </summary>
    class GroupControl : AbstractUIContainer
    {
        private string orientation;
        private bool enableScroll;

        public override void CreateUIElement()
        {
            GroupBox panel = new GroupBox();
            panel.BorderThickness = new System.Windows.Thickness(2);

            bool showBorder = Input.HasParameter("showBorder") ? (bool)Input.GetInput("showBorder") : true;

            string description = Input.HasParameter("Description") ? (string)Input.GetInput("Description") : "Columns";

            if (showBorder)
                panel.Header = description;

            UIElement element = CreateComponentsPanel();

            panel.Content = element;
            if (enableScroll)
            {
                //Size size = element.RenderSize;
                //panel.ChangeDimension(size.Height + 25, size.Width + 30);
            }

            panel.Height = 300;
            panel.Width = 1000;

            UIElement = panel;
        }

        /// <summary>
        /// Adds the controls to a stackpanel inside a scroll viewer.
        /// </summary>
        /// <returns></returns>
        private UIElement CreateComponentsPanel()
        {
            StackPanel panel = new StackPanel();
            ScrollViewer scrollViewer = new ScrollViewer();
            scrollViewer.Content = panel;
            scrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            scrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
            
            panel.Orientation = orientation.Equals("horizontal") ? Orientation.Horizontal : Orientation.Vertical;

            int n = GetControlCount();
            for (int i = 0; i < n; i++)
            {
                IUIControl control = GetControl(i);
                UIElement u = control.GetUIElement();
                panel.Children.Add(u);
            }

            return scrollViewer;
        }

        public override void SetInput(IUIInput input)
        {
            base.SetInput(input);

            orientation = Input.HasParameter("orientation") ? (string)Input.GetInput("orientation") : "vertical";

            enableScroll = Input.HasParameter("enableScroll") ? (bool)Input.GetInput("enableScroll") : false;
        }
    }
}
