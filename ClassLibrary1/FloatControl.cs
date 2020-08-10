using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ClassLibrary1
{
    public class FloatControl : StringControl
    {
        public override bool Validate(object val)
        {
            return (val != null && val is float) ? true : false;
        }
    }
}
