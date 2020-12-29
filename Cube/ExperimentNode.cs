using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
