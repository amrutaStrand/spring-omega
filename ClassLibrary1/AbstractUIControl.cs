﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Agilent.OpenLab.Spring.Omega
{
    public abstract class AbstractUIControl : IUIControl
    {
        public abstract object Value { get; set; }
        public abstract Dictionary<string, object> Parameters { get; set; }
        public string Id { get; set; }

        protected UIElement UIElement { get; set; }


        public void SetEnabled(bool isEnabled)
        {
            UIElement.IsEnabled = isEnabled;
        }

        public abstract void SetParameters(Dictionary<string, object> valuePairs);

        public abstract void CreateUIElement();
        public abstract UIElement GetUIElement();
        public abstract bool Validate(object value);
    }
}
