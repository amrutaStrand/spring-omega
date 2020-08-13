using System.Windows;
using System.Windows.Media;

namespace Agilent.OpenLab.Spring.Omega
{
    /// <summary>
    /// This class is responsible for creating the Int control which internally is a string control where just the validation treatment is different.
    /// </summary>
    public class IntControl : StringControl
    {
        public override object Value
        {
            get => int.Parse(this.TextBox.Text);
            set
            {
                if (Validate(value))
                    TextBox.Text = value.ToString();
                else
                {
                    this.BorderColor = Color.FromRgb(255, 0, 0);
                    MessageBox.Show(MessageInfo.INT_ERROR_MESSAGE); // Red star
                    CreateUIElement();
                }
            }
        }
        /// <summary>
        /// The Entered text should be of int type and not to be a null value.
        /// It returns false which states that the validation failed.
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public override bool Validate(object val)
        {
            return (val != null && val is int) ? true : false; // validation message
        }
    }
}
