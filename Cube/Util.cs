using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
//using Infragistics.Windows.DataPresenter;

namespace Cube
{
    public class Util
    {
        static Dictionary<string, string> IconRegistry = new Dictionary<string, string>
        {
            { "AbstractDataNode", "pack://application:,,,/Cube;component/Icons/exp.png" },
            { "Experiment", "pack://application:,,,/Cube;component/Icons/exp.png" },
            { "CompoundGroups", "pack://application:,,,/Cube;component/Icons/cg.png" },
            { "hcsNode", "pack://application:,,,/Cube;component/Icons/cluster.png" }
        };

        static public Image GetIcon(string NodeType)
        {
            Image test = new Image();

            BitmapImage bi3 = new BitmapImage();
            bi3.BeginInit();
            bi3.UriSource = new Uri(IconRegistry[NodeType]);

            bi3.EndInit();

            test.Source = bi3;

            test.Stretch = Stretch.Fill;
            test.MaxWidth = 20;
            test.MaxHeight = 20;
            return test;
        }

        static public TreeView GetTree(IDataNode RootNode)
        {
            TreeView tree = new TreeView();
            tree.Items.Add(RootNode.Node);
            return tree;
        }
    }
}
