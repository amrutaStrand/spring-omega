using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Agilent.OpenLab.Spring.Omega
{
    public class FloatControl : StringControl
    {
        public override bool Validate(object val)
        {
            return (val != null && val is float) ? true : false;
        }
    }
}
