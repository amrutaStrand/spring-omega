using System;
using System.Windows;

namespace Agilent.OpenLab.Spring.Omega
{
    /// <summary>
    /// This class is responsible for creating the Float control which internally is a string control where just the validation treatment is different.
    /// </summary>
    public class FloatControl : StringControl
    {
        public override void SetValue(object val)
        {
            Value = float.Parse(val.ToString());
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
            MessageBox.Show(MessageInfo.FLOAT_ERROR_MESSAGE);
        }
    }
}
