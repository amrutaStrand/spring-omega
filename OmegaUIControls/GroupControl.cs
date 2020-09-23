﻿using System;
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

        /// <summary>
        /// Creates UIElement of <see cref="GroupControl"/> which is a <see cref="GroupBox"/>.
        /// </summary>
        public override void CreateUIElement()
        {
            GroupBox box = new GroupBox();
            box.BorderThickness = new System.Windows.Thickness(0);

            bool showBorder = Input.HasParameter("showBorder") ? (bool)Input.GetInput("showBorder") : true;

            string description = Input.HasParameter("Description") ? (string)Input.GetInput("Description") : "Omega Container";

            if (showBorder)
            {
                box.BorderThickness = new System.Windows.Thickness(2);
                box.Header = description;
            }

            UIElement panel = CreateComponentsPanel();

            box.Content = panel;

            //box.Height = 300;
            //box.Width = 1000;

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

            //set orientation of the stack panel depending on the input parameter
            panel.Orientation = orientation.Equals("horizontal") ? Orientation.Horizontal : Orientation.Vertical;

            //add controls in the stack panel
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
        }
    }
}