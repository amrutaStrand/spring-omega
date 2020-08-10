using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ClassLibrary1
{
    public abstract class AbstractUIControl : UserControl, IUIControl
    {
        public abstract object Value { get; set; }
        public abstract Dictionary<string, object> Parameters { get; set; }
        string IUIControl.Id { get => this.Uid; set => this.Uid = value; }


        public void SetEnabled(bool isEnabled)
        {
            this.IsEnabled = isEnabled;
        }

        public void SetParameters(Dictionary<string, object> valuePairs)
        {
            this.Parameters = valuePairs;
        }
    }
}
