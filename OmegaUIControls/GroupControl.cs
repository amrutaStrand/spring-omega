using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Agilent.OpenLab.Spring.Omega
{
    /// <summary>
    /// This class groups multiple <see cref="UIElement"/> or <see cref="IUIControl"/>.
    /// </summary>
    class GroupControl : AbstractUIContainer
    {
        public override void CreateUIElement()
        {
            throw new NotImplementedException();
        }

        public override void SetInput(IUIInput input)
        {
            base.SetInput(input);

        }
    }
}
