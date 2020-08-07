using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ClassLibrary1
{
    public class UIControlFactory
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static UIElement GetUIControl(string id, Dictionary<string, object> parameters)
        {
            //if (id.Equals("String"))
            
            StringControl stringControl = new StringControl();
            stringControl.SetParameters(parameters);
            return stringControl;
        }
    }
}
