using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ClassLibrary1
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

        public void SetParameters(Dictionary<string, object> valuePairs)
        {
            this.Parameters = valuePairs;
        }

        public abstract void CreateUIElement();
        public abstract UIElement GetUIElement();
        public abstract bool Validate(object value);
    }
}
