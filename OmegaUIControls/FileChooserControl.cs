using System.Windows.Controls;
using OmegaUIControls.OmegaUIUtils;

namespace Agilent.OpenLab.Spring.Omega
{
    /// <summary>
    /// FileChooserControl is a container encapsulating a <see cref="TextBox"/> and <see cref="Button"/>. 
    /// The button click launches an <see cref="OpenFileDialog"/> and the selected file path is displayed 
    /// in the text box. This class is derived from <see cref="FileControl"/>.
    /// </summary>
    class FileChooserControl : FileControl
    {

        /// <summary>
        /// A <see cref="TextBox"/> and <see cref="Button"/> are added in a <see cref="LayoutPanel"/>.
        /// </summary>
        public override void CreateUIElement()
        {
            CreateDialog();
            CreateTextBox();
            CreateButton();

            var panel = new LayoutPanel(1, 3);
            panel.Add(textBox, 3);
            panel.Add(button, 1);
            panel.ChangeDimension(60, 400);
            SetResources(panel);
            UIElement = panel;
        }
    }
}
