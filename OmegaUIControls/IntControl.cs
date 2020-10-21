using System;
using System.Windows;

namespace Agilent.OpenLab.Spring.Omega
{
    /// <summary>
    /// This class is responsible for creating the Int control which internally is a string 
    /// control where just the validation treatment is different.
    /// </summary>
    public class IntControl : StringControl
    {
        public override object Value
        {
            get
            {
                return TextBox.Text;
            }
            set
            {
                TextBox.Text = int.Parse(value.ToString()).ToString();
            }
        }

        /// <summary>
        /// The Entered text should be of int type and not to be a null value.
        /// It returns false which states that the validation failed.
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public override bool IsValid(object val)
        {
            if (val == null)
                return false;
            return Int32.TryParse(val.ToString(), out int test);
        }

        public override void ShowValidationError()
        {
            MessageBox.Show(MessageInfo.INT_ERROR_MESSAGE);
        }
    }
}
