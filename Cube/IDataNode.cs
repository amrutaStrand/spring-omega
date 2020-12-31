using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Cube
{
    public interface IDataNode
    {
        /// <summary>
        /// Name to be displayed in the tree.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Type of the data node.
        /// </summary>
        string Type { get; set; }

        string IconPath { get; set; }

        string Notes { get; set; }

        string HoverText { get; set; }

        TreeViewItem Node { get; set; }

        ActionCommand DeleteChilds { get; set; }

        ActionCommand AddChildCmd { get; set; }

        Dictionary<string, ActionCommand> ContextMenuOptions { get; set; }

        /// <summary>
        /// This delegate fires the single and double click actions
        /// </summary>
        MouseButtonEventHandler Node_MouseLeftButtonDown { get; set; }

        ObservableCollection<IDataNode> Childrens { get; set; }

        /// <summary>
        /// Adds a child to the node.
        /// </summary>
        /// <param name="node"></param>
        void AddChild(IDataNode node);

        /// <summary>
        /// Removes a child from the node.
        /// </summary>
        /// <param name="node"></param>
        void RemoveChild(IDataNode node);
    }
}
