using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Timers;
using System.Windows.Controls;
using System.Windows.Input;

namespace Cube
{
    public class AbstractDataNode : IDataNode
    {
        public string Name { get; set; }

        public string Type { get; set; }

        public string IconPath { get; set; }

        public string Notes { get; set; }

        public string HoverText { get; set; }

        public MouseButtonEventHandler Node_MouseLeftButtonDown { get; set; }

        public Dictionary<string, ActionCommand> ContextMenuOptions { get; set; }

        public ObservableCollection<IDataNode> Childrens { get; set; }

        private Timer ClickTimer;
        private int ClickCounter;

        #region Execute methods of commands

        private void DeleteChildsExecute(object p)
        {
            MessageBox.Show(string.Format("Removing all childs: Count is {0}", Childrens.Count()));
            Childrens.Clear();
        }

        private void AddChildExecute(object p)
        {
            MessageBox.Show("Adding child...");
        }

        #endregion

        public AbstractDataNode()
        {
            Type = "AbstractDataNode";
            Childrens = new ObservableCollection<IDataNode>();

            Node_MouseLeftButtonDown = new MouseButtonEventHandler(TextBlock1_MouseLeftButtonDown);

            ContextMenuOptions = new Dictionary<string, ActionCommand>
            {
                {"Delete Childs",  new ActionCommand(DeleteChildsExecute)},
                {"Add Child", new ActionCommand(AddChildExecute)}
            };

            ClickTimer = new Timer(200);
            ClickTimer.Elapsed += new ElapsedEventHandler(EvaluateClicks);
        }

        private void TextBlock1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ClickTimer.Stop();
            ClickCounter++;
            ClickTimer.Start();
        }

        private void EvaluateClicks(object source, ElapsedEventArgs e)
        {
            ClickTimer.Stop();
            if (ClickCounter == 1)
            {
                OnSingleClick();
            }
            if (ClickCounter == 2)
            {
                OnDoubleClick();
            }
            ClickCounter = 0;
        }

        protected virtual void OnSingleClick()
        {
            MessageBox.Show(string.Format("Single clicked on {0} which is of type {1}", Name, Type));
        }

        protected virtual void OnDoubleClick()
        {
            MessageBox.Show(string.Format("Double clicked on {0} which is of type {1}", Name, Type));
        }

        public void AddChild(IDataNode node)
        {
            Childrens.Add(node);
        }

        public void RemoveChild(IDataNode node)
        {
            Childrens.Remove(node);
        }
    }
}
