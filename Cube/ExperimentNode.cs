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
        private void Item1Execute(object p)
        {
            MessageBox.Show("Executing Item1...");
        }

        private void Item2Execute(object p)
        {
            MessageBox.Show("Executing Item2...");
        }

        public ExperimentNode() : this("MyExperiment")
        {
        }

        public ExperimentNode(string expName)
        {
            Name = expName;
            Type = "Experiment";
            IconPath = Util.IconRegistry[Type];
            HoverText = string.Format("This is a {0} node", Type);
            ContextMenuOptions.Add("Item1", new ActionCommand(Item1Execute));
            ContextMenuOptions.Add("Item2", new ActionCommand(Item2Execute));
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
