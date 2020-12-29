using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cube
{
    public class CompoundsGroupNode : AbstractDataNode
    {
        public CompoundsGroupNode() : this("CompoundGroups")
        {
        }

        public CompoundsGroupNode(string CompoundGroupsName)
        {
            Name = CompoundGroupsName;
            Type = "CompoundGroups";
            HoverText = string.Format("This is a {0} node", Type);
        }
    }
}
