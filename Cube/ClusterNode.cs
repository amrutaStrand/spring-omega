using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cube
{
    public class ClusterNode : AbstractDataNode
    {
        public ClusterNode() : this("Cluster Node")
        {

        }

        public ClusterNode(string ClusterName)
        {
            Name = ClusterName;
            Type = "hcsNode";
            HoverText = string.Format("This is a {0} node", Type);
        }
    }
}
