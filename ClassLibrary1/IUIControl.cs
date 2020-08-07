using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ClassLibrary1
{
    /// <summary>
    /// Interface that defines ui control for a datatype.
    /// </summary>
    public interface IUIControl
    {
        string Id { get; set; }
        object Value { get; set; }
        Dictionary<string, object> Parameters { get; set; }
        void SetEnabled(bool isEnabled);

    }
}
