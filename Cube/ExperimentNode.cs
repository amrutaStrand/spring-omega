using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Cube
{
    public class ExperimentNode : AbstractDataNode
    {
        public ExperimentNode() : this("MyExperiment")
        {
        }

        public ExperimentNode(string expName)
        {
            Name = expName;
            Type = "Experiment";
            HoverText = string.Format("This is a {0} node", Type);
        }

        protected override void OnSingleClick()
        {
            MessageBox.Show(string.Format("Overriding single click method in {0}", Type));
        }

        protected override void OnDoubleClick()
        {
            MessageBox.Show(string.Format("Overriding double click method in {0}", Type));
        }
    }
}
