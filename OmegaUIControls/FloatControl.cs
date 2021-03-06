﻿using System;
using System.Windows;

namespace Agilent.MHDA.Omega
{
    /// <summary>
    /// This class is responsible for creating the Float control which internally is a string 
    /// control where just the validation treatment is different.
    /// </summary>
    public class FloatControl : StringControl
    {
        public override object Value
        {
            get
            {
                return TextBox.Text;
            }
            set
            {
                TextBox.Text = float.Parse(value.ToString()).ToString();
            }
        }

        /// <summary>
        /// The Entered text should be of float type and not to be a null value.
        /// It returns false which states that the validation failed.
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public override bool IsValid(object val)
        {
            if (val == null)
                return false;
            return float.TryParse(val.ToString(), out float test);
        }

        public override void ShowValidationError()
        {
            errorMsg = MessageInfo.FLOAT_ERROR_MESSAGE;
            ErrorToolTip.Content = errorMsg;
        }
    }
}
