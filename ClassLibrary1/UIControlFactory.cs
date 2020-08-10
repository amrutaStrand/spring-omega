﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Practices.Unity;

namespace ClassLibrary1
{
    public class UIControlFactory //UIControlRegister
    {

        public static void RegisterUIControls(IUnityContainer container)
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static UIElement GetUIControl(string id, Dictionary<string, object> parameters)
        {

            IUIControl control = null;
            if (id.Equals(CONSTANTS.STRING_COLUMN))
                control = new StringControl();
            if (id.Equals(CONSTANTS.INT_COLUMN))
                control = new IntControl();
            if (id.Equals(CONSTANTS.FLOAT_COLUMN))
                control = new FloatControl();
            control.SetParameters(parameters);
            return control as UIElement;
        }
    }
}
